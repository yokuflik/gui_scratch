using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GuiScratch
{
    public static class BlocksImageCreator
    {
        internal static Bitmap drawBitmap(Block.BlockKinds kind, BlockInfo info, PictureBox PB, bool canBeParent = true, int topPinX = 15, int bottomPinX = 15)
        {
            return drawBitmapKind(kind, info, PB, true, info.getWidth(),info.getHeight(), canBeParent, topPinX, bottomPinX);
        }

        internal static Bitmap drawBitmap(Block.BlockKinds kind, List<BlockInfo> infos, PictureBox PB, int height, bool canBeParent = true, int topPinX = 15, int bottomPinX = 15)
        {
            return drawBitmapKind(kind, infos, PB, true, getMaxWidthFromInfoList(infos), height, canBeParent, topPinX, bottomPinX);
        }

        public static int getMaxWidthFromInfoList(List<BlockInfo> infos)
        {
            int max = 0;
            for (int i = 0; i <= infos.Count - 1; i++)
            {
                max = Math.Max(max, infos[i].getWidth());
            }
            return max;
        }

        public static Bitmap drawBitmap(Block.BlockKinds kind, BlockInfo info, PictureBox PB, int width, bool canBeParent = true, int topPinX = 15, int bottomPinX = 15)
        {
            return drawBitmapKind(kind, info, PB, true, width, info.getHeight(), canBeParent, topPinX, bottomPinX);
        }

        public static Bitmap drawBitmap(Block.BlockKinds kind, BlockInfo info, int width, int height, int topPinX = 15, int bottomPinX = 15)
        {
            return drawBitmapKind(kind, info, null, false, width,height, info.CanBeParent, topPinX, bottomPinX);
        }

        private static Bitmap drawBitmapKind(Block.BlockKinds kind, BlockInfo info, PictureBox PB, bool containsInfoDraw, int width, int height, bool canBeParent, int topPinX, int bottomPinX)
        {
            return drawBitmapKind(kind, new List<BlockInfo>() { info }, PB, containsInfoDraw, width, height, canBeParent, topPinX, bottomPinX);
        }

        private static Bitmap drawBitmapKind(Block.BlockKinds kind, List<BlockInfo> infos, PictureBox PB, bool containsInfoDraw, int width, int height, bool canBeParent, int topPinX, int bottomPinX)
        {
            if (kind == Block.BlockKinds.Action)
            {
                infos[0].drawHeight = 9;

                return drawActionBlock(infos[0], PB, containsInfoDraw, width,height, canBeParent, topPinX, bottomPinX);
            }
            else if (kind == Block.BlockKinds.Value)
            {
                infos[0].drawHeight = 7;
                return drawValueBlock(infos[0], PB, containsInfoDraw, width,height, canBeParent, topPinX, bottomPinX);
            }
            else if(kind == Block.BlockKinds.Contain)
            {
                return drawContainBlock(infos, PB, containsInfoDraw, width, height, canBeParent, topPinX, bottomPinX);
            }
            else if (kind == Block.BlockKinds.OnStart)
            {
                infos[0].drawHeight = 1;
                return drawOnStartBlock(infos[0], PB, containsInfoDraw, width, height, bottomPinX);
            }
            return null;
        }

        private static Bitmap drawActionBlock(BlockInfo info, PictureBox PB, bool containsInfoDraw, int width, int height, bool canBeParent, int topPinX, int bottomPinX)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            SolidBrush sb = new SolidBrush(info.BackColor);
            
            int top = height - 5;
            if (!info.CanBeParent)
            {
                top = height;
            }

            GraphicsPath gp = new GraphicsPath();
            if (topPinX == 15)
            {
                gp.AddArc(new Rectangle(0, 0, 6, 6), 180, 90);
            }
            else
            {
                gp.AddLine(0, 0, topPinX, 0);
            }
            
            gp.AddLine(topPinX, 0, topPinX+5, 5);
            gp.AddLine(topPinX+13, 5, topPinX+18, 0);
            gp.AddArc(new Rectangle(width-8, 0, 8, 8), 270, 90);
            
            gp.AddArc(new Rectangle(width-10, top - 10, 10, 10), 0, 90);
            
            if (info.CanBeParent)
            {
                gp.AddLine(bottomPinX + 18, top, bottomPinX+13, height);
                gp.AddLine(bottomPinX+5, height, bottomPinX, top);
            }

            if (bottomPinX == 15)
            {
                gp.AddArc(new Rectangle(0, top - 8, 8, 8), 90, 90);
            }
            else
            {
                gp.AddLine(bottomPinX, top, 0, top);
            }

            gp.CloseFigure();

            g.FillPath(sb, gp);
            
            if (containsInfoDraw)
            {
                info.drawToBitmap(g, PB);
            }

            g.Dispose();

            return bmp;
        }

        private static Bitmap drawValueBlock(BlockInfo info, PictureBox PB, bool containsInfoDraw, int width, int height, bool canBeParent, int topPinX, int bottomPinX)
        {
            info.Kind = Block.BlockKinds.Value;
            if (info.ValueKind == ValueBlock.ValueKinds.boolean)
            {
                width += 20;
                info.drawXStart = 15;
            }

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            SolidBrush sb = new SolidBrush(info.BackColor);
            
            if (info.ValueKind == ValueBlock.ValueKinds.text || info.ValueKind == ValueBlock.ValueKinds.color)
            {
                g.FillRectangle(sb, new Rectangle(0, 0, width, height));
            }
            else if (info.ValueKind == ValueBlock.ValueKinds.number)
            {
                g.FillPath(sb, RoundedRect(new Rectangle(0, 0, width, height), 5));
            }
            else if (info.ValueKind == ValueBlock.ValueKinds.boolean)
            {

                GraphicsPath gp = new GraphicsPath();
                gp.AddLine(0, bmp.Height / 2, 10, 0);
                gp.AddLine(10, 0, bmp.Width - 10, 0);
                gp.AddLine(bmp.Width - 10, 0, bmp.Width, bmp.Height / 2);
                gp.AddLine(bmp.Width, bmp.Height / 2, bmp.Width - 10, bmp.Height);
                gp.AddLine(bmp.Width - 10, bmp.Height, 10, bmp.Height);
                gp.AddLine(10, bmp.Height, 0, bmp.Height / 2);

                gp.CloseFigure();

                g.FillPath(sb, gp);
            }

            if (containsInfoDraw)
            {
                info.drawToBitmap(g, PB);
            }

            g.Dispose();

            return bmp;
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private static Bitmap drawContainBlock(List<BlockInfo> infos, PictureBox PB, bool containsInfoDraw, int width, int height, bool canBeParent, int topPinX, int bottomPinX)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            int y = 0;

            for (int i = 0; i <= infos.Count - 1; i++)
            {
                //draw the top
                g.DrawImage(drawActionBlock(infos[i], PB, true, width, 32, true, topPinX, 27), new Point(0, y));
                topPinX = 27;

                //draw the center
                g.FillRectangle(new SolidBrush(infos[0].BackColor), new RectangleF(0, y+27, 12, 20));

                y += 47;
            }

            //draw the bottom
            g.DrawImage(drawActionBlock(infos[0], PB, false, width, 32, infos[0].CanBeParent, 27, 15), new Point(0,y));

            return bmp;
        }

        private static Bitmap drawOnStartBlock(BlockInfo info, PictureBox PB, bool containsInfoDraw, int width, int height, int bottomPinX)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            SolidBrush sb = new SolidBrush(info.BackColor);
            
            //g.FillRectangle(sb, 0, 15, width, 30);
            //g.FillRectangle(sb, 15, height - 5, 15, 5);

            g.FillEllipse(sb, 0, 0, 100, 39);
            
            int top = height - 5;

            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(8, 20, width - 8, 20);
            gp.AddArc(new Rectangle(width - 8, 20, 8, 8), 270, 90);

            gp.AddArc(new Rectangle(width - 10, top - 10, 10, 10), 0, 90);

            gp.AddLine(bottomPinX + 18, top, bottomPinX + 13, height);
            gp.AddLine(bottomPinX + 5, height, bottomPinX, top);

            gp.AddArc(new Rectangle(0, top - 8, 8, 8), 90, 90);

            gp.AddLine(0, top - 8, 0, 20);

            gp.CloseFigure();

            g.FillPath(sb, gp);

            if (containsInfoDraw)
            {
                info.drawToBitmap(g, PB);
            }

            g.Dispose();

            return bmp;
        }


    }
}