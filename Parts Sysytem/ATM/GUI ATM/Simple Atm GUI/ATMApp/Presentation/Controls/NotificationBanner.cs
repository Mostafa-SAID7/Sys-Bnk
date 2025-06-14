using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATMApp.Presentation.Controls
{
    public class NotificationBanner : Panel
    {
        private Label messageLabel;
        private Timer slideTimer;
        private int targetTop = 0;

        public NotificationBanner(string message, Color bgColor)
        {
            this.Height = 40;
            this.Dock = DockStyle.Top;
            this.BackColor = bgColor;
            this.Visible = false;
            this.messageLabel = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };
            this.Controls.Add(messageLabel);

            slideTimer = new Timer { Interval = 15 };
            slideTimer.Tick += (s, e) =>
            {
                if (this.Top < targetTop)
                {
                    this.Top += 5;
                    if (this.Top >= targetTop)
                    {
                        slideTimer.Stop();
                        var closeTimer = new Timer { Interval = 3000 };
                        closeTimer.Tick += (s2, e2) => { this.Visible = false; closeTimer.Stop(); };
                        closeTimer.Start();
                    }
                }
            };
        }

        public void ShowBanner()
        {
            this.Top = -this.Height;
            this.Visible = true;
            targetTop = 0;
            slideTimer.Start();
        }
    }
}
