using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SchützenVerwaltung
{
    public class ErgebnissDB
    {
        private string filepath = Application.StartupPath + RWKEngine.Properties.Settings.Default.ErgDB;

        public void createXml()
        {
            XmlTextWriter xtw;
            xtw = new XmlTextWriter(filepath, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement(RWKEngine.Properties.Settings.Default.ErgDBLvL1);
            xtw.WriteEndElement();
            xtw.Close();
        }

        public void writeXml(Ergebniss erg)
        {

            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement(RWKEngine.Properties.Settings.Default.ErgDBLvL2);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL21, erg.SchNr);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL22, erg.Datum);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL23, erg.Erg30);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL24, erg.Erg15);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL25, erg.FHG10);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL26, erg.FHP10);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(filepath);
        }

        public List<Ergebniss> ReadXml()
        {
            List<Ergebniss> erg = new List<Ergebniss>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.ErgDBLvL2);
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.ErgDBLvL2)[i];
                erg.Add(new Ergebniss(cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL21), cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL22), cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL23), cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL24), cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL25), cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL26)));
            }
            rfile.Close();
            return erg;
        }

        public void RemoveXml(string SNR, string Date)
        {
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.ErgDBLvL2);
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.ErgDBLvL2)[i];
                if (cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL21) == SNR && cl.GetAttribute(RWKEngine.Properties.Settings.Default.ErgDBLvL22) == Date)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(filepath);
        }
    }
}
