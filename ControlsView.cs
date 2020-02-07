using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    internal class ControlsView
    {
        public ControlsView(Values myValues, FlowLayoutPanel controlsFLP,FlowLayoutPanel controlsKindsFLP, Panel propertiesPanel)
        {
            MyValues = myValues;
            ControlsFLP = controlsFLP;
            ControlsKindsFLP = controlsKindsFLP;

            setControlsKindsFlp();

            PropertiesPanel = propertiesPanel;

            myP = new myProperties(propertiesPanel, myValues, setControlsView);
        }

        #region controls kinds

        private void setControlsKindsFlp()
        {
            ControlsKindsFLP.Controls.Clear();

            addKindToControlsKindsFlp("Label", 0, true);

            addKindToControlsKindsFlp("Picture Box", 1);
            addKindToControlsKindsFlp("Button", 2);
        }

        private void addKindToControlsKindsFlp(string name, int kindIndex, bool Check = false)
        {
            RadioButton rb = new RadioButton();
            rb.Name = kindIndex.ToString();
            rb.Text = name;
            rb.AutoSize = true;
            rb.Checked = Check;

            ControlsKindsFLP.Controls.Add(rb);
        }

        public MyControl.ControlKinds getCurControlKind()
        {
            //check witch radio button is checked
            RadioButton rb;
            for (int i = 0; i <= ControlsKindsFLP.Controls.Count - 1; i++)
            {
                rb = ControlsKindsFLP.Controls[i] as RadioButton;
                if (rb.Checked)
                {
                    return (MyControl.ControlKinds)int.Parse(rb.Name);
                }
            }

            return MyControl.ControlKinds.Label;
        }

        #endregion

        #region controls view

        public void setControlsView()
        {
            ControlsFLP.Controls.Clear();

            //add the controls
            for (int i = 0; i <= MyValues.controls.Count-1; i++)
            {
                addControlToView(MyValues.controls[i], i);
            }
            
        }

        private void addControlToView(MyControl myControl, int index)
        {
            //create the panel
            Panel p = new Panel();
            p.Size = new Size(ControlsFLP.Width-8, 70);
            p.BorderStyle = BorderStyle.FixedSingle;

            //add the name property
            addTextLabels(p, "Name: ", MyValues.controls[index].Name, 5);

            //add the text property
            addTextLabels(p, "Kind: ", MyValues.controls[index].Kind.ToString(), 25);

            //add the visibile property
            addTextLabels(p, "Visibile: ", MyValues.controls[index].Visible.ToString(), 45);

            ControlsFLP.Controls.Add(p);

            addTheClickFuncPanelControls(p, index.ToString());
        }
        
        private void addTextLabels(Panel p, string name, string text, int height)
        {
            Label l = new Label();
            l.Text = name;
            l.Location = new Point(10, height);
            l.AutoSize = true;
            p.Controls.Add(l);

            Label nameL = new Label();
            nameL.Text = text;
            nameL.Location = new Point(l.Right + 20, height);
            nameL.AutoSize = true;
            p.Controls.Add(nameL);
        }

        private void addTheClickFuncPanelControls(Control p, string name)
        {
            p.Name = name;
            p.MouseDown += ControlsView_MouseDown;
            for (int i = 0; i <= p.Controls.Count - 1; i++)
            {
                p.Controls[i].Name = name;
                p.Controls[i].MouseDown += ControlsView_MouseDown;
            }
        }

        private void ControlsView_MouseDown(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            showValueProperties(int.Parse(c.Name));
        }

        int selectedIndex;
        private void showValueProperties(int index)
        {
            selectedIndex = index;

            myP.showValueProperties(index);
        }

        #endregion

        public Values MyValues;
        public FlowLayoutPanel ControlsFLP;
        
        public FlowLayoutPanel ControlsKindsFLP;

        public Panel PropertiesPanel;
        public TableLayoutPanel propertiesT;

        public myProperties myP;
        
    }
}