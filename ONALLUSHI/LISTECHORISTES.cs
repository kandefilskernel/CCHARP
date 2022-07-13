using MySql.Data.MySqlClient;
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
    public partial class LISTECHORISTES : UserControl
    {
        public LISTECHORISTES()
        {
            InitializeComponent();
        }
        MySqlConnection cn;
        bool Connecté = false;
        public void connexion()
        {
           
        }
        public void RECHERCHES()
        {
            
                listView1.Items.Clear();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM choristes WHERE idchoriste=@id", cn);
                cmd.Parameters.AddWithValue("@id",textBox1.Text);
                using (MySqlDataReader lire = cmd.ExecuteReader())
                {
                    while (lire.Read())
                    {
                    string ID = lire["idchoriste"].ToString();
                    string adresse = lire["nomcomplet"].ToString();
                    string tel = lire["districtapostolique"].ToString();
                    string filiere = lire["districtancien"].ToString();
                    string genre = lire["sousdistrict"].ToString();
                    string tel1 = lire["voix"].ToString();
                    string anneac = lire["tel"].ToString();
                    string codeetudiant = lire["typechorale"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { ID, adresse, tel, filiere, genre, tel1, anneac, codeetudiant }));

                }
                }
               
            }
            public void affichage()
        {
            if (Connecté)
            {
                listView1.Items.Clear();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM choristes", cn);
                using (MySqlDataReader lire = cmd.ExecuteReader())
                {
                    while (lire.Read())
                    {
                        string ID = lire["idchoriste"].ToString();
                        string adresse = lire["nomcomplet"].ToString();
                        string tel = lire["districtapostolique"].ToString();
                        string filiere = lire["districtancien"].ToString();
                        string genre = lire["sousdistrict"].ToString();
                        string tel1 = lire["voix"].ToString();
                        string anneac = lire["tel"].ToString();
                        string codeetudiant = lire["typechorale"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { ID, adresse, tel, filiere, genre, tel1, anneac, codeetudiant }));

                    }
                }
            }
            else
            {
                MessageBox.Show("Vous n'etes pas connecté au serveur de la base de donnée");

            }

        }
        private void LISTECHORISTES_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.Columns.Add("ID Choriste",90);
            listView1.Columns.Add("Nom et Post nom du choriste",250);
            listView1.Columns.Add("Dsitrit Apostolique ", 190);
            listView1.Columns.Add("District D'ancien", 190);
            listView1.Columns.Add("Sous District", 190);
            listView1.Columns.Add("Voix", 100);
            listView1.Columns.Add("Tel", 150);
            listView1.Columns.Add("Type chorale",130);
            connexion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Se Connecter")
            {
                cn = new MySqlConnection("SERVER=127.0.0.1; PORT=3306; DATABASE=onal; UID=root; PWD=;");
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    button1.Text = "Se Déconnecter";
                    Connecté = true;
                    affichage();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("L'ERREUR EST ISTE" + ex);
                }

            }
            else
            {
                cn.Close();
                button1.Text = "Se Connecter";
                Connecté = false;
            }

        }

        private void supprimerChoristeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Connecté)
            {
                if (listView1.SelectedItems != null)
                {
                    var confirmation = MessageBox.Show("Voulez-vous vraiment supprimer?", "Suppression",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question
                       );
                    if (confirmation == DialogResult.Yes)
                    {
                        if (listView1.SelectedItems.Count > 0)
                        {
                            ListViewItem element = listView1.SelectedItems[0];
                            string ID = element.SubItems[0].Text;
                            MySqlCommand cmd = new MySqlCommand("DELETE  FROM choristes  WHERE idchoriste=@id", cn);
                            cmd.Parameters.AddWithValue("@id", ID);
                            cmd.ExecuteNonQuery();
                            element.Remove();
                            MessageBox.Show("Suppression reussie");
                        }
                    }
                }
            }
        }

        private void modfierDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string ID = element.SubItems[0].Text;
                string NOM = element.SubItems[1].Text;
                string ADRESSE = element.SubItems[2].Text;
                string TEL = element.SubItems[3].Text;
                string FILIERE = element.SubItems[4].Text;
                string TELEPHONE = element.SubItems[5].Text;
                string ANNEEACA = element.SubItems[6].Text;
                string CodeETUDIANT = element.SubItems[7].Text;
                using (Modifier kernel = new Modifier())
                {
                    kernel.Idetu = ID;
                    kernel.nometudiant = NOM;
                    kernel.promotion = ADRESSE;
                    kernel.filiere = TEL;
                    kernel.genre = FILIERE;
                    kernel.tel = TELEPHONE;
                    kernel.annne = ANNEEACA;
                    kernel.CODEETUDIANT = CodeETUDIANT;
                    if (kernel.ShowDialog() == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE choristes SET nomcomplet=@adresse,districtapostolique=@tel,districtancien=@filiere,sousdistrict=@genre,voix=@telephone,tel=@anneeaca,	typechorale=@CODEETUDIANT  WHERE idchoriste=@id", cn);

                        cmd.Parameters.AddWithValue("@adresse", kernel.nometudiant);
                        cmd.Parameters.AddWithValue("@tel", kernel.promotion);
                        cmd.Parameters.AddWithValue("@filiere", kernel.filiere);
                        cmd.Parameters.AddWithValue("@genre", kernel.genre);
                        cmd.Parameters.AddWithValue("@telephone", kernel.tel);
                        cmd.Parameters.AddWithValue("@anneeaca", kernel.annne);
                        cmd.Parameters.AddWithValue("@CODEETUDIANT", kernel.CODEETUDIANT);
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.ExecuteNonQuery();
                        element.SubItems[1].Text = kernel.nometudiant;
                        element.SubItems[2].Text = kernel.promotion;
                        element.SubItems[3].Text = kernel.filiere;
                        element.SubItems[4].Text = kernel.genre;
                        element.SubItems[5].Text = kernel.tel;
                        element.SubItems[6].Text = kernel.annne;
                        element.SubItems[7].Text = kernel.CODEETUDIANT;

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RECHERCHES();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            affichage();
        }
    }
}
