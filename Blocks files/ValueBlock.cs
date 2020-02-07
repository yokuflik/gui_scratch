using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GuiScratch
{
    public class ValueBlock : Block
    {
        public ValueBlock(Control container, ValueKinds valueKind, Point location, BlockInfo blockInfo, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> blockRightClick, Action<Block, bool> blockStartMoving, decimal blockIndex)
        {
            Kind = BlockKinds.Value;

            BlockMoveFunc = blockStartMoving;

            //create the bmp
            setBlock(container, blockInfo, checkClientsAndParents, blockRightClick, blockIndex);
            PB.BackColor = Color.Transparent;
            Info.drawHeight = 7;
            Info.ValueKind = valueKind;
            
            Bmp = BlocksImageCreator.drawBitmap(BlockKinds.Value, Info, PB);

            setPB(location);
        }

        public override Block Clone(decimal newIndex)
        {
            return new ValueBlock(Container, Info.ValueKind, PB.Location, Info.Clone(), CheckClientsAndParents, BlockRightClick,BlockMoveFunc, newIndex);
        }

        public enum ValueKinds { text, number, boolean, color}

        public void StartLikeClient(Action<bool> updateBlock, Action clientRemoved)
        {
            PassedUpdateFunc = updateBlock;
            PassedClientRemoved = clientRemoved;
            PB.MouseDown -= PB_MouseDown;
            PB.MouseDown += ValuePB_MouseDown;

            isAValueClient = true;
        }

        private void ValuePB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //remove from the block
                removeFromBlock();
                PB_MouseDown(sender, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                Control c = sender as Control;
                cms.Show(c.PointToScreen(e.Location));

                BlockRightClick(this);
            }
        }

        public void removeFromBlock(bool moveToSide = false)
        {
            isAValueClient = false;

            Point pbLocInScreen = PB.PointToScreen(new Point());
            Point coLocInScreen = Container.PointToScreen(new Point());
            PB.Parent = null;
            PB.Location = new Point(pbLocInScreen.X - coLocInScreen.X, pbLocInScreen.Y - coLocInScreen.Y);
            PB.Parent = Container;

            PassedClientRemoved();
            PassedUpdateFunc = null;
            PB.BackColor = Container.BackColor;
            PB.MouseDown -= ValuePB_MouseDown;
            PB.MouseDown += PB_MouseDown;

            if (moveToSide)
            {
                setLocationToCurrentPlace(100, 30);
            }

        }

        public override void updateBlock(bool updateInfo)
        {
            base.updateBlock(updateInfo);
            if (PassedUpdateFunc != null)
            {
                PassedUpdateFunc(updateInfo);
            }
        }

        public Action<bool> PassedUpdateFunc;
        public Action PassedClientRemoved;

        public bool isAValueClient;
    }
}