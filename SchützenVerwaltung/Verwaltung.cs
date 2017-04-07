using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchützenVerwaltung
{
    public partial class Verwaltung : Form
    {
        public Verwaltung()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SchützenVerwaltung.SchuetzenVerwaltung frm1 = new SchützenVerwaltung.SchuetzenVerwaltung();
            frm1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ErgebnisseAnzeigen frm2 = new ErgebnisseAnzeigen();
            frm2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Eingabe frm = new Eingabe();
            frm.Show();
        }
    }
}
