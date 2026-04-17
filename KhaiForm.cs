using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class KhaiForm : Form
    {
        public KhaiForm()
        {
            this.Text = "Trang Khải";
            this.Size = new Size(700, 500);
            this.BackColor = Color.FromArgb(5, 20, 15);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var canvas = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            canvas.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = canvas.ClientRectangle;

                using (var br = new LinearGradientBrush(rect,
                    Color.FromArgb(5, 30, 20), Color.FromArgb(5, 15, 10),
                    LinearGradientMode.ForwardDiagonal))
                    g.FillRectangle(br, rect);

                // Accent
                using (var pen = new Pen(Color.FromArgb(50, 100, 230, 160), 2))
                    g.DrawEllipse(pen, 470, -60, 280, 280);

                Point[] tri = { new Point(550, 350), new Point(650, 450), new Point(450, 450) };
                using (var pen2 = new Pen(Color.FromArgb(25, 100, 230, 160), 1))
                    g.DrawPolygon(pen2, tri);

                // Emoji
                using (var ef = new Font("Segoe UI Emoji", 56))
                    g.DrawString("🔥", ef, Brushes.White, 40, 60);

                // Name
                using (var nf = new Font("Segoe UI", 38, FontStyle.Bold))
                using (var nb = new SolidBrush(Color.FromArgb(100, 230, 160)))
                    g.DrawString("KHẢI", nf, nb, 145, 75);

                // Divider
                using (var p = new Pen(Color.FromArgb(100, 230, 160), 3))
                    g.DrawLine(p, 40, 175, 320, 175);

                DrawInfo(g, "📌  Vai trò:", "Thành viên nhóm", 40, 210);
                DrawInfo(g, "⚡  Sở thích:", "Thể thao, công nghệ", 40, 260);
                DrawInfo(g, "💬  Châm ngôn:", "\"Nỗ lực không bao giờ phản bội\"", 40, 310);
                DrawInfo(g, "🎂  Năm sinh:", "2005", 40, 360);
            };

            var btnBack = new Button
            {
                Text = "← Quay lại",
                Size = new Size(120, 40),
                Location = new Point(40, 420),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(100, 230, 160),
                ForeColor = Color.FromArgb(5, 25, 15),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => this.Close();

            this.Controls.Add(canvas);
            canvas.Controls.Add(btnBack);
        }

        private void DrawInfo(Graphics g, string label, string value, int x, int y)
        {
            using (var lf = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var lb = new SolidBrush(Color.FromArgb(150, 210, 190)))
                g.DrawString(label, lf, lb, x, y);

            using (var vf = new Font("Segoe UI", 10))
            using (var vb = new SolidBrush(Color.White))
                g.DrawString(value, vf, vb, x + 155, y);
        }
    }
}
