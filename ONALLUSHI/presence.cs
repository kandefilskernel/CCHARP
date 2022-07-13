using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using com.google.zxing.qrcode;
using MySql.Data.MySqlClient;
using QRCoder;


namespace ONALLUSHI
{
    public partial class presence : UserControl
    {
        public presence()
        {
            InitializeComponent();
        }
        bool isGenered = false;
        string infomationchoriste;
        FilterInfoCollection filterinfocollection;
        VideoCaptureDevice captureDevice;
        MySqlConnection cn;
        bool Connecté = false;
        string KANDE, KANDE1;

        private void presence_Load(object sender, EventArgs e)
        {
            filterinfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterinfocollection)
                comboBox5.Items.Add(filterInfo.Name);
                comboBox5.SelectedIndex = 0;
        }
        private void CaptureDevise_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            captureDevice = new VideoCaptureDevice(filterinfocollection[comboBox5.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevise_NewFrame;
            captureDevice.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                QRCode qRCo = new QRCode();
                //BarcodeReader barcodeReader = new BarcodeReader();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
