using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GuiScratch
{
    public class BlockInfoEvent
    {
        public BlockInfoEvent(string text)
        {
            Text = text;
            Kind = Kinds.text;
        }

        #region combo box

        #region controls combo box

        public enum ControlsKinds { none, all }
        public ControlsKinds ControlKind;

        public BlockInfoEvent(ControlsKinds controlKind, Values myValues)
        {
            Kind = Kinds.comboBox;
            ControlKind = controlKind;

            setComboBoxOptions(myValues);

            startComboBox(comboBoxOptions);
        }

        private void setComboBoxOptions(Values myValues)
        {
            if (ControlKind == ControlsKinds.all)
            {
                comboBoxOptions = myValues.getControls();
            }
        }
        
        public void update(Values values)
        {
            //update the controls names in the combo box if the control kind isnt none
            if (Kind == Kinds.comboBox && ControlKind != ControlsKinds.none)
            {
                int selectedIndex = comboBox.SelectedIndex;

                comboBox.Items.Clear();
                if (ControlKind == ControlsKinds.all)
                {
                    comboBox.Items.AddRange(values.getControls());
                }

                if (selectedIndex > comboBox.Items.Count - 1 || selectedIndex == -1)
                {
                    comboBox.SelectedIndex = 0;
                }
                else
                {
                    comboBox.SelectedIndex = selectedIndex;
                }

                setComboBoxSize();
                
            }
        }

        #endregion
        
        public BlockInfoEvent(string[] comboBoxOptions, int selectedIndex = 0, ControlsKinds controlKind = ControlsKinds.none)
        {
            ControlKind = controlKind;
            startComboBox(comboBoxOptions, selectedIndex);
        }
        
        private void startComboBox(string[] options, int selectedIndex = 0)
        {
            Kind = Kinds.comboBox;
            comboBox = new ComboBox();
            comboBox.Font = new Font("Microsoft Sans Serif", 8.25f);

            if (options.Length > 0)
            {
                comboBox.Items.AddRange(options);
                comboBox.SelectedIndex = selectedIndex;
                setComboBoxSize();
            }
            
            currentC = comboBox;
        }

        private void setComboBoxSize()
        {
            comboBox.Size = new Size(getMaxWidthFromStringList(comboBox.Items)+30, 8);
        }

        private int getMaxWidthFromStringList(ComboBox.ObjectCollection items)
        {
            int max = 0;
            for (int i = 0; i <= items.Count - 1; i++)
            {
                int cur = TextRenderer.MeasureText(items[i].ToString(), new System.Drawing.Font("Microsoft Sans Serif", 9f)).Width + 5;
                max = Math.Max(max, cur);
            }
            return max;
        }
        
        #endregion

        #region number input 

        public BlockInfoEvent(decimal startValue)
        {
            Kind = Kinds.numberInput;
            
            Text = startValue.ToString();

            startNumberInput();
        }
        
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //makes that the user can write only numbers in the text box

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        
        private void startNumberInput()
        {
            startTextBox();

            textBox.KeyPress += TextBox_KeyPress;
        }

        #endregion

        #region textInput

        public BlockInfoEvent(Kinds kind, string text)
        {
            Kind = kind;
            if (Kind == Kinds.textInput)
            {
                Text = text;
                startTextBox();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (UpdateBlock != null)
            {
                UpdateBlock(false);
            }
        }

        private void startTextBox()
        {
            textBox = new TextBox();
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Font = new Font("Microsoft Sans Serif", 10f);
            textBox.Text = Text;
            textBox.BorderStyle = BorderStyle.None;

            currentC = textBox;
        }

        #endregion

        #region boolean input

        public BlockInfoEvent(Kinds kind)
        {
            Kind = kind;
            if (kind == Kinds.booleanInput)
            {
                startBooleanPanel();
            }
        }

        private void startBooleanPanel()
        {
            booleanPanel = new Panel();
            booleanPanel.Size = new Size(50, 17);
            booleanPanel.BackColor = Color.Transparent;

            currentC = booleanPanel;
        }

        #endregion

        #region color input

        public BlockInfoEvent(Color color)
        {
            Kind = Kinds.colorInput;

            this.color = color;

            startColorInput();
        }

        private void startColorInput()
        {
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(17, 17);
            pictureBox.BackColor = color;

            pictureBox.MouseClick += PictureBox_MouseClick;

            currentC = pictureBox;
        }

        ColorDialog cd;
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            cd = new ColorDialog();
            cd.FullOpen = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                color = cd.Color;
                pictureBox.BackColor = color;
            }
        }

        #endregion

        #region client

        public bool CheckValueBlock(ValueBlock currentBlock)
        {
            if (Kind == Kinds.text || IsDragAble)
            {
                return false;
            }

            Point curBlockLoc = currentBlock.PB.PointToScreen(new Point());
            Point curCLoc = currentC.PointToScreen(new Point());
            
            if (curCLoc.X < curBlockLoc.X+currentBlock.PB.Width && curCLoc.X+currentC.Width > curBlockLoc.X)
            {
                if (Kind == Kinds.textInput ||
                    Kind == Kinds.numberInput && currentBlock.Info.ValueKind == ValueBlock.ValueKinds.number ||
                    Kind == Kinds.booleanInput && currentBlock.Info.ValueKind == ValueBlock.ValueKinds.boolean ||
                    Kind == Kinds.comboBox && currentBlock.Info.ValueKind == ValueBlock.ValueKinds.text ||
                    Kind == Kinds.colorInput && currentBlock.Info.ValueKind == ValueBlock.ValueKinds.color )
                {
                    addClient(currentBlock);
                    return true;
                }
            }

            if (Client != null)
            {
                if (Client.checkValueClient(currentBlock))
                {
                    return true;
                }
            }
            return false;
        }
        
        Control currentC;

        public void addClient(ValueBlock currentBlock)
        {
            if (Client != null) //already has a client
            {
                if (Client.checkValueClient(currentBlock))
                {
                    return;
                }
                else
                {
                    //remove the client
                    Client.removeFromBlock(true);
                    Client = null;
                }
            }
            currentC.Visible = false;
            currentBlock.PB.Parent = currentC.Parent;

            currentBlock.PB.BackColor = Color.Transparent;
            
            Client = currentBlock;
            Client.StartLikeClient(UpdateBlock, clientRemoved);
            
            UpdateBlock(false);
        }

        public void clientRemoved()
        {
            currentC.Visible = true;

            Client = null;

            UpdateBlock(false);
        }

        #endregion

        #region drag able

        public Label label = new Label();
        public BlockCode code;
        public BlockInfoEvent(Kinds kind, string text, BlockCode code, bool isDragAble)
        {
            Kind = kind;
            IsDragAble = isDragAble;
            this.code = code;
            Text = text;

            setDragAbleTextBox(ref label, text);
        }

        private void setDragAbleTextBox(ref Label label, string text)
        {
            label = new Label();
            label.Text = text;
            label.BackColor = Color.Blue;
            label.ForeColor = Color.White;
            label.Size = new Size(getTextSize(label.Text).Width, 16);
            label.Font = new Font("Microsoft Sans Serif", 9.75f);
            
            label.MouseDown += DragAbleControlMouseDown;
            label.MouseMove += DragAbleControlMouseMove;
            label.MouseUp += DragAbleControlMouseUp;
        }

        Point lastPoint;
        Label newLabel;

        private void DragAbleControlMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && AddVar != null)
            {
                Label label = sender as Label;

                //create a clone of the text box

                setDragAbleTextBox(ref newLabel, label.Text);
                newLabel.Location = label.Location;
                newLabel.Parent = label.Parent;

                //set the current textbox to be a client of the all blocks panel
                
                Point pbLocInScreen = label.PointToScreen(new Point());
                Point coLocInScreen = label.Parent.Parent.PointToScreen(new Point());
                //label.Parent = null;

                label.Location = new Point(pbLocInScreen.X - coLocInScreen.X, pbLocInScreen.Y - coLocInScreen.Y);

                //label.Location = label.PointToClient(label.Location);
                label.Parent = label.Parent.Parent;
                label.BringToFront();

                this.label = newLabel;
                
                lastPoint = e.Location;
            }
        }

        private void DragAbleControlMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && AddVar != null)
            {
                Label label = sender as Label;
                label.Left += (e.X - lastPoint.X);
                label.Top += (e.Y - lastPoint.Y);
            }
        }

        private void DragAbleControlMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && AddVar != null)
            {
                Label label = sender as Label;
                AddVar(Clone(), label.Location);
                label.Dispose();
            }
        }

        #endregion

        
        #region draw image to bmp

        public void drawImageToBmp(ref Bitmap bmp, ref Point loc, ref Graphics g)
        {
            Point myLoc = new Point(loc.X+5, loc.Y + marginElse);
            if (Client != null)
            {
                Client.drawImageToBmp(ref bmp, ref myLoc, ref g);
            }
            
            /*else
            {
                switch (Kind)
                {
                    case Kinds.colorInput:
                        myLoc.Y -= 2;
                        g.FillRectangle(new SolidBrush(color), new Rectangle(myLoc, pictureBox.Size));
                        break;
                    case Kinds.numberInput:
                        myLoc.X += marginNI;
                        myLoc.Y -= 1;
                        textBox.DrawToBitmap(bmp, new Rectangle(myLoc, textBox.Size));
                        break;
                    case Kinds.textInput:
                        textBox.DrawToBitmap(bmp, new Rectangle(myLoc, textBox.Size));
                        break;
                    case Kinds.comboBox:
                        myLoc.Y -= 3;
                        comboBox.DrawToBitmap(bmp, new Rectangle(myLoc, comboBox.Size));
                        break;
                }
            }*/

            //add the width to the myLoc
            loc.X += getWidth();
        }

        #endregion
    

        public bool IsDragAble = false;
        public Action<BlockInfoEvent, Point> AddVar;
        
        public string getText()
        {
            if (Client == null)
            {
                switch (Kind)
                {
                    case Kinds.textInput:
                        return "\"" + textBox.Text + "\"";
                    case Kinds.numberInput:
                        return textBox.Text;
                    case Kinds.comboBox:
                        if (ControlKind == ControlsKinds.none)
                        {
                            return comboBox.Text.Substring(0, 1).ToUpper() + comboBox.Text.Substring(1); //returns the combo box text and makes the first letter to be upper case
                        }
                        else
                        {
                            return comboBox.Text;
                        }
                    case Kinds.booleanInput:
                        return null;
                    case Kinds.colorInput:
                        return "Color.FromArgb(" + color.ToArgb().ToString() + ")";
                }
            }
            else
            {
                string res = Client.Info.BlockCode.getCode(Client.Info);
                if (Kind == Kinds.textInput && Client.Info.ValueKind != ValueBlock.ValueKinds.text)
                {
                    res += ".ToString()";
                }
                return res;
            }
            
            return "";
        }

        #region to and from string

        public override string ToString()
        {
            //returns kind isDragAble (if is darg able BlockCode) var controlKind ((if is a combo box and control kind is none)comboBoxOptionsCount
            // comboBoxOptions) client
            string res = "\x0001" + ((int)Kind).ToString() + "\x0001" + IsDragAble.ToString() + "\x0001";

            if (IsDragAble)
            {
                res += code.ToString() + "\x0001";
                res += Text;
            }
            else
            {
                //add the var
                switch (Kind)
                {
                    case Kinds.colorInput:
                        res += pictureBox.BackColor.ToArgb().ToString();
                        break;
                    case Kinds.comboBox:
                        res += comboBox.SelectedIndex.ToString();
                        break;
                    case Kinds.numberInput:
                    case Kinds.textInput:
                        res += textBox.Text;
                        break;
                    case Kinds.text:
                        res += Text;
                        break;
                }
            }

            res += "\x0001" + ((int)ControlKind).ToString();

            //add the combo box options
            if (Kind == Kinds.comboBox && ControlKind == ControlsKinds.none)
            {
                res += "\x0001" + comboBox.Items.Count;

                for (int i = 0; i <= comboBox.Items.Count - 1; i++)
                {
                    res += "\x0001" + comboBox.Items[i].ToString();
                }
            }

            res += "\x0001";
            if (Client == null)
            {
                res += "-1";
            }
            else
            {
                res += Client.BlockIndex.ToString();
            }

            return res;
        }

        string varS;
        public int clientIndex;

        string[] comboBoxOptions;

        public BlockInfoEvent(string[] vars, ref int index, Values myValues)
        {
            Kind = (Kinds)int.Parse(vars[index++]);

            IsDragAble = BlockInfo.stringToBool(vars[index++]);

            if (IsDragAble)
            {
                code = new BlockCode(vars, ref index);
            }

                varS = vars[index++];

            ControlKind = (ControlsKinds)int.Parse(vars[index++]);

            //check for the combo box options
            if (Kind == Kinds.comboBox && ControlKind == ControlsKinds.none)
            {
                int itemsCount = int.Parse(vars[index++]);

                comboBoxOptions = new string[itemsCount];

                for (int i = 0; i <= itemsCount - 1; i++)
                {
                    comboBoxOptions[i] = vars[index++];
                }
            }

            clientIndex = int.Parse(vars[index++]);

            setTheInfoEvent(myValues);
        }

        private void setTheInfoEvent(Values myValues)
        {
            if (IsDragAble)
            {
                Text = varS;
                setDragAbleTextBox(ref label, varS);
            }
            else
            {
                switch (Kind)
                {
                    case Kinds.text:
                        Text = varS;
                        break;
                    case Kinds.booleanInput:
                        startBooleanPanel();
                        break;
                    case Kinds.textInput:
                        Text = varS;
                        startTextBox();
                        break;
                    case Kinds.numberInput:
                        Text = varS;
                        startNumberInput();
                        break;
                    case Kinds.comboBox:
                        setComboBoxOptions(myValues);
                        if (comboBoxOptions == null)
                        {
                            comboBoxOptions = new string[0];
                        }
                        startComboBox(comboBoxOptions, int.Parse(varS));
                        break;
                    case Kinds.colorInput:
                        color = Color.FromArgb(int.Parse(varS));
                        startColorInput();
                        break;

                }
            }
        }

        #endregion

        #region vars

        public enum Kinds { text, comboBox, numberInput, textInput, booleanInput, colorInput }
        public Kinds Kind;

        string Text;

        ComboBox comboBox;

        TextBox textBox;

        Panel booleanPanel;

        Color color;
        PictureBox pictureBox;

        public ValueBlock Client { get; set; }

        public Action<bool> UpdateBlock;

        #endregion

        #region buliding funcs

        public int getWidth()
        {
            if (IsDragAble)
            {
                if (Kind == Kinds.textInput)
                {
                    return getTextSize(label.Text).Width;
                }
                else if (Kind == Kinds.numberInput)
                {
                    return getTextSize(label.Text).Width+20;
                }
                else if (Kind == Kinds.booleanInput)
                {
                    return getTextSize(label.Text).Width + 20;
                }
            }
            else if (Client == null)
            {
                if (Kind == Kinds.text)
                {
                    return getTextSize(Text).Width;
                }
                else if (Kind == Kinds.comboBox)
                {
                    return comboBox.Width + 5;
                }
                else if (Kind == Kinds.numberInput)
                {
                    setTextBoxSize();
                    return textBox.Width + 20;
                }
                else if (Kind == Kinds.textInput)
                {
                    setTextBoxSize();
                    return textBox.Width + 5;
                }
                else if (Kind == Kinds.booleanInput)
                {
                    return booleanPanel.Width + 20;
                }
                else if (Kind == Kinds.colorInput)
                {
                    return pictureBox.Width;
                }
            }
            else //has a client
            {
                return Client.PB.Width+10;
            }
            return 100;
        }

        public int getHeight()
        {
            if (Client == null)
            {
                return 17;
            }
            else
            {
                return Client.Info.getHeight() + 4;
            }
        }

        int marginCB = -3;
        int marginElse = -2;
        int marginNI = 9;

        public void drawToBitmap(Graphics g, PictureBox PB, Point drawPoint, Color backColor)
        {
            if (Client == null)
            {
                if (Kind == Kinds.text)
                {
                    g.DrawString(Text, new Font("Microsoft Sans Serif", 10f), Brushes.White, drawPoint);
                }
                else if (Kind == Kinds.comboBox)
                {
                    comboBox.Location = new Point(drawPoint.X, drawPoint.Y +marginCB);
                    PB.Controls.Add(comboBox);
                }
                else if (Kind == Kinds.numberInput)
                {
                    if (IsDragAble)
                    {
                        //g.FillRectangle(Brushes.Blue, new Rectangle(new Point(drawPoint.X + 9, drawPoint.Y - 1), getTextSize(label.Text)));
                        Size lSize = getTextSize(label.Text);

                        g.FillEllipse(Brushes.Blue, new Rectangle(drawPoint.X, drawPoint.Y - 3, 15, 17));
                        g.FillEllipse(Brushes.Blue, new Rectangle(drawPoint.X + lSize.Width, drawPoint.Y - 3, 15, 17));
                        
                        //draw the text
                        //g.DrawString(textBox.Text, textBox.Font, Brushes.White, new Point(drawPoint.X + 9, drawPoint.Y - 1));

                        //label.Location = drawPoint;
                        label.Location = new Point(drawPoint.X +marginNI, drawPoint.Y +marginElse);

                        label.Parent = PB;
                    }
                    else
                    {
                        setTextBoxSize();
                        drawPoint.Y += 1;
                        //textBox.Size = new Size(TextRenderer.MeasureText(textBox.Text, new System.Drawing.Font("Microsoft Sans Serif", 10f)).Width, 20);
                        textBox.Location = new Point(drawPoint.X + 9, drawPoint.Y +marginElse);

                        PB.Controls.Add(textBox);
                        if (Client == null)
                        {
                            g.FillEllipse(Brushes.White, new Rectangle(drawPoint.X, drawPoint.Y - 3, 15, 17));
                            g.FillEllipse(Brushes.White, new Rectangle(drawPoint.X + textBox.Width, drawPoint.Y - 3, 15, 17));
                        }
                    }

                }
                else if (Kind == Kinds.textInput)
                {
                    setTextBoxSize();
                    //textBox.Size = new Size(TextRenderer.MeasureText(textBox.Text, new System.Drawing.Font("Microsoft Sans Serif", 10f)).Width, 20);
                    textBox.Location = drawPoint;
                    PB.Controls.Add(textBox);
                }
                else if (Kind == Kinds.booleanInput)
                {
                    booleanPanel.Location = new Point(drawPoint.X + 9, drawPoint.Y +marginElse);
                    PB.Controls.Add(booleanPanel);
                    //booleanPanel.BackColor = backColor;
                    //PB.Controls.Add(booleanPanel);

                    //draw the border
                    drawPoint.Y -= 3;
                    drawPoint.X += 7;
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddLine(drawPoint.X - 10, drawPoint.Y + booleanPanel.Height / 2, drawPoint.X, drawPoint.Y);
                    gp.AddLine(drawPoint.X, drawPoint.Y, drawPoint.X + booleanPanel.Width, drawPoint.Y);
                    gp.AddLine(drawPoint.X + booleanPanel.Width, drawPoint.Y, drawPoint.X + booleanPanel.Width + 10, drawPoint.Y + booleanPanel.Height / 2);
                    gp.AddLine(drawPoint.X + booleanPanel.Width + 10, drawPoint.Y + booleanPanel.Height / 2, drawPoint.X + booleanPanel.Width, drawPoint.Y + booleanPanel.Height);
                    gp.AddLine(drawPoint.X + booleanPanel.Width, drawPoint.Y + booleanPanel.Height, drawPoint.X, drawPoint.Y + booleanPanel.Height);
                    gp.AddLine(drawPoint.X, drawPoint.Y + booleanPanel.Height, drawPoint.X - 10, drawPoint.Y + booleanPanel.Height / 2);

                    gp.CloseFigure();

                    g.FillPath(new SolidBrush(Color.FromArgb(100, Color.White)), gp);
                    g.DrawPath(new Pen(Color.Black, 1), gp);

                    booleanPanel.Location = new Point(drawPoint.X - 10, drawPoint.Y);
                }
                else if (Kind == Kinds.colorInput)
                {
                    pictureBox.Location = new Point(drawPoint.X, drawPoint.Y +marginElse);
                    pictureBox.Parent = PB;
                }
            }
            else
            {
                Client.setLocation(drawPoint.X, drawPoint.Y+marginElse);
            }
        }

        private void setTextBoxSize()
        {
            if (Client == null)
            {
                setTextBoxSize(textBox);
                //textBox.Size = new Size(Math.Max(10, TextRenderer.MeasureText(textBox.Text, new Font(textBox.Font.FontFamily, 9.75f), new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width), 20);
            }
        }

        private void setTextBoxSize(TextBox textBox)
        {
            textBox.Size = new Size(Math.Max(10, getTextSize(textBox.Text).Width), 20);
        }

        private Size getTextSize(string text)
        {
            return TextRenderer.MeasureText(text, new Font("Microsoft Sans Serif", 9.75f), new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
        }

        public BlockInfoEvent Clone()
        {
            if (IsDragAble)
            {
                return new BlockInfoEvent(Kind, label.Text,code, IsDragAble);
            }
            else
            {
                switch (Kind)
                {
                    case Kinds.text:
                        return new BlockInfoEvent(Text);
                    case Kinds.comboBox:
                        //make combo box items to a list of string
                        string[] res = new string[comboBox.Items.Count];
                        for (int i = 0; i <= comboBox.Items.Count - 1; i++)
                        {
                            res[i] = comboBox.Items[i].ToString();
                        }

                        return new BlockInfoEvent(res, comboBox.SelectedIndex, ControlKind);
                    case Kinds.numberInput:
                        return new BlockInfoEvent(decimal.Parse(textBox.Text));
                    case Kinds.textInput:
                        return new BlockInfoEvent(Kinds.textInput, textBox.Text);
                    case Kinds.booleanInput:
                        return new BlockInfoEvent(Kinds.booleanInput);
                    case Kinds.colorInput:
                        return new BlockInfoEvent(color);
                }
            }
            return null;
        }

        #endregion
    }
}