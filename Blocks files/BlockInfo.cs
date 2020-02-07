using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    public class BlockInfo
    {
        public BlockInfo(Values myValues, List<BlockInfoEvent> blockInfoEvents, Color backColor, BlockCode blockCode, bool canBeParent = true, bool canBeClient = true, bool needForUpdate = false)
        {
            setInfo(myValues, blockInfoEvents, backColor, blockCode, canBeParent, canBeClient, needForUpdate);
        }

        public BlockInfo(Values myValues, List<BlockInfoEvent> blockInfoEvents, Block.BlockKinds kind, Color backColor, BlockCode blockCode, bool canBeParent = true, bool canBeClient = true, bool needForUpdate = false)
        {
            Kind = kind;
            setInfo(myValues, blockInfoEvents, backColor, blockCode, canBeParent, canBeClient, needForUpdate);
        }

        private void setInfo(Values myValues, List<BlockInfoEvent> blockInfoEvents, Color backColor, BlockCode blockCode, bool canBeParent = true, bool canBeClient = true, bool needForUpdate = false)
        {
            MyValues = myValues;

            BlockInfoEvents = blockInfoEvents;

            Kind = Block.BlockKinds.Action;

            drawHeight = 9;
            drawXStart = 5;

            BackColor = backColor;

            BlockCode = blockCode;

            CanBeParent = canBeParent;
            CanBeClient = canBeClient;

            CanDeleteAndDouplicate = true;

            NeedForUpdate = needForUpdate;
        }

        #region to and from string

        #region save

        public override string ToString()
        {
            //returns kind valueKind drawHeight drawXStart backColor(argb) canBeParent canBeClient canDeleteAndDouplicate 
            //needForUpdate funcName blockCode blockInfos.Count blockInfos
            string res = "\x0001" + ((int)Kind).ToString()+ "\x0001" + ((int)ValueKind).ToString() + "\x0001" + drawHeight.ToString() + "\x0001" + drawXStart.ToString() + "\x0001" +
                BackColor.ToArgb().ToString() + "\x0001" + CanBeParent.ToString() + "\x0001" + CanBeClient.ToString() + "\x0001" +
                CanDeleteAndDouplicate.ToString() + "\x0001" + NeedForUpdate.ToString()+ "\x0001" + FuncName;

            //res += "\" blockCode: \"";

            res += "\x0001";
            //add the block code
            if (BlockCode != null)
            {
                res += BlockCode.ToString();
            }

            //res += "\" blockInfos: \"";

            //add the block infos
            res += "\x0001" + BlockInfoEvents.Count.ToString() + getStringFromBlockInfos();

            //res += "\"";

            return res;
        }

        private string getStringFromBlockInfos()
        {
            string res = "";

            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                res += BlockInfoEvents[i].ToString();
            }

            return res;
        }

        #endregion
        
        public BlockInfo(Values values, string[] vars, ref int index)
        {
            //make the block info from the vars
            MyValues = values;

            Kind = (Block.BlockKinds) int.Parse(vars[index++]);
            ValueKind = (ValueBlock.ValueKinds)int.Parse(vars[index++]);

            drawHeight = int.Parse(vars[index++]);
            drawXStart = int.Parse(vars[index++]);

            BackColor = Color.FromArgb(int.Parse(vars[index++]));

            CanBeParent = stringToBool(vars[index++]);
            CanBeClient = stringToBool(vars[index++]);

            CanDeleteAndDouplicate = stringToBool(vars[index++]);

            NeedForUpdate = stringToBool(vars[index++]);

            FuncName = vars[index++];

            if (vars[index] == "")
            {
                index++;
            }
            else
            {
                BlockCode = new BlockCode(vars, ref index);
            }

            int blockInfosCount = int.Parse(vars[index++]);

            //create the block infos
            BlockInfoEvents = new List<BlockInfoEvent>();

            for (int i = 0; i <= blockInfosCount - 1; i++)
            {
                BlockInfoEvents.Add(new BlockInfoEvent(vars, ref index, values));
            }
        }

        public static bool stringToBool(string s)
        {
            if (s == "True")
            {
                return true;
            }
            return false;
        }


        #endregion

        public void drawImageToBmp(ref Bitmap bmp, ref Point loc, ref Graphics g)
        {
            Point myLoc = new Point(loc.X, loc.Y + drawHeight);
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                BlockInfoEvents[i].drawImageToBmp(ref bmp, ref myLoc, ref g);
            }
        }

        public BlockInfo Clone()
        {
            List<BlockInfoEvent> bie = new List<BlockInfoEvent>();
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                bie.Add(BlockInfoEvents[i].Clone());
            }

            BlockInfo res = new BlockInfo(MyValues, bie, BackColor, BlockCode);

            res.drawHeight = drawHeight;
            res.CanBeParent = CanBeParent;
            res.CanBeClient = CanBeClient;
            res.NeedForUpdate = NeedForUpdate;
            res.CanDeleteAndDouplicate = CanDeleteAndDouplicate;

            res.Kind = Kind;
            res.ValueKind = ValueKind;

            res.FuncName = FuncName;

            return res;
        }

        #region update

        internal void setUpdateFunc(Action<bool> updateBlock)
        {
            UpdateBlock = updateBlock;

            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                BlockInfoEvents[i].UpdateBlock = updateBlock;
            }
        }

        public void update()
        {
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                BlockInfoEvents[i].update(MyValues);
            }
        }

        public int getWidth()
        {
            int width = 8;
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                width += BlockInfoEvents[i].getWidth();
            }
            return width;
        }

        public int getHeight()
        {
            int height = 0;
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                height = Math.Max(height, BlockInfoEvents[i].getHeight());
            }

            if (Kind == Block.BlockKinds.Action||Kind == Block.BlockKinds.Contain)
            {
                height += 15;
            }
            else if (Kind == Block.BlockKinds.OnStart)
            {
                height += 33;
            }

            return height;
        }

        public void drawToBitmap(Graphics g, PictureBox PB)
        {
            this.PB = PB;

            int height = getHeight();

            Point drawPoint = new Point(drawXStart, height/2 - drawHeight);
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                BlockInfoEvents[i].drawToBitmap(g, PB, drawPoint, BackColor);
                drawPoint.X += BlockInfoEvents[i].getWidth();
            }
        }

        #endregion

        public void setAddVarFunc(Action<BlockInfoEvent, Point> addVar)
        {
            for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
            {
                BlockInfoEvents[i].AddVar = addVar;
            }
        }

        public bool checkValueClient(ValueBlock currentBlock)
        {
            if (PB != null)
            {
                for (int i = 0; i <= BlockInfoEvents.Count - 1; i++)
                {
                    if (BlockInfoEvents[i].CheckValueBlock(currentBlock))
                    {
                        currentBlock.StartLikeClient(BlockInfoEvents[i].UpdateBlock, BlockInfoEvents[i].clientRemoved);
                        return true;
                    }
                }
            }
            return false;
        }

        #region vars

        public string FuncName;

        public List<BlockInfoEvent> BlockInfoEvents;

        public BlockCode BlockCode;

        public Block.BlockKinds Kind;

        public ValueBlock.ValueKinds ValueKind;

        public int drawHeight;

        public int drawXStart;

        public Color BackColor;

        public bool CanBeParent;
        public bool CanBeClient;

        public bool CanDeleteAndDouplicate;

        public Action<bool> UpdateBlock;

        PictureBox PB;
        public Values MyValues;

        public bool NeedForUpdate;

        #endregion
    }
}