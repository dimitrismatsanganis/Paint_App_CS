using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paint_App
{
    public partial class Paint : Form
    {
        Graphics g;
        Pen p = new Pen(Color.Black, 5);
        float x1, y1, x2, y2, x3, y3;
        bool Drawing = false;
        bool Eraser = false;
        bool Rectangle = false;
        bool Ellipse = false;
        bool Line = false;
        ColorDialog c = new ColorDialog();

        public Paint()
        {
            InitializeComponent();
            p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            Eraser = false;
            Rectangle = false;
            Ellipse = false;
            Line = false;
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            if (c.ShowDialog() == DialogResult.OK)
            {
                p.Color = c.Color;
            }
        }
		
        private void btn_Eraser_Click(object sender, EventArgs e)
        {
            Eraser = true;
            Rectangle = false;
            Ellipse = false;
            Line = false;
            Drawing = false;

        }

        private void btn_DrawLine_Click(object sender, EventArgs e)
        {
            Drawing = false;
            Eraser = false;
            Rectangle = false;
            Ellipse = false;
            Line = true;
        }

        private void btn_DrawEllipse_Click(object sender, EventArgs e)
        {
            Drawing = false;
            Eraser = false;
            Rectangle = false;
            Ellipse = true;
            Line = false;
        }

        private void btn_DrawRectangle_Click(object sender, EventArgs e)
        {
            Drawing = false;
            Eraser = false;
            Rectangle = true;
            Ellipse = false;
            Line = false;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
            x3 = e.X - x2;
            y3 = e.Y - y2;
            if (Rectangle)
            {
                g = panel1.CreateGraphics();
                g.DrawRectangle(p, x2, y2, x3, y3);
                p.Color = c.Color;
            }
            else if (Ellipse)
            {
                g = panel1.CreateGraphics();
                g.DrawEllipse(p, x2, y2, x3, y3);
                p.Color = c.Color;
            }
            else if (Line)
            {
                g = panel1.CreateGraphics();
                g.DrawLine(p, x2, y2, e.X, e.Y);
                p.Color = c.Color;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: " + Convert.ToString(e.X);
            label2.Text = "Y: " + Convert.ToString(e.Y);
            if (Drawing && !Eraser)
            {
                g = panel1.CreateGraphics();
                g.DrawLine(p, x1, y1, e.X, e.Y);
                x1 = e.X;
                y1 = e.Y;
                p.Color = c.Color;
            }
            else if (Drawing && Eraser)
            {
                p.Color = Color.White;
                g = panel1.CreateGraphics();
                g.DrawLine(p, x1, y1, e.X, e.Y);
                x1 = e.X;
                y1 = e.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
            if (e.Button == MouseButtons.Left && (Rectangle || Ellipse || Line))
            {
                Drawing = false;
                x2 = e.X;
                y2 = e.Y;
            }
            else if (e.Button == MouseButtons.Left && (!Rectangle || !Ellipse || !Line))
            {
                Drawing = true;
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            p.Width = trackBar1.Value;
            label3.Text = "Size: " + trackBar1.Value;
        }
    }
}
