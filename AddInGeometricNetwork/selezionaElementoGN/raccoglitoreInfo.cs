using System;


namespace AddInGeometricNetwork.selezionaElementoGN.raccoglitore
{
    public class raccoglitoreInfoSimpleEdge_Event : System.EventArgs
    {
        public string _OID = String.Empty;
        public string _FID = String.Empty;
        public string _EID = String.Empty;
        public string _OID_JunctionInziale = String.Empty;
        public string _OID_JunctionFinale = String.Empty;

        public raccoglitoreInfoSimpleEdge_Event(string OID, string FID, string EID, string OID_JunctionInziale, string OID_JunctionFinale)
        {
            this._OID = OID;
            this._FID = FID;
            this._EID = EID;
            this._OID_JunctionInziale = OID_JunctionInziale;
            this._OID_JunctionFinale = OID_JunctionFinale;
        }
    }

    public class RaccoglitoreInfoComplexEdge_Event : System.EventArgs
    {
        public string _OID = String.Empty;
        public string _FID = String.Empty;
        public string _EID = String.Empty;
        public string _SUBID = String.Empty;
        public string _OID_JunctionInziale = String.Empty;
        public string _OID_JunctionFinale = String.Empty;

        public RaccoglitoreInfoComplexEdge_Event(string OID, string FID, string EID, string SUBID, string OID_JunctionInziale, string OID_JunctionFinale)
        {
            this._OID = OID;
            this._FID = FID;
            this._EID = EID;
            this._SUBID = SUBID;
            this._OID_JunctionInziale = OID_JunctionInziale;
            this._OID_JunctionFinale = OID_JunctionFinale;
        }
    }

    public class RaccoglitoreInfoSimpleJunction_Event : System.EventArgs
    {
        public string _OID = String.Empty;
        public string _FID = String.Empty;
        public string _EID = String.Empty;
        public string _X = String.Empty;
        public string _Y = String.Empty;
        public string _longi = String.Empty;
        public string _lati = String.Empty;

        public RaccoglitoreInfoSimpleJunction_Event(string OID, string FID, string EID, string X, string Y, string longi, string lati)
        {
            this._OID = OID;
            this._FID = FID;
            this._EID = EID;
            this._X = X;
            this._Y = Y;
            this._longi = longi;
            this._lati = lati;
        }
    }

    public class RaccogliInfoPerListView
    {
        private static RaccogliInfoPerListView _istanza = null;

        public event EventHandler InfoAggiornateSimpleEdge;
        public event EventHandler InfoAggiornateComplexEdge;
        public event EventHandler InfoAggiornateSimpleJunction;

        private RaccogliInfoPerListView()
        {
        }

        public static RaccogliInfoPerListView Instance()
        {
            if (_istanza == null)
            {
                _istanza = new RaccogliInfoPerListView();
            }

            return _istanza;
        }

        public void AggiungiInfoSimpleEdge(long OID, long FID, long EID, long OID_JunctionIniziale, long OID_JunctionFinale)
        {
            // Raise un evento di update
            InfoAggiornateSimpleEdge?.Invoke(this,
                new raccoglitoreInfoSimpleEdge_Event(OID.ToString(), FID.ToString(), EID.ToString(), 
                OID_JunctionIniziale.ToString(), OID_JunctionFinale.ToString()));
        }

        public void AggiungiInfoComplexEdge(long OID, long FID, long EID, long SUBID, long OID_JunctionInziale, long OID_JunctionFinale)
        {
            // Raise un evento di update
            InfoAggiornateComplexEdge?.Invoke(this,
                new RaccoglitoreInfoComplexEdge_Event(OID.ToString(), FID.ToString(), EID.ToString(), 
                SUBID.ToString(), OID_JunctionInziale.ToString(), OID_JunctionFinale.ToString()));
        }

        public void AggiungiInfoSimpleJunction(long OID, long FID, long EID, double X, double Y, double longi, double lati)
        {
            // Raise un evento di update
            InfoAggiornateSimpleJunction?.Invoke(this,
                new RaccoglitoreInfoSimpleJunction_Event(OID.ToString(), FID.ToString(), EID.ToString(),
                X.ToString(), Y.ToString(), longi.ToString(), lati.ToString()));
        }
    }
}
