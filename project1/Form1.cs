using System;
using System.Drawing;
using System.Windows.Forms;

namespace project1
{
    public partial class Form1 : Form
    {
        private int tracbarValue;
        private int height = 0;
        private Timer timer = new Timer();
        private Color color = Color.Cyan;
        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(frmMain_Paint);
            timer.Interval = 100;
            this.timer.Tick += timer_tick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
        }
        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White, 1);
            SolidBrush b = new SolidBrush(color);
            
            // temporary rectangle for boundaries
            Rectangle rectBoundary = new Rectangle(70, 200, 200, 250);

            // Bucket for fluid to drop in
            g.DrawLine(p, new Point(rectBoundary.X-1, rectBoundary.Y), new Point(rectBoundary.X-1, rectBoundary.Y+250));
            g.DrawLine(p, new Point(rectBoundary.X+200, rectBoundary.Y), new Point(rectBoundary.X+200, rectBoundary.Y+250));
            g.DrawLine(p, new Point(rectBoundary.X, rectBoundary.Y + 250), new Point(rectBoundary.X + 200, rectBoundary.Y + 250));
            
            // Rectanlge to fill the bucket
            Rectangle fillingRect = new Rectangle(rectBoundary.X, (rectBoundary.Bottom-height), 200, height);
            g.FillRectangle(b, fillingRect);
            
            // Handle the width of fluid dropping
            if (tracbarValue > 0 && height < 240)
            {
                if (tracbarValue >= 7)
                    g.FillRectangle(b, new Rectangle(82, 100, 20, 350));
                else if (tracbarValue >= 3)
                    g.FillRectangle(b, new Rectangle(85, 100, 15, 350));
                else
                    g.FillRectangle(b, new Rectangle(87, 100, 10, 350));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // close application when close button is pressed
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // start the timer when trachbar value is greater than 0
            if (tracbarValue > 0)
            {
                timer.Start();
            }
            // If trackbar value is 0, stop the timer
            if (tracbarValue == 0)
                timer.Stop();

            // store trackBar value
            tracbarValue = trackBar1.Value;
        }
        private void timer_tick(object sender, EventArgs e)
        {
            // On every timer tick, increase the height of rectangle
            height += tracbarValue;
            this.Invalidate();

            // when rectangle is filled completely, 
            // set trackbar value to 0 and stop the timer.
            if (height >= 240)
            {
                trackBar1.Value = 0;
                tracbarValue = 0;
                height = 0;
                timer.Stop();
            }
        }
    }

}
