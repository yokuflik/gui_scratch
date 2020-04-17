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
    public partial class AddEventFuncForm : Form
    {
        public AddEventFuncForm(Values myValues, AddBlockInfoToTheForm addBlock)
        {
            InitializeComponent();
            //Location = startLocation;

            MyValues = myValues;
            AddBlock = addBlock;
        }
        
        public Values MyValues;

        AddBlockInfoToTheForm AddBlock;
        
        private void AddEventFuncForm_Load(object sender, EventArgs e)
        {
            eventKindCB.SelectedIndex = 0;

            //set the controls list
            controlsCB.Items.AddRange(MyValues.getControls());
            controlsCB.SelectedIndex = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        BlockInfo info;

        private void addButton_MouseDown(object sender, MouseEventArgs e)
        {
            //add the right block
            Color color = Color.Orange;
            Event eventFunc = null;

            if (eventKindCB.SelectedIndex == 0) //mouse down
            {
                addMouseEvent(Event.Kinds.mouseDown, ref eventFunc, ref info, color);
            }

            else if (eventKindCB.SelectedIndex == 1) //mouse move
            {
                addMouseEvent(Event.Kinds.mouseMove, ref eventFunc, ref info, color);
            }

            else if (eventKindCB.SelectedIndex == 2) //mouse up
            {
                addMouseEvent(Event.Kinds.mouseUp, ref eventFunc, ref info, color);
            }

            //add the event to the list
            MyValues.events.Add(eventFunc);

            //add the block
            info.Kind = Block.BlockKinds.OnStart;
            info.FuncName = eventFunc.FuncName;
            
            AddBlock.add(info, addButton.PointToScreen(e.Location));

            Close();
        }

        private void addMouseEvent(Event.Kinds eventKind, ref Event eventFunc, ref BlockInfo info, Color color)
        {
            eventFunc = new Event(eventKind, MyValues.getEventName(eventKind, controlsCB.Text), controlsCB.Text);

            BlockCode code = new BlockCode(new List<CodePart>() { new CodePart(MyValues.getEventFuncName(eventFunc.Kind, eventFunc.FuncName)) });
            BlockCode xCode = new BlockCode(new List<CodePart>() { new CodePart("e.X") });
            BlockCode yCode = new BlockCode(new List<CodePart>() { new CodePart("e.Y") });

            if (eventKind == Event.Kinds.mouseDown)
            {
                info = new BlockInfo(MyValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When"),
                    new BlockInfoEvent(controlsCB.Text), new BlockInfoEvent("clicked"),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "X", xCode, true),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "Y", yCode, true) }, color, code);
            }
            else if (eventKind == Event.Kinds.mouseMove)
            {
                info = new BlockInfo(MyValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When the mouse moved on"),
                    new BlockInfoEvent(controlsCB.Text),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "X", xCode, true),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "Y", yCode, true) }, color, code);
            }
            else if (eventKind == Event.Kinds.mouseUp)
            {
                info = new BlockInfo(MyValues, new List<BlockInfoEvent>() { new BlockInfoEvent("When the mouse stopped clicking on"),
                    new BlockInfoEvent(controlsCB.Text),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "X", xCode, true),
                    new BlockInfoEvent(BlockInfoEvent.Kinds.numberInput, "Y", yCode, true) }, color, code);
            }
        }
    }
}
