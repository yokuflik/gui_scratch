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
        #region form1 start funcs

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

        private void Form1_Load(object sender, EventArgs e)
        {
            //make the screenPB cms
            createBScreenPBCms();
            if (filePath == "")
            {
                newFile();
            }
            else
            {
                openFile();
            }
        }

        #region form cms

        ContextMenuStrip screenPBCms = new ContextMenuStrip();

        private void createBScreenPBCms()
        {
            //set the cms buttons

            //create paste button
            ToolStripButton pasteButton = new ToolStripButton();
            pasteButton.Text = "Paste";
            pasteButton.Click += PasteButton_Click;

            //create undo button
            ToolStripButton undoButton = new ToolStripButton();
            undoButton.Text = "Undo";
            undoButton.Click += undoToolStripMenuItem_Click;

            //create redo button
            ToolStripButton redoButton = new ToolStripButton();
            redoButton.Text = "Redo";
            redoButton.Click += redoToolStripMenuItem_Click;

            screenPBCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            pasteButton, undoButton, redoButton});

            //cms.Size = new Size(102, 70);
            screenPB.MouseClick += ScreenPB_MouseClick;

        }

        private void ScreenPB_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                screenPBCms.Show(screenPB.PointToScreen(e.Location));
            }
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            pasteBlocksFromText(Clipboard.GetText());
        }

        private void pasteBlocksFromText(string text, bool isFromClipBoard = true)
        {
            //check if the clip board text is a real block
            string[] vars = text.Split(new char[] { '\x0001' });
            if (vars[0] != blocksCopyStart && isFromClipBoard)
            {
                showTextNotRecognaizedMB();
                return;
            }

            //try to add the blocks to the screen and if has an error it is because the text isn't a blocks text
            try
            {
                makeTheBlocks(vars);
            }
            catch (Exception v)
            {
                if (isFromClipBoard)
                {
                    showTextNotRecognaizedMB();
                }
            }
        }

        private void makeTheBlocks(string[] vars)
        {
            //int index = 1;

            //set the blocks
            List<BlockFromString> bfs = getBfsListFromString(vars);
            /*new List<BlockFromString>();
        while (index < vars.Length)
        {
            bfs.Add(new BlockFromString(myValues, vars, ref index));
        }*/

            //set the blocks clients
            List<Block> newBlocks = new List<Block>();

            for (int i = 0; i <= bfs.Count - 1; i++)
            {
                newBlocks.Add(getBlockFromBfs(bfs[i]));
            }

            setAllBlocksClients(bfs, newBlocks);

            //set the blocks indexes and add the block index
            for (int i = 0; i <= newBlocks.Count - 1; i++)
            {
                newBlocks[i].BlockIndex = blockIndex++;
                addBlockToList(newBlocks[i]);
            }

            //let the user to drag the blocks after the pasting
            startBlock = blocks[blocks.Count - newBlocks.Count];
            moveBlockAfterDouplicate();
        }

        private List<BlockFromString> getBfsListFromString(string[] vars)
        {
            int index = 1;

            List<BlockFromString> bfs = new List<BlockFromString>();
            while (index < vars.Length)
            {
                bfs.Add(new BlockFromString(myValues, vars, ref index));
            }

            return bfs;
        }

        private void showTextNotRecognaizedMB()
        {
            MessageBox.Show("The info was not recognaized. Maybe you copied a text after coping the blocks.");
        }

        #endregion

        #endregion

        #region variabels

        List<Block> blocks = new List<Block>();
        OnStartBlock whenProgramStartsBlock;
        BlocksToAdd bta;
        AddBlockInfoToTheForm AddBlockInfo;

        ControlsView controlsView;

        Values myValues;

        decimal blockIndex = 0;

        #endregion

        #region file

        private void newFile()
        {
            filePath = "";

            //set the values
            myValues = new Values();
            myValues.controls.Add(new MyControl(MyControl.ControlKinds.Form, "this"));

            //set the blocks and the when start block
            blocks = new List<Block>();

            BlockInfo whenStartInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When program starts") }, Color.Orange, null);
            whenStartInfo.CanDeleteAndDouplicate = false;
            whenProgramStartsBlock = new OnStartBlock(screenPB, new Point(20, 50), whenStartInfo, setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++);

            addBlockToList(whenProgramStartsBlock);

            //set all the other things after the new file
            afterFileOpeningSetup();
        }

        private void afterFileOpeningSetup()
        {
            setFormText();

            myValues.updateBlocksFunc = updateBlocksFunc;

            Point programinPanelLocation = new Point(screenPanel.Left + 4, 52);
            AddBlockInfo = new AddBlockInfoToTheForm(this, programinPanelLocation, addBlock, blockMove);

            bta = new BlocksToAdd(addBlocksPanel, myValues, AddBlockInfo, addUndoInfo);

            controlsView = new ControlsView(myValues, controlsFLP, controlsKindsPanel, propertiesPanel);

            //set the window rolling
            setRollingMinusForTheSides(screenPB);

            //set the undo and redo
            undoInfos = new List<UndoInfo>();
            redoInfos = new List<UndoInfo>();
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
                    b = new ActionBlock(screenPB, location, blockInfo[0], setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++, blockInfo[0].CanBeParent);
                    break;
                case Block.BlockKinds.Value:
                    b = new ValueBlock(screenPB, blockInfo[0].ValueKind, location, blockInfo[0], setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++);
                    break;
                case Block.BlockKinds.Contain:
                    b = new ContainBlock(screenPB, location, blockInfo, setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++, blockInfo[0].CanBeParent);
                    break;
                case Block.BlockKinds.OnStart:
                    b = new OnStartBlock(screenPB, location, blockInfo[0], setClientsAndParents, blockRightMouseClick, blockMove, blockIndex++);
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
            b.Info.setAddVarFunc(addVarBlockFromDragAble, AddBlockInfo);
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
                case BlockInfoEvent.Kinds.colorInput:
                    info.ValueKind = ValueBlock.ValueKinds.color;
                    break;
            }

            addBlock(info, location);
        }

        #endregion

        #region move block

        private void blockMove(Block b, bool start)
        {
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

                //set the blocks visibility
                setBlocksVisibility(!start);

            }
            else
            {
                //set the blocks visibility
                setBlocksVisibility(!start);

                //clear all the blocks pictures
                screenPB.BackgroundImage = new Bitmap(1, 1);

                if (b != null)
                {
                    //set the rolling of the window
                    setRolling(screenPB, b.getBottomBlock().getBottomPB());
                }
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
                removeBlockAndHisClientsFromNotMovingList(b, ref res);
            }

            return res;
        }

        private void removeBlockAndHisClientsFromNotMovingList(Block b, ref List<decimal> res)
        {
            Block curB = b;
            while (curB != null)
            {
                res.Remove(getBlockIndexInListFromBlockIndex(curB.BlockIndex, blocks));
                checkInsideClients(ref res, curB);
                curB = curB.Client;
            }
        }

        private void checkInsideClients(ref List<decimal> res, Block curB)
        {
            if (curB.Kind == Block.BlockKinds.Contain)
            {
                ContainBlock cb = curB as ContainBlock;
                for (int i = 0; i <= cb.insideClients.Count - 1; i++)
                {
                    if (cb.insideClients[i] != null)
                    {
                        removeBlockAndHisClientsFromNotMovingList(cb.insideClients[i], ref res);
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            //set the rooling when the widow is resised
            setRollingMinusForTheSides(screenPB);
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
                        //add the current block to the undo list
                        addBlockRemovedEventToUndoList(currentBlock);

                        //delete the block
                        removeBlockByBlockIndex(currentBlock.BlockIndex, true);
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
                            if (blocks[i].Kind != Block.BlockKinds.Value)
                            {
                                if (currentBlock.CheckClient(blocks[i]))
                                {
                                    return true;
                                }
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

            ToolStripButton copyButton = new ToolStripButton();
            copyButton.Size = new Size(151, 22);
            copyButton.Text = "Copy";
            copyButton.Click += copyButton_Click;

            if (!blocks[blocks.Count - 1].Info.CanDeleteAndDouplicate)
            {
                deleteButton.Enabled = douplicateButton.Enabled = copyButton.Enabled = false;
            }
            else if (blocks[blocks.Count - 1].Info.Kind == Block.BlockKinds.OnStart)
            {
                douplicateButton.Enabled = copyButton.Enabled = false;
            }

            ToolStripButton addCommentButton = new ToolStripButton();
            addCommentButton.Size = new Size(151, 22);
            addCommentButton.Text = "Add Comment";
            addCommentButton.Click += AddCommentButton_Click;

            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            deleteButton,
            douplicateButton,
            copyButton,
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
            //add all the blocks that you need to delete to the undo list
            addBlockRemovedEventToUndoList(currentBlock);

            //remove block by block index
            callAllBlockReomveFuncs(currentBlock);

            removeBlockByBlockIndex(currentBlock.BlockIndex, true);

            //set the window rolling
            setRollingMinusForTheSides(screenPB);
        }

        private void callAllBlockReomveFuncs(Block b, bool alsoClient = false)
        {
            if (alsoClient) //delete from client
            {
                if (b.Client != null) //check if is a parent
                {
                    b.Client.removeClient();
                }

                //check for contain blocks
                if (b.Kind == Block.BlockKinds.Contain)
                {
                    ContainBlock cb = b as ContainBlock;
                    for (int i = 0; i <= cb.insideClients.Count - 1; i++)
                    {
                        if (cb.insideClients[i] != null)
                        {
                            //remove all the inside clients
                            cb.insideClients[i].PassedInsideClientRemoved(cb.insideClients[i].InsideClientIndex);
                        }
                    }
                }
            }

            if (b.PassedRemoveFromParent != null) //is a client
            {
                b.PassedRemoveFromParent();
            }
            if (b.PassedInsideClientRemoved != null)
            {
                b.PassedInsideClientRemoved(b.InsideClientIndex);
            }
            if (b.PassedClientChangedClients != null)
            {
                b.PassedClientChangedClients();
            }
        }

        private void removeBlockByBlockIndex(decimal blockIndex, bool removeClients = false, bool removeOnlyThisBlock = false)
        {
            int i = getBlockIndexInListFromBlockIndex(blockIndex, blocks);

            if (i == -1) return; //has no block with this index in the list

            Block curBlock = blocks[i];

            if (removeOnlyThisBlock)
            {
                callAllBlockReomveFuncs(curBlock, true); //if need to delete only this block so call all the block delete funcs to set the view of the other blocks
            }

            blocks.RemoveAt(i);
            curBlock.remove();

            if (!removeOnlyThisBlock)
            {
                removeInsideClients(curBlock);
            }

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

            for (int i = 0; i <= cb.insideClients.Count - 1; i++)
            {
                if (cb.insideClients[i] != null)
                {
                    removeBlockByBlockIndex(cb.insideClients[i].BlockIndex, true);
                }
            }
        }

        private int getBlockIndexInListFromBlockIndex(decimal blockIndex, List<Block> blocks)
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

            moveBlockAfterDouplicate();
        }

        private void moveBlockAfterDouplicate()
        {
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
            startBlockMovingPB.Location = new Point(mouseLocation.X - 10, mouseLocation.Y - 10);
        }

        #endregion

        private Block douplicateBlockByBlockIndex(decimal curBlockIndex, bool removeClients = false)
        {
            int i = getBlockIndexInListFromBlockIndex(curBlockIndex, blocks);

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
            while (curClient != null)
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
                res[res.Count - 1] = douplicateTheValueBlocksOfABlock(curClient, res[res.Count - 1]);

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

        #region copy

        string blocksCopyStart = "blocks copy";
        private void copyButton_Click(object sender, EventArgs e)
        {
            string res = blocksCopyStart;

            copyGetStringFromBlockAndClients(ref res, currentBlock);

            //put the res in the clip board
            Clipboard.SetText(res);
        }

        private void copyGetStringFromBlockAndClients(ref string res, Block curB)
        {
            while (curB != null)
            {
                res += curB.ToString();

                copyCheckForCB(ref res, curB);

                curB = curB.Client;
            }
        }

        private void copyCheckForValueBlock(ref string res, BlockInfo info)
        {
            //check if has a value block and if has add his text to the res
            for (int i = 0; i <= info.BlockInfoEvents.Count - 1; i++)
            {
                if (info.BlockInfoEvents[i].Client != null)
                {
                    res += info.BlockInfoEvents[i].Client.ToString();
                }
            }
        }

        private void copyCheckForCB(ref string res, Block curB)
        {
            if (curB.Kind == Block.BlockKinds.Contain)
            {
                ContainBlock cb = curB as ContainBlock;
                for (int i = 0; i <= cb.insideClients.Count - 1; i++)
                {
                    //add the value blocks
                    copyCheckForValueBlock(ref res, cb.infos[i]);

                    //add the inside clients
                    copyGetStringFromBlockAndClients(ref res, cb.insideClients[i]);
                }
            }
            else
            {
                //if isn't a contain block add the value blocks
                copyCheckForValueBlock(ref res, curB.Info);
            }
        }

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
                //save the project and then run it
                saveFile();
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

        #region file

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
                    filePath = sfd.FileName + "\\" + onlyFileName + ".gus";
                    var s = File.Create(filePath);
                    s.Close();
                }
            }

            //save the file in the file path
            StreamWriter sw = new StreamWriter(filePath);
            string text = FileSaveAndOpen.saveFile(blockIndex, blocks, myValues);
            sw.Write(text);
            sw.Close();

            /*Clipboard.SetText(text);

            openFile();*/
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
            //dont show the user the screen pb until the program ended to load all the project
            screenPB.Visible = false;

            //delete all the blocks
            deleteAllBlocks();

            List<BlockFromString> blocksFromString = new List<BlockFromString>();
            myValues = FileSaveAndOpen.openFile(filePath, ref blockIndex, ref blocksFromString, myValues);

            //start the blocks
            blocks = new List<Block>();
            addBfsListToBlockList(blocksFromString, blocks);

            setAllBlocksClients(blocksFromString, blocks);

            //set all the things after opening the file
            afterFileOpeningSetup();

            //show again the screen pb
            screenPB.Visible = true;
        }

        private void addBfsListToBlockList(List<BlockFromString> blocksFromString, List<Block> blocks)
        {
            Block b;
            for (int i = 0; i <= blocksFromString.Count - 1; i++)
            {
                b = getBlockFromBfs(blocksFromString[i]);

                //set the block
                addBlockToList(b);
            }
        }

        private Block getBlockFromBfs(BlockFromString bfs)
        {
            Block b = bfs.ToBlock(screenPB, setClientsAndParents, blockRightMouseClick, blockMove);

            if (b.Info.NeedForUpdate)
            {
                b.updateBlock(true);
            }
            if (b.Info.Kind == Block.BlockKinds.Value || b.Info.Kind == Block.BlockKinds.OnStart)
            {
                setDragAbleAddValueBlockFunc(ref b);
            }

            return b;
        }

        private void setAllBlocksClients(List<BlockFromString> blocksFromString, List<Block> blocks)
        {
            for (int i = 0; i <= blocksFromString.Count - 1; i++)
            {
                if (blocksFromString[i].isAContain) //check if has inside clients
                {
                    int index = getBlockIndexInListFromBlockIndex(blocksFromString[i].BlockIndex, blocks);
                    ContainBlock cb = blocks[index] as ContainBlock;

                    //add the inside clients
                    for (int j = 0; j <= blocksFromString[i].insideClientsIndexs.Count - 1; j++)
                    {
                        if (blocksFromString[i].insideClientsIndexs[j] != -1)
                        {
                            int insideClientIndex = getBlockIndexInListFromBlockIndex(blocksFromString[i].insideClientsIndexs[j], blocks);
                            cb.AddInsideClient(blocks[insideClientIndex], j);
                        }

                    }

                    //set the value blocks
                    for (int j = 0; j <= blocksFromString[i].infos.Count - 1; j++)
                    {
                        setValueBlocksForInfo(blocksFromString[i].infos[j], blocks);
                    }
                }
                else //if isnt a contain block
                {
                    setValueBlocksForInfo(blocksFromString[i].Info, blocks);
                }

                if (blocksFromString[i].clientIndex != -1) //check if has a client
                {
                    //find the client and parent
                    int index = getBlockIndexInListFromBlockIndex(blocksFromString[i].BlockIndex, blocks);
                    int clientIndex = getBlockIndexInListFromBlockIndex(blocksFromString[i].clientIndex, blocks);

                    //set the client to be the parnet client
                    blocks[index].AddClient(blocks[clientIndex]);
                }


            }
        }

        private void setValueBlocksForInfo(BlockInfo info, List<Block> blocks)
        {
            for (int i = 0; i <= info.BlockInfoEvents.Count - 1; i++)
            {
                //check if has a value block
                if (info.BlockInfoEvents[i].clientIndex != -1)
                {
                    int valueIndex = getBlockIndexInListFromBlockIndex(info.BlockInfoEvents[i].clientIndex, blocks);

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
                Text = "Gui Scratch - " + Path.GetFileName(filePath);
            }
        }

        #endregion

        #endregion

        #region edit

        #region undo

        List<UndoInfo> undoInfos;
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoInfos.Count > 0) //check if has something to undo
            {
                UndoInfo curUndo = undoInfos[undoInfos.Count - 1];
                switch (curUndo.Kind)
                {
                    case UndoInfo.Kinds.removeBlock:
                        undoRemoveBlock(ref curUndo);
                        break;
                    case UndoInfo.Kinds.removeValue:
                        if (!undoRemoveValue(curUndo)) //it happens when the deleted var name is already in use
                        {
                            undoInfos.RemoveAt(undoInfos.Count - 1); //remove it from the undo list
                            return;
                        }
                        break;
                }

                //save the last undo in the redo list and then delete it
                redoInfos.Add(curUndo);
                undoInfos.RemoveAt(undoInfos.Count - 1);
            }
        }

        #region remove block

        private void undoRemoveBlock(ref UndoInfo curUndo)
        {
            //save the last blocks count to know what is the new block index of the first block
            int lastBlockCount = blocks.Count;

            //create the blocks and putt them in let the user to drag them with the mouse
            pasteBlocksFromText(curUndo.InfoText, false);

            //get the new blocks text
            Block firstBlock = blocks[lastBlockCount];
            string res = "";
            copyGetStringFromBlockAndClients(ref res, firstBlock);

            //set the curUndo text
            curUndo.InfoText = res;
        }

        private void addBlockRemovedEventToUndoList(Block removedBlock)
        {
            string blocksText = "";
            copyGetStringFromBlockAndClients(ref blocksText, removedBlock);
            undoInfos.Add(new UndoInfo(UndoInfo.Kinds.removeBlock, blocksText));
        }

        #endregion

        #region remove value

        private void addUndoInfo(UndoInfo undoInfo)
        {
            undoInfos.Add(undoInfo);
        }

        private bool undoRemoveValue(UndoInfo curUndo)
        {
            //undo that remove value
            string[] vars = FileSaveAndOpen.getStringList(curUndo.InfoText);
            int index = 1;

            switch (curUndo.RemoveValueKind)
            {
                case UndoInfo.RemoveValueKinds.variabel:
                    MyVar deletedVar = new MyVar(vars, ref index);

                    //check if the name is in use
                    if (checkIfTheNameIsInUse(myValues.getVars(), deletedVar.Name, "var"))
                    {
                        return false; //returns false when needs to delete the last undo
                    }

                    myValues.addVar(deletedVar);

                    break;
                case UndoInfo.RemoveValueKinds.list:
                    MyList deletedList = new MyList(vars, ref index);

                    //check if the name is in use
                    if (checkIfTheNameIsInUse(myValues.getLists(), deletedList.Name, "list"))
                    {
                        return false; //returns false when needs to delete the last undo
                    }

                    myValues.addList(deletedList);

                    break;
                case UndoInfo.RemoveValueKinds.func:
                    myValues.addFunc(new MyFunc(myValues, vars, ref index));
                    break;
            }

            return true; //return true if every thing is ok;
        }

        private bool checkIfTheNameIsInUse(string[] names, string curName, string kindString)
        {
            //check if the names list contains the curName
            for (int i = 0; i <= names.Length - 1; i++)
            {
                if (names[i] == curName)
                {
                    //the name is now in use so the user can't do it
                    MessageBox.Show("The name of the " + kindString + " is in use now so you can't undo the " + kindString + ".\nif you want you can make a new" + kindString);
                    return true;
                }
            }

            return false; //returns false if the name isn't in use now
        }

        #endregion

        #endregion

        #region redo

        List<UndoInfo> redoInfos;
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void redo()
        {
            if (redoInfos.Count > 0) //check if has something to undo
            {
                UndoInfo curRedo = redoInfos[redoInfos.Count - 1];
                switch (curRedo.Kind)
                {
                    case UndoInfo.Kinds.removeBlock:
                        redoRemoveBlock(curRedo);
                        break;
                    case UndoInfo.Kinds.removeValue:
                        if (!redoRemoveValue(curRedo)) //happens when the user deleted the value
                        {
                            redoInfos.RemoveAt(redoInfos.Count - 1);

                            //make the next redo and return
                            redo();
                            return;
                        }

                        updateBlocksFunc(); //update the blocks view

                        break;
                }

                //save the last redo in the undo list and then delete it
                undoInfos.Add(curRedo);
                redoInfos.RemoveAt(redoInfos.Count - 1);
            }
        }

        private void redoRemoveBlock(UndoInfo curRedo)
        {
            //get all the blocks indexes and remove them

            //get all the blocks info
            string[] vars = FileSaveAndOpen.getStringList(curRedo.InfoText);
            List<BlockFromString> bfs = getBfsListFromString(vars);

            //delete all the blocks by there block index
            for (int i = 0; i <= bfs.Count - 1; i++)
            {
                //get the current block index in the list and then delete it
                removeBlockByBlockIndex(bfs[i].BlockIndex, false, true);
            }
        }

        #region redo remove value

        private bool redoRemoveValue(UndoInfo curRedo)
        {
            //redo that remove value
            string[] vars = FileSaveAndOpen.getStringList(curRedo.InfoText);
            int index = 1;
            int nameIndex;

            //find the name
            switch (curRedo.RemoveValueKind)
            {
                case UndoInfo.RemoveValueKinds.variabel:
                    MyVar deletedVar = new MyVar(vars, ref index);

                    nameIndex = getIndexOfNameInList(myValues.getVars(), deletedVar.Name);

                    if (nameIndex == -1)//when the var was removed
                    {
                        return false;
                    }

                    myValues.vars.RemoveAt(nameIndex);

                    break;
                case UndoInfo.RemoveValueKinds.list:
                    MyList deletedList = new MyList(vars, ref index);

                    nameIndex = getIndexOfNameInList(myValues.getLists(), deletedList.Name);

                    if (nameIndex == -1)//when the var was removed
                    {
                        return false;
                    }

                    myValues.lists.RemoveAt(nameIndex);

                    break;
                case UndoInfo.RemoveValueKinds.func:
                    MyFunc deletedFunc = new MyFunc(myValues, vars, ref index);

                    //find the func by the func name
                    for (int i = 0; i <= myValues.funcs.Count - 1; i++)
                    {
                        if (myValues.funcs[i].Name == deletedFunc.Name)
                        {
                            myValues.funcs.RemoveAt(i);
                            return true;
                        }
                    }

                    //the user deleted the func so return false ro call the next redo
                    return false;
            }

            return true; //if every thing is ok
        }

        private int getIndexOfNameInList(string[] names, string name)
        {
            for (int i = 0; i <= names.Length - 1; i++)
            {
                if (names[i] == name)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #endregion

        #endregion

        #region advanced

        private void addNewBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show the create new block form
            CreateNewBlockForm myForm = new CreateNewBlockForm(myValues, AddBlockInfo);

            myForm.Owner = this;
            myForm.Show();
        }

        #region update files

        List<List<string>> allPossiableBlocks;
        private void updateTheFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make the user to choose a folder to update all the files in it
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose a folder with a folders of the gui scratch projects";
            fbd.SelectedPath = "D:\\Yoel\\gui scratch\\projects";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //make the block to dont show the screen PB
                //screenPB.Parent = null;
                string lastFilePath = filePath;

                allPossiableBlocks = bta.getAllPossibleBlocks();

                checkFolders(fbd.SelectedPath);
                
                //screenPB.Parent = screenPanel;

                //set the opend file to the last file
                filePath = lastFilePath;
                if (lastFilePath != "")
                {
                    openFile();
                }
                else
                {
                    newFile();
                }
            }
        }

        private void checkFolders(string path)
        {
            //check for all the gui scratch folders
            string[] folders = Directory.GetDirectories(path);

            //for every folder check if it is a gui scratch project folder and if it is update it
            for (int i = 0; i <= folders.Length - 1; i++)
            {
                checkFolder(folders[i]);
            }
        }

        private void checkFolder(string path)
        {
            string[] files = Directory.GetFiles(path);

            for (int i = 0; i <= files.Length - 1; i++)
            {
                if (Path.GetExtension(files[i]) == ".gus") //the file is a gui scratch project
                {
                    openTheCurrentFile(files[i]);
                }
            }
        }

        private void openTheCurrentFile(string path)
        {
            //open the file and then save it
            filePath = path;

            openFile();

            setTheBlocks(); //update the blocks

            saveFile();
        }

        private void setTheBlocks()
        {
            //check if a block is a block from the possible blocks list
            List<string> curBlockCode = new List<string>();
            for (int i =1;i<=blocks.Count-1;i++)
            {
                //set the cur block code list and then check to witch block code it matches
                if (blocks[i].Kind == Block.BlockKinds.Contain)
                {
                    curBlockCode = new List<string>();
                    ContainBlock cb = blocks[i] as ContainBlock;
                    foreach (BlockInfo bi in cb.infos)
                    {
                        curBlockCode.Add(bi.BlockCode.ToString());
                    }
                }
                else
                {
                    curBlockCode = new List<string>() { blocks[i].Info.BlockCode.ToString() };
                }

                //check to witch block it matches
                foreach (List<string> curB in allPossiableBlocks)
                {
                    if (setTheBlockCode(curB, curBlockCode, i))
                    {
                        break;
                    }
                }
            }

        }

        private bool setTheBlockCode(List<string> blockInfo, List<string> blockCode, int indexInBlocksList)
        {
            List<string> blockInfoCode = new List<string>();
            List<BlockInfo> blockInfos = new List<BlockInfo>();
            
            for (int i = 0; i <= blockInfo.Count - 1; i++)
            {
                //make the block infos in the list to areal block info and then save there code
                string[] vars = FileSaveAndOpen.getStringList(blockInfo[i]);
                int index = 1;
                BlockInfo curInfo = new BlockInfo(myValues, vars, ref index);
                blockInfos.Add(curInfo);

                blockInfoCode.Add(curInfo.BlockCode.ToString());
            }

            if (checkTwoCodeListsIfEquals(blockInfoCode, blockCode))
            {
                //the block founded

                //save the block
                if (blocks[indexInBlocksList].Kind == Block.BlockKinds.Contain)
                {
                    ContainBlock cb = blocks[indexInBlocksList] as ContainBlock;
                    
                    //save all the infos
                    for (int i = 0; i <= blockInfos.Count - 1; i++)
                    {
                        cb.infos[i] = blockInfos[i];
                    }

                    //save the block
                    blocks[indexInBlocksList] = cb;
                }
                else
                {
                    blocks[indexInBlocksList].Info = blockInfos[0];
                }
            }

            return false;
        }

        private bool checkTwoCodeListsIfEquals(List<string> one, List<string> two)
        {
            if (one.Count == two.Count)
            {
                for (int i = 0; i <= one.Count - 1; i++)
                {
                    if (one[i] != two[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #endregion
    }
}
