using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class MainForm : Form
    {
        private Panel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Button btnNhan;
        private Button btnKhanh;
        private Button btnKhai;
        private Panel bgPanel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Trang Chủ";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(12, 15, 30);

            // Background panel with gradient
            bgPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            bgPanel.Paint += BgPanel_Paint;
            this.Controls.Add(bgPanel);

            // Header Panel
            headerPanel = new Panel
            {
                Size = new Size(900, 120),
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            bgPanel.Controls.Add(headerPanel);

            // Title
            titleLabel = new Label
            {
                Text = "⬡  NHÓM BẠN",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 220, 100),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            titleLabel.Location = new Point((900 - 260) / 2, 25);
            headerPanel.Controls.Add(titleLabel);

            subtitleLabel = new Label
            {
                Text = "Chọn thành viên để xem trang cá nhân",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.FromArgb(160, 180, 220),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            subtitleLabel.Location = new Point((900 - 300) / 2, 78);
            headerPanel.Controls.Add(subtitleLabel);

            // Cards
            int cardY = 180;
            int cardW = 220, cardH = 260;
            int gap = 40;
            int totalW = 3 * cardW + 2 * gap;
            int startX = (900 - totalW) / 2;

            btnNhan = CreateMemberCard("NHÂN", "🎯", Color.FromArgb(255, 100, 130), startX, cardY, cardW, cardH);
            btnKhanh = CreateMemberCard("KHÁNH", "🌟", Color.FromArgb(100, 200, 255), startX + cardW + gap, cardY, cardW, cardH);
            btnKhai = CreateMemberCard("KHẢI", "🔥", Color.FromArgb(100, 230, 160), startX + 2 * (cardW + gap), cardY, cardW, cardH);

            btnNhan.Click += (s, e) => OpenPage(new NhanForm());
            btnKhanh.Click += (s, e) => OpenPage(new KhanhForm());
            btnKhai.Click += (s, e) => OpenPage(new KhaiForm());

            bgPanel.Controls.Add(btnNhan);
            bgPanel.Controls.Add(btnKhanh);
            bgPanel.Controls.Add(btnKhai);

            // Footer
            Label footer = new Label
            {
                Text = "© 2025 Nhóm Bạn — Nhân • Khánh • Khải",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(80, 100, 140),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            footer.Location = new Point((900 - 300) / 2, 520);
            bgPanel.Controls.Add(footer);
        }

        private Button CreateMemberCard(string name, string emoji, Color accentColor, int x, int y, int w, int h)
        {
            var btn = new Button
            {
                Size = new Size(w, h),
                Location = new Point(x, y),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(25, 30, 55),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = accentColor,
                Text = ""
            };
            btn.FlatAppearance.BorderColor = accentColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(35, 42, 75);

            btn.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = btn.ClientRectangle;

                // Glow top border
                using (var topBrush = new LinearGradientBrush(
                    new Point(0, 0), new Point(w, 0),
                    Color.Transparent, Color.Transparent))
                {
                    topBrush.InterpolationColors = new ColorBlend
                    {
                        Colors = new[] { Color.Transparent, accentColor, Color.Transparent },
                        Positions = new[] { 0f, 0.5f, 1f }
                    };
                    g.FillRectangle(topBrush, 0, 0, w, 3);
                }

                // Emoji
                using (var emojiFont = new Font("Segoe UI Emoji", 36))
                {
                    var emojiSize = g.MeasureString(emoji, emojiFont);
                    g.DrawString(emoji, emojiFont, Brushes.White,
                        (w - emojiSize.Width) / 2, 40);
                }

                // Divider
                using (var pen = new Pen(Color.FromArgb(50, 70, 100), 1))
                    g.DrawLine(pen, 30, 120, w - 30, 120);

                // Name
                using (var nameFont = new Font("Segoe UI", 18, FontStyle.Bold))
                using (var nameBrush = new SolidBrush(accentColor))
                {
                    var nameSize = g.MeasureString(name, nameFont);
                    g.DrawString(name, nameFont, nameBrush,
                        (w - nameSize.Width) / 2, 135);
                }

                // Subtitle
                using (var subFont = new Font("Segoe UI", 9))
                using (var subBrush = new SolidBrush(Color.FromArgb(120, 140, 180)))
                {
                    string sub = "Xem trang cá nhân →";
                    var subSize = g.MeasureString(sub, subFont);
                    g.DrawString(sub, subFont, subBrush,
                        (w - subSize.Width) / 2, 190);
                }
            };

            return btn;
        }

        private void BgPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = bgPanel.ClientRectangle;

            using (var brush = new LinearGradientBrush(rect,
                Color.FromArgb(12, 15, 30), Color.FromArgb(20, 25, 50),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            // Decorative dots
            using (var dotBrush = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
            {
                var rng = new Random(42);
                for (int i = 0; i < 60; i++)
                {
                    int dx = rng.Next(0, 900), dy = rng.Next(0, 600);
                    int ds = rng.Next(1, 4);
                    g.FillEllipse(dotBrush, dx, dy, ds, ds);
                }
            }
        }

        private void OpenPage(Form page)
        {
            page.Owner = this;
            page.StartPosition = FormStartPosition.CenterScreen;
            page.Show();
        }
    }
}
