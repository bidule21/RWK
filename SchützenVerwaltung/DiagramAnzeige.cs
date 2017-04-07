using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SchützenVerwaltung
{
    public partial class DiagramAnzeige : Form
    {
        private PrintDocument printDocument1 = new PrintDocument();
        public string d1 = "00.00.0000";
        public string d2 = "00.00.0000";
        public string u1 = "None";
        public string a1 = "All";
        public DiagramAnzeige()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void makeDiagramm(string sql, string user, string d1, string d2)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            da.SelectCommand.Parameters.Add("?", OleDbType.BigInt).Value =
                Convert.ToInt32(user);
            da.SelectCommand.Parameters.Add("?", OleDbType.DBDate).Value =
                Convert.ToDateTime(d1);
            da.SelectCommand.Parameters.Add("?", OleDbType.DBDate).Value =
                Convert.ToDateTime(d2);
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
            List<DateTime> Dlst = ds.Tables[0].AsEnumerable().Select(r => r.Field<DateTime>("Datum")).ToList();
            List<Int32> Ilst = ds.Tables[0].AsEnumerable().Select(r => r.Field<Int32>(0)).ToList();
            Dictionary<DateTime,Int32> values = new Dictionary<DateTime,int>();
            for(int a = 0; a < Ilst.Count; a++)
            {
                values.Add(Dlst[a],Ilst[a]);
            }
            chart1.Series.Clear();
            Series serie = new Series();
            serie.XValueType = ChartValueType.DateTime;
            serie.XValueMember = "Key";
            serie.YValueType = ChartValueType.Int32;
            serie.YValueMembers = "Value";
            chart1.Series.Add(serie);
            chart1.DataSource = values;
            chart1.GetToolTipText +=
            new EventHandler<ToolTipEventArgs>(chart1_GetToolTipText);
        }
        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                e.Text = "Datum: " + (DateTime.FromOADate(dp.XValue).ToShortDateString() +
                             "\r\nErgebniss: " + dp.YValues[0].ToString()+" Ringe");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog pdi = new PrintDialog();
            pdi.Document = pd;
            if (pdi.ShowDialog() == DialogResult.OK)
                pdi.Document.Print();
        }

        private void printDocument1_PrintPage(System.Object sender,
               System.Drawing.Printing.PrintPageEventArgs ev)
        {
            using (var f = new System.Drawing.Font("Arial", 10))
            {
                var size = ev.Graphics.MeasureString(Text, f);
                ev.Graphics.DrawString(getArt(a1)+" Ergebnisse\r\nSchütze :"+getWer(u1)+"\r\nZeitraum vom "+d1+" bis "+d2+"\r\nErtsellt am : "+DateTime.Now.ToShortDateString(), f, Brushes.Black, ev.PageBounds.X + (ev.PageBounds.Width - size.Width) / 2, ev.PageBounds.Y);
            }

            //Note, the chart printing code wants to print in pixels.
            Rectangle marginBounds = ev.MarginBounds;
            if (ev.Graphics.PageUnit != GraphicsUnit.Pixel)
            {
                ev.Graphics.PageUnit = GraphicsUnit.Pixel;
                marginBounds.X = (int)(marginBounds.X * (ev.Graphics.DpiX / 100f));
                marginBounds.Y = (int)(marginBounds.Y * (ev.Graphics.DpiY / 100f));
                marginBounds.Width = (int)(marginBounds.Width * (ev.Graphics.DpiX / 100f));
                marginBounds.Height = (int)(marginBounds.Height * (ev.Graphics.DpiY / 100f));
            }

            chart1.Printing.PrintPaint(ev.Graphics, marginBounds);
        }

        private string getArt(string value)
        {
            string erg ="none";
            switch (value)
            {
                case "0":
                    erg = "Luftgewehr 30 Schuss";
                    break;
                case "1":
                    erg = "Luftgewehr 15 Schuss";
                    break;
                case "2":
                    erg = "Freihand Luftgewehr 10 Schuss";
                    break;
                case "3":
                    erg = "Freihand Pistole 10 Schuss";
                    break;
                default:
                    erg = "Unbekannter Wettkampf";
                    break;
            }
            return erg;
        }

        private string getWer(string value)
        {
            string erg = "none";
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=RWKTraining.mdb;";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM qrySchützenSortiert WHERE ID="+value, conn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "wer");
            dt = ds.Tables["wer"];
            foreach(DataRow dr in dt.Rows){
                erg = dr["wer"].ToString();
            }
            return erg;
        }
    }
}
