using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    public class myProperties
    {
        public myProperties(Panel propertiesPanel, Values myValues, Action updateTheControlsView)
        {
            PropertiesPanel = propertiesPanel;

            MyValues = myValues;

            UpdateTheControlsView = updateTheControlsView;
        }

        int selectedIndex;
        public void showValueProperties(int index)
        {
            selectedIndex = index;

            //set the properties
            createThePropertiesT();

            //add the name property
            addTextBoxPropertyToProperties(ref nameTB, "Name", MyValues.controls[selectedIndex].Name, NameTB_LostFocus);
            
            //add the text property
            addTextBoxPropertyToProperties(ref textTB, "Text", MyValues.controls[selectedIndex].Text, TextTB_LostFocus);

            //add the visibile property
            addComboBoxTrueFalsePropertyToProperties(ref visibileCB, "Visible", new string[] { "True", "False" }, VisibileCB_ValueChanged, MyValues.controls[selectedIndex].Visible);

            //add the location text boxes
            addNumberTextBoxPropertyToProperties(ref xTB, "X", MyValues.controls[selectedIndex].location.X, xTB_LostFocus);
            addNumberTextBoxPropertyToProperties(ref yTB, "Y", MyValues.controls[selectedIndex].location.Y, yTB_LostFocus);

            //add the size text boxes
            addNumberTextBoxPropertyToProperties(ref widthTB, "Width", MyValues.controls[selectedIndex].size.Width, widthTB_LostFocus);
            addNumberTextBoxPropertyToProperties(ref heightTB, "Height", MyValues.controls[selectedIndex].size.Height, heightTB_LostFocus);
        }

        #region all controls properties

        #region create properties

        private void addPropertyNameToProperties(string name)
        {
            addRowToProperties();

            Label l = new Label();
            l.Text = name;
            l.AutoSize = true;
            propertiesT.Controls.Add(l);
        }

        #region add text box

        private void addTextBoxPropertyToProperties(ref TextBox tb, string name, string text, EventHandler lostFocus)
        {
            addPropertyNameToProperties(name);
            
            tb = new TextBox();
            tb.Text = text;
            tb.Size = new Size(100, 20);
            tb.BorderStyle = BorderStyle.None;
            propertiesT.Controls.Add(tb);

            tb.LostFocus += lostFocus;
            tb.KeyDown += Tb_KeyDown;
        }
        
        private void Tb_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
            {
                Control c = sender as Control;
                c.Parent.Focus();
            }
        }
        
        private void addNumberTextBoxPropertyToProperties(ref TextBox tb, string name, int num, EventHandler lostFocus)
        {
            addTextBoxPropertyToProperties(ref tb, name, num.ToString(), lostFocus);

            tb.KeyPress += Tb_KeyPress;
        }

        private void Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            //makes that the user can write only numbers in the text box
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region combo box

        private void addComboBoxPropertyToProperties(ref ComboBox cb, string name, string[] options, EventHandler valueChanged, int selectedIndex = 0)
        {
            addPropertyNameToProperties(name);
            
            cb = new ComboBox();
            cb.Items.AddRange(options);
            cb.SelectedIndex = selectedIndex;
            cb.Font = new Font("Microsoft Sans Serif", 8F);
            propertiesT.Controls.Add(cb);

            cb.SelectedValueChanged += valueChanged;
        }

        private void addComboBoxTrueFalsePropertyToProperties(ref ComboBox visibileCB, string name, string[] options, EventHandler valueChanged, bool b)
        {
            if (b == true)
            {
                addComboBoxPropertyToProperties(ref visibileCB, name, options, valueChanged, 0);
            }
            else
            {
                addComboBoxPropertyToProperties(ref visibileCB, name, options, valueChanged, 1);
            }
        }

        #endregion

        #endregion

        #region properties

        #region name

        TextBox nameTB;

        private void NameTB_LostFocus(object sender, System.EventArgs e)
        {
            if (MyValues.checkIfNameIsInUse(nameTB.Text, selectedIndex)) //if the name is in use
            {
                MessageBox.Show("This name is already in use. Choose a new name");
                nameTB.Select();
            }
            else
            {
                //set the name to the current control
                MyValues.controls[selectedIndex].Name = nameTB.Text;

                UpdateTheControlsView();

                //update the blocks
                MyValues.updateBlocksFunc();
            }
        }

        #endregion

        #region text
        
        TextBox textTB;

        private void TextTB_LostFocus(object sender, System.EventArgs e)
        {
            //set the text to the current control
            MyValues.controls[selectedIndex].Text = textTB.Text;

            UpdateTheControlsView();
        }
        
        #endregion

        #region visible

        ComboBox visibileCB;

        private void VisibileCB_ValueChanged(object sender, System.EventArgs e)
        {
            //set the text to the current control
            if (visibileCB.SelectedIndex == 0)
            {
                MyValues.controls[selectedIndex].Visible = true;
            }
            else
            {
                MyValues.controls[selectedIndex].Visible = false;
            }

            UpdateTheControlsView();
        }


        #endregion

        #region location

        TextBox xTB;
        TextBox yTB;

        private void xTB_LostFocus(object sender, System.EventArgs e)
        {
            //set the text to the current control
            MyValues.controls[selectedIndex].location.X = (int)decimal.Parse(xTB.Text);

            UpdateTheControlsView();
        }

        private void yTB_LostFocus(object sender, System.EventArgs e)
        {
            //set the text to the current control
            MyValues.controls[selectedIndex].location.Y = (int)decimal.Parse(yTB.Text);

            UpdateTheControlsView();
        }

        #endregion

        #region size

        TextBox widthTB;
        TextBox heightTB;

        private void widthTB_LostFocus(object sender, System.EventArgs e)
        {
            //set the text to the current control
            MyValues.controls[selectedIndex].size.Width = (int)decimal.Parse(widthTB.Text);

            //UpdateTheControlsView();
        }

        private void heightTB_LostFocus(object sender, System.EventArgs e)
        {
            //set the text to the current control
            MyValues.controls[selectedIndex].size.Height = (int)decimal.Parse(heightTB.Text);

            //UpdateTheControlsView();
        }

        #endregion

        #endregion

        #endregion

        private void addRowToProperties()
        {
            propertiesT.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            propertiesT.Height = propertiesT.RowStyles.Count * 20;
        }

        private void createThePropertiesT()
        {
            if (propertiesT != null)
            {
                propertiesT.Dispose();
            }

            propertiesT = new TableLayoutPanel();
            propertiesT.AutoSize = true;
            propertiesT.Location = new Point(13, 40);
            propertiesT.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            propertiesT.ColumnCount = 2;
            propertiesT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            propertiesT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            PropertiesPanel.Controls.Add(propertiesT);
        }

        public Values MyValues;

        public Action UpdateTheControlsView;

        public Panel PropertiesPanel;

        public TableLayoutPanel propertiesT;
    }
}