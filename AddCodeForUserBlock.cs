using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public partial class AddCodeForUserBlock : Form
    {
        public AddCodeForUserBlock(BlockInfo info, addBlockInfoToTheForm addBlock)
        {
            InitializeComponent();

            //save the values
            Info = info;
            AddBlock = addBlock;
        }

        BlockInfo Info;
        BlockCode Code;

        addBlockInfoToTheForm AddBlock;

        #region start

        private void AddCodeForUserBlock_Load(object sender, EventArgs e)
        {
            //set the startup
            addInfoIndexNUD.Maximum = Info.BlockInfoEvents.Count-1;

            Code = new BlockCode(new List<CodePart>() { });
            
            startCreate();
        }

        private void startCreate()
        {
            //show the blockInfo
            infoPB.Image = BlocksImageCreator.drawBitmap(Info.Kind, Info, infoPB);

            //set the add panels clicks
            setAddInfoButtonClick(addTextPanel, addText_Click);
            setAddInfoButtonClick(addInfoIndexPanel, addInfoIndex_Click);
        }

        #endregion

        #region remove info

        private void addRemovePartsButtons()
        {
            //remove all the last buttons
            removeCodeItemPanel.Controls.Clear();
            
            int x = 0;
            int curWidth = 0;
            //add a remove button on the top of every info
            for (int i = 0; i <= Code.Parts.Count - 1; i++)
            {
                curWidth = codeViewPanel.Controls[i].Width;

                PictureBox p = new PictureBox();
                p.Location = new Point(x + curWidth / 2 - 7, 10);
                p.Image = Properties.Resources.close_icon;
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Size = new Size(15, 15);
                p.Name = i.ToString();
                p.Click += removeInfoBtn_Click;
                removeCodeItemPanel.Controls.Add(p);

                x += curWidth+5;
            }
        }

        private void removeInfoBtn_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            //remove the relevant info
            Code.Parts.RemoveAt(int.Parse(c.Name));

            updateView();
        }

        #endregion

        #region add code parts

        private void setAddInfoButtonClick(Control c, EventHandler click)
        {
            c.Click += click;

            //add this func to all the panel controls that are not combo boxes
            for (int i = 0; i <= c.Controls.Count - 1; i++)
            {
                if (!(c.Controls[i] is NumericUpDown))
                {
                    c.Controls[i].Click += click;
                }
            }
        }

        private void addText_Click(object sender, EventArgs e)
        {
            Code.Parts.Add(new CodePart(""));

            //update the view
            updateView();
        }

        private void addInfoIndex_Click(object sender, EventArgs e)
        {
            //check what kind of info it is
            int index = (int)addInfoIndexNUD.Value;
            BlockInfoEvent b = Info.BlockInfoEvents[index];

            CodePart p = new CodePart(index);

            if (b.Kind == BlockInfoEvent.Kinds.comboBox)
            {
                if (b.ControlKind != BlockInfoEvent.ControlsKinds.none)
                {
                    p.IsAName = true; //is a control selection
                }
                else if (b.ContainVars)
                {
                    if (b.ContainLists)
                    {
                        p.IsAUserList = true; //is a list selection
                    }
                    else
                    {
                        p.IsAUserVar = true; //is a var selection
                    }
                }
            }

            //add the code part and update the view
            Code.Parts.Add(p);

            updateView();
        }

        #endregion

        #region code view

        private void updateView()
        {
            //clear all the last views
            codeViewPanel.Controls.Clear();

            //show all the code parts
            for (int i = 0; i <= Code.Parts.Count - 1; i++)
            {
                addCodePartToView(Code.Parts[i], i);
            }

            //add the remove buttons
            addRemovePartsButtons();
        }
        
        private void addCodePartToView(CodePart p, int index)
        {
            //check witch kind is the current code part and add a control to the view
            switch (p.kind)
            {
                case CodePart.Kinds.text:
                    //if the kind is text so add a text box that is changing its size when you write in it
                    RichTextBox tb = new RichTextBox();
                    tb.TextChanged += TBView_TextChanged;
                    tb.Name = index.ToString(); //saves the part index in the name of the control
                    tb.Font = new Font("Microsoft Sans Serif", 11f);
                    tb.Height = 25;
                    tb.Multiline = false;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    tb.Text = p.Text;
                    setTextBoxSize(tb);

                    codeViewPanel.Controls.Add(tb);
                    break;
                case CodePart.Kinds.info:
                    //if the kind is info so add NUD
                    NumericUpDown nud = new NumericUpDown();
                    nud.Maximum = Info.BlockInfoEvents.Count-1;
                    nud.Size = new Size(50, 25);
                    nud.Font = new Font("Microsoft Sans Serif", 10.5f);
                    nud.Value = p.Index;
                    nud.Name = index.ToString();
                    nud.ValueChanged += NudView_ValueChanged;

                    codeViewPanel.Controls.Add(nud);

                    break;
            }
        }

        #region text

        private void TBView_TextChanged(object sender, EventArgs e)
        {
            //set the text box size
            RichTextBox tb = sender as RichTextBox;
            setTextBoxSize(tb);

            //save the text
            Code.Parts[int.Parse(tb.Name)].Text = tb.Text;

            //update the remove buttons
            addRemovePartsButtons();
        }

        private void setTextBoxSize(RichTextBox tb)
        {
            Size size = TextRenderer.MeasureText(tb.Text, tb.Font);
            tb.Width = Math.Max(size.Width, 20);
        }

        #endregion

        #region nud

        private void NudView_ValueChanged(object sender, EventArgs e)
        {
            //set the text box size
            NumericUpDown nud = sender as NumericUpDown;

            //save the text
            Code.Parts[int.Parse(nud.Name)].Index = (int)nud.Value;
        }

        #endregion

        #endregion

        #region bottom buttons

        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            //add the block to the screen and then close the window
            Info.BlockCode = Code;
            AddBlock.add(Info, addButton.PointToScreen(e.Location));

            Close();
        }

        #region get code

        private void getCodeButton_Click(object sender, EventArgs e)
        {
            //putt the code string in the clip board and then close the window
            string res = "\n//add the "+ blockNameTB.Text+" block";

            //add the code
            res += "\nBlockCode " + blockNameTB.Text + "Code = new BlockCode(new List<CodePart>() { ";

            //add all the code parts
            for (int i = 0; i <= Code.Parts.Count - 1; i++)
            {
                res += getCodePartString(Code.Parts[i])+", ";
            }

            res.Remove(res.Length - 3); //remove the last comma

            res += "});";

            //add the info
            res += "\nBlockInfo " + blockNameTB.Text + "Info = makeBlockInfo(new List<BlockInfoEvent>() { ";

            //add all the block info events
            for (int i = 0; i <= Info.BlockInfoEvents.Count - 1; i++)
            {
                res += getBlockInfoEventString(Info.BlockInfoEvents[i]) + ", ";
            }

            res.Remove(res.Length - 3); //remove the last comma

            res += "}," + blockNameTB.Text + "Code" + ", color, " +checkIfNeedForUpdate(Info.BlockInfoEvents).ToString().ToLower() + ");";

            //check if is a value block 
            if (Info.Kind == Block.BlockKinds.Value)
            {
                res += "\n"+ blockNameTB.Text + "Info.ValueKind = ValueBlock.ValueKinds." + Info.ValueKind.ToString()+";";
            }

            res += "\naddBlock(Block.BlockKinds." + Info.Kind.ToString() + ", " + blockNameTB.Text + "Info);";

            //putt the res in the clip board
            Clipboard.SetText(res);

            /*
        //add the while block
        BlockCode whileCode = new BlockCode(new List<CodePart>() { new CodePart("while ("), new CodePart(1), new CodePart(")") });
        BlockInfo whileInfo = makeBlockInfo(new List<BlockInfoEvent>() { new BlockInfoEvent("While "), new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput) }, whileCode, color);
        addBlock(Block.BlockKinds.Contain, whileInfo);
        */
            Close();

        }

        private string getCodePartString(CodePart p)
        {
            string res = "new CodePart(";
            if (p.kind == CodePart.Kinds.text)
            {
                res += "\""+p.Text+"\"";
            }
            else
            {
                bool isAName = p.IsAName;
                bool isAUserVar = p.IsAUserVar;
                bool isAUserList = p.IsAUserList;

                string index = p.Index.ToString()+", " + isAName.ToString() + ", " + isAUserVar.ToString() + ", " + isAUserList.ToString();

                res += index.ToLower();
                
            }

            res += ")";

            return res;
        }

        private string getBlockInfoEventString(BlockInfoEvent b)
        {
            string res = "new BlockInfoEvent(";

            switch (b.Kind)
            {
                case BlockInfoEvent.Kinds.text:
                    res += "\"" + b.getText() + "\"";
                    break;
                case BlockInfoEvent.Kinds.booleanInput:
                    res += "BlockInfoEvent.Kinds.booleanInput";
                    break;
                case BlockInfoEvent.Kinds.colorInput:
                    res += b.getText();
                    break;
                case BlockInfoEvent.Kinds.comboBox:
                    if (b.ControlKind != BlockInfoEvent.ControlsKinds.none)
                    {
                        res += "myValues, BlockInfoEvent.ControlsKinds." + b.ControlKind.ToString();
                    }
                    else if (b.ContainVars) //check if is a contain vars or lists
                    {
                        res += "myValues";
                        if (b.ContainLists) //is a list
                        {
                            res += ", true";
                        }
                    }
                    break;
                case BlockInfoEvent.Kinds.numberInput:
                    res += "(decimal)" + b.getText();
                    break;
                case BlockInfoEvent.Kinds.textInput:
                    res += "BlockInfoEvent.Kinds.textInput, " + b.getText();
                    break;
            }

            //add the end
            res += ")";

            return res;
        }

        private bool checkIfNeedForUpdate(List<BlockInfoEvent> l)
        {
            //check in the list if any of the parts contains vars lists or controls and if it contains return true
            for (int i = 0; i <= l.Count - 1; i++)
            {
                if (l[i].ControlKind != BlockInfoEvent.ControlsKinds.none || l[i].ContainVars)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
