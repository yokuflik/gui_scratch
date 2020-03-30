using System;
using System.Collections.Generic;

namespace GuiScratch
{
    public class BlockCode
    {
        public BlockCode(List<CodePart> parts, bool hasVar = false)
        {
            Parts = parts;

            HasVar = hasVar;
        }

        public string getCode(BlockInfo info, bool addEndLine = true)
        {
            if (HasVar)
            {
                //make the rand var name
                varName = info.MyValues.getVarNumberName();
            }

            string res = "";
            if (info.Kind != Block.BlockKinds.Value)
            {
                res += "\n";
            }
            for (int i = 0; i <= Parts.Count - 1; i++)
            {
                if (Parts[i].kind == CodePart.Kinds.text)
                {
                    if (Parts[i].IsAVar)
                    {
                        res += Values.getVarName(Parts[i].Text);
                    }
                    else if (Parts[i].IsAUserVar)
                    {
                        res += Values.getUserVarName(Parts[i].Text);
                    }
                    else
                    {
                        res += Parts[i].Text;
                    }
                }
                else
                {
                    if (Parts[i].IsAName)
                    {
                        res += Values.getControlName(info.BlockInfoEvents[Parts[i].Index].getText());
                    }
                    else if (Parts[i].RandVar)
                    {
                        res += varName;
                    }
                    else if (Parts[i].IsAUserVar)
                    {
                        res += Values.getUserVarName(info.BlockInfoEvents[Parts[i].Index].getText());
                    }
                    else if (Parts[i].IsAUserList)
                    {
                        res += Values.getUserListName(info.BlockInfoEvents[Parts[i].Index].getText());
                    }
                    else
                    {
                        res += info.BlockInfoEvents[Parts[i].Index].getText();
                    }
                }
            }
            if (info.Kind != Block.BlockKinds.Value && addEndLine)
            {
                res += ";";
            }
            return res;
        }

        #region to and from string

        public override string ToString()
        {
            //returns hasVar parts.Count parts
            string res = HasVar.ToString() + "\x0001" + Parts.Count +getStringFromParts();

            return res;
        }

        private string getStringFromParts()
        {
            string res = "";

            for (int i = 0; i <= Parts.Count - 1; i++)
            {
                res += Parts[i].ToString();
            }

            return res;
        }

        public BlockCode(string[] vars, ref int index)
        {
            HasVar = BlockInfo.stringToBool(vars[index++]);

            int partsCount = int.Parse(vars[index++]);

            //create the parts list
            Parts = new List<CodePart>();

            for (int i = 0; i <= partsCount - 1; i++)
            {
                Parts.Add(new CodePart(vars, ref index));
            }
        }

        #endregion

        public List<CodePart> Parts;

        public bool HasVar;

        public string varName;
    }

    public class CodePart
    {
        public CodePart(bool randVar)
        {
            RandVar = randVar;

            kind = Kinds.info;
        }

        public CodePart(int index, bool isAName = false, bool isAUserVar = false, bool isAUserList = false)
        {
            Index = index;
            kind = Kinds.info;

            IsAName = isAName;
            IsAUserVar = isAUserVar;
            IsAUserList = isAUserList;
        }

        public CodePart(string text, bool isAVar = false, bool isAUserVar = false, bool isAUserList = false)
        {
            Text = text;
            kind = Kinds.info;

            IsAVar = isAVar;
            IsAUserVar = isAUserVar;
            IsAUserList = isAUserList;
        }
        public CodePart(string text)
        {
            Text = text;
            kind = Kinds.text;
        }

        #region to and from string

        public override string ToString()
        {
            //returns kind randVar index isAName Text isAVar isAUserVar
            string res = "\x0001" + ((int)kind).ToString()+ "\x0001"+RandVar.ToString()+"\x0001"+Index.ToString() + "\x0001"+
                IsAName.ToString() + "\x0001"+Text+"\x0001"+IsAVar.ToString()+ "\x0001" + IsAUserVar.ToString();

            return res;
        }

        public CodePart(string[] vars, ref int index)
        {
            kind = (Kinds)int.Parse(vars[index++]);

            RandVar = BlockInfo.stringToBool(vars[index++]);

            Index = int.Parse(vars[index++]);
            IsAName = BlockInfo.stringToBool(vars[index++]);

            Text = vars[index++];
            IsAVar = BlockInfo.stringToBool(vars[index++]);

            IsAUserVar = BlockInfo.stringToBool(vars[index++]);
        }

        #endregion

        public bool RandVar;

        public int Index;
        public bool IsAName;
        
        public string Text;
        public bool IsAVar;
        public bool IsAUserVar;
        public bool IsAUserList;

        public enum Kinds { text, info}
        public Kinds kind;
    }
}