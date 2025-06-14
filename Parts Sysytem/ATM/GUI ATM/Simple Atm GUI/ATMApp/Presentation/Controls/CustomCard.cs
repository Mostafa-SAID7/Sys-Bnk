using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATMApp.Presentation.Controls
{
    public class CustomCard : Panel
    {
        public CustomCard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Padding = new Padding(10);
            this.Margin = new Padding(10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (GraphicsPath path = GetRoundedRectPath(bounds, 12))
            using (Pen pen = new Pen(Color.LightGray))
            {
                this.Region = new Region(path);
                g.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}
