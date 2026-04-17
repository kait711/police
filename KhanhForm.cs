using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class KhanhForm : Form
    {
        public KhanhForm()
        {
            this.Text = "Trang Khánh";
            this.Size = new Size(700, 500);
            this.BackColor = Color.FromArgb(5, 20, 30);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var canvas = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            canvas.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = canvas.ClientRectangle;

                using (var br = new LinearGradientBrush(rect,
                    Color.FromArgb(5, 25, 40), Color.FromArgb(5, 10, 20),
                    LinearGradientMode.ForwardDiagonal))
                    g.FillRectangle(br, rect);

                // Accent
                using (var pen = new Pen(Color.FromArgb(60, 100, 200, 255), 2))
                    g.DrawEllipse(pen, 450, -100, 350, 350);
                using (var pen2 = new Pen(Color.FromArgb(25, 100, 200, 255), 1))
                    g.DrawRectangle(pen2, 410, 200, 250, 200);

                // Emoji
                using (var ef = new Font("Segoe UI Emoji", 56))
                    g.DrawString("🌟", ef, Brushes.White, 40, 60);

                // Name
                using (var nf = new Font("Segoe UI", 38, FontStyle.Bold))
                using (var nb = new SolidBrush(Color.FromArgb(100, 200, 255)))
                    g.DrawString("KHÁNH", nf, nb, 145, 75);

                // Divider
                using (var p = new Pen(Color.FromArgb(100, 200, 255), 3))
                    g.DrawLine(p, 40, 175, 360, 175);

                DrawInfo(g, "📌  Vai trò:", "Thành viên nhóm", 40, 210);
                DrawInfo(g, "⚡  Sở thích:", "Âm nhạc, thiết kế", 40, 260);
                DrawInfo(g, "💬  Châm ngôn:", "\"Sáng tạo không có giới hạn\"", 40, 310);
                DrawInfo(g, "🎂  Năm sinh:", "2005", 40, 360);
            };

            var btnBack = new Button
            {
                Text = "← Quay lại",
                Size = new Size(120, 40),
                Location = new Point(40, 420),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(100, 200, 255),
                ForeColor = Color.FromArgb(5, 20, 35),
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
            using (var lb = new SolidBrush(Color.FromArgb(150, 200, 230)))
                g.DrawString(label, lf, lb, x, y);

            using (var vf = new Font("Segoe UI", 10))
            using (var vb = new SolidBrush(Color.White))
                g.DrawString(value, vf, vb, x + 155, y);
        }
    }
}
