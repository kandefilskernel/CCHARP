using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONALLUSHI
{
    public partial class Modifier : Form
    {
        public Modifier()
        {
            InitializeComponent();
        }
        public string Idetu { set { textBox1.Text = value; } }
        public string nometudiant { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string promotion { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string filiere { get { return textBox4.Text; } set { textBox4.Text = value; } }
        public string genre { get { return textBox5.Text; } set { textBox5.Text = value; } }
        public string tel { get { return textBox6.Text; } set { textBox6.Text = value; } }
        public string annne { get { return textBox7.Text; } set { textBox7.Text = value; } }
        public string CODEETUDIANT { get { return textBox8.Text; } set { textBox8.Text = value; } }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
