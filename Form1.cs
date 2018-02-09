using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Externalisation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 tf = new Form2();
            tf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Voulez-vous afficher une image ? ", "Avertissement !", MessageBoxButtons.YesNo);
            if (DialogResult == System.Windows.Forms.DialogResult.Yes) // Si c'est oui 
            {
                pictureBox1.Visible = true;
                MessageBox.Show("Félicitations !!!!");

            }

        }
    }
}
