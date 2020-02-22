using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GuiScratch
{
    public class OnStartBlock :Block
    {
        public OnStartBlock(Control container, Point location,BlockInfo blockInfo, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> rightMouseClick, Action<Block, bool> blockStartMoving, decimal blockIndex)
        {
            Kind = BlockKinds.OnStart;
            regularBlockSize = 50;
            parentBlockSize = 45;
            
            setBlock(container, blockInfo,checkClientsAndParents, rightMouseClick,blockStartMoving, blockIndex);
            Info.CanBeClient = false;
            Info.drawHeight = 2;
            Info.Kind = Kind;

            //create the bmp
            //createTheBitmap();
            Bmp = BlocksImageCreator.drawBitmap(BlockKinds.OnStart, Info, PB);

            setPB(location);
        }

        public override string getCode()
        {
            string res = "\n"+Info.BlockCode.getCode(Info, false);

            res += "\n{";

            if (Client != null)
            {
                res += Client.getCode();
            }

            res += "\n}";

            return res;
        }
    }
}