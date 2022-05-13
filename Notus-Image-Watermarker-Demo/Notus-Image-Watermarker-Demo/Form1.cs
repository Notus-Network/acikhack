using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notus_Image_Watermarker_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Dosyası (*.jpg)|*.jpg|PNG Dosyası (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Notus.Image.Watermarker.Watermarker.ProtectionLevel protectionLevel = (Notus.Image.Watermarker.Watermarker.ProtectionLevel)comboBox1.SelectedIndex;
                bool isLight = false;
                if (comboBox2.SelectedIndex == 0)
                    isLight = true;
                else
                    isLight = false;

                try
                {
                    Notus.Image.Watermarker.Watermarker.AddWatermarkToImage(openFileDialog1.FileName, "tmp" + Path.GetExtension(openFileDialog1.FileName), "testwalletkey", protectionLevel, isLight);
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    pictureBox2.Image = Image.FromFile("tmp" + Path.GetExtension(openFileDialog1.FileName));
                } catch { }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
    }
}
