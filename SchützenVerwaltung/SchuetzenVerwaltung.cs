using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchützenVerwaltung
{
    public partial class SchuetzenVerwaltung : Form
    {
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;");
        public SchuetzenVerwaltung()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                showSchuetzenTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        private void showSchuetzenTable()
        {
            OleDbDataAdapter da = new OleDbDataAdapter(
                "SELECT SchützenNR, Name, Vorname FROM Schützen", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textBox1.DataBindings.Add("Text", bindingSource1, "SchützenNR");
            textBox2.DataBindings.Add("Text", bindingSource1, "Vorname");
            textBox3.DataBindings.Add("Text", bindingSource1, "Name");
            bindingSource1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
