using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    internal class BlocksToAdd
    {
        private Form form;
        private Panel AddBlocksPanel;
        private Action<List<BlockInfo>, Point> AddBlock;
        public Values myValues;
        Panel btnsPanel;

        public BlocksToAdd(Form form,Panel addBlocksPanel, Values myValues, Action<List<BlockInfo>, Point> addBlockToProgramingPanel, Point programingPanelLocation)
        {
            this.form = form;
            AddBlocksPanel = addBlocksPanel;

            AddBlock = addBlockToProgramingPanel;

            this.myValues = myValues;
            
            ProgramingPanelLocation = programingPanelLocation;

            blockInfos = new List<List<BlockInfo>>();

            //set the buttons panel
            btnsPanel = new Panel();
            btnsPanel.Location = new Point(7, 102);
            btnsPanel.Size = new Size(270, AddBlocksPanel.Height-140);
            btnsPanel.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | AnchorStyles.Left);
            btnsPanel.AutoScroll = true;
            AddBlocksPanel.Controls.Add(btnsPanel);
            btnsPanel.BringToFront();
            
            setTheCategoryButtons();

        }

        #region categories

        FlowLayoutPanel catagoryButtonsPanel;

        RadioButton mainRB;
        RadioButton controlsRB;
        RadioButton operatorsRB;
        RadioButton valuesRB;

        private void setTheCategoryButtons()
        {
            //create the catagory panel
            catagoryButtonsPanel = new FlowLayoutPanel();
            catagoryButtonsPanel.Location = new Point(7, 2);
            catagoryButtonsPanel.Size = new Size(270, 100);
            //catagoryButtonsPanel.BorderStyle = BorderStyle.FixedSingle;
            catagoryButtonsPanel.Font = new Font("Microsoft Sans Serif", 10);
            AddBlocksPanel.Controls.Add(catagoryButtonsPanel);
            catagoryButtonsPanel.BringToFront();

            addCatagoryButton(ref mainRB, "Main", Color.Orange, true);
            addCatagoryButton(ref controlsRB, "Controls", Color.CadetBlue);
            addCatagoryButton(ref operatorsRB, "Operators", Color.MediumSeaGreen);
            addCatagoryButton(ref valuesRB, "Values", Color.Purple);

            //create the current buttons
            Rb_CheckedChanged(null, null);
        }

        private void addCatagoryButton(ref RadioButton rb, string name, Color color, bool isChecked = false)
        {
            rb = new RadioButton();
            rb.Appearance = Appearance.Button;
            rb.Text = name;
            rb.BackColor = color;
            rb.Size = new Size(80, 27);
            rb.Checked = isChecked;
            rb.CheckedChanged += Rb_CheckedChanged;

            catagoryButtonsPanel.Controls.Add(rb);
        }

        private void addButtonToCatagory(string text, EventHandler Btn_Click)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.AutoSize = true;
            btn.Location = new Point(20, height);
            btn.Font = new Font("Microsoft Sans Serif", 10f);
            btn.BackColor = Color.White;
            btn.Click += Btn_Click;

            btnsPanel.Controls.Add(btn);

            height += btn.Height + 15;
        }
        
        public void Rb_CheckedChanged(object sender, EventArgs e)
        {
            //check witch radio button is checked and set the buttons
            clearTheCurButtons();

            if (mainRB.Checked)
            {
                showMainButtons();
            }
            else if (controlsRB.Checked)
            {
                showControlsButtons();
            }
            else if (operatorsRB.Checked)
            {
                showOperatorsButtons();
            }
            else if (valuesRB.Checked)
            {
                showValuesButtons();
            }
        }

        private void clearTheCurButtons()
        {
            blockInfos.Clear();
            btnsPanel.Controls.Clear();

            height = 20;
        }

        #region buttons

        private void showMainButtons()
        {
            Color color = mainRB.BackColor;

            /*//add when clicked block
            BlockCode whenClickedXCode = new BlockCode(new List<CodePart>() { new CodePart("e.X") });
            BlockInfo whenClickedInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When this clicked: "), new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "X",whenClickedXCode, true) }, color, null);
            addBlock(Block.BlockKinds.OnStart, whenClickedInfo);*/
            addButtonToCatagory("Add event func to a control", AddEventFunc_Click);

            //add if block
            BlockCode ifCode = new BlockCode(new List<CodePart>() { new CodePart("if ("), new CodePart(1), new CodePart(")") });
            BlockCode elseCode = new BlockCode(new List<CodePart>() { new CodePart("else ") });

            BlockInfo ifInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("If  "), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, color, ifCode);
            BlockInfo elseInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Else") }, color, elseCode);
            List<BlockInfo> ifElseInfos = new List<BlockInfo>() { ifInfo, elseInfo };
            addBlock(Block.BlockKinds.Contain, ifElseInfos);

            //add repaet block
            BlockCode repeatCode = new BlockCode(new List<CodePart>() { new CodePart("for (int "), new CodePart(true),
                new CodePart("=0;"), new CodePart(true), new CodePart("<(int)"), new CodePart(1), new CodePart(";"),
                new CodePart(true), new CodePart("++)") }, true);

            BlockInfo repeatInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Repeat"), new BlockInfoEvent(10), new BlockInfoEvent("Times") }, color, repeatCode);
            addBlock(Block.BlockKinds.Contain, repeatInfo);

            //add sleep seconds
            BlockCode sleepCode = new BlockCode(new List<CodePart>() { new CodePart("System.Threading.Thread.Sleep((int)"), new CodePart(1), new CodePart(")") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Sleep"), new BlockInfoEvent(1000), new BlockInfoEvent("milliseconds") }, color, sleepCode));

            //add show message box
            BlockCode showMsbCode = new BlockCode(new List<CodePart>() { new CodePart("MessageBox.Show("), new CodePart(1), new CodePart(")") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Show message box"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "Hello world!") }, color, showMsbCode));

            //add exit block
            BlockCode exitCode = new BlockCode(new List<CodePart>() { new CodePart("System.Environment.Exit(1)") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Exit") }, color, exitCode, false));

        }
        
        private void showControlsButtons()
        {
            Color color = controlsRB.BackColor;

            //add set visibility block
            BlockCode setVisibileCode = new BlockCode(new List<CodePart>() { new CodePart(1, true), new CodePart(".Visible = "), new CodePart(3) });
            BlockInfo visibilityInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Set visibility of"), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("To"), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, color, setVisibileCode, true, true, true);
            addBlock(Block.BlockKinds.Action, visibilityInfo);

            //add set text block
            BlockCode setTextCode = new BlockCode(new List<CodePart>() { new CodePart(1, true), new CodePart(".Text = "), new CodePart(3) });
            BlockInfo textInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Set Text of"), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("To"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "Hello world!") }, color, setTextCode, true, true, true);
            addBlock(Block.BlockKinds.Action, textInfo);
            
            //add the set all controls numbers sets block - left, top, width, height
            BlockCode setAllNumbersCode = new BlockCode(new List<CodePart>() { new CodePart(3, true), new CodePart("."),
            new CodePart(1), new CodePart(" = (int)"), new CodePart(5)});
            BlockInfo setAllNumbersInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Set"), new BlockInfoEvent(new string[] { "left", "top", "width", "height" }), new BlockInfoEvent("of"), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("to"), new BlockInfoEvent(100) }, color, setAllNumbersCode, true,true,true);
            addBlock(Block.BlockKinds.Action, setAllNumbersInfo);
            
            //add the set back color block
            BlockCode setBackColorCode = new BlockCode(new List<CodePart>() { new CodePart(1, true), new CodePart(".BackColor = "), new CodePart(3) });
            BlockInfo setBackColorInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Set back color of "), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("to"), new BlockInfoEvent(Color.Blue) }, color, setBackColorCode, true, true, true);
            addBlock(Block.BlockKinds.Action, setBackColorInfo);

            //add the get all controls numbers gets - left, top, width, height
            BlockCode getAllNumbersCode = new BlockCode(new List<CodePart>() { new CodePart(2,true), new CodePart("."), new CodePart(0)});
            BlockInfo getAllNumbersInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(new string[] { "left", "top", "width", "height" }), new BlockInfoEvent("of"), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues) }, color, getAllNumbersCode, true,true,true);
            getAllNumbersInfo.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, getAllNumbersInfo);
        }

        private void showOperatorsButtons()
        {
            Color color = operatorsRB.BackColor;

            //simple math operations info
            BlockCode simpleMathOperationsCode = new BlockCode(new List<CodePart>() { new CodePart("((decimal)"), new CodePart(0), new CodePart(1), new CodePart("(decimal)"), new CodePart(2), new CodePart(")") });
            BlockInfo simpleMathOperations = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(1), new BlockInfoEvent(new string[] { "+", "-", "*", "/", "%" }), new BlockInfoEvent(1) }, color, simpleMathOperationsCode);
            simpleMathOperations.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, simpleMathOperations);

            //equal strings
            BlockCode stringsEqualsCode = new BlockCode(new List<CodePart>() { new CodePart("("), new CodePart(0), new CodePart(1), new CodePart(2), new CodePart(")") });
            BlockInfo stringsEqualsInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), new BlockInfoEvent(new string[] { " == ", "!=" }), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "") }, color, stringsEqualsCode);
            stringsEqualsInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, stringsEqualsInfo);
            
            //equal numbers
            BlockCode numbersEqualsCode = new BlockCode(new List<CodePart>() { new CodePart("("), new CodePart(0), new CodePart(1), new CodePart(2), new CodePart(")") });
            BlockInfo numbersEqualsInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(1), new BlockInfoEvent(new string[] { "==","!=", "<=", ">=", "<", ">" }), new BlockInfoEvent(1) }, color, numbersEqualsCode);
            numbersEqualsInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, numbersEqualsInfo);
            
            //equal booleans
            BlockCode booleansEqualsCode = new BlockCode(new List<CodePart>() { new CodePart("("), new CodePart(0), new CodePart(1), new CodePart(2), new CodePart(")") });
            BlockInfo booleansEqualsInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput), new BlockInfoEvent(new string[] { "==", "!=" }), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, color, booleansEqualsCode);
            booleansEqualsInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, booleansEqualsInfo);
            
            //and booleans
            BlockCode booleansAndCode = new BlockCode(new List<CodePart>() { new CodePart("("), new CodePart(0), new CodePart("&&"), new CodePart(2), new CodePart(")") });
            BlockInfo booleansAndInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput), new BlockInfoEvent("and"), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, color, booleansEqualsCode);
            booleansAndInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, booleansAndInfo);

            //or booleans
            BlockCode booleansOrCode = new BlockCode(new List<CodePart>() { new CodePart("("), new CodePart(0), new CodePart("||"), new CodePart(2), new CodePart(")") });
            BlockInfo booleansOrInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput), new BlockInfoEvent("or"), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, color, booleansEqualsCode);
            booleansOrInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, booleansOrInfo);
        }

        private void showValuesButtons()
        {
            Color color = valuesRB.BackColor;

            //add get text value block
            BlockCode getTextCode = new BlockCode(new List<CodePart>() { new CodePart(1,true), new CodePart(".Text") });
            addBlock(Block.BlockKinds.Value, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Text of") ,new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues)}, Color.Purple, getTextCode, true,true,true));

            //add true button
            BlockCode trueCode = new BlockCode(new List<CodePart>() { new CodePart("true") });
            BlockInfo trueBlockInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("True") }, Color.Purple, trueCode);
            trueBlockInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, trueBlockInfo);

            //add false button
            BlockCode falseCode = new BlockCode(new List<CodePart>() { new CodePart("false") });
            BlockInfo falseBlockInfo = new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("False") }, Color.Purple, falseCode);
            falseBlockInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, falseBlockInfo);

        }

        #region add event func to a control
        
        private void AddEventFunc_Click(object sender, EventArgs e)
        {
            //set the window start position
            Button b = sender as Button;
            Point loc = b.PointToScreen(new Point());
            loc.Offset(-70, -30);

            AddEventFuncForm form = new AddEventFuncForm(myValues, loc,this.form,ProgramingPanelLocation, AddBlock);
            form.Owner = this.form;
            form.Show();
        }
        
        #endregion

        #endregion

        #endregion

        #region add block

        int height = 20;
        List<List<BlockInfo>> blockInfos;

        private void addBlock(Block.BlockKinds kind, BlockInfo blockInfo)
        {
            addBlock(kind, new List<BlockInfo>() { blockInfo });
        }
        
        private void addBlock(Block.BlockKinds kind, List<BlockInfo> infos)
        {
            PictureBox block = new PictureBox();
            drawTheBlock(block, kind, infos);

            block.Name = blockInfos.Count.ToString();

            infos[0].Kind = kind;
            blockInfos.Add(infos);
            
            block.MouseDown += Block_MouseDown;
            block.MouseMove += BlockMessageBox_MouseMove;
            block.MouseUp += BlockMessageBox_MouseUp;
        }

        private void drawTheBlock(PictureBox block, Block.BlockKinds kind, List<BlockInfo> blockInfo)
        {
            int blockHeight = getBlockHeight(kind, blockInfo.Count);
            blockInfo[0].Kind = kind;
            setPictureBox(block, new Point(20, height), BlocksImageCreator.drawBitmap(kind, blockInfo, block, blockHeight));
            height += block.Height + 15;
        }

        private int getBlockHeight(Block.BlockKinds kind, int infosCount)
        {
            if (kind == Block.BlockKinds.Action)
            {
                return 32;
            }
            else if (kind == Block.BlockKinds.Value)
            {
                return 17;
            }
            else if (kind == Block.BlockKinds.Contain)
            {
                return 32+infosCount*47;
            }
            else if (kind == Block.BlockKinds.OnStart)
            {
                return 50;
            }
            return 0;
        }

        private void setPictureBox(PictureBox pb,Point location, Image bmp)
        {
            pb.Size = bmp.Size;
            pb.Location = location;
            pb.Image = bmp;
            btnsPanel.Controls.Add(pb);
        }
        
        #endregion

        #region draging block

        Point lastPoint;

        PictureBox newBlock;
        
        private void Block_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = (PictureBox)sender;

                pb.Parent = form;
                pb.BringToFront();
                pb.BackColor = SystemColors.ControlDark;
                lastPoint = e.Location;

                //create the new block that will stay in the place of the last button
                newBlock = new PictureBox();
                setPictureBox(newBlock, pb.Location, BlocksImageCreator.drawBitmap(blockInfos[int.Parse(pb.Name)][0].Kind, getCloneInfoList(int.Parse(pb.Name)), newBlock, getBlockHeight(blockInfos[int.Parse(pb.Name)][0].Kind, blockInfos[int.Parse(pb.Name)].Count)));
                newBlock.Name = pb.Name;
            }
        }

        private List<BlockInfo> getCloneInfoList(int index)
        {
            List<BlockInfo> res = new List<BlockInfo>();
            for (int i = 0; i <= blockInfos[index].Count - 1; i++)
            {
                res.Add(blockInfos[index][i].Clone());
            }
            return res;
        }

        private void BlockMessageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox curBlock = sender as PictureBox;
                curBlock.Left += (e.X - lastPoint.X);
                curBlock.Top += (e.Y - lastPoint.Y);
            }
        }

        private void BlockMessageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox curBlock = sender as PictureBox;
                if (curBlock.Left > ProgramingPanelLocation.X)
                {
                    AddBlock(getCloneInfoList(int.Parse(curBlock.Name)), new Point(curBlock.Left - ProgramingPanelLocation.X, curBlock.Top - ProgramingPanelLocation.Y));
                }

                form.Controls.Remove(curBlock);

                //set the cur block
                curBlock = newBlock;

                curBlock.MouseDown += Block_MouseDown;
                curBlock.MouseMove += BlockMessageBox_MouseMove;
                curBlock.MouseUp += BlockMessageBox_MouseUp;
            }
        }
        
        #endregion
        
        public Point ProgramingPanelLocation;
    }
}