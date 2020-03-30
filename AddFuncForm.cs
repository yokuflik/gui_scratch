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
    public partial class AddFuncForm : Form
    {
        public AddFuncForm(Values myValues, addBlockInfoToTheForm addBlock, Action updateViewFunc)
        {
            InitializeComponent();

            //save the variables
            MyValues = myValues;

            AddBlock = addBlock;

            UpdateViewFunc = updateViewFunc;
        }

        Values MyValues;
        addBlockInfoToTheForm AddBlock;

        Action UpdateViewFunc;
        
        BlockInfo Info;
        List<BlockInfoEvent> infoEvents;

        #region start create

        private void AddFuncForm_Load(object sender, EventArgs e)
        {
            //set the combo box
            inputKindCB.SelectedIndex = 0;

            startCreate();
        }

        Color color = Color.Coral;
        private void startCreate()
        {
            //set the block info and show it
            infoEvents = new List<BlockInfoEvent>() { new BlockInfoEvent("Func:"), new BlockInfoEvent("Write text here", true) };
            Info = new BlockInfo(MyValues, infoEvents, color, null);
            Info.Kind = Block.BlockKinds.OnStart;

            updateTheBlock();

            //select the text
            infoEvents[1].CurrentC_Click(null, null);

            //set the add infos buttons
            CreateNewBlockForm.setAddInfoButtonClick(addTextPanel, addText_Click);
            CreateNewBlockForm.setAddInfoButtonClick(addInputPanel, addInput_Click);
        }

        #endregion

        #region update

        private void updateAfterEditInfo()
        {
            //set all the infos to be not selected
            CreateNewBlockForm.clearSelectedInfos(infoEvents);
            
            updateTheBlock();
        }

        private void updateTheBlock(bool lastRemoved = false)
        {
            //save the new events
            Info.BlockInfoEvents = infoEvents;

            //set the view
            CreateNewBlockForm.updateTheView(lastRemoved, infoPB, Info, updateTheBlock);

            //set the remove info buttons
            CreateNewBlockForm.addRemoveInfoButtons(removeInfoBtnsPanel, infoEvents, removeInfoBtn_Click, 2, 1);
        }

        private void removeInfoBtn_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            if (infoEvents.Count != 2)
            {
                //remove the relevant info
                infoEvents.RemoveAt(int.Parse(c.Name));
            }

            updateTheBlock(true);
        }

        #endregion

        #region add infos

        private void addText_Click(object sender, EventArgs e)
        {
            //add text to the last text
            if (infoEvents[infoEvents.Count - 1].Kind != BlockInfoEvent.Kinds.text)
            {
                infoEvents.Add(new BlockInfoEvent("add text", true));
            }

            //update the block view
            updateTheBlock();

            //you cant put text after a text so it will open the last text input
            infoEvents[infoEvents.Count - 1].CurrentC_Click(null, null);
        }

        private void addInput_Click(object sender, EventArgs e)
        {
            //add an input to the block info
            switch (inputKindCB.SelectedIndex)
            {
                case 0://number
                    infoEvents.Add(new BlockInfoEvent((decimal)0, true));
                    break;
                case 1://text
                    infoEvents.Add(new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, "",true));
                    break;
                case 2://boolean
                    infoEvents.Add(new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput, true));
                    break;
                case 3://color
                    infoEvents.Add(new BlockInfoEvent(Color.Black, true));
                    break;
            }

            //set the var start text
            infoEvents[infoEvents.Count - 1].textBox.Text = "var" + (infoEvents.Count - 2).ToString();

            //update the block view
            updateAfterEditInfo();

            infoEvents[infoEvents.Count - 1].textBox.Select();
        }

        #endregion

        #region bottom buttons

        #region add
        
        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            //set the info and code
            BlockInfo info = makeInfo();
            BlockCode code = makeCode(info);
            info.BlockCode = code;

            //add the func to the funcs list
            MyValues.addFunc(new MyFunc(info, funcName));

            //add the func to the screen and then close this window
            AddBlock.add(info, addButton.PointToScreen(e.Location));

            Close();
        }

        #region make code

        string funcName;
        private BlockCode makeCode(BlockInfo info)
        {
            funcName = MyValues.getFuncName();

            List<CodePart> parts = new List<CodePart>();
            parts.Add(new CodePart("private void "+ funcName + "("+getFuncVarsCode(info.BlockInfoEvents)+")"));
            
            return new BlockCode(parts);
        }

        string funcVarStart = "funcVar_";
        private string getFuncVarsCode(List<BlockInfoEvent> l)
        {
            string res = "";
            for (int i = 1; i <= l.Count - 1; i++)
            {
                if (l[i].IsDragAble && l[i].Kind != BlockInfoEvent.Kinds.text)
                {
                    res += getVarCodeFromKind(l[i].Kind) + " "+funcVarStart + l[i].getText();
                    if (i != l.Count - 1)
                    {
                        res += ", ";
                    }
                }
            }

            return res;
        }

        private string getVarCodeFromKind(BlockInfoEvent.Kinds kind)
        {
            switch (kind)
            {
                case BlockInfoEvent.Kinds.textInput:
                    return "string";
                case BlockInfoEvent.Kinds.numberInput:
                    return "decimal";
                case BlockInfoEvent.Kinds.booleanInput:
                    return "bool";
                case BlockInfoEvent.Kinds.colorInput:
                    return "Color";
            }
            return "";
        }

        #endregion

        #region make info

        private BlockInfo makeInfo()
        {
            List<BlockInfoEvent> l = new List<BlockInfoEvent>();

            //add all the block info events
            for (int i = 0; i <= infoEvents.Count - 1; i++)
            {
                if (infoEvents[i].Kind == BlockInfoEvent.Kinds.text)
                {
                    l.Add(infoEvents[i].Clone());
                }
                else
                {
                    l.Add(new BlockInfoEvent(infoEvents[i].Kind, infoEvents[i].getText(), makeCodeForInfoEvent(infoEvents[i]), true));
                }
            }

            BlockInfo res = new BlockInfo(MyValues, l, Color.Coral, null);
            res.Kind = Block.BlockKinds.OnStart;
            return res;
        }

        private BlockCode makeCodeForInfoEvent(BlockInfoEvent b)
        {
            CodePart p = new CodePart(funcVarStart + b.getText());
            return new BlockCode(new List<CodePart>() { p });
        }

        #endregion

        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        
    }
}
