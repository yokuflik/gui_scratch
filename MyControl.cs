using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    public class MyControl
    {
        public MyControl(ControlKinds kind, string name)
        {
            Kind = kind;

            Name = name;

            Text = name;
            Visible = true;
            location = new Point();
            size = new Size();
        }

        public override string ToString()
        {
            //returns kind name text visible x y width height
            string res = "\x0001" + ((int)Kind).ToString() + "\x0001" + Name + "\x0001" + Text + "\x0001" + Visible.ToString()
                + "\x0001" + location.X.ToString() + "\x0001" + location.Y.ToString() + "\x0001" + size.Width.ToString() + "\x0001" +
                size.Height.ToString();

            return res;
        }

        public MyControl(string[] vars, ref int index)
        {
            Kind = (ControlKinds)int.Parse(vars[index++]);

            Name = vars[index++];
            Text = vars[index++];
            Visible = BlockInfo.stringToBool(vars[index++]);

            location = new Point(int.Parse(vars[index++]), int.Parse(vars[index++]));
            size = new Size(int.Parse(vars[index++]), int.Parse(vars[index++]));
        }

        public enum ControlKinds { Label, PictureBox, Button, Form }
        public ControlKinds Kind;

        public string Name;

        public string Text;

        public bool Visible;

        public Point location;

        public Size size;
    }
}