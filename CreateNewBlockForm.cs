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
    public partial class CreateNewBlockForm : Form
    {
        public CreateNewBlockForm(Values myValues, addBlockInfoToTheForm addBlock)
        {
            InitializeComponent();

            //save all the variabels
            MyValues = myValues;

            AddBlock = addBlock;
        }
        
        Values MyValues;

        addBlockInfoToTheForm AddBlock;

        BlockInfo Info;
        List<BlockInfoEvent> infoEvents;

        #region start

        private void CreateNewBlockForm_Load(object sender, EventArgs e)
        {
            //set the things to thier start state
            inputKindCB.SelectedIndex = 0;
            selectionsCB.SelectedIndex = 0;

            //set the block kind cb
            blockKindCB.Items.AddRange(Enum.GetNames(typeof(Block.BlockKinds)));

            //set the value kind cb
            valueKindPanel.Visible = false;
            valueKindCB.Items.AddRange(Enum.GetNames(typeof(ValueBlock.ValueKinds)));

            //set the kinds cb
            blockKindCB.SelectedIndex = 1;
            valueKindCB.SelectedIndex = 0;

            blockKindCB.SelectedIndexChanged += blockKindCB_SelectedIndexChanged;
            valueKindCB.SelectedIndexChanged += valueKindCB_SelectedIndexChanged;

            //start to create the block info
            startCreate();
        }
        
        private void startCreate()
        {
            //set the info startup
            infoEvents = new List<BlockInfoEvent>() { new BlockInfoEvent("Write text here", true) };
            Info = new BlockInfo(MyValues, infoEvents, Color.Purple, null);
            Info.Kind = Block.BlockKinds.Action;
            updateTheBlock();

            //select the first block event
            infoEvents[0].CurrentC_Click(null, null);

            //set the add infos buttons
            setAddInfoButtonClick(addTextPanel, addText_Click);
            setAddInfoButtonClick(addInputPanel, addInput_Click);
            setAddInfoButtonClick(addCBOptionsPanel, addSelectionCB_Click);
        }

        #endregion

        #region update

        private void updateAfterEditInfo()
        {
            //set all the infos to be not selected
            clearSelectedInfos(infoEvents);

            updateTheBlock();
        }

        private void updateTheBlock(bool lastRemoved = false)
        {
            //save the new events
            Info.BlockInfoEvents = infoEvents;

            //set the view
            updateTheView(lastRemoved, infoPB, Info, updateTheBlock);

            //set the remove info buttons
            addRemoveInfoButtons();
        }

        public static void updateTheView(bool lastRemoved, PictureBox infoPB, BlockInfo Info, Action<bool> updateFunc)
        {
            //we need to save the selected index
            int selcetedIndex = getSelectedIndex(Info.BlockInfoEvents);

            if (lastRemoved)
            {
                //clear all the last controls
                infoPB.Controls.Clear();
            }

            //draw the new info
            infoPB.Image = BlocksImageCreator.drawBitmap(Info.Kind, Info, infoPB);
            Info.setUpdateFunc(updateFunc);

            if (lastRemoved)
            {
                //set the last selected
                setToLastSelect(selcetedIndex, Info.BlockInfoEvents);
            }
        }
        
        #region set selection

        public static void clearSelectedInfos(List<BlockInfoEvent> infoEvents)
        {
            //check in all the infos if they are selected
            for (int i = 0; i <= infoEvents.Count - 1; i++)
            {
                if (infoEvents[i].IsInUserCreate)
                {
                    infoEvents[i].setToNotSelected();
                }
            }
        }

        private static void setToLastSelect(int selcetedIndex, List<BlockInfoEvent> infoEvents)
        {
            if (selcetedIndex != -1)
            {
                infoEvents[selcetedIndex].CurrentC_Click(null, null);
            }
        }

        private static int getSelectedIndex(List<BlockInfoEvent> infoEvents)
        {
            for (int i = 0; i <= infoEvents.Count - 1; i++)
            {
                if (infoEvents[i].checkIfIsSelected())
                {
                    return i;
                }
            }

            //if any of the infos is selected retrun -1
            return -1;
        }

        #endregion

        #endregion

        #region add infos

        public static void setAddInfoButtonClick(Control c, EventHandler click)
        {
            c.Click += click;

            //add this func to all the panel controls that are not combo boxes
            for (int i = 0; i <= c.Controls.Count - 1; i++)
            {
                if (!(c.Controls[i] is ComboBox))
                {
                    c.Controls[i].Click += click;
                }
            }
        }

        private void addText_Click(object sender, EventArgs e)
        {
            //add text to the last text
            if (infoEvents[infoEvents.Count - 1].Kind != BlockInfoEvent.Kinds.text)
            {
                infoEvents.Add(new BlockInfoEvent("add text", true));
            }

            infoEvents[infoEvents.Count - 1].setToNotSelected();

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
                    infoEvents.Add(new BlockInfoEvent((decimal)0));
                    break;
                case 1://text
                    infoEvents.Add(new BlockInfoEvent(BlockInfoEvent.Kinds.textInput, ""));
                    break;
                case 2://boolean
                    infoEvents.Add(new BlockInfoEvent(BlockInfoEvent.Kinds.booleanInput));
                    break;
                case 3://color
                    infoEvents.Add(new BlockInfoEvent(Color.Black));
                    break;
            }

            //update the block view
            updateAfterEditInfo();
        }

        private void addSelectionCB_Click(object sender, EventArgs e)
        {
            //add a selection to the block info
            switch (selectionsCB.SelectedIndex)
            {
                case 0: //controls
                    infoEvents.Add(new BlockInfoEvent(BlockInfoEvent.ControlsKinds.all, MyValues));
                    break;
                case 1: //vars
                    infoEvents.Add(new BlockInfoEvent(MyValues));
                    break;
                case 2: //lists
                    infoEvents.Add(new BlockInfoEvent(MyValues, true));
                    break;
            }

            //update the block view
            updateAfterEditInfo();
        }

        #endregion

        #region remove info

        private void addRemoveInfoButtons()
        {
            addRemoveInfoButtons(removeInfoBtnsPanel, infoEvents, removeInfoBtn_Click);
        }

        public static void addRemoveInfoButtons(Panel removeInfoBtnsPanel, List<BlockInfoEvent> infoEvents, EventHandler click, int minimumInfoCount = 1, int infoStartIndex = 0)
        {
            //remove all the last buttons
            removeInfoBtnsPanel.Controls.Clear();

            if (infoEvents.Count == minimumInfoCount)
            {
                return; //because you cant remove the last view
            }

            int x = 0;
            int curWidth = 0;
            //add a remove button on the top of every info
            for (int i = 0; i <= infoEvents.Count - 1; i++)
            {
                curWidth = infoEvents[i].getWidth();

                if (i >= infoStartIndex)
                {
                    PictureBox p = new PictureBox();
                    p.Location = new Point(x + curWidth / 2 - 7, 10);
                    p.Image = Properties.Resources.close_icon;
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Size = new Size(15, 15);
                    p.Name = i.ToString();
                    p.Click += click;
                    removeInfoBtnsPanel.Controls.Add(p);
                }

                x += curWidth;
            }
        }
        
        private void removeInfoBtn_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            if (infoEvents.Count != 1)
            {
                //remove the relevant info
                infoEvents.RemoveAt(int.Parse(c.Name));
            }

            updateTheBlock(true);
        }

        #endregion

        #region bottom buttons
        
        private void addButton_MouseUp(object sender, MouseEventArgs e)
        {
            AddBlock.add(Info.Clone(), addButton.PointToScreen(e.Location));
            Close();
        }
        
        private void AddCodeButton_Click(object sender, EventArgs e)
        {
            AddCodeForUserBlock addCode = new AddCodeForUserBlock(Info.Clone(), AddBlock);

            addCode.Owner = AddBlock.form;
            addCode.Show();

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region kind

        private void blockKindCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new block kind and update the view
            Info.Kind = (Block.BlockKinds)blockKindCB.SelectedIndex;
            if (blockKindCB.SelectedIndex == 3) //if is a value block
            {
                valueKindPanel.Visible = true;
            }
            else
            {
                valueKindPanel.Visible = false;
            }

            updateAfterEditInfo();
        }

        private void valueKindCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new value kind and update the view
            Info.ValueKind = (ValueBlock.ValueKinds)valueKindCB.SelectedIndex;

            updateAfterEditInfo();
        }

        #endregion

    }
}
