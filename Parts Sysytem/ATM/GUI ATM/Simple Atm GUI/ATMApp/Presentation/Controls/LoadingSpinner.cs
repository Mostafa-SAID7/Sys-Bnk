using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATMApp.Presentation.Controls
{
    public class LoadingSpinner : Control
    {
        private Timer timer;
        private float angle;

        public LoadingSpinner()
        {
            this.Size = new Size(50, 50);
            this.DoubleBuffered = true;
            timer = new Timer { Interval = 30 };
            timer.Tick += (s, e) => { angle += 6; this.Invalidate(); };
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.RotateTransform(angle);
            using (Pen pen = new Pen(Color.Gray, 4))
            {
                e.Graphics.DrawArc(pen, -20, -20, 40, 40, 0, 270);
            }
        }
    }
}
