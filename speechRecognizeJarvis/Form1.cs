using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace speechRecognizeJarvis
{
    internal class Form1 : Form
    {
        public string name;
        public string url;

        public Form1(string nam,string ul)
        {
            this.name = nam;
            this.url = ul;
            this.Text = "filmOnerisi";
            StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(300, 500);

            PictureBox pictureBox1 = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(300, 500),
                Location = new Point(0, 0),
            };
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(pictureBox1);

            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
            }
        }

        public void veriler(string n,string u)
        {
            name = n;
            url = u;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

    }
}