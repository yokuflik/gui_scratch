using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiScratch
{
    public class ContainBlock : Block
    {
        public ContainBlock(Control container, Point location, List<BlockInfo> blockInfos, Func<Block, bool, bool, bool> checkClientsAndParents, Action<Block> blockRightClick, decimal blockIndex, bool canBeParent = true)
        {
            Kind = BlockKinds.Contain;
            regularBlockSize = 32;
            parentBlockSize = 27;

            infos = blockInfos;
            setBlock(container, infos[0], checkClientsAndParents, blockRightClick, blockIndex);

            //create the bmps
            /*Bmp = BlocksImageCreator.drawBitmap(BlockKinds.Action, Info, topPB, 32,Info.CanBeParent, 15,27);
            bottomBmp = BlocksImageCreator.drawBitmap(BlockKinds.Action, Info, Bmp.Width,32, 27);*/
            startTheLists();

            updateBlock(false);

            setPbs(location);
        }

        public override void remove()
        {
            //remove the top pbs
            for (int  i=0;i<= topPBs.Count - 1; i++)
            {
                topPBs[i].Parent = null;
                centerPB[i].Parent = null;
            }

            bottomPB.Parent = null;
        }

        public override void AddMouseMoveAndDownFuncsToPB(MouseEventHandler mouseDownFunc, MouseEventHandler mouseMoveFunc)
        {
            topPBs[0].MouseDown += mouseDownFunc;
            topPBs[0].MouseMove += mouseMoveFunc;
        }

        public override void RemoveMouseMoveAndDownFuncsToPB(MouseEventHandler mouseDownFunc, MouseEventHandler mouseMoveFunc)
        {
            topPBs[0].MouseDown -= mouseDownFunc;
            topPBs[0].MouseMove -= mouseMoveFunc;
        }

        public override string getCode()
        {
            string res = "";
            for (int i = 0; i <= infos.Count - 1; i++)
            {
                res += "\n"+infos[i].BlockCode.getCode(infos[i], false)+"\n{";

                if (insideClients[i] != null)
                {
                    res += insideClients[i].getCode();
                }

                res += "\n}";
            }

            return res;
        }

        public override string ToString()
        {
            //return blockIndex x y isAContain infosCount infos insideClients(if has a client putts the index of it else putts -1) client
            string res = "\x0001" + BlockIndex.ToString() + "\x0001" + topPBs[0].Left + "\x0001" + topPBs[0].Top+"\x0001"+true.ToString();

            //add the infos
            res += "\x0001" + infos.Count;

            for (int i = 0; i <= infos.Count - 1; i++)
            {
                res += infos[i].ToString();
            }

            //add the clients
            for (int i = 0; i <= insideClients.Count - 1; i++)
            {
                if (insideClients[i] == null)
                {
                    res += "\x0001-1";
                }
                else
                {
                    res += "\x0001" + insideClients[i].BlockIndex.ToString();
                }
            }

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

        #region clone

        public override Block Clone(decimal newIndex)
        {
            return new ContainBlock(Container, topPBs[0].Location, getInfosListClone(), CheckClientsAndParents, BlockRightClick, newIndex, Info.CanBeParent);
        }

        private List<BlockInfo> getInfosListClone()
        {
            List<BlockInfo> res = new List<BlockInfo>();
            for (int i = 0; i <= infos.Count - 1; i++)
            {
                res.Add(infos[i].Clone());
            }
            return res;
        }

        #endregion

        #region start setup

        private void startTheLists()
        {
            topPBs = new List<PictureBox>();
            topBitmaps = new List<Bitmap>();
            centerPB = new List<PictureBox>();

            insideClients = new List<Block>();

            for (int i = 0; i <= infos.Count - 1; i++)
            {
                topPBs.Add(new PictureBox());
                topBitmaps.Add(null);
                centerPB.Add(new PictureBox());

                insideClients.Add(null);
            }

            bottomPB = new PictureBox();
        }
        
        public override void updateBlock(bool updateInfo)
        {
            if (updateInfo)
            {
                //update the infoes
                for (int i = 0; i <= infos.Count - 1; i++)
                {
                    infos[i].update();
                }
            }

            int width = BlocksImageCreator.getMaxWidthFromInfoList(infos);
            
            int topPinX = 15;

            //create the top bitmaps
            for (int i = 0; i <= topBitmaps.Count - 1; i++)
            {
                topPBs[i].Image= topBitmaps[i] = BlocksImageCreator.drawBitmap(BlockKinds.Action, infos[i], topPBs[i], width, true, topPinX, 27);
                topPinX = 27;

                topPBs[i].Width = topBitmaps[i].Width;
            }

            //create the bottom bmp
            bottomPB.Image = bottomBmp = BlocksImageCreator.drawBitmap(BlockKinds.Action, infos[0], width, regularBlockSize, 27);
            bottomPB.Width = bottomBmp.Width;

            clientChangedClientsToPass();
        }

        private void setTheTopPpsHeight()
        {
            for (int i = 0; i <= topPBs.Count - 1; i++)
            {
                if (insideClients[i] != null) //topPbs[i] is a parent
                {
                    topPBs[i].Height = infos[i].getHeight() - 5;
                }
                else
                {
                    topPBs[i].Height = infos[i].getHeight();
                }
            }

            if (isAClient())
            {
                setTopWhenClient(lastTopColor);
            }

            if (isAParent())
            {
                bottomPB.Height = 25;
            }
            else
            {
                bottomPB.Height = 32;
            }
        }

        private void setPbs(Point location)
        {
            //set the top pbs
            for (int i = 0; i <= topPBs.Count - 1; i++)
            {
                topPBs[i].Size = topBitmaps[i].Size;
                topPBs[i].Image = topBitmaps[i];
                topPBs[i].BackColor = Container.BackColor;
                topPBs[i].MouseDown += PB_MouseDown;
                topPBs[i].MouseMove += MouseMove;
                topPBs[i].MouseUp += MouseUp;

                Container.Controls.Add(topPBs[i]);
            }
            
            //set the center pbs
            for (int i = 0; i <= centerPB.Count - 1; i++)
            {
                centerPB[i] = new PictureBox();
                centerPB[i].Size = new Size(12, 20);
                centerPB[i].BackColor = infos[i].BackColor;
                centerPB[i].MouseDown += PB_MouseDown;
                centerPB[i].MouseMove += MouseMove;
                centerPB[i].MouseUp += MouseUp;

                Container.Controls.Add(centerPB[i]);
            }

            //pb is the bottom of the if statement
            bottomPB = new PictureBox();
            bottomPB.Size = bottomBmp.Size;
            bottomPB.Image = bottomBmp;
            bottomPB.BackColor = Container.BackColor;
            bottomPB.MouseDown += PB_MouseDown;
            bottomPB.MouseMove += MouseMove;
            bottomPB.MouseUp += MouseUp;

            Container.Controls.Add(bottomPB);

            setLocation(location.X, location.Y);
            
            bringToFront();

            //set the update funcs
            for (int i = 0; i <= infos.Count - 1; i++)
            {
                infos[i].setUpdateFunc(updateBlock);
            }
        }

        #endregion

        #region move block
        
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                setLocation(topPBs[0].Left + (e.X - lastPoint.X), topPBs[0].Top + (e.Y - lastPoint.Y));
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            CheckClientsAndParents(this, infos[0].CanBeParent, true);
        }

        public override void bringToFront()
        {
            for (int i = 0; i <= topPBs.Count - 1; i++)
            {
                topPBs[i].BringToFront();
                centerPB[i].BringToFront();
            }

            bottomPB.BringToFront();

            //bring the inside clients to front
            for (int i = 0; i <= insideClients.Count - 1; i++)
            {
                if (insideClients[i] != null)
                {
                    insideClients[i].bringToFront();
                }
            }
            base.bringToFront();
        }
        
        public override void setLocation(int x, int y)
        {
            topPBs[0].Location = new Point(x, y);
            centerPB[0].Location = new Point(topPBs[0].Left, topPBs[0].Bottom - 5);
            if (insideClients[0] != null)
            {
                insideClients[0].setLocation(centerPB[0].Right, topPBs[0].Bottom);
            }

            //set the pbs and blocks location
            for (int i = 1; i <= topPBs.Count - 1; i++)
            {
                topPBs[i].Location = new Point(centerPB[i - 1].Left, centerPB[i - 1].Bottom);
                centerPB[i].Location = new Point(topPBs[i].Left, topPBs[i].Bottom - 5);
                if (insideClients[i] != null)
                {
                    insideClients[i].setLocation(centerPB[i].Right, topPBs[i].Bottom);
                }
            }

            //set the bottom pb location
            bottomPB.Location = new Point(centerPB[centerPB.Count - 1].Left, centerPB[centerPB.Count - 1].Bottom);

            //set the clients location
            if (Client != null)
            {
                Client.setLocation(bottomPB.Left, bottomPB.Bottom);
            }
        }
        
        #endregion

        #region set clients
        
        public override void setTopWhenClient(Color blockColor)
        {
            lastTopColor = blockColor;
            topBitmaps[0] = setTopToBitmap(topBitmaps[0], blockColor);

            topPBs[0].Image = topBitmaps[0];
        }

        public override void setToParentSize()
        {
            bottomPB.Height = parentBlockSize;
        }

        public override void setToRegularSize()
        {
            bottomPB.Height = regularBlockSize;
        }

        #endregion

        #region check clients

        #region get block values

        public override int getHalfTop()
        {
            return topPBs[0].Top + topPBs[0].Height / 2;
        }

        public override int getHalfBottom()
        {
            return bottomPB.Top + bottomPB.Height / 2;
        }

        public override int getBottom()
        {
            return bottomPB.Bottom;
        }

        public override int getTop()
        {
            return topPBs[0].Top;
        }

        public override int getLeft()
        {
            return topPBs[0].Left;
        }

        public override Rectangle getBounds()
        {
            return new Rectangle(topPBs[0].Location, new Size(topPBs[0].Width, bottomPB.Bottom - topPBs[0].Top));
        }

        public override PictureBox getBottomPB()
        {
            return bottomPB;
        }

        #endregion

        public override bool CheckClient(Block block, bool checkContain = true)
        {
            if (base.CheckClient(block, checkContain))
            {
                return true;
            }

            if (!checkContain)
            {
                Rectangle bounds = block.getBounds();

                //check if the block is an inside client
                for (int i = 0; i <= topPBs.Count - 1; i++)
                {
                    if (topPBs[i].Bounds.IntersectsWith(bounds))
                    {
                        if (topPBs[i].Bottom < block.getHalfTop())
                        {
                            if (block.Info.CanBeClient && !block.isAClient())
                            {
                                //is a parent
                                AddInsideClient(block, i);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        #region inside client

        public void AddInsideClient(Block block, int index)
        {
            Block curInsideClient = insideClients[index];
            AddClient(block, ref curInsideClient, topPBs[index], index);
            curInsideClient.PassedInsideClientRemoved = insideClientRemovedToPass;

            insideClients[index] = curInsideClient;

            clientChangedClientsToPass();
        }
        
        private void insideClientRemovedToPass(int index)
        {
            insideClients[index].getBottomBlock().setToRegularSize();

            insideClients[index].PassedRemoveFromParent = null;
            insideClients[index] = null;
            topPBs[index].Height = infos[index].getHeight();
            setInsideClientTop(index, true);
        }
        
        public override void clientChangedClientsToPass()
        {
            setSize();

            base.clientChangedClientsToPass();
        }

        private void setSize()
        {
            setTheTopPpsHeight();

            setAllInsideClientsTop();

            for (int i = 0; i <= insideClients.Count - 1; i++)
            {
                int height;
                if (insideClients[i] == null)
                {
                    height = 20;
                }
                else
                {
                    height = 5;
                    Block curClient = insideClients[i];
                    while (curClient != null)
                    {
                        height += curClient.getBounds().Height;
                        curClient = curClient.Client;
                    }
                }

                centerPB[i].Height = height;
            }

            //set the location after the size setting
            setLocationToCurrentPlace();

            bringToFront();
        }
        
        private void setAllInsideClientsTop()
        {
            for (int i = 0; i <= insideClients.Count - 1; i++)
            {
                if (insideClients[i] != null)
                {
                    setInsideClientTop(i, false);

                    //set the inside client to parent size
                    insideClients[i].setToParentSize();
                    insideClients[i].getBottomBlock().setToParentSize();
                }
                else
                {
                    setInsideClientTop(i, true);
                }
            }
        }

        private void setInsideClientTop(int index, bool clear)
        {
            Color blockColor;
            if (clear)
            {
                blockColor = Color.Transparent;
            }
            else
            {
                blockColor = insideClients[index].getBottomBlock().Info.BackColor;
            }

            Bitmap bmp;
            if (index == topPBs.Count - 1)
            {
                bmp = bottomBmp;
            }
            else
            {
                bmp = topBitmaps[index + 1];
            }

            if (bmp.GetPixel(27,0) == blockColor)
            {
                //the color is right
                return;
            }
            
            setTopToBitmap(ref bmp, blockColor, 27);

            PictureBox bottomPB;
            if (index == topPBs.Count-1)
            {
                bottomPB = this.bottomPB;
            }
            else
            {
                bottomPB = topPBs[index + 1];
            }

            bottomPB.Image = bmp;
        }

        #endregion

        public override bool checkValueClient(ValueBlock valueBlock)
        {
            //check all the infos
            for (int i = 0; i <= infos.Count - 1; i++)
            {
                if (valueBlock.getBounds().IntersectsWith(topPBs[i].Bounds))
                {
                    if (infos[i].checkValueClient(valueBlock))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
        
        public List<BlockInfo> infos;

        public List<PictureBox> topPBs;
        public List<PictureBox> centerPB;
        public PictureBox bottomPB;
        
        List<Bitmap> topBitmaps;
        Bitmap bottomBmp;

        public List<Block> insideClients;

    }
}