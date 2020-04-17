using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public class AddBlockInfoToTheForm
    {
        public AddBlockInfoToTheForm(Form form, Point programingPanelLocation, Action<List<BlockInfo>, Point> addBlock, Action<Block, bool> moveBlock)
        {
            this.form = form;

            ProgramingPanelLocation = programingPanelLocation;

            AddBlock = addBlock;

            MoveBlock = moveBlock;
        }
        
        public Form form;

        public Point ProgramingPanelLocation;

        public Action<List<BlockInfo>, Point> AddBlock;
        public Action<Block, bool> MoveBlock;

        BlockInfo Info;
        Point lastPoint;
        PictureBox pb;
        PictureBox screen;

        public void add(BlockInfo info, Point locationInScreen)
        {
            //save the variabels
            Info = info;

            pb = new PictureBox();
            Bitmap bmp = BlocksImageCreator.drawBitmap(info.Kind, info, pb);
            pb.Size = bmp.Size;
            pb.BackColor = Color.Transparent;
            pb.Image = bmp;

            //pb.Location = Cursor.Position;
            Point loc = locationInScreen;
            loc.Offset(-form.Left, -form.Top);
            loc.Offset(-40, -40);

            pb.Location = loc;

            //set the pb mouse events
            pb.MouseMove += Pb_MouseMove;
            pb.MouseDown += Pb_MouseDown;

            //form.Controls.Add(pb);
            //create a pb that contains all the form controls pictures
            makeTheScreen(pb);

            screen.MouseMove += Pb_MouseMove;

            //pb.BringToFront();

            lastPoint = new Point(10, 10);

        }

        public void makeTheScreen(PictureBox movingPB)
        {
            screen = new PictureBox();
            screen.Size = new Size(form.Width, form.Height);
            //screen.Location = new Point(-8, -31); //the image offset when using screen.DrawToBitmap

            //create the image
            Bitmap screenBmp = new Bitmap(screen.Width, screen.Height);

            //set all the blocks images
            MoveBlock(null, true);
            for (int i = 0; i <= form.Controls.Count - 1; i++)
            {
                form.Controls[i].DrawToBitmap(screenBmp, form.Controls[i].Bounds);
            }

            //form.DrawToBitmap(screenBmp, new Rectangle(new Point(), form.Size));

            screen.Image = screenBmp;
            form.Controls.Add(screen);
            screen.BringToFront();

            screen.Controls.Add(movingPB);
        }

        private void Pb_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseLoc = pb.PointToClient(Cursor.Position);
            pb.Left += (mouseLoc.X - lastPoint.X);
            pb.Top += (mouseLoc.Y - lastPoint.Y);
        }

        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pb.Parent = null;
                disposeTheScreen();

                AddBlock(new List<BlockInfo>() { Info }, new Point(pb.Left - ProgramingPanelLocation.X, pb.Top - ProgramingPanelLocation.Y));

            }
        }

        public void disposeTheScreen()
        {

            screen.Dispose();

            MoveBlock(null, false);

        }

    }
}
