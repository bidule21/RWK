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
        private string filepath = Application.StartupPath + RWKEngine.Properties.Settings.Default.SNDB;

        public void createXml()
        {
            XmlTextWriter xtw;
            xtw = new XmlTextWriter(filepath, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement(RWKEngine.Properties.Settings.Default.SNDBLvL1);
            xtw.WriteEndElement();
            xtw.Close(); 
        }

        public void writeXml(Schuetze s)
        {

            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement(RWKEngine.Properties.Settings.Default.SNDBLvL2);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL22, s.Nname);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL23, s.Vname);
            cl.SetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL21, s.SchNr);
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
            XmlNodeList list = xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2);
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2)[i];
                erg.Add(new Schuetze(cl.GetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL22), cl.GetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL23), cl.GetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL21)));
            }
            rfile.Close();
            return erg;
        }

        public void UpdateXml(string key, string SNr, string newValue)
        {
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(filepath, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2);
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2)[i];
                if (cu.GetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL21) == SNr)
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
            XmlNodeList list = tdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2);
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName(RWKEngine.Properties.Settings.Default.SNDBLvL2)[i];
                if (cl.GetAttribute(RWKEngine.Properties.Settings.Default.SNDBLvL21) == SNR)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(filepath);
        }
    }
}
