// --- FILE: ATMApp.Presentation.Controls.CustomCard.cs ---
// [unchanged — content skipped for brevity]

// --- FILE: ATMApp.Presentation.Controls.RoundedButton.cs ---
// [unchanged — content skipped for brevity]

// --- FILE: ATMApp.Presentation.Controls.LoadingSpinner.cs ---
// [unchanged — content skipped for brevity]

// --- FILE: ATMApp.Presentation.Controls.NotificationBanner.cs ---
// [unchanged — content skipped for brevity]

// --- FILE: ATMApp.Presentation.Forms.MainForm.cs ---
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using ATMApp.Application.Services;
using ATMApp.Presentation.Controls;

namespace ATMApp.Presentation.Forms
{
    public class MainForm : Form
    {
        private readonly AccountService accountService = new AccountService();
        private Label lblBalance;
        private TextBox txtAmount;
        private NotificationBanner notificationBanner;
        private bool isDarkMode = false;

        public MainForm()
        {
            this.Text = "Modern ATM UI";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.MinimumSize = new Size(500, 350);

            InitializeLayout();
            ApplyTheme(false);
        }

        private void InitializeLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            var header = new Panel { Dock = DockStyle.Fill, Height = 60 };
            var title = new Label
            {
                Text = "💳 ATM Dashboard",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Left,
                AutoSize = true
            };
            var darkToggle = new CheckBox
            {
                Text = "Dark Mode",
                Dock = DockStyle.Right
            };
            darkToggle.CheckedChanged += (s, e) => ApplyTheme(darkToggle.Checked);

            header.Controls.Add(title);
            header.Controls.Add(darkToggle);

            var balanceCard = new CustomCard();
            lblBalance = new Label
            {
                Text = $"Balance: {accountService.GetBalance():C}",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            balanceCard.Controls.Add(lblBalance);

            var actionCard = new CustomCard();
            txtAmount = new TextBox { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 10) };
            var btnDeposit = new RoundedButton { Text = "Deposit", Dock = DockStyle.Top, Height = 40, BackColor = Color.MediumSeaGreen, ForeColor = Color.White };
            var btnWithdraw = new RoundedButton { Text = "Withdraw", Dock = DockStyle.Top, Height = 40, BackColor = Color.IndianRed, ForeColor = Color.White };
            var btnCheck = new RoundedButton { Text = "Check Balance", Dock = DockStyle.Top, Height = 40, BackColor = Color.SteelBlue, ForeColor = Color.White };

            btnDeposit.Click += (s, e) => HandleDeposit();
            btnWithdraw.Click += (s, e) => HandleWithdraw();
            btnCheck.Click += (s, e) => ShowBalance();

            actionCard.Controls.Add(btnCheck);
            actionCard.Controls.Add(btnWithdraw);
            actionCard.Controls.Add(btnDeposit);
            actionCard.Controls.Add(txtAmount);

            notificationBanner = new NotificationBanner("Welcome to the ATM App", Color.Gray);
            this.Controls.Add(notificationBanner);

            layout.Controls.Add(header);
            layout.Controls.Add(balanceCard);
            layout.Controls.Add(actionCard);
            this.Controls.Add(layout);
        }

        private void HandleDeposit()
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                accountService.Deposit(amount);
                SystemSounds.Asterisk.Play();
                ShowNotification("Deposit successful!", Color.MediumSeaGreen);
                ShowBalance();
            }
            catch (Exception ex)
            {
                SystemSounds.Exclamation.Play();
                ShowNotification(ex.Message, Color.OrangeRed);
            }
        }

        private void HandleWithdraw()
        {
            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                if (accountService.Withdraw(amount))
                {
                    SystemSounds.Asterisk.Play();
                    ShowNotification("Withdrawal successful!", Color.IndianRed);
                }
                else
                {
                    SystemSounds.Hand.Play();
                    ShowNotification("Insufficient funds.", Color.Maroon);
                }
                ShowBalance();
            }
            catch (Exception ex)
            {
                SystemSounds.Exclamation.Play();
                ShowNotification(ex.Message, Color.OrangeRed);
            }
        }

        private void ShowBalance()
        {
            lblBalance.Text = $"Balance: {accountService.GetBalance():C}";
        }

        private void ApplyTheme(bool dark)
        {
            isDarkMode = dark;
            this.BackColor = dark ? Color.FromArgb(30, 30, 30) : Color.White;
            this.ForeColor = dark ? Color.White : Color.Black;
        }

        private void ShowNotification(string message, Color bgColor)
        {
            notificationBanner.Dispose();
            notificationBanner = new NotificationBanner(message, bgColor);
            this.Controls.Add(notificationBanner);
            notificationBanner.BringToFront();
            notificationBanner.ShowBanner();
        }
    }
}
