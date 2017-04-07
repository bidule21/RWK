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
    public partial class Eingabe : Form
    {
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;");
        public Eingabe()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                LoadComboLogged();
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

        private void LoadComboLogged()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM qrySchützenSortiert", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbSchuetze.DataSource = ds.Tables[0];
            cbSchuetze.DisplayMember = "wer";
            cbSchuetze.ValueMember = "ID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String sID = cbSchuetze.SelectedValue +"";
                String date = dateTimePicker1.Text;
                String erg30 = textBox1.Text;
                String erg15 = textBox2.Text;
                String fg10 = textBox3.Text;
                String fp10 = textBox4.Text;
                String my_querry = "INSERT INTO Ergebniss(SchützenID,Datum,Erg30,Erg15,FG10,FP10)VALUES('" +sID+ "','" +date+ "','" +erg30+ "','" +erg15+ "','" +fg10+ "','" +fp10+ "')";

                OleDbCommand cmd = new OleDbCommand(my_querry, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data saved successfuly...!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed due to" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
