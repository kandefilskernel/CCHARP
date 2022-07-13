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
using QRCoder;
using IronBarCode;
using System.Drawing.Imaging;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;

namespace ONALLUSHI
{
    public partial class choristes : UserControl
    {
        public choristes()
        {
            InitializeComponent();
        }
        bool isGenered = false;
        string infomationchoriste;
        private FilterInfoCollection filterinfocollection;
        private VideoCaptureDevice captureDevice;
        MySqlConnection cn;
        bool Connecté = false;
        string KANDE, KANDE1;

        void getalcamera()
        {
            filterinfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterinfocollection)
            comboBox5.Items.Add(filterInfo.Name);
            comboBox5.SelectedIndex = 0;
        }
        private void choristes_Load(object sender, EventArgs e)
        {
            comboBox4.Items.Add("ALTO");
            comboBox4.Items.Add("BASSE");
            comboBox4.Items.Add("TENOR");
            comboBox4.Items.Add("SOPRANO");
            comboBox1.Items.Add("District Apostolique L'SHI NORD");
            comboBox1.Items.Add("District Apostolique L'SHI SUD");
            filterinfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterinfocollection)
                comboBox5.Items.Add(filterInfo.Name);
                 comboBox5.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text== "District Apostolique L'SHI NORD") 
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Nouveau Bel air");
                comboBox2.Items.Add("Kawama");
                comboBox2.Items.Add("Kigoma");
                comboBox2.Items.Add("Lwano");
            }
            if (comboBox1.Text == "District Apostolique L'SHI SUD")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Nouveau Kassapa");
                comboBox2.Items.Add("Kamalondo");
                comboBox2.Items.Add("Katuba");
                comboBox2.Items.Add("Makumbi");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Nouveau Kassapa")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("KABULAMESHI");
                comboBox3.Items.Add("KASSAPA");
                comboBox3.Items.Add("BOIT FORT");
        
            }
            if (comboBox2.Text == "Nouveau Bel air")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("NEEMA");
                comboBox3.Items.Add("BEL AIR II");
                comboBox3.Items.Add("IMANI");

            }
            if (comboBox2.Text == "Kamalondo")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("KAMALONDO");
                comboBox3.Items.Add("KAMPEMBA");
        

            }
            if (comboBox2.Text == "Kigoma")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Kigoma");
            }
            if (comboBox2.Text == "Kawama")
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Kawama");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             
            //timer1.Start();
        
        }

        private void CaptureDevise_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Entrez le nom");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Entrez le Numero Télephone");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Entrez le Numero Id du chorite");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Selectionnez le district d'Ancien");
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("Selectionnez le sous district");
            }
            else if (comboBox4.Text == "")
            {
                MessageBox.Show("Selectionnez la voix");
            }
            else
            {
                if (Connecté)
                {

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO choristes(idchoriste,nomcomplet,districtapostolique,districtancien,sousdistrict,voix,tel,typechorale) VALUES(@nom1,@nom,@promotion,@filiere,@genre,@tel,@anneaca,@type)", cn);
                    cmd.Parameters.AddWithValue("@nom1", textBox3.Text);
                    cmd.Parameters.AddWithValue("@nom", textBox1.Text);
                    cmd.Parameters.AddWithValue("@promotion", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@filiere", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@genre", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@tel", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@anneaca", textBox2.Text);
                    cmd.Parameters.AddWithValue("@type", KANDE);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    MessageBox.Show("Choriste Enregistré!!");

                    string path = @"C:\Users\FilsKernel\Pictures";
                    var dialog = new SaveFileDialog();
                    dialog.InitialDirectory = path;
                    dialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image.Save(dialog.FileName);

                    }

                    //pictureBox1.Image.Save(path + "" + DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString() + ".jpg",
                    //ImageFormat.Jpeg);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox1.Focus();
                    this.pictureBox1.Image = null;
                    this.pictureBox3.Image = null;
                }
                else
                {
                    MessageBox.Show("Vous n'etes pas connecté au serveur de la base de donnée");
                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (button4.Text == "Se Connecter")
            {
                cn = new MySqlConnection("SERVER=127.0.0.1; PORT=3306; DATABASE=onal; UID=root; PWD=;");
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    button4.Text = "Se Déconnecter";
                    Connecté = true;
                    //affichage();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("l'erreur" + ex);
                }

            }
            else
            {
                cn.Close();
                button4.Text = "Se Connecter";
                Connecté = false;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                KANDE = "ONAL";
            }
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                KANDE = "CHORALE ORDINAIRE";
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                KANDE = "ORCHESTRE";
            }
        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.CodeQrBarcodeDraw qrBarcodeDraw = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox1.Image = qrBarcodeDraw.Draw(textBox3.Text, 150);
        }

        private void textBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            isGenered = true;
            infomationchoriste = textBox1.Text + "/" + textBox2.Text;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.CodeQrBarcodeDraw qrBarcodeDraw = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox1.Image = qrBarcodeDraw.Draw(textBox3.Text, 150);
            checkBox1.Checked = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                captureDevice = new VideoCaptureDevice(filterinfocollection[comboBox5.SelectedIndex].MonikerString);
                captureDevice.NewFrame +=new NewFrameEventHandler(NewvideoFrame);
                captureDevice.Start();
                button1.Visible = false;
                button2.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void NewvideoFrame(Object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();

        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog KERNEL = new SaveFileDialog();
            KERNEL.Title = "Save la photo";
            KERNEL.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif";
            ImageFormat imageFormat = ImageFormat.Png;
            if (KERNEL.ShowDialog() == DialogResult.OK)
            {
                string fils = System.IO.Path.GetExtension(KERNEL.FileName);
                switch (fils)
                {
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp ;
                        break;


                }
                pictureBox2.Image.Save(KERNEL.FileName, imageFormat);
                button5.Visible = true;
                button2.Visible = false;
                

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }
    }
}
