using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Visualizador_de_imagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_load;
        }

        Panel Panel = new Panel();
        TextBox TextBox = new TextBox();

        private void Form1_load(object sender, EventArgs e)
        {
            this.Text = "Visualizador de Imagens";
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.WindowState = FormWindowState.Maximized;
            this.Height = 600;
            Panel.Top = 50;
            Panel.Width = Screen.PrimaryScreen.Bounds.Width;
            Panel.Height = 700;
            Panel.BackColor = Color.White;
            Panel.AutoScroll = true;
            Panel.Dock = DockStyle.Bottom;
            TextBox.Width = 300;
            TextBox.Top = 10;
            TextBox.Left = (Screen.PrimaryScreen.Bounds.Width / 2) - 150;
            TextBox.Font = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
            TextBox.TextChanged += TextBox_textChanged;
            this.Controls.Add(Panel);
            this.Controls.Add(TextBox);
        }

        PictureBox[] I = new PictureBox[100];
        Panel[] Panels = new Panel[100];
        int j = 0, left = 5, top = 5;

        private void TextBox_textChanged(object sender, EventArgs e)
        {
            Panel.Controls.Clear();
            Top = 5;
            Left = 5;
            string[] s = Directory.GetFiles(TextBox.Text);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(".png") || s[i].Contains(".jpg"))
                {
                    I[j] = new PictureBox();
                    Bitmap bitmap = new Bitmap(s[i]);
                    I[j].Width = bitmap.Width / 5;
                    I[j].Height = bitmap.Height / 5;
                    Panels[j] = new Panel();
                    Panels[j].Width = (bitmap.Width / 5) + 4;
                    Panels[j].Height = (bitmap.Height / 5) + 4;
                    Panels[j].Controls.Add(I[j]);
                    Panels[j].Left = left;
                    Panels[j].Top = top;
                    I[j].SizeMode = PictureBoxSizeMode.StretchImage;
                    I[j].MouseHover += Form1_MouseHover;
                    I[j].MouseLeave += Form1_MouseLeave;
                    I[j].Name = j + "";
                    I[j].Left = 2;
                    I[j].Top = 2;
                    I[j].BackColor = Color.White;
                    I[j].Load(s[i]);
                    left += I[j].Width + 5;
                    int w = I[0].Height;
                    for (int k = 1; k < j; k++)
                    {
                        if (I[k].Height > w) w = I[k].Height;
                    }
                    if (left >= Panel.Width)
                    {
                        top += w + 5;
                        left = 5;
                    }
                    Panel.Controls.Add(Panels[j]);
                    j++;
                }
            }
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox p = sender as PictureBox;
                Panels[Convert.ToInt32(p.Name)].BackColor = Color.White;
            }
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox p = sender as PictureBox;
                Panels[Convert.ToInt32(p.Name)].BackColor = Color.White;
            }
        }
    }
}
