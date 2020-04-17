using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    class BlockFromString
    {
        public int BlockIndex;
        Point location;
        public BlockInfo Info;
        public int clientIndex;

        public bool isAContain;

        public List<BlockInfo> infos;
        public List<int> insideClientsIndexs;

        public BlockFromString(Values values, string[] vars, ref int index)
        {
            //make a block from the block vars
            BlockIndex = int.Parse(vars[index++]);

            location = new Point(int.Parse(vars[index++]), int.Parse(vars[index++]));

            isAContain = BlockInfo.stringToBool(vars[index++]);

            if (isAContain)
            {
                //get the infos
                infos = new List<BlockInfo>();

                int infosCount= int.Parse(vars[index++]);
                for (int i = 0; i <= infosCount - 1; i++)
                {
                    infos.Add(new BlockInfo(values, vars, ref index));
                }

                Info = infos[0];

                //get the inside clients indexes
                insideClientsIndexs = new List<int>();
                for (int i = 0; i <= infosCount - 1; i++)
                {
                    insideClientsIndexs.Add(int.Parse(vars[index++]));
                }
            }
            else
            {
                Info = new BlockInfo(values, vars, ref index);
            }

            clientIndex = int.Parse(vars[index++]);
        }

        public Block ToBlock(Control container, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> rightClick, Action<Block, bool> startMoving)
        {
            return ToBlock(container, checkClientsAndParents, rightClick, startMoving, BlockIndex);
        }

        public Block ToBlock(Control container, Func<Block, bool,bool,bool> checkClientsAndParents, Action<Block> rightClick, Action<Block, bool> startMoving, int BlockIndex)
        {
            switch (Info.Kind)
            {
                case Block.BlockKinds.Action:
                    return new ActionBlock(container, location, Info, checkClientsAndParents, rightClick, startMoving, BlockIndex, Info.CanBeParent);
                case Block.BlockKinds.OnStart:
                    return new OnStartBlock(container, location, Info, checkClientsAndParents, rightClick, startMoving, BlockIndex);
                case Block.BlockKinds.Value:
                    return new ValueBlock(container, Info.ValueKind, location, Info, checkClientsAndParents, rightClick,startMoving, BlockIndex);
                case Block.BlockKinds.Contain:
                    return new ContainBlock(container, location, infos, checkClientsAndParents, rightClick, startMoving, BlockIndex, Info.CanBeParent);
            }

            return null;
        }
    }
}
