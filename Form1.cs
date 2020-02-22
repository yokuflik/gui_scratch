using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();

            //check the args and if has a file name open it
            filePath = "";

            if (args.Length > 0)
            {
                //check if the file that was opend is in the correct format and if it is open it
                if (Path.GetExtension(args[0]) == ".gus")
                {
                    filePath = args[0];
                }
            }


        }

        List<Block> blocks = new List<Block>();
        OnStartBlock whenProgramStartsBlock;
        BlocksToAdd bta;

        ControlsView controlsView;

        Values myValues;

        decimal blockIndex = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (filePath == "")
            {
                newFile();
            }
            else
            {
                openFile();
            }
        }

        #region file

        private void newFile()
        {
            filePath = "";

            //set the values
            myValues = new Values();
            myValues.controls.Add(new MyControl(MyControl.ControlKinds.Form,"this"));

            //set the blocks and the when start block
            blocks = new List<Block>();

            BlockInfo whenStartInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When program starts") }, Color.Orange, null);
            whenStartInfo.CanDeleteAndDouplicate = false;
            whenProgramStartsBlock = new OnStartBlock(screenPB, new Point(20, 50), whenStartInfo , setClientsAndParents, blockRightMouseClick,blockMove, blockIndex++);
            
            addBlockToList(whenProgramStartsBlock);

            //set all the other things after the new file
            afterFileOpeningSetup();
        }

        private void afterFileOpeningSetup()
        {
            setFormText();

            myValues.updateBlocksFunc = updateBlocksFunc;

            bta = new BlocksToAdd(this, addBlocksPanel, myValues, addBlock, new Point(screenPanel.Left+4, 52));

            controlsView = new ControlsView(myValues, controlsFLP, controlsKindsPanel, propertiesPanel);
        }

        #endregion

        #region blocks

        #region update blocks

        private void updateBlocksFunc()
        {
            //check in all the blocks list and search for the blocks that needs to update and update them
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                if (blocks[i].Info.NeedForUpdate)
                {
                    blocks[i].updateBlock(true);
                }
            }

            //update the add blocks func
            bta.Rb_CheckedChanged(null, null);
        }

        #endregion

        #region add blocks

        public void addBlock(BlockInfo blockInfo, Point location)
        {
            addBlock(new List<BlockInfo>() { blockInfo }, location);
        }

        public void addBlock(List<BlockInfo> blockInfo, Point location)
        {
            Block b = null;

            //add the block
            switch (blockInfo[0].Kind)
            {
                case Block.BlockKinds.Action:
                    b = new ActionBlock(screenPB, location, blockInfo[0], setClientsAndParents, blockRightMouseClick,blockMove, blockIndex++, blockInfo[0].CanBeParent);
                    break;
                case Block.BlockKinds.Value:
                    b = new ValueBlock(screenPB, blockInfo[0].ValueKind, location, blockInfo[0], setClientsAndParents, blockRightMouseClick,blockMove, blockIndex++);
                    break;
                case Block.BlockKinds.Contain:
                    b = new ContainBlock(screenPB, location, blockInfo, setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++, blockInfo[0].CanBeParent);
                    break;
                case Block.BlockKinds.OnStart:
                    b = new OnStartBlock(screenPB, location, blockInfo[0], setClientsAndParents, blockRightMouseClick,blockMove, blockIndex++);
                    setDragAbleAddValueBlockFunc(ref b);
                    break;
            }

            //b.updateBlock(true);
            addBlockToList(b);

            setClientsAndParents(b, b.Info.CanBeParent, b.Info.CanBeClient);

            //updateBlocksFunc();
        }

        #endregion

        #region dragAble

        private void setDragAbleAddValueBlockFunc(ref Block b)
        {
            b.Info.setAddVarFunc(addVarBlockFromDragAble);
        }

        private void addVarBlockFromDragAble(BlockInfoEvent bie, Point location)
        {
            //add the right value block
            BlockInfo info = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(bie.label.Text) }, Color.Blue, new BlockCode(new List<CodePart>() { new CodePart(bie.label.Text, true) }));
            info.Kind = Block.BlockKinds.Value;
            info.BlockCode = bie.code;

            switch (bie.Kind)
            {
                case BlockInfoEvent.Kinds.numberInput:
                    info.ValueKind = ValueBlock.ValueKinds.number;
                    break;
                case BlockInfoEvent.Kinds.textInput:
                    info.ValueKind = ValueBlock.ValueKinds.text;
                    break;
                case BlockInfoEvent.Kinds.booleanInput:
                    info.ValueKind = ValueBlock.ValueKinds.boolean;
                    break;
            }

            addBlock(info,location);
        }

        #endregion

        #region move block
        
        private void blockMove(Block b,bool start)
        {
            //set the blocks visibility
            setBlocksVisibility(!start);
            
            if (start) //the block starts to move
            {
                //draw the blocks that are in the view to the container
                Bitmap bmp = new Bitmap(screenPB.Width, screenPB.Height);
                
                //get all the blocks that are not moving to a list
                List<decimal> notMovingBIndexes = getNotMovingBlockIndexes(b);

                //draw all the not moving block to the bmp
                Graphics blocksG = Graphics.FromImage(bmp);
                //int height = 0;
                Point loc;
                for (int i = 0; i <= notMovingBIndexes.Count - 1; i++)
                {
                    if (blocks[(int)notMovingBIndexes[i]].Kind == Block.BlockKinds.Value)
                    {
                        ValueBlock vb = (ValueBlock)blocks[(int)notMovingBIndexes[i]];
                        if (vb.isAValueClient)
                        {
                            continue; //dont draw the value to the bmp
                        }
                    }
                    loc = blocks[(int)notMovingBIndexes[i]].getBounds().Location;
                    blocks[(int)notMovingBIndexes[i]].drawImageToBmp(ref bmp, ref loc, ref blocksG, false);
                }

                //show the bitmap with all the blocks pictures
                screenPB.BackgroundImage = bmp;
            }
            else
            {
                //clear all the blocks pictures
                screenPB.BackgroundImage = new Bitmap(1,1);
                
                //set the rolling of the window
                setRolling(screenPB, b.getBottomBlock().getBottomPB());

                setFormText();
            }
        }

        private List<decimal> getNotMovingBlockIndexes(Block b)
        {
            List<decimal> res = new List<decimal>();

            //add all the blocks numbers
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                res.Add(i);
            }

            //remove the moving blocks indexes from the list
            if (b != null)
            {
                removeBlockAndHisClients(b, ref res);
            }

            return res;
        }

        private void removeBlockAndHisClients(Block b, ref List<decimal> res)
        {
            Block curB = b;
            while (curB != null)
            {
                res.Remove(curB.BlockIndex);
                checkInsideClients(ref res, curB);
                curB = curB.Client;
            }
        }

        private void checkInsideClients(ref List<decimal> res, Block curB)
        {
            if (curB.Kind == Block.BlockKinds.Contain)
            {
                ContainBlock cb = curB as ContainBlock;
                for (int i =0;i<= cb.insideClients.Count - 1; i++)
                {
                    if (cb.insideClients[i] != null)
                    {
                        removeBlockAndHisClients(cb.insideClients[i], ref res);
                    }
                }
            }
        }

        private void setBlocksVisibility(bool visible)
        {
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                blocks[i].setBlockVisible(visible);
            }
        }

        #region window rolling

        int space = 10;
        private void setRolling(Control screen, Control lastMoved)
        {
            if (!setRollingPlusForTheSides(screen, lastMoved)) //the last move was in the size of the screen
            {
                //check if needs to make the screen smaller
                setRollingMinusForTheSides(screen);
            }
        }

        private bool setRollingPlusForTheSides(Control screen, Control lastMoved)
        {
            int newHeight = screen.Size.Height;
            if (lastMoved.Bottom > screen.Height)
            {
                //set the new screen size
                newHeight = lastMoved.Bottom + space;
            }

            int newWidth = screen.Size.Width;
            if (lastMoved.Right > screen.Width)
            {
                newWidth = lastMoved.Right + space;
            }

            if (newWidth > screen.Size.Width || newHeight > screen.Size.Height)
            {
                //set the new screen size
                screen.Size = new Size(newWidth, newHeight);
                return true; //because changed the size
            }

            return false; //because didn't change the size
        }

        private void setRollingMinusForTheSides(Control screen)
        {
            Size screenMinSize = screen.Parent.Size;
            Size maxSize = new Size();

            PictureBox curBottomBlock;
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                curBottomBlock = blocks[i].getBottomBlock().getBottomPB();

                //check the block controls bottom
                maxSize = new Size(Math.Max(maxSize.Width, curBottomBlock.Right + space), Math.Max(maxSize.Height, curBottomBlock.Bottom + space));
            }

            //check if the max size is smaller then the screen min size
            maxSize = new Size(Math.Max(maxSize.Width, screenMinSize.Width), Math.Max(maxSize.Height, screenMinSize.Height));

            screen.Size = maxSize;
        }

        private void screenPanel_SizeChanged(object sender, EventArgs e)
        {
            //check if the screen pb is smaller then the screen panel and if it is make it the same size
            if (screenPB.Width < screenPanel.Width)
            {
                screenPB.Width = screenPanel.Width;
            }

            if (screenPB.Height < screenPanel.Height)
            {
                screenPB.Height = screenPanel.Height;
            }
        }

        #endregion
        
        #endregion

        #region clients and parents

        private bool setClientsAndParents(Block currentBlock, bool canBeParent, bool canBeClient)
        {
            //check if is out from the side
            if (currentBlock.getBounds().Left < -25)
            {
                //check if can delete the block and if can't set the location
                if (currentBlock.Info.CanDeleteAndDouplicate)
                {
                    if (!currentBlock.isAClient())
                    {
                        //delete the block
                        removeBlockByBlockIndex(currentBlock.BlockIndex,true);
                    }
                }
                else
                {
                    currentBlock.setLocationToCurrentPlace(-currentBlock.getBounds().Left, 0);
                }
                return false;
            }

            //check if is in anathor block
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                if (blocks[i].BlockIndex != currentBlock.BlockIndex)
                {
                    if (blocks[i].getBounds().IntersectsWith(currentBlock.getBounds()))
                    {
                        if (currentBlock.Kind == Block.BlockKinds.Value) //set the value blocks
                        {
                            //check if can be a value
                            if (blocks[i].checkValueClient(currentBlock as ValueBlock))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (currentBlock.CheckClient(blocks[i]))
                            {
                                return true;
                            }
                        
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region cms

        private void setCmsToBlock()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            //set the cms buttons
            ToolStripButton deleteButton = new ToolStripButton();
            deleteButton.Size = new Size(151, 22);
            deleteButton.Text = "Delete";
            deleteButton.Click += DeleteButton_Click;

            ToolStripButton douplicateButton = new ToolStripButton();
            douplicateButton.Size = new Size(151, 22);
            douplicateButton.Text = "Douplicate";
            douplicateButton.Click += DouplicateButton_Click;

            if (!blocks[blocks.Count-1].Info.CanDeleteAndDouplicate)
            {
                deleteButton.Enabled = douplicateButton.Enabled = false;
            }

            ToolStripButton addCommentButton = new ToolStripButton();
            addCommentButton.Size = new Size(151, 22);
            addCommentButton.Text = "Add Comment";
            addCommentButton.Click += AddCommentButton_Click;

            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            deleteButton,
            douplicateButton,
            addCommentButton});
            cms.Size = new Size(102, 70);
            
            blocks[blocks.Count - 1].cms = cms;
        }

        Block currentBlock;
        private void blockRightMouseClick(Block b)
        {
            currentBlock = b;
        }

        #region delete

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (currentBlock.PassedRemoveFromParent != null) //is a client
            {
                currentBlock.PassedRemoveFromParent();
            }
            if (currentBlock.PassedInsideClientRemoved != null)
            {
                currentBlock.PassedInsideClientRemoved(currentBlock.InsideClientIndex);
            }
            if (currentBlock.PassedClientChangedClients != null)
            {
                currentBlock.PassedClientChangedClients();
            }

            //remove block by block index
            removeBlockByBlockIndex(currentBlock.BlockIndex, true);

            //set the window rolling
            setRollingMinusForTheSides(screenPB);
        }

        private void removeBlockByBlockIndex(decimal blockIndex, bool removeClients = false)
        {
            int i = getBlockIndexInListFromBlockIndex(blockIndex);

            if (i == -1) return; //has no block with this index in the list

            Block curBlock = blocks[i];
            

            blocks.RemoveAt(i);
            curBlock.remove();
            removeInsideClients(curBlock);

            if (removeClients)
            {
                //remove the clients
                removeTheClients(curBlock.Client);
            }

            //if the cur block is a value block delete it from his parent
            if (curBlock.Kind == Block.BlockKinds.Value)
            {
                ValueBlock vb = curBlock as ValueBlock;
                if (vb.isAValueClient) //check if it is a client
                {
                    vb.removeFromBlock();
                }
            }

            //if the cur block is on start block remove the func
            else if (curBlock.Kind == Block.BlockKinds.OnStart)
            {
                OnStartBlock osb = curBlock as OnStartBlock;
                //delete the block event from the list
                for (int j = 0; j <= myValues.events.Count - 1; j++)
                {
                    if (myValues.events[j].FuncName == osb.Info.FuncName)
                    {
                        myValues.events.RemoveAt(j);
                        break;
                    }
                }
            }


            return;
        }

        private void removeInsideClients(Block curBlock)
        {
            if (curBlock.Kind != Block.BlockKinds.Contain)
            {
                return;
            }

            ContainBlock cb = curBlock as ContainBlock;

            for (int i = 0; i <= cb.insideClients.Count-1; i++)
            {
                if (cb.insideClients[i] != null)
                {
                    removeBlockByBlockIndex(cb.insideClients[i].BlockIndex, true);
                }
            }
        }

        private int getBlockIndexInListFromBlockIndex(decimal blockIndex)
        {
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                if (blocks[i].BlockIndex == blockIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        private void removeTheClients(Block curClient)
        {
            while (curClient != null)
            {
                removeBlockByBlockIndex(curClient.BlockIndex);
                curClient = curClient.Client;
            }
        }

        #endregion

        #region douplicate
        
        Block startBlock;
        PictureBox startBlockMovingPB;

        private void DouplicateButton_Click(object sender, EventArgs e)
        {
            startBlock = douplicateBlockByBlockIndex(currentBlock.BlockIndex, true);
            
            startBlock.AddMouseMoveAndDownFuncsToPB(ScreenPanel_MouseDownAfterDouplicate, ScreenPanel_MouseMoveAfterDouplicate);
            
            startBlockMovingPB = startBlock.movingPB;

            //set the location
            ScreenPanel_MouseMoveAfterDouplicate(null, null);
            
            //add the mouse move func
            screenPB.MouseMove += ScreenPanel_MouseMoveAfterDouplicate;
        }

        #region move the block after douplicate

        private void ScreenPanel_MouseDownAfterDouplicate(object sender, MouseEventArgs e)
        {
            startBlock.RemoveMouseMoveAndDownFuncsToPB(ScreenPanel_MouseDownAfterDouplicate, ScreenPanel_MouseMoveAfterDouplicate);
            screenPB.MouseMove -= ScreenPanel_MouseMoveAfterDouplicate;
        }

        private void ScreenPanel_MouseMoveAfterDouplicate(object sender, MouseEventArgs e)
        {
            Point mouseLocation = System.Windows.Forms.Cursor.Position;
            mouseLocation = screenPB.PointToClient(mouseLocation);
            startBlockMovingPB.Location = new Point(mouseLocation.X-10, mouseLocation.Y-10);
        }

        #endregion

        private Block douplicateBlockByBlockIndex(decimal curBlockIndex, bool removeClients = false)
        {
            int i = getBlockIndexInListFromBlockIndex(curBlockIndex);

            if (i == -1) return null; //has no block with this index in the list
            
            //add the clients
            List<Block> clients = getListOfClientsFromBlock(blocks[i]);
            addListOfClientsToBlock(clients);

            return clients[0];
        }

        private void addBlockToList(Block block)
        {
            blocks.Add(block);
            setCmsToBlock();
            block.bringToFront();
        }

        #region clients

        private List<Block> getListOfClientsFromBlock(Block block)
        {
            List<Block> res = new List<Block>();
            Block curClient = block;
            while(curClient != null)
            {
                if (curClient.Kind == Block.BlockKinds.Contain)
                {
                    ContainBlock cb = curClient as ContainBlock;

                    ContainBlock resCB = curClient.Clone(blockIndex++) as ContainBlock;

                    //add the inside clients
                    for (int i = 0; i <= cb.insideClients.Count - 1; i++)
                    {
                        if (cb.insideClients[i] != null)
                        {
                            List<Block> clients = getListOfClientsFromBlock(cb.insideClients[i]);

                            addListOfClientsToBlock(clients);
                            
                            resCB.AddInsideClient(clients[0], i);

                        }
                    }

                    res.Add(resCB);
                }
                else
                {
                    res.Add(curClient.Clone(blockIndex++));
                }
                //check the value client
                res[res.Count - 1] = douplicateTheValueBlocksOfABlock(curClient, res[res.Count-1]);

                curClient = curClient.Client;
            }

            return res;
        }

        private void addListOfClientsToBlock(List<Block> clients)
        {
            addBlockToList(clients[0]);
            
            //add the clients to the blocks list
            for (int i = 1; i <= clients.Count - 1; i++)
            {
                addBlockToList(clients[i]);
                clients[i - 1].AddClient(clients[i]);
            }
        }

        private Block douplicateTheValueBlocksOfABlock(Block originalB, Block newBlock)
        {
            if (originalB.Kind == Block.BlockKinds.Contain)
            {
                ContainBlock originalCB = originalB as ContainBlock;
                ContainBlock newCB = newBlock as ContainBlock;

                for (int j = 0; j <= originalCB.infos.Count - 1; j++)
                {
                    for (int i = 0; i <= originalCB.infos[j].BlockInfoEvents.Count - 1; i++)
                    {
                        if (originalCB.infos[j].BlockInfoEvents[i].Client != null)
                        {
                            newCB.infos[j].BlockInfoEvents[i].addClient(douplicateBlockByBlockIndex(originalCB.infos[j].BlockInfoEvents[i].Client.BlockIndex) as ValueBlock);
                        }
                    }
                }
                
                newBlock = newCB;
            }
            else
            {
                //check if has value block and douplicate it
                for (int i = 0; i <= originalB.Info.BlockInfoEvents.Count - 1; i++)
                {
                    if (originalB.Info.BlockInfoEvents[i].Client != null)
                    {
                        ValueBlock newVClient = douplicateBlockByBlockIndex(originalB.Info.BlockInfoEvents[i].Client.BlockIndex) as ValueBlock;
                        newBlock.Info.BlockInfoEvents[i].addClient(newVClient);
                    }
                }
            }

            return newBlock;
        }

        #endregion

        #endregion

        private void AddCommentButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region run program

        Run run = new Run();
        private void runButton_Click(object sender, EventArgs e)
        {
            myValues.SetupBeforeRuning();

            string folderPath = "";
            if (filePath != "")
            {
                folderPath = Path.GetDirectoryName(filePath);
            }
            
            run.runProgram(blocks, myValues, folderPath);
        }
        
        #endregion

        #region controls

        private void addControlButton_Click(object sender, EventArgs e)
        {
            MyControl.ControlKinds controlKind = controlsView.getCurControlKind();
            myValues.AddControl(new MyControl(controlKind, myValues.getName(controlKind)));
            
            controlsView.setControlsView();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //it is called when the controls tab is selected
            controlsView.setControlsView();
        }

        #endregion

        #region menu buttons

        string filePath;

        #region new

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ask if wants to save
            if (askToSave()) //if wants to save or dousn't click the cancel button
            {
                deleteAllBlocks();

                newFile();
            }
        }

        #endregion

        #region save

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private bool askToSave()
        {
            string fileText;
            string text;

            if (filePath == "")
            {
                fileText = "Untiteld";
                text = "save ";
            }
            else
            {
                fileText = Path.GetFileName(filePath);
                text = "save changes of ";
            }

            DialogResult dr = MessageBox.Show("Do you want to " + text + fileText + "?", "Save?", MessageBoxButtons.YesNoCancel);

            if (dr == DialogResult.Yes)
            {
                saveFile();
                return true;
            }
            else if (dr == DialogResult.No)
            {
                return true;
            }

            return false;
        }

        private void saveFile()
        {
            if (filePath == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "Gui Scratch Files (*.gus)|*.gus";
                sfd.FileName = "Untiteld";
                if (sfd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    //create a folder that the file will be in it
                    Directory.CreateDirectory(sfd.FileName);

                    string onlyFileName = Path.GetFileName(sfd.FileName);
                    filePath = sfd.FileName+"\\"+onlyFileName+".gus";
                    var s = File.Create(filePath);
                    s.Close();
                }
            }

            //save the file in the file path
            StreamWriter sw = new StreamWriter(filePath);
            string text = FileSaveAndOpen.saveFile(blockIndex, blocks, myValues);
            sw.Write(text);
            sw.Close();

            Clipboard.SetText(text);

            openFile();
        }

        #endregion

        #region open

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Gui Scratch Files (*.gus)|*.gus";

            if (ofd.ShowDialog() == DialogResult.Cancel) //if the user pressed cancel
            {
                return;
            }

            filePath = ofd.FileName;

            openFile();
        }
        
        private void openFile()
        {
            //delete all the blocks
            deleteAllBlocks();

            List<BlockFromString> blocksFromString = new List<BlockFromString>();
            myValues = FileSaveAndOpen.openFile(filePath, ref blockIndex, ref blocksFromString, myValues);
            
            //start the blocks
            blocks = new List<Block>();
            Block b;

            for (int i = 0; i <= blocksFromString.Count - 1; i++)
            {
                b = blocksFromString[i].ToBlock(screenPB, setClientsAndParents, blockRightMouseClick, blockMove);
                
                if (b.Info.NeedForUpdate)
                {
                    b.updateBlock(true);
                }
                if (b.Info.Kind == Block.BlockKinds.Value || b.Info.Kind == Block.BlockKinds.OnStart)
                {
                    setDragAbleAddValueBlockFunc(ref b);
                }

                //set the block
                addBlockToList(b);
            }

            setAllBlocksClients(blocksFromString);

            //set all the things after opening the file
            afterFileOpeningSetup();
        }

        private void setAllBlocksClients(List<BlockFromString> blocksFromString)
        {
            for (int i = 0; i <= blocksFromString.Count - 1; i++)
            {
                if (blocksFromString[i].isAContain) //check if has inside clients
                {
                    int index = getBlockIndexInListFromBlockIndex(blocksFromString[i].BlockIndex);
                    ContainBlock cb = blocks[index] as ContainBlock;

                    //add the inside clients
                    for (int j = 0; j <= blocksFromString[i].insideClientsIndexs.Count - 1; j++)
                    {
                        if (blocksFromString[i].insideClientsIndexs[j] != -1)
                        {
                            int insideClientIndex = getBlockIndexInListFromBlockIndex(blocksFromString[i].insideClientsIndexs[j]);
                            cb.AddInsideClient(blocks[insideClientIndex], j);
                        }
                        
                    }

                    //set the value blocks
                    for (int j = 0; j <= blocksFromString[i].infos.Count - 1; j++)
                    {
                        setValueBlocksForInfo(blocksFromString[i].infos[j]);
                    }
                }
                else //if isnt a contain block
                {
                    setValueBlocksForInfo(blocksFromString[i].Info);
                }

                if (blocksFromString[i].clientIndex != -1) //check if has a client
                {
                    //find the client and parent
                    int index = getBlockIndexInListFromBlockIndex(blocksFromString[i].BlockIndex);
                    int clientIndex = getBlockIndexInListFromBlockIndex(blocksFromString[i].clientIndex);

                    //set the client to be the parnet client
                    blocks[index].AddClient(blocks[clientIndex]);
                }


            }
        }

        private void setValueBlocksForInfo(BlockInfo info)
        {
            for (int i = 0; i <= info.BlockInfoEvents.Count - 1; i++)
            {
                //check if has a value block
                if (info.BlockInfoEvents[i].clientIndex != -1)
                {
                    int valueIndex = getBlockIndexInListFromBlockIndex(info.BlockInfoEvents[i].clientIndex);

                    //set the value block to be in its place
                    info.BlockInfoEvents[i].addClient(blocks[valueIndex] as ValueBlock);
                }
            }
        }

        private void deleteAllBlocks()
        {
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                blocks[i].remove();
            }
        }

        private void setFormText()
        {
            if (filePath == "")
            {
                Text = "Gui Scratch2 - Untiteld";
            }
            else
            {
                Text = "Gui Scratch - "+ Path.GetFileName(filePath);
            }
        }

        #endregion

        #endregion

    }
}
