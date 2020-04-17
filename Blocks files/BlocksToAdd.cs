using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    internal class BlocksToAdd
    {
        #region variabels

        private Form form;
        private Panel AddBlocksPanel;
        private Action<List<BlockInfo>, Point> AddBlock;
        public Values myValues;
        Panel btnsPanel;

        AddBlockInfoToTheForm AddBlockInfo;

        public Point ProgramingPanelLocation;

        Action<UndoInfo> AddUndoFunc;

        #endregion

        public BlocksToAdd(Panel addBlocksPanel, Values myValues, AddBlockInfoToTheForm addBlockInfo, Action<UndoInfo> addUndoFunc)
        {
            //save all the variabels
            form = addBlockInfo.form;
            AddBlocksPanel = addBlocksPanel;

            AddBlock = addBlockInfo.AddBlock;

            this.myValues = myValues;
            
            ProgramingPanelLocation = addBlockInfo.ProgramingPanelLocation;
            
            AddBlockInfo = addBlockInfo;

            AddUndoFunc = addUndoFunc;

            blockInfos = new List<BTABlockInfo>();

            //set the buttons panel
            btnsPanel = new Panel();
            btnsPanel.Location = new Point(7, 112);
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
        RadioButton variabelsRB;
        RadioButton funcsRB;

        //Button createBlockBtn;

        private void setTheCategoryButtons()
        {
            //add the add block button
            /*createBlockBtn = new Button();
            createBlockBtn.Location = new Point(40, 8);
            createBlockBtn.Size = new Size(200, 30);
            createBlockBtn.Text = "Create a new block";
            createBlockBtn.Font = new Font("Microsoft Sans Serif", 12f);
            createBlockBtn.BackColor = Color.Orange;
            createBlockBtn.Click += CreateBlockBtn_Click;
            AddBlocksPanel.Controls.Add(createBlockBtn);*/

            //create the catagory panel
            catagoryButtonsPanel = new FlowLayoutPanel();
            catagoryButtonsPanel.Location = new Point(7, 12);
            catagoryButtonsPanel.Size = new Size(270, 100);
            //catagoryButtonsPanel.BorderStyle = BorderStyle.FixedSingle;
            catagoryButtonsPanel.Font = new Font("Microsoft Sans Serif", 10);
            AddBlocksPanel.Controls.Add(catagoryButtonsPanel);
            catagoryButtonsPanel.BringToFront();

            addCatagoryButton(ref mainRB, "Main", Color.Orange, true);
            addCatagoryButton(ref controlsRB, "Controls", Color.CadetBlue);
            addCatagoryButton(ref operatorsRB, "Operators", Color.MediumSeaGreen);
            addCatagoryButton(ref valuesRB, "Values", Color.Purple);
            addCatagoryButton(ref variabelsRB, "Variabels", Color.Red);
            addCatagoryButton(ref funcsRB, "Funcs", Color.Coral);

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
        
        //this func is called when the selected catagory is changed
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
            else if (variabelsRB.Checked)
            {
                showVariabelButtons();
            }
            else if (funcsRB.Checked)
            {
                showFuncsButtons();
            }
        }

        private void clearTheCurButtons()
        {
            blockInfos.Clear();
            btnsPanel.Controls.Clear();

            height = 20;
        }

        #region all blocks

        private void showMainButtons()
        {
            Color color = mainRB.BackColor;
            
            addButtonToCatagory("Add event func to a control", AddEventFunc_Click);
            
            //add the if block
            BlockCode onlyIfCode = new BlockCode(new List<CodePart>() { new CodePart("if ("), new CodePart(1, false, false, false), new CodePart(")"), });
            BlockInfo onlyIfInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("if "), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput), }, onlyIfCode, color, false);
            addBlock(Block.BlockKinds.Contain, onlyIfInfo);

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

            //add the while block
            BlockCode whileCode = new BlockCode(new List<CodePart>() { new CodePart("while ("), new CodePart(1), new CodePart(")") });
            BlockInfo whileInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("While "), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, whileCode, color);
            addBlock(Block.BlockKinds.Contain, whileInfo);

            //add sleep seconds
            BlockCode sleepCode = new BlockCode(new List<CodePart>() { new CodePart("System.Threading.Thread.Sleep((int)"), new CodePart(1), new CodePart(")") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Sleep"), new BlockInfoEvent(1000), new BlockInfoEvent("milliseconds") }, color, sleepCode));

            //add show message box
            BlockCode showMsbCode = new BlockCode(new List<CodePart>() { new CodePart("MessageBox.Show("), new CodePart(1), new CodePart(")") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Show message box2"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "Hello world!") }, color, showMsbCode));

            //add exit block
            BlockCode exitCode = new BlockCode(new List<CodePart>() { new CodePart("System.Environment.Exit(1)") });
            addBlock(Block.BlockKinds.Action, new BlockInfo(myValues, new List<BlockInfoEvent>() { new BlockInfoEvent("Exit") }, color, exitCode, false));

        }
        
        private void showControlsButtons()
        {
            Color color = controlsRB.BackColor;

            //bring to front button
            BlockCode bringToFrontCode = new BlockCode(new List<CodePart>() { new CodePart(1, true), new CodePart(".BringToFront();") });
            BlockInfo bringToFrontInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Bring "), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("to be on the front of the screen") }, bringToFrontCode, color, true, true, true);
            addBlock(Block.BlockKinds.Action, bringToFrontInfo);

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

            //add the set image block
            BlockCode setBlockImageCode = new BlockCode(new List<CodePart>() { new CodePart(1, true), new CodePart(".Image = Image.FromFile("), new CodePart(3), new CodePart(");") });
            BlockInfo setBlockImageInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Set image of "), new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, myValues), new BlockInfoEvent("to "), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "") }, setBlockImageCode, color, true, true, true);
            addBlock(Block.BlockKinds.Action, setBlockImageInfo);
            
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
            
            //add the highMathFuncs block
            BlockCode highMathFuncsCode = new BlockCode(new List<CodePart>() { new CodePart("Math."), new CodePart(0, false, false, false), new CodePart("("), new CodePart(2, false, false, false), new CodePart(")"), });
            BlockInfo highMathFuncsInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent(new string[]{ "sin", "cos", "tan", "max", "min","abs","ceiling","floor","round", "sqrt", "sinh","cosh","tanh","asin","acos","atan" }), new BlockInfoEvent("of"), new BlockInfoEvent((decimal)0), }, highMathFuncsCode, color, false);
            highMathFuncsInfo.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, highMathFuncsInfo);

            //add the converToRadians block
            BlockCode converToRadiansCode = new BlockCode(new List<CodePart>() { new CodePart("(Math.PI / 180) * (double)"), new CodePart(1, false, false, false), });
            BlockInfo converToRadiansInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Convert"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("to radians"), }, converToRadiansCode, color, false);
            converToRadiansInfo.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, converToRadiansInfo);

            //add the pickRandomNumber block
            BlockCode pickRandomNumberCode = new BlockCode(new List<CodePart>() { new CodePart("rnd.Next("), new CodePart(1, false, false, false), new CodePart(", "), new CodePart(3, false, false, false), new CodePart(")"), });
            BlockInfo pickRandomNumberInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("pick a random number from"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("to"), new BlockInfoEvent((decimal)0), }, pickRandomNumberCode, color, false);
            pickRandomNumberInfo.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, pickRandomNumberInfo);

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

            //add the not block
            BlockCode notCode = new BlockCode(new List<CodePart>() { new CodePart("!"), new CodePart(1, false, false, false)});
            BlockInfo notInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("not"), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput), }, notCode, color, false);
            notInfo.ValueKind = ValueBlock.ValueKinds.boolean;
            addBlock(Block.BlockKinds.Value, notInfo);
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

            //add string to int button
            BlockCode stringToIntCode = new BlockCode(new List<CodePart>() { new CodePart("int.Parse("), new CodePart(0), new CodePart(")") });
            BlockInfo stringToIntInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "") }, stringToIntCode, color);
            stringToIntInfo.ValueKind = ValueBlock.ValueKinds.number;
            addBlock(Block.BlockKinds.Value, stringToIntInfo);

        }

        #region show varaibels and lists

        private void showVariabelButtons(bool showLists = true)
        {
            Color color = variabelsRB.BackColor;

            //add the addNewVariabel block
            BlockCode addNewVariabelCode = new BlockCode(new List<CodePart>() { new CodePart("string "), new CodePart(1, false, true), new CodePart(" = "), new CodePart(3, false, false, false), });
            BlockInfo addNewVariabelInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Create new variabel - Name:"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "myVar", true), new BlockInfoEvent("Value:"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), }, addNewVariabelCode, color, false);
            addBlock(Block.BlockKinds.Action, addNewVariabelInfo);

            //add the set var value
            BlockCode setValueCode = new BlockCode(new List<CodePart>() { new CodePart(1, false, true), new CodePart(" = "), new CodePart(3) });
            BlockInfo setValueInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Set"),
                new BlockInfoEvent(myValues), new BlockInfoEvent("to"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput,"") },
                setValueCode, color, true);
            addBlock(Block.BlockKinds.Action, setValueInfo);

            //add the getValueOfUserVar block
            BlockCode getValueOfUserVarCode = new BlockCode(new List<CodePart>() { new CodePart(1, false, true) });
            BlockInfo getValueOfUserVarInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Value of"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "", true), }, getValueOfUserVarCode, color, false);
            getValueOfUserVarInfo.ValueKind = ValueBlock.ValueKinds.text;
            addBlock(Block.BlockKinds.Value, getValueOfUserVarInfo);

            //add the add variabels button
            addButtonToCatagory("Add variabel", AddVariabels_Click);

            //show this buttons only if the user made a var
            if (myValues.vars.Count != 0)
            {
                //add all the variabels blocks

                //add the vars open list button
                addButtonToCatagory("Vars", ShowVars_Click);
            }

            if (showLists)
            {
                //set the vars to be closed
                varsOpend = false;
                listsOpend = false;

                showAllTheListBlocks();
            }

        }

        private void showAllTheListBlocks()
        {
            Color color = Color.Red;

            //add the userCreateNewList block
            BlockCode userCreateNewListCode = new BlockCode(new List<CodePart>() { new CodePart("List<string> "), new CodePart(1, false, false, true), new CodePart(" = new List<string>()"), });
            BlockInfo userCreateNewListInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Create new list - Name:"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "myList", true), }, userCreateNewListCode, color, false);
            addBlock(Block.BlockKinds.Action, userCreateNewListInfo);

            //add the add lists button
            addButtonToCatagory("Add list", AddLists_Click);

            //show this buttons only if the user made a list
            if (myValues.lists.Count != 0)
            {
                //add all the lists blocks

                //add the add item to list
                BlockCode addItemCode = new BlockCode(new List<CodePart>() { new CodePart(3, false, false, true), new CodePart(".Add("), new CodePart(1), new CodePart(")") });
                BlockInfo addItemInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Add"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), new BlockInfoEvent("to"), new BlockInfoEvent(myValues, true) }, addItemCode, color, true);
                addBlock(Block.BlockKinds.Action, addItemInfo);

                List<string> l = new List<string>();
                //l.Insert(1, "");

                //add the delete item from list
                BlockCode deleteItemCode = new BlockCode(new List<CodePart>() { new CodePart(3, false, false, true), new CodePart(".Remove("), new CodePart(1), new CodePart(")") });
                BlockInfo deleteItemInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Remove"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), new BlockInfoEvent("from"), new BlockInfoEvent(myValues, true) }, deleteItemCode, color, true);
                addBlock(Block.BlockKinds.Action, deleteItemInfo);

                //add the remove range block
                BlockCode removeRangeCode = new BlockCode(new List<CodePart>() { new CodePart(5, false, false, true), new CodePart(".RemoveRange("), new CodePart(1), new CodePart(", "), new CodePart(3), new CodePart(")") });
                BlockInfo removeRangeInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Remove from"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("Count:"), new BlockInfoEvent((decimal)1), new BlockInfoEvent("from"), new BlockInfoEvent(myValues, true) }, removeRangeCode, color, true);
                addBlock(Block.BlockKinds.Action, removeRangeInfo);

                //add the insert item block
                BlockCode insertItemCode = new BlockCode(new List<CodePart>() { new CodePart(5, false, false, true), new CodePart(".Insert((int)"), new CodePart(3), new CodePart(", "), new CodePart(1), new CodePart(")") });
                BlockInfo insertItemInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Insert"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), new BlockInfoEvent("at"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("in"), new BlockInfoEvent(myValues, true) }, insertItemCode, color, true);
                addBlock(Block.BlockKinds.Action, insertItemInfo);
                
                //add the replaceItem block
                BlockCode replaceItemCode = new BlockCode(new List<CodePart>() { new CodePart(3, false, false, true), new CodePart("["), new CodePart(1, false, false, false), new CodePart("] = "), new CodePart(5, false, false, false), });
                BlockInfo replaceItemInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Replace item"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("of"), new BlockInfoEvent(myValues, true), new BlockInfoEvent("with"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), }, replaceItemCode, color, true);
                addBlock(Block.BlockKinds.Action, replaceItemInfo);
                
                //add the itemOfList block
                BlockCode itemOfListCode = new BlockCode(new List<CodePart>() { new CodePart(3, false, false, true), new CodePart("["), new CodePart(1, false, false, false), new CodePart("]"), });
                BlockInfo itemOfListInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Item"), new BlockInfoEvent((decimal)0), new BlockInfoEvent("of"), new BlockInfoEvent(myValues, true), }, itemOfListCode, color, true);
                itemOfListInfo.ValueKind = ValueBlock.ValueKinds.text;
                addBlock(Block.BlockKinds.Value, itemOfListInfo);
                
                //add the Length block
                BlockCode LengthCode = new BlockCode(new List<CodePart>() { new CodePart(1, false, false, true), new CodePart(".Count"), });
                BlockInfo LengthInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("Length of"), new BlockInfoEvent(myValues, true), }, LengthCode, color, true);
                LengthInfo.ValueKind = ValueBlock.ValueKinds.number;
                addBlock(Block.BlockKinds.Value, LengthInfo);

                //add the checkIfContains block
                BlockCode checkIfContainsCode = new BlockCode(new List<CodePart>() { new CodePart(0, false, false, true), new CodePart(".Contains("), new CodePart(2, false, false, false), new CodePart(")"), });
                BlockInfo checkIfContainsInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent(myValues, true), new BlockInfoEvent("contains"), new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""), new BlockInfoEvent("?"), }, checkIfContainsCode, color, true);
                checkIfContainsInfo.ValueKind = ValueBlock.ValueKinds.boolean;
                addBlock(Block.BlockKinds.Value, checkIfContainsInfo);

                //add the lists open list button
                addButtonToCatagory("Lists", ShowLists_Click);
            }
        }

        #endregion
        
        private void showFuncsButtons()
        {
            Color color = Color.Coral;

            //add the add func Button
            addButtonToCatagory("Add func", AddFunc_Click);

            //create all the funcs buttons
            for (int i = 0; i <= myValues.funcs.Count - 1; i++)
            {
                //show the call func block
                addBlock(Block.BlockKinds.Action, myValues.funcs[i].Info, listBlock_Click, i, BTABlockInfo.ListKinds.funcs);
            }
        }
        
        private BlockInfo makeBlockInfo(List<BlockInfoEvent> events, BlockCode code, Color color, bool needForUpdate = false, bool canBeParent = true, bool canBeClient = true)
        {
            return new BlockInfo(myValues, events, color, code, canBeParent, canBeClient, needForUpdate);
        }

        #region buttons

        private void openFormFromAButton(object sender, Form form)
        {
            //set the window start position
            Button b = sender as Button;
            Point loc = b.PointToScreen(new Point());
            loc.Offset(-70, -30);

            loc.X = Math.Max(10, loc.X); //set the widow location to not be after the 0

            form.Location = loc;

            form.Owner = this.form;
            form.Show();
        }

        #region add event func to a control

        private void AddEventFunc_Click(object sender, EventArgs e)
        {
            AddEventFuncForm form = new AddEventFuncForm(myValues,AddBlockInfo);
            openFormFromAButton(sender, form);
        }

        #endregion

        #region add variabels and lists

        #region variabels

        private void AddVariabels_Click(object sender, EventArgs e)
        {
            openFormFromAButton(sender, new AddVariabelsAndListsForm(myValues, updateVars, true));
        }

        private void ShowVars_Click(object sender, EventArgs e)
        {
            updateVars();
        }
        
        bool varsOpend = false;
        private void updateVars(bool showVars = false)
        {
            //show all the vars in a list
            if (showVars)
            {
                //this thing is made when the user adds a variabel so it opens the list of the vars outomaticly
                varsOpend = true;
            }
            else
            {
                varsOpend = !varsOpend;
            }

            Color color = Color.Red;

            //clear all the values and set the height to the startup value
            btnsPanel.Controls.Clear();

            height = 20;

            if (varsOpend)
            {
                //show the variabels blocks
                showVariabelButtons(false);
                
                //show the vars
                for (int i = 0; i <= myValues.vars.Count - 1; i++)
                {
                    //myValues.vars[i]
                    BlockCode varCode = new BlockCode(new List<CodePart>() { new CodePart(myValues.vars[i].Name, false, true) });
                    BlockInfo varInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent(myValues.vars[i].Name) }, varCode, color);
                    addBlock(Block.BlockKinds.Value, varInfo, listBlock_Click, i, BTABlockInfo.ListKinds.vars);
                }

                //add the lists blocks
                showAllTheListBlocks();
            }
            else
            {
                showVariabelButtons();
            }
        }

        #endregion
        
        #region lists

        private void AddLists_Click(object sender, EventArgs e)
        {
            openFormFromAButton(sender, new AddVariabelsAndListsForm(myValues, null, false));
        }

        private void ShowLists_Click(object sender, EventArgs e)
        {
            updateLists();
        }

        bool listsOpend = false;
        private void updateLists(bool showLists = false)
        {
            //show all the lists in a list
            if (showLists)
            {
                //this thing is made when the user adds a list so it opens the list of the lists outomaticly
                listsOpend = true;
            }
            else
            {
                listsOpend = !listsOpend;
            }

            Color color = Color.Red;

            if (listsOpend)
            {
                //show the lists
                for (int i = 0; i <= myValues.lists.Count - 1; i++)
                {
                    BlockCode listCode = new BlockCode(new List<CodePart>() { new CodePart(myValues.lists[i].Name, false, true) });
                    BlockInfo listInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent(myValues.lists[i].Name) }, listCode, color);
                    addBlock(Block.BlockKinds.Value, listInfo, listBlock_Click, i, BTABlockInfo.ListKinds.lists);
                }
            }
            else
            {
                //clear all the lists blocks
                for (int i = 0; i <= myValues.lists.Count - 1; i++)
                {
                    btnsPanel.Controls.RemoveAt(btnsPanel.Controls.Count - 1);
                }

                //set the height
                height = btnsPanel.Controls[btnsPanel.Controls.Count - 1].Bottom + 15;
            }
        }
        

        #endregion

        #endregion

        #region add func

        private void AddFunc_Click(object sender, EventArgs e)
        {
            AddFuncForm form = new AddFuncForm(myValues, AddBlockInfo, showFuncsButtons);
            openFormFromAButton(sender, form);
        }

        #endregion

        #region delete from list

        private void listBlock_Click(object sender, MouseEventArgs e)
        {
            //only if it was a right click
            if (e.Button == MouseButtons.Right)
            {
                Control c = sender as Control;

                //show a delete button
                ContextMenuStrip cms = new ContextMenuStrip();

                ToolStripButton deleteButton = new ToolStripButton();
                deleteButton.Size = new Size(60, 22);
                deleteButton.Text = "Delete";
                deleteButton.Click += listBlockDeleteButton_Click;
                deleteButton.Name = c.Name;

                cms.Items.Add(deleteButton);
                
                cms.AutoClose = true;

                cms.Show(c, e.Location);
            }
            
        }

        private void listBlockDeleteButton_Click(object sender, EventArgs e)
        {
            ToolStripButton c = sender as ToolStripButton;
            blockInfos[int.Parse(c.Name)].deleteTheCurIndexFromTheList(myValues, AddUndoFunc); //remove the current list index
            
        }

        #endregion

        #endregion

        #endregion

        #endregion
        
        #region add block

        int height = 20;
        List<BTABlockInfo> blockInfos;

        private void addBlock(Block.BlockKinds kind, BlockInfo blockInfo, MouseEventHandler blockClick = null, int listIndex = 0, BTABlockInfo.ListKinds listKind = BTABlockInfo.ListKinds.funcs)
        {
            addBlock(kind, new List<BlockInfo>() { blockInfo }, blockClick, listIndex, listKind);
        }
        
        private void addBlock(Block.BlockKinds kind, List<BlockInfo> infos, MouseEventHandler blockClick = null, int listIndex = 0, BTABlockInfo.ListKinds listKind = BTABlockInfo.ListKinds.funcs)
        {
            PictureBox block = new PictureBox();
            BTABlockInfo blockInfo = new BTABlockInfo(infos, blockClick, listIndex, listKind);

            //set the block kind
            blockInfo.Infos[0].Kind = kind;

            drawTheBlock(block, kind, blockInfo);

            //save the block count in the block info list in his name
            block.Name = blockInfos.Count.ToString();

            blockInfos.Add(blockInfo);
            
        }

        private void drawTheBlock(PictureBox block, Block.BlockKinds kind, BTABlockInfo blockInfo)
        {
            int blockHeight = getBlockHeight(kind, blockInfo.Infos.Count);
            setPictureBox(block, new Point(20, height), BlocksImageCreator.drawBitmap(kind, blockInfo.Infos, block, blockHeight));
            
            //set the block funcs
            setBlockMouseFuncs(block, blockInfo);

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

            //make the block image to contain all the controls images but not th real controls
            Bitmap newBmp = new Bitmap(pb.Width, pb.Height);
            pb.DrawToBitmap(newBmp, new Rectangle(new Point(), pb.Size));
            newBmp.MakeTransparent(SystemColors.Control);
            pb.Image = newBmp;
            pb.Controls.Clear();

            btnsPanel.Controls.Add(pb);
        }

        private void setBlockMouseFuncs(PictureBox pb, BTABlockInfo blockInfo)
        {
            pb.MouseDown += Block_MouseDown;
            pb.MouseMove += BlockMessageBox_MouseMove;
            pb.MouseUp += BlockMessageBox_MouseUp;

            if (blockInfo.Click != null)
            {
                pb.MouseClick += blockInfo.Click;
            }
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
                addPBToForm(pb, e.Location);

                //create the new block that will stay in the place of the last button
                newBlock = new PictureBox();
                setPictureBox(newBlock, pb.Location, BlocksImageCreator.drawBitmap(blockInfos[int.Parse(pb.Name)].Infos[0].Kind, getCloneInfoList(int.Parse(pb.Name)), newBlock, getBlockHeight(blockInfos[int.Parse(pb.Name)].Infos[0].Kind, blockInfos[int.Parse(pb.Name)].Infos.Count)));
                newBlock.Name = pb.Name;
            }
        }

        private void addPBToForm(PictureBox pb, Point location)
        {
            /*pb.BackColor = SystemColors.ControlDark;
            pb.Parent = form;
            pb.BringToFront();*/
            pb.BackColor = Color.Transparent;
            AddBlockInfo.makeTheScreen(pb);

            lastPoint = location;
        }

        private List<BlockInfo> getCloneInfoList(int index)
        {
            List<BlockInfo> res = new List<BlockInfo>();
            for (int i = 0; i <= blockInfos[index].Infos.Count - 1; i++)
            {
                res.Add(blockInfos[index].Infos[i].Clone());
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
                AddBlockInfo.disposeTheScreen();

                PictureBox curBlock = sender as PictureBox;
                if (curBlock.Left > ProgramingPanelLocation.X)
                {
                    AddBlock(getCloneInfoList(int.Parse(curBlock.Name)), new Point(curBlock.Left - ProgramingPanelLocation.X, curBlock.Top - ProgramingPanelLocation.Y));
                }
                
                form.Controls.Remove(curBlock);

                //set the cur block
                curBlock = newBlock;

                //set the curBlock mouse funcs
                setBlockMouseFuncs(curBlock, blockInfos[int.Parse(curBlock.Name)]);
            }
        }
        
        #endregion

        #region get all possible blocks

        public List<List<string>> getAllPossibleBlocks()
        {
            List<List<string>> res = new List<List<string>>();

            //hide the add blocks panel
            AddBlocksPanel.Visible = false;

            //move on all the catagories and add all the blocks to the list
            for (int i = 0; i <= catagoryButtonsPanel.Controls.Count - 1; i++)
            {
                //change the catagory
                RadioButton rb = catagoryButtonsPanel.Controls[i] as RadioButton;
                rb.Select();

                //add all the blocks
                for (int j = 0; j <= blockInfos.Count - 1; j++)
                {
                    res.Add(new List<string>()); //add all the block infos in a list of string
                    foreach (BlockInfo bi in blockInfos[j].Infos)
                    {
                        res[res.Count-1].Add(bi.ToString());
                    }
                }
            }

            //show the add blocks panel
            AddBlocksPanel.Visible = true;

            return res;
        }

        #endregion
    }
}