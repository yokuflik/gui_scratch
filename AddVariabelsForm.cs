using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public partial class AddVariabelsAndListsForm : Form
    {
        #region when the window starts

        public AddVariabelsAndListsForm(Values myValues, Action<bool> updateVars, bool var)
        {
            InitializeComponent();

            MyValues = myValues;

            UpdateView = updateVars;

            Var = var;

            if (!Var)
            {
                varOrListLabel.Text = "List name: ";
                nameTB.Left = varOrListLabel.Right + 20;
            }
        }

        Values MyValues;

        Action<bool> UpdateView;

        bool Var;
        
        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string reason = "";
            if (checkIfTheNameIsGood(ref reason))
            {
                //add the var to the list of the vars
                if (Var)
                {
                    MyVar var = new MyVar(nameTB.Text);
                    MyValues.addVar(var);

                    //update the vars view
                    UpdateView(true);
                }
                else
                {
                    MyList list = new MyList(nameTB.Text);
                    MyValues.addList(list);
                }

                Close();
            }
            else
            {
                MessageBox.Show(reason);
            }
        }

        private bool checkIfTheNameIsGood(ref string reason)
        {
            if (!Values.checkIfTheNameIsGoodForVarOrList(ref reason, nameTB.Text))
            {
                return false;
            }
            
            string[] names;

            if (Var)
            {
                names = MyValues.getVars();
            }
            else
            {
                names = MyValues.getLists();
            }

            //check if the user dousnt use the same name of another variabel
            for (int i = 0; i <= names.Length - 1; i++)
            {
                if (names[i] == nameTB.Text)
                {
                    reason = "The name you choosed is already in use. Choose another name.";
                    return false;
                }
            }
            
            return true;
        }
        
        private void nameTB_TextChanged(object sender, EventArgs e)
        {
            string reason = "";
            if (checkIfTheNameIsGood(ref reason))
            {
                reasonLabel.Text = "The name is OK";
                reasonLabel.BackColor = Color.Green;
            }
            else
            {
                reasonLabel.Text = reason;
                reasonLabel.BackColor = Color.Red;
            }
        }
    }
}
