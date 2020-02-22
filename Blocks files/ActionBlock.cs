using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    internal class ActionBlock : Block
    {
        public ActionBlock(Control container, Point location, BlockInfo blockInfo, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> blockRightClick,Action<Block, bool> blockStartMoving, decimal blockIndex, bool canBeParent = true)
        {
            Kind = BlockKinds.Action;
            regularBlockSize = 32;
            parentBlockSize = 27;
            
            //create the bmp
            setBlock(container, blockInfo, checkClientsAndParents, blockRightClick,blockStartMoving, blockIndex);
            Info.drawHeight = 9;

            //createTheBitmap();
            updateBlock(false);

            setPB(location);
        }

        public override Block Clone(decimal newIndex)
        {
            return new ActionBlock(Container, PB.Location, Info.Clone(), CheckClientsAndParents, BlockRightClick, BlockMoveFunc, newIndex, Info.CanBeParent);
        }
        
    }
}