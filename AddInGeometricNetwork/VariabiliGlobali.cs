using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;



namespace AddInGeometricNetwork.Globali
{
    internal static class StringheAvvisiUtente
    {
        internal static string avvisoDownloadCompletato = $@"Download completato!{Environment.NewLine}Il file è disponibile sul desktop.";
        internal static string avvisoSceltaInvalidaCombobox = "Scelta invalida!";
    }

    internal static class StatoEstensione
    {
        internal static bool estensioneAttiva = false;
    }

    internal static class VariabiliGlobaliClass
    {
        // Dizionario contenente le informazioni sul Layer contenuti nella TOC e il loro
        // numero posizionale!
        internal static Dictionary<int, ILayer2> dizNumPosizLayer = new Dictionary<int, ILayer2>();

        // Variabile IFeature contenente l'area cliccata dall'utente sulla quale capire
        // chi casca all'interno!
        internal static IFeature featureLineare = null;

        internal static IFeatureLayer2 featureLayerTrace = null;

        internal static DockableWindow DockableWindow { get; set; }

        internal static bool blnZoomSuTratta = false;

        internal static dockableSelezione DockableSelezione { get; set; }

        internal static string UID_PAN_ARCMAP = "esriArcMapUI.PagePanTool";
    }

    /// <summary>
    /// Classe dedicata alla conversione di una ListView in CSV
    /// </summary>
    internal static class ListViewToCSVClass
    {
        internal static void ConvertiListViewToCSV(ListView listView, string filePath, bool includeHidden)
        {
            //make header string
            StringBuilder result = new StringBuilder();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text, ";");

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
               WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text, ";");

            File.WriteAllText(filePath, result.ToString());
        }

        private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue, string separatore)
        {
            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append(separatore);
                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }
    }

    public class PuntoGiancaGISClass : IPuntoGiancaGIS
    {
        public IPoint ObjPuntoESRI { get; set; }

        public IPoint ObjPuntoESRI_WGS84 { get; set; }

        private void PuntoCopiaInWGS84()
        {
            if (this.ObjPuntoESRI != null)
            {
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironment();
                ISpatialReference spatialReference;

                IGeographicCoordinateSystem WGS84 = srFactory.CreateGeographicCoordinateSystem(4326);

                spatialReference = WGS84;

                this.ObjPuntoESRI_WGS84 = new Point() as IPoint;
                this.ObjPuntoESRI_WGS84.SpatialReference = this.ObjPuntoESRI.SpatialReference;
                this.ObjPuntoESRI_WGS84.X = this.ObjPuntoESRI.X;
                this.ObjPuntoESRI_WGS84.Y = this.ObjPuntoESRI.Y;

                this.ObjPuntoESRI_WGS84.Project(spatialReference);
            }
        }

        private void RicavaCentroide()
        {
            if (this.ObjPuntoESRI != null)
            {
                X = this.ObjPuntoESRI.X;
                Y = this.ObjPuntoESRI.Y;
            }
        }

        private double X { get; set; }
        private double Y { get; set; }

        /// <summary>
        /// Restituisce la coordinata planare X
        /// </summary>
        /// <returns></returns>
        public double CoordX()
        {
            this.RicavaCentroide();
            return Math.Round(this.X, 6);
        }

        /// <summary>
        /// Restituisce la coordinata planare y
        /// </summary>
        /// <returns></returns>
        public double CoordY()
        {
            this.RicavaCentroide();
            return Math.Round(this.Y, 6);
        }

        /// <summary>
        /// Restituisce la longitudine in WGS84
        /// </summary>
        /// <returns></returns>
        public double Longitudine()
        {
            if (this.ObjPuntoESRI_WGS84 == null)
                this.PuntoCopiaInWGS84();

            this.ObjPuntoESRI_WGS84.QueryCoords(out double longitudine, out _);

            return Math.Round(longitudine, 6);
        }

        /// <summary>
        /// Restituisce la latitudine in WGS84
        /// </summary>
        /// <returns></returns>
        public double Latitudine()
        {
            if (this.ObjPuntoESRI_WGS84 == null)
                this.PuntoCopiaInWGS84();

            this.ObjPuntoESRI_WGS84.QueryCoords(out _, out double latitudine);

            return Math.Round(latitudine, 6);
        }


        public string CentroideToString() => $@"({X}, {Y})";
        

        public IFeature Feature { get; set; }

        public IFeatureLayer FLayer { get; set; }

        public bool IsSimpleJunction { get; set; }

        public IGeometricNetwork GN { get; set; }

        public void RicavaGN()
        {
            if (this.ObjPuntoESRI is IFeature)
            {
                this.Feature = this.ObjPuntoESRI as IFeature;

                this.GN = ((INetworkFeature)Feature).GeometricNetwork;
            }
        }

        public int RicavaEID(IMap dataframe)
        {
            IPointToEID pointToEID = new PointToEID() as IPointToEID;
            pointToEID.GeometricNetwork = this.GN;
            pointToEID.SourceMap = dataframe;
            pointToEID.SnapTolerance = 10;     //  set a snap tolerance of 10 map units
            pointToEID.GetNearestJunction(this.ObjPuntoESRI, out int EID, out IPoint outPoint);

            return EID;
        }
    }

    public class LineaGiancaGISClass : ILineaGiancaGIS
    {
        public IPolyline ObjLineaESRI { get; set; }

        private List<IPoint> ListaPuntiEstremi { get; set; }

        /// <summary>
        /// Fornisce lista di IPoint, primo punto è quello iniziale, il secondo quello finale
        /// </summary>
        /// <returns></returns>
        public List<IPoint> RicavaPuntiEstremi()
        {
            this.ListaPuntiEstremi = new List<IPoint>();

            if (this.ObjLineaESRI != null)
            {
                this.ListaPuntiEstremi.Add(this.ObjLineaESRI.FromPoint);
                this.ListaPuntiEstremi.Add(this.ObjLineaESRI.ToPoint);
            }

            return this.ListaPuntiEstremi;
        }

        public IFeature Feature { get; set; }

        public IFeatureLayer FLayer { get; set; }

        public bool IsComplexEdge { get; set; }

        public IGeometricNetwork GN { get; set; }

        public void RicavaGN()
        {
            if (this.ObjLineaESRI is IFeature)
            {
                this.Feature = this.ObjLineaESRI as IFeature;

                this.GN = ((INetworkFeature)Feature).GeometricNetwork;
            }
        }
    }

    /// <summary>
    /// Creo Interfaccia che implementa tutto il Punto ad eccezione del Centroide
    /// </summary>
    public interface IPuntoGiancaGIS
    {
        IPoint ObjPuntoESRI { get; set; }

        IFeature Feature { get; set; }

        IFeatureLayer FLayer { get; set; }

        bool IsSimpleJunction { get; set; }

        IGeometricNetwork GN { get; set; }

        void RicavaGN();

        int RicavaEID(IMap dataframe);
    }

    public interface ILineaGiancaGIS
    {
        IPolyline ObjLineaESRI { get; set; }

        IFeature Feature { get; set; }

        IFeatureLayer FLayer { get; set; }

        bool IsComplexEdge { get; set; }

        IGeometricNetwork GN { get; set; }

        void RicavaGN();
    }
}