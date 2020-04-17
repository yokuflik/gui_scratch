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
    public partial class InputForm : Form
    {
        public InputForm(string text, Action<string> returnFunc)
        {
            InitializeComponent();

            Text = text;
            ReturnFunc = returnFunc;
        }

        string Text;
        Action<string> ReturnFunc;

        private void InputForm_Load(object sender, EventArgs e)
        {
            rtb.Text = Text;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ReturnFunc(rtb.Text);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
