using System;
using System.IO;
using System.Windows.Forms;

namespace Notus_Image_Watermarker_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateWatermarkImage()
        {
            Notus.Image.Watermarker.Watermarker.ProtectionLevel protectionLevel = (Notus.Image.Watermarker.Watermarker.ProtectionLevel)comboBox1.SelectedIndex;
            bool isLight = false;
            bool isJpeg = false;
            if (comboBox2.SelectedIndex == 0)
                isLight = true;
            else
                isLight = false;

            if (Path.GetExtension(openFileDialog1.FileName) == ".jpg")
                isJpeg = false;
            else
                isJpeg = true;

            pictureBox2.Image = null;
            pictureBox2.Refresh();

            try
            {
                bool tmpResult = Notus.Image.Watermarker.Watermarker.AddWatermarkToImage(openFileDialog1.FileName, "tmp" + Path.GetExtension(openFileDialog1.FileName), "testwalletkey", protectionLevel, isJpeg, isLight);
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox2.ImageLocation = "tmp" + Path.GetExtension(openFileDialog1.FileName);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Dosyası (*.jpg)|*.jpg|PNG Dosyası (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CreateWatermarkImage();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBoxChangedIndex(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                CreateWatermarkImage();
            }
        }
    }
}
