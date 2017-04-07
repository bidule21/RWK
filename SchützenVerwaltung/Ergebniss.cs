using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchützenVerwaltung
{
    public class Ergebniss
    {
        private string _SchNr;
        private string _Datum;
        private string _Erg30;
        private string _Erg15;
        private string _FHG10;
        private string _FHP10;

        public string SchNr
        {
            get { return _SchNr; }
            set { this._SchNr = value; }
        }
        public string Datum
        {
            get { return _Datum; }
            set { this._Datum = value; }
        }
        public string Erg30
        {
            get { return _Erg30; }
            set { this._Erg30 = value; }
        }
        public string Erg15
        {
            get { return _Erg15; }
            set { this._Erg15 = value; }
        }
        public string FHG10
        {
            get { return _FHG10; }
            set { this._FHG10 = value; }
        }
        public string FHP10
        {
            get { return _FHP10; }
            set { this._FHP10 = value; }
        }

        public Ergebniss(string SN = "000000", string DA = "00.00.0000", string E30 = "0", string E15 = "0", string FHG = "0", string FHP ="0")
        {
            this._SchNr = SN;
            this._Datum = DA;
            this._Erg30 = E30;
            this._Erg15 = E15;
            this._FHG10 = FHG;
            this._FHP10 = FHP;
        }
    }
}
