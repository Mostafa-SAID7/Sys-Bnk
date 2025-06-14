using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ATMApp.Presentation.Controls
{
    public class RoundedButton : Button
    {
        public int CornerRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(Width - CornerRadius, Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(0, Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();
            Region = new Region(path);
        }
    }
}
