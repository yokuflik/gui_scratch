using System.Collections.Generic;

namespace GuiScratch
{
    public class MyList
    {
        public MyList(string name)
        {
            Name = name;

            list = new List<string>();
        }

        public string Name;

        public List<string> list;

        #region to and from string

        public override string ToString()
        {
            //Name Count list
            string res = "\x0001" + Name + "\x0001" + list.Count.ToString();

            //add all the lists to the string
            for (int i = 0; i <= list.Count - 1; i++)
            {
                res += "\x0001" + list[i];
            }

            return res;
        }

        public MyList(string[] vars, ref int index)
        {
            Name = vars[index++];
            
            int count = int.Parse(vars[index++]);

            list = new List<string>();

            for (int i = 0; i <= count - 1; i++)
            {
                this.list.Add(vars[index++]);
            }
        }

        #endregion
    }
}