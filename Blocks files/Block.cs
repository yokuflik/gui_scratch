using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GuiScratch
{
    public class Block
    {
        #region start setup

        public void setBlock(Control container, BlockInfo blockInfo, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> blockRightClick, Action<Block, bool> blockStartMoving, decimal blockIndex)
        {
            Container = container;

            BlockIndex = blockIndex;

            Info = blockInfo;
            Info.Kind = Kind;
            Info.setUpdateFunc(updateBlock);

            PB = new PictureBox();

            CheckClientsAndParents = checkClientsAndParents;

            BlockRightClick = blockRightClick;

            BlockMoveFunc = blockStartMoving;
        }
        
        public void setPB(Point location)
        {
            PB.Location = location;
            PB.Size = Bmp.Size;
            PB.Image = Bmp;
            PB.BackColor = Container.BackColor;
            PB.MouseDown += PB_MouseDown;
            PB.MouseMove += PB_MouseMove;
            PB.MouseUp += PB_MouseUp;
            Container.Controls.Add(PB);

        }

        #endregion

        #region clone and remove

        public virtual Block Clone(decimal newIndex)
        {
            //every kind of block returns the clone of his kind
            return null;
        }

        public virtual void remove()
        {
            PB.Parent = null;
        }

        #endregion

        #region code

        public virtual string getCode()
        {
            string res = "\n"+Info.BlockCode.getCode(Info);

            if (Client != null)
            {
                res += Client.getCode();
            }
            return res;
        }

        #endregion

        #region move the block after duplicate

        public virtual void AddMouseMoveAndDownFuncsToPB(MouseEventHandler mouseDownFunc, MouseEventHandler mouseMoveFunc)
        {
            startMovingPB();

            movingPB.MouseDown += mouseDownFunc;
            movingPB.MouseMove += mouseMoveFunc;
        }

        public virtual void RemoveMouseMoveAndDownFuncsToPB(MouseEventHandler mouseDownFunc, MouseEventHandler mouseMoveFunc)
        {
            movingPB.MouseDown -= mouseDownFunc;
            movingPB.MouseMove -= mouseMoveFunc;
        }

        #endregion

        #region move block
        
        public PictureBox movingPB;

        #region start

        public Point lastPoint;
        public void PB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //check if is a client and remove it if it is
                if (Info.CanBeClient && PassedRemoveFromParent != null)
                {
                    removeClient();
                }

                lastPoint = e.Location;

                startMovingPB();
            }
            else if (e.Button == MouseButtons.Right)
            {
                //cms.Show(PB.PointToScreen(e.Location));
                Control c = sender as Control;
                cms.Show(c.PointToScreen(e.Location));

                BlockRightClick(this);
            }
        }

        public void startMovingPB()
        {
            //get the image
            Size bmpSize = new Size();
            getImageSize(ref bmpSize);
            Bitmap bmp = new Bitmap(bmpSize.Width, bmpSize.Height);
            Point loc = new Point();
            Graphics g = Graphics.FromImage(bmp);
            drawImageToBmp(ref bmp, ref loc, ref g);

            //when the blocks are drawing to the bmp it draws also the background color so I need to make it transparent
            bmp.MakeTransparent(Container.BackColor);

            //set the PB
            movingPB = new PictureBox();
            movingPB.Location = getBounds().Location;
            movingPB.Size = bmpSize;
            movingPB.Image = bmp;
            movingPB.BackColor = Color.Transparent;
            movingPB.MouseMove += PB_MouseMove;
            movingPB.MouseUp += PB_MouseUp;

            //draw all the other blocks to the screenPB
            if (BlockMoveFunc != null)
            {
                BlockMoveFunc(this, true);
            }
            
            //show the moving pb
            movingPB.Parent = Container;
            movingPB.BringToFront();
            movingPB.Select();
        }
        
        public virtual void setBlockVisible(bool visible)
        {
            PB.Visible = visible;
        }

        #region draw to bitmap

        public Rectangle setBlockRectangle(Point loc, Size size)
        {
            if (loc.X < 0)
            {
                size.Width += loc.X;
                if (size.Width < 0)
                {
                    size.Width = 0;
                }
                loc.X = 0;
            }
            if (loc.Y < 0)
            {
                size.Height += loc.Y;
                if (size.Height < 0)
                {
                    size.Height = 0;
                }
                loc.Y = 0;
            }

            return new Rectangle(loc, size);
        }

        public void drawPBToBmp(ref Bitmap bmp, PictureBox PB, Point loc)
        {
            Rectangle blockBounds = setBlockRectangle(loc, PB.Size);
            if (blockBounds.Height > 0 && blockBounds.Width > 0)
            {
                PB.DrawToBitmap(bmp, blockBounds);
            }
        }

        public virtual void drawImageToBmp(ref Bitmap bmp, ref Point loc, ref Graphics g, bool drawClients = true)
        {
            drawPBToBmp(ref bmp, PB, loc);

            //Info.drawImageToBmp(ref bmp, ref loc, ref g);

            loc.Y += PB.Height;
            if (isAParent() && drawClients)
            {
                Client.drawImageToBmp(ref bmp, ref loc, ref g);
            }
                /*g.DrawImage(Bmp.Clone(new Rectangle(new Point(0,0), bounds.Size), Bmp.PixelFormat), loc);

                //draw the info
                Info.drawImageToBmp(ref bmp, ref loc, ref g);*/

            /*g.DrawImage(Bmp, loc);

            //draw the info
            Info.drawImageToBmp(ref bmp, ref loc, ref g);*/
        }

        public virtual void getImageSize(ref Size bmpSize)
        {
            Rectangle b = getBounds(true);
            bmpSize.Width = Math.Max(b.Width, bmpSize.Width);
            bmpSize.Height += b.Height;

            if (isAParent())
            {
                Client.getImageSize(ref bmpSize);
            }
        }

        #endregion

        public virtual void bringToFront()
        {
            PB.BringToFront();
            if (Client != null)
            {
                Client.bringToFront();
            }
        }

        #endregion

        public void PB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && movingPB != null)
            {
                movingPB.Location = new Point(movingPB.Left + e.X - lastPoint.X, movingPB.Top + e.Y - lastPoint.Y);

                lastPoint = e.Location;
            }
        }

        public void setLocationToCurrentPlace()
        {
            Rectangle bounds = getBounds();
            setLocation(bounds.X, bounds.Y);
        }

        public void setLocationToCurrentPlace(int xOffset, int yOffset)
        {
            Rectangle bounds = getBounds();
            setLocation(bounds.X+xOffset, bounds.Y+yOffset);
        }

        public virtual void setLocation(int x, int y)
        {
            PB.Location = new Point(x, y);
            
            //set the clients location
            if (Client != null)
            {
                Client.setLocation(x, PB.Bottom);
            }
        }

        #endregion

        #region set clients and parents after move

        public void PB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && movingPB != null)
            {
                //set the blocks
                setLocation(movingPB.Left, movingPB.Top);

                movingPB.Dispose();

                //call the move block func
                if (BlockMoveFunc != null)
                {
                    BlockMoveFunc(this, false);
                }
                
                //check if is a client or a parent
                if (Client == null)
                {
                    if (CheckClientsAndParents(this, Info.CanBeParent, Info.CanBeClient))
                    {
                        return;
                    }
                }
                else
                {
                    if (CheckClientsAndParents(this, false, Info.CanBeClient))
                    {
                        return;
                    }
                    Block bottomB = getBottomBlock();
                    if (CheckClientsAndParents(bottomB, bottomB.Info.CanBeParent, false))
                    {
                        return;
                    }
                }
            }
        }
        
        public Block getBottomBlock()
        {
            Block curBlock = this;
            while (curBlock.Client != null)
            {
                curBlock = curBlock.Client;
            }
            return curBlock;
        }

        #endregion

        #region client
        
        #region add client

        public void AddClient(Block currentBlock)
        {
            AddClient(currentBlock, ref Client, getBottomPB(), -1);

            setLocationToCurrentPlace();
        }

        public void AddClient(Block currentBlock, ref Block Client, PictureBox bottomPB, int insideClientIndex)
        {
            //set the client
            setToParentSize();

            currentBlock.setTopWhenClient(Info.BackColor);

            currentBlock.PassedRemoveFromParent = removeFromParentToPass;
            currentBlock.PassedClientChangedClients = clientChangedClientsToPass;

            if (Client != null)
            {
                if (currentBlock.Client == null)
                {
                    //Client.PassedRemoveFromParent();
                    if (currentBlock.Info.CanBeParent)
                    {
                        currentBlock.AddClient(Client);
                    }
                    else
                    {
                        Client.setLocation(PB.Right + 30, Client.PB.Top);

                        Client.removeClient();
                    }
                }
                else
                {
                    Block bottom = currentBlock.getBottomBlock();
                    if (bottom.Info.CanBeParent)
                    {
                        bottom.AddClient(Client);
                    }
                    else
                    {
                        bottom.setLocation(PB.Right + 30, bottom.getTop());
                        bottom.removeClient();
                    }
                }
                Client = null;
            }

            Client = currentBlock;
            Client.InsideClientIndex = insideClientIndex;

            //set the contian block if is in a contain block
            if (PassedClientChangedClients != null)
            {
                PassedClientChangedClients();
            }
            
        }

        #endregion

        #region set view and size

        public Color lastTopColor;
        public virtual void setTopWhenClient(Color blockColor)
        {
            lastTopColor = blockColor;

            setTopToBitmap(ref Bmp, blockColor);

            PB.Image = Bmp;
        }

        public Bitmap setTopToBitmap(Bitmap bmp, Color color, int topPinX = 15)
        {
            setTopToBitmap(ref bmp, color, topPinX);
            return bmp;
        }

        public void setTopToBitmap(ref Bitmap bmp, Color color, int topPinX = 15)
        {
            int[] xList = new int[] { 18, 16, 14, 12, 10 };
            for (int y = 0; y < 5; y++)
            {
                for (int x = topPinX+y; x < topPinX+y+xList[y]; x++)
                {
                    bmp.SetPixel(x, y, color);
                }
            }
        }

        public virtual void setToParentSize()
        {
            PB.Height = Info.getHeight() - 5;
        }

        public virtual void setToRegularSize()
        {
            PB.Height = Info.getHeight();
        }

        #endregion

        public virtual void clientChangedClientsToPass()
        {
            if (PassedClientChangedClients != null)
            {
                PassedClientChangedClients();
            }
        }

        public virtual bool checkValueClient(ValueBlock valueBlock)
        {
            if (Info.checkValueClient(valueBlock))
            {
                return true;
            }
            return false;
        }

        #region remove client

        public virtual void removeFromParentToPass()
        {
            Client = null;

            setToRegularSize();
        }

        public void removeClient()
        {
            //set the bmp
            setTopWhenClient(Color.Transparent);

            if (!isAParent())
            {
                setToRegularSize();
            }
            
            if (PassedInsideClientRemoved != null)
            {
                PassedInsideClientRemoved(InsideClientIndex);
                PassedInsideClientRemoved = null;
            }
            else
            {
                PassedRemoveFromParent();
                PassedRemoveFromParent = null;
            }
            
            if (PassedClientChangedClients != null)
            {
                PassedClientChangedClients();
                PassedClientChangedClients = null;
            }
        }

        #endregion

        #endregion

        #region block values

        public virtual void updateBlock(bool updateInfo)
        {
            if (updateInfo)
            {
                Info.update();
            }

            PB.Image = Bmp = BlocksImageCreator.drawBitmap(Info.Kind, Info, PB);

            if (isAParent())
            {
                PB.Size = new Size(Bmp.Width, Bmp.Height - 5);
            }
            else
            {
                PB.Size = Bmp.Size;
            }

            if (isAClient())
            {
                setTopWhenClient(lastTopColor);
            }

            setLocationToCurrentPlace();

            clientChangedClientsToPass();
        }

        #endregion

        #region block get info funcs

        public bool isAParent()
        {
            if (Client == null)
            {
                return false;
            }
            return true;
        }

        public bool isAClient()
        {
            if (PassedRemoveFromParent == null)
            {
                return false;
            }
            return true;
        }
        
        public virtual int getHalfTop()
        {
            return PB.Top + PB.Height / 2;
        }

        public virtual int getHalfBottom()
        {
            return getHalfTop();
        }
        
        public virtual int getBottom()
        {
            return PB.Bottom;
        }

        public virtual int getTop()
        {
            return PB.Top;
        }

        public virtual int getLeft()
        {
            return PB.Left;
        }
        
        public virtual Rectangle getBounds(bool fullBounds = false)
        {
            return PB.Bounds;
        }

        public virtual PictureBox getBottomPB()
        {
            return PB;
        }

        public virtual bool CheckClient(Block block, bool checkContain = true)
        {
            if (getTop() > block.getHalfBottom())
            {
                if (Info.CanBeClient && block.Info.CanBeParent)
                {
                    //is a client
                    block.AddClient(this);
                    return true;
                }
            }
            else if (getBottom() < block.getHalfTop())
            {
                if (Info.CanBeParent && block.Info.CanBeClient && block.PassedInsideClientRemoved == null)
                {
                    //is a parent
                    AddClient(block);
                    return true;
                }
            }
            if (block.Kind == BlockKinds.Contain && checkContain)
            {
                if (block.CheckClient(this, false))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region to and from string

        public override string ToString()
        {
            //return blockIndex x y isAContain info client(if has a client putts the index of it else putts -1)
            string res = "\x0001" + BlockIndex.ToString() + "\x0001" + PB.Left + "\x0001" + PB.Top+"\x0001"+false.ToString();

            //add the info
            res += Info.ToString();

            //add the client
            if (Client == null)
            {
                res += "\x0001-1";
            }
            else
            {
                res += "\x0001" + Client.BlockIndex.ToString();
            }

            return res;
        }

        #endregion

        #region vars

        public enum BlockKinds { OnStart, Action, Contain, Value }
        
        public BlockKinds Kind;

        public Control Container;

        public ContextMenuStrip cms;

        public PictureBox PB;

        public Bitmap Bmp;

        public BlockInfo Info;

        public Block Client;
        
        public Func<Block, bool, bool, bool> CheckClientsAndParents;

        public decimal BlockIndex;

        public Action PassedRemoveFromParent;
        
        public Action PassedClientChangedClients;

        public int InsideClientIndex;

        public int regularBlockSize;
        public int parentBlockSize;

        public Action<int> PassedInsideClientRemoved { get; internal set; }
        
        public Action<Block> BlockRightClick;

        public Action<Block, bool> BlockMoveFunc;

        #endregion
    }
}