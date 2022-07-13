namespace ONALLUSHI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MENUS_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nOUVEAUCHORISTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choristes n = new choristes();
            n.Dock = DockStyle.Fill;
            MENUS.Controls.Clear();
            MENUS.Controls.Add(n);
        }

        private void fERMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lISTEDESCHORISTESToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LISTECHORISTES n = new LISTECHORISTES();
            n.Dock = DockStyle.Fill;
            MENUS.Controls.Clear();
            MENUS.Controls.Add(n);
        }

        private void lAPRESENCEToolStripMenuItem_Click(object sender, EventArgs e)
        {

            presence n = new presence();
            n.Dock = DockStyle.Fill;
            MENUS.Controls.Clear();
            MENUS.Controls.Add(n);
        }
    }
}