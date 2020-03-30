using System.Drawing;

namespace GuiScratch
{
    public class MyVar
    {
        public MyVar(string name)
        {
            Name = name;
        }

        public string Name;
        
        public string Text;

        #region to and from string

        public override string ToString()
        {
            //Name Text
            return "\x0001" + Name + "\x0001" + Text;
        }

        public MyVar(string[] vars, ref int index)
        {
            Name = vars[index++];
            Text = vars[index++];
        }

        #endregion
    }
}