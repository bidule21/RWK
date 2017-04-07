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
    public class SchuetzenDB
    {
        private string filepath = Application.StartupPath + "Schuetzen.xml";

        public void createXml()
        {
            XmlTextWriter xtw;
            xtw = new XmlTextWriter(filepath, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Schuetzen");
            xtw.WriteEndElement();
            xtw.Close(); 
        }

        public void writeXml(Schuetze s)
        {

            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Schuetze");
            cl.SetAttribute("Nname", s.Nname);
            cl.SetAttribute("Vname", s.Vname);
            cl.SetAttribute("SchNr", s.SchNr);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(filepath);
        }

        public List<Schuetze> ReadXml() 
        {
            List<Schuetze> erg = new List<Schuetze>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName("Schuetze");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Schuetze")[i];
                erg.Add(new Schuetze(cl.GetAttribute("Nname"), cl.GetAttribute("Vname"), cl.GetAttribute("SchNr")));
            }
            rfile.Close();
            return erg;
        }

        public void UpdateXml(string key, string Value, string newValue)
        {
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(filepath, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Schuetze");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Schuetze")[i];
                if (cu.GetAttribute(key) == Value)
                {
                    cu.SetAttribute(key, newValue);
                }
            }
            up.Close();
            xdoc.Save(filepath);
        }

        public void RemoveXml(string SNR)
        {
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName("Schuetze");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName("Schuetze")[i];
                if (cl.GetAttribute("SchNr") == SNR)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(filepath);
        }
    }
}
