
namespace SchützenVerwaltung
{
    public class Schuetze
    {
        #region Private Members
        private string _nname;
        private string _vname;
        private string _schnr;
        #endregion
        #region Public Members
        public string Nname
        {
            get{return this._nname;}
            set{this._nname = value;}
        }
        public string Vname
        {
            get { return this._vname; }
            set { this._vname = value; }
        }
        public string SchNr
        {
            get { return this._schnr; }
            set { this._schnr = value; }
        }
        #endregion
        #region Constructor
        public Schuetze(string NN = "none", string VN = "none", string SN = "000000")
        {
            this._nname = NN;
            this._vname = VN;
            this._schnr = SN;
        }
        #endregion
    }
}
