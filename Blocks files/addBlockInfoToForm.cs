using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public class addBlockInfoToTheForm
    {
        public addBlockInfoToTheForm(Form form, Point programingPanelLocation, Action<List<BlockInfo>, Point> addBlock)
        {
            this.form = form;

            ProgramingPanelLocation = programingPanelLocation;

            AddBlock = addBlock;
        }
        
        public Form form;

        Point ProgramingPanelLocation;

        Action<List<BlockInfo>, Point> AddBlock;

        BlockInfo Info;
        Point lastPoint;
        PictureBox pb;

        public void add(BlockInfo info, Point locationInScreen)
        {
            Info = info;

            pb = new PictureBox();
            Bitmap bmp = BlocksImageCreator.drawBitmap(info.Kind, info, pb);
            pb.Size = bmp.Size;
            pb.BackColor = SystemColors.ControlDark;
            pb.Image = bmp;

            //pb.Location = Cursor.Position;
            Point loc = locationInScreen;
            loc.Offset(-form.Left, -form.Top);
            loc.Offset(-40, -40);

            pb.Location = loc;

            pb.MouseMove += Pb_MouseMove;
            pb.MouseDown += Pb_MouseDown;

            form.Controls.Add(pb);
            pb.BringToFront();

            lastPoint = new Point(10, 10);
            
        }

        private void Pb_MouseMove(object sender, MouseEventArgs e)
        {
            pb.Left += (e.X - lastPoint.X);
            pb.Top += (e.Y - lastPoint.Y);
        }

        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pb.Parent = null;

                AddBlock(new List<BlockInfo>() { Info }, new Point(pb.Left - ProgramingPanelLocation.X, pb.Top - ProgramingPanelLocation.Y));

            }
        }

    }
}
