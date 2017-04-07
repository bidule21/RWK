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
        private string filepath = Application.StartupPath + "Ergebniss.xml";

        public void createXml()
        {
            XmlTextWriter xtw;
            xtw = new XmlTextWriter(filepath, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Ergebnisse");
            xtw.WriteEndElement();
            xtw.Close();
        }

        public void writeXml(Ergebniss erg)
        {

            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Ergebniss");
            cl.SetAttribute("SNr", erg.SchNr);
            cl.SetAttribute("Dat", erg.Datum);
            cl.SetAttribute("E30", erg.Erg30);
            cl.SetAttribute("E15", erg.Erg15);
            cl.SetAttribute("FHG", erg.FHG10);
            cl.SetAttribute("FHP", erg.FHP10);
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
            XmlNodeList list = xdoc.GetElementsByTagName("Ergebniss");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Ergebniss")[i];
                erg.Add(new Ergebniss(cl.GetAttribute("SNr"), cl.GetAttribute("Dat"), cl.GetAttribute("E30"), cl.GetAttribute("E15"), cl.GetAttribute("FHG"), cl.GetAttribute("FHP")));
            }
            rfile.Close();
            return erg;
        }

        public void UpdateXml(string key, string Value, string newValue)
        {
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(filepath, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Ergebniss");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Ergebniss")[i];
                if (cu.GetAttribute(key) == Value)
                {
                    cu.SetAttribute(key, newValue);
                }
            }
            up.Close();
            xdoc.Save(filepath);
        }

        public void RemoveXml(string SNR, string Date)
        {
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName("Ergebniss");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName("Ergebniss")[i];
                if (cl.GetAttribute("SNr") == SNR && cl.GetAttribute("Dat") == Date)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(filepath);
        }
    }
}
