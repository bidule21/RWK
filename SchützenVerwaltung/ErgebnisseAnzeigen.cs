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
    public partial class ErgebnisseAnzeigen : Form
    {
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;");
        private string sql;

        public ErgebnisseAnzeigen()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDiziplin();
        }

        private void LoadComboLogged()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM qrySchützenSortiert", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //ds.WriteXml("Customers.xml", XmlWriteMode.WriteSchema); 
            cbSchuetze.DataSource = ds.Tables[0];
            cbSchuetze.DisplayMember = "wer";
            cbSchuetze.ValueMember = "ID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbDataAdapter da = new OleDbDataAdapter(this.sql, conn);
            da.SelectCommand.Parameters.Add("?",OleDbType.BigInt).Value =
                Convert.ToInt32(cbSchuetze.SelectedValue);
            da.SelectCommand.Parameters.Add("?", OleDbType.DBDate).Value =
                Convert.ToDateTime(dateTimePicker1.Text);
            da.SelectCommand.Parameters.Add("?", OleDbType.DBDate).Value =
                Convert.ToDateTime(dateTimePicker2.Text);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds, "Abfrage");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Abfrage";
            if (comboBox1.SelectedIndex <= 3 && comboBox1.SelectedIndex >= 0)
            {
                int zwg = 0;
                int count = 0;
                int b = 0;
                int s = 300;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        zwg += System.Convert.ToInt32(dr[0]);
                        if (System.Convert.ToInt32(dr[0]) != 0)
                            count++;
                        if (System.Convert.ToInt32(dr[0]) > b)
                            b = System.Convert.ToInt32(dr[0]);
                        if (System.Convert.ToInt32(dr[0]) != 0 && System.Convert.ToInt32(dr[0]) < s)
                            s = System.Convert.ToInt32(dr[0]);
                    }
                }

                if (count != 0 && zwg != 0)
                    zwg = zwg / count;

                lbAverage.Text = "Mittelwert der Ergebnisse : \r\n" + zwg + "Ringe  aus " + count + " Ergebnissen\r\n";
                lbAverage.Text += "Beste Ergebniss : " + b + " Ringe \r\n";
                lbAverage.Text += "Schlechteste Ergebniss : " + s + " Ringe \r\n";
            }
            else
            {
                lbAverage.Text = "Mittwelerte für den Zeitraum : \r\n";
                int b1 = 0;
                int b2 = 0;
                int b3 = 0;
                int b4 = 0;
                int s1 = 300;
                int s2 = 150;
                int s3 = 100;
                int s4 = 110;
                int zwg1 = 0;
                int zwg2 = 0;
                int zwg3 = 0;
                int zwg4 = 0;
                int count1 = 0;
                int count2 = 0;
                int count3 = 0;
                int count4 = 0;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        zwg1 += System.Convert.ToInt32(dr[0]);
                        if (System.Convert.ToInt32(dr[0]) != 0)
                            count1++;
                        zwg2 += System.Convert.ToInt32(dr[1]);
                        if (System.Convert.ToInt32(dr[1]) != 0)
                            count2++;
                        zwg3 += System.Convert.ToInt32(dr[2]);
                        if (System.Convert.ToInt32(dr[2]) != 0)
                            count3++;
                        zwg4 += System.Convert.ToInt32(dr[3]);
                        if (System.Convert.ToInt32(dr[3]) != 0)
                            count4++;
                        if (System.Convert.ToInt32(dr[0]) > b1)
                            b1 = System.Convert.ToInt32(dr[0]);
                        if (System.Convert.ToInt32(dr[1]) > b2)
                            b2 = System.Convert.ToInt32(dr[1]);
                        if (System.Convert.ToInt32(dr[2]) > b3)
                            b3 = System.Convert.ToInt32(dr[2]);
                        if (System.Convert.ToInt32(dr[3]) > b4)
                            b4 = System.Convert.ToInt32(dr[3]);
                        if (System.Convert.ToInt32(dr[0]) != 0 && System.Convert.ToInt32(dr[0]) < s1)
                            s1 = System.Convert.ToInt32(dr[0]);
                        if (System.Convert.ToInt32(dr[1]) != 0 && System.Convert.ToInt32(dr[1]) < s2)
                            s2 = System.Convert.ToInt32(dr[1]); 
                        if (System.Convert.ToInt32(dr[2]) != 0 && System.Convert.ToInt32(dr[2]) < s3)
                            s3 = System.Convert.ToInt32(dr[2]);
                        if (System.Convert.ToInt32(dr[3]) != 0 && System.Convert.ToInt32(dr[3]) < s4)
                            s4 = System.Convert.ToInt32(dr[3]);
                    }
                }

                if (count1 != 0 && zwg1 != 0)
                    lbAverage.Text += zwg1 / count1 + " Ringe bei " + count1 + " Ergebnissen LG 30 \r\n";
                if (count2 != 0 && zwg2 != 0)
                    lbAverage.Text += zwg2 / count2 + " Ringe bei " + count2 + " Ergebnissen LG 15 \r\n";
                if (count3 != 0 && zwg3 != 0)
                    lbAverage.Text += zwg3 / count3 + " Ringe bei " + count3 + " Ergebnissen FG 10 \r\n";
                if (count4 != 0 && zwg4 != 0)
                    lbAverage.Text  += zwg4/count4 + " Ringe bei "+count4+" Ergebnissen FP 10 \r\n \r\n Beste und Schlechteste Ergebnisse\r\n";
                lbAverage.Text += "Beste LG30 Ergebniss : " + b1 + " Ringe \r\n";
                lbAverage.Text += "Schlechteste LG30 Ergebniss : " + s1 + " Ringe \r\n";
                lbAverage.Text += "Beste LG15 Ergebniss : " + b2 + " Ringe \r\n";
                lbAverage.Text += "Schlechteste LG15 Ergebniss : " + s2 + " Ringe \r\n";
                lbAverage.Text += "Beste FH10 Ergebniss : " + b3 + " Ringe \r\n";
                lbAverage.Text += "Schlechteste FH10 Ergebniss : " + s3 + " Ringe \r\n";
                lbAverage.Text += "Beste FP10 Ergebniss : " + b4 + " Ringe \r\n";
                lbAverage.Text += "Schlechteste FP10 Ergebniss : " + s4 + " Ringe \r\n";
            }
        }

        private void ChangeDiziplin()
        {
            this.sql = "SELECT Erg30, Erg15, FG10, FP10, Datum FROM Ergebniss " +
                "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.sql = "SELECT Erg30, Datum FROM Ergebniss " +
                        "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
                    break;
                case 1:
                    this.sql = "SELECT Erg15, Datum FROM Ergebniss " +
                        "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
                    break;
                case 2:
                    this.sql = "SELECT FG10, Datum FROM Ergebniss " +
                        "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
                    break;
                case 3:
                    this.sql = "SELECT FP10, Datum FROM Ergebniss " +
                        "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
                    break;
                default:
                    this.sql = "SELECT Erg30, Erg15, FG10, FP10 FROM Ergebniss " +
                        "WHERE SchützenID = ? AND Datum BETWEEN ? AND ?";
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DiagramAnzeige frm = new DiagramAnzeige();
            frm.d1 = dateTimePicker1.Text;
            frm.d2 = dateTimePicker2.Text;
            frm.u1 = cbSchuetze.SelectedValue + "";
            frm.a1 = comboBox1.SelectedIndex + "";
            frm.makeDiagramm(this.sql, cbSchuetze.SelectedValue+"", dateTimePicker1.Text, dateTimePicker2.Text);         
            frm.Show();
        }
    }
}
