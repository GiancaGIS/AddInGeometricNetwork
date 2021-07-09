using AddInGeometricNetwork.Globali;
using AddInGeometricNetwork.selezionaElementoGN.raccoglitore;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;


namespace AddInGeometricNetwork
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class dockableSelezione : UserControl
    {
        private string sceltaCombobox = String.Empty;

        private RaccogliInfoPerListView _spazzolatoreInfo = null;

        private IFeatureClass _fcSimpleJunction = null;
        private IFeatureClass _fcSimpleEdge = null;
        private IFeatureClass _fcComplexEdge = null;

        private IFeatureLayer _fLayerSimpleJunction = null;
        private IFeatureLayer _fLayerSimpleEdge = null;
        private IFeatureLayer _fLayerComplexEdge = null;

        public dockableSelezione(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
            VariabiliGlobaliClass.DockableSelezione = this;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }


        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private dockableSelezione m_windowUI;

            public AddinImpl()
            {             
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new dockableSelezione(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

            /// <summary>
            /// Ritorna la Dockable senza la classe AddinImpl
            /// </summary>
            internal dockableSelezione UI
            {
                get { return m_windowUI; }
            }
        }

        public void InitLog(RaccogliInfoPerListView hookHelper)
        {
            if (_spazzolatoreInfo == null)
            {
                _spazzolatoreInfo = hookHelper;
            }
            _spazzolatoreInfo.InfoAggiornateSimpleEdge -= new EventHandler(SpazzolatoreSimpleEdge_ListViewUpdated);
            _spazzolatoreInfo.InfoAggiornateSimpleEdge += new EventHandler(SpazzolatoreSimpleEdge_ListViewUpdated);

            _spazzolatoreInfo.InfoAggiornateSimpleJunction -= new EventHandler(SpazzolatoreSimpleJunction_ListViewUpdated);
            _spazzolatoreInfo.InfoAggiornateSimpleJunction += new EventHandler(SpazzolatoreSimpleJunction_ListViewUpdated);

            _spazzolatoreInfo.InfoAggiornateComplexEdge -= new EventHandler(SpazzolatoreComplexEdge_ListViewUpdated);
            _spazzolatoreInfo.InfoAggiornateComplexEdge += new EventHandler(SpazzolatoreComplexEdge_ListViewUpdated);
        }

        private void SpazzolatoreSimpleJunction_ListViewUpdated(object sender, EventArgs e)
        {
            listViewSimpleJunction.SuspendLayout();

            RaccoglitoreInfoSimpleJunction_Event infoEvento = e as RaccoglitoreInfoSimpleJunction_Event;

            ListViewItem item = new ListViewItem
            {
                Text = infoEvento._OID
            };

            item.SubItems.Add(infoEvento._FID);
            item.SubItems.Add(infoEvento._EID);
            item.SubItems.Add(infoEvento._X);
            item.SubItems.Add(infoEvento._Y);
            item.SubItems.Add(infoEvento._longi);
            item.SubItems.Add(infoEvento._lati);

            // Aggiungo l'item, e lo metto in lista!
            listViewSimpleJunction.Items.Add(item);
            // Refresho e soprattutto: RIABILITO la List View!
            listViewSimpleJunction.Refresh();
            listViewSimpleJunction.ResumeLayout();
        }

        private void SpazzolatoreSimpleEdge_ListViewUpdated(object sender, EventArgs e)
        {
            this.listViewSimpleEdge.SuspendLayout();

            raccoglitoreInfoSimpleEdge_Event infoEvento = e as raccoglitoreInfoSimpleEdge_Event;

            ListViewItem item = new ListViewItem
            {
                Text = infoEvento._OID
            };
            item.SubItems.Add(infoEvento._FID);
            item.SubItems.Add(infoEvento._EID);
            item.SubItems.Add(infoEvento._OID_JunctionInziale);
            item.SubItems.Add(infoEvento._OID_JunctionFinale);

            // Aggiungo l'item, e lo metto in lista!
            this.listViewSimpleEdge.Items.Add(item);
            // Refresho e soprattutto: RIABILITO la List View!
            this.listViewSimpleEdge.Refresh();
            this.listViewSimpleEdge.ResumeLayout();
        }

        private void SpazzolatoreComplexEdge_ListViewUpdated(object sender, EventArgs e)
        {
            this.listViewComplexEdge.SuspendLayout();
            RaccoglitoreInfoComplexEdge_Event infoEvento = e as RaccoglitoreInfoComplexEdge_Event;

            ListViewItem item = new ListViewItem
            {
                Text = infoEvento._OID
            };

            item.SubItems.Add(infoEvento._FID);
            item.SubItems.Add(infoEvento._SUBID);
            item.SubItems.Add(infoEvento._EID);
            item.SubItems.Add(infoEvento._OID_JunctionInziale);
            item.SubItems.Add(infoEvento._OID_JunctionFinale);

            this.listViewComplexEdge.Items.Add(item);
            this.listViewComplexEdge.Refresh();
            this.listViewComplexEdge.ResumeLayout();
        }

        private void buttonSvuotaComplexEdge_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.listViewComplexEdge.Items.Clear();
                    this.listViewComplexEdge.Refresh();
                }
                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message, "Errore nello svuotamento della ListView Complex Edge!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{this.textBoxNomeFile.Text}.csv");

                if (this.sceltaCombobox.ToLower() == "complex edge")
                {
                    ListViewToCSVClass.ConvertiListViewToCSV(this.listViewComplexEdge, path, false);
                    MessageBox.Show(StringheAvvisiUtente.avvisoDownloadCompletato, "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.sceltaCombobox.ToLower() == "simple edge")
                {
                    ListViewToCSVClass.ConvertiListViewToCSV(this.listViewSimpleEdge, path, false);
                    MessageBox.Show(StringheAvvisiUtente.avvisoDownloadCompletato, "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.sceltaCombobox.ToLower() == "simple junction")
                {
                    ListViewToCSVClass.ConvertiListViewToCSV(this.listViewSimpleJunction, path, false);
                    MessageBox.Show(StringheAvvisiUtente.avvisoDownloadCompletato, "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(StringheAvvisiUtente.avvisoSceltaInvalidaCombobox, "ATTENZIONE!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            }
        }

        private void listViewComplexEdge_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(250);

            try
            {
                SelectedListViewItemCollection collection = this.listViewComplexEdge.SelectedItems;

                if (collection.Count > 0)
                {
                    ListViewItem item = collection[0];

                    var OID = (item.Text.ToString() ?? "none");

                    var EID_Edge = (item.SubItems[3].Text.ToString() ?? "none");

                    IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                    IActiveView activeView = mxDocument.ActiveView;

                    this.ZoommasuFeatureSelezionata_ListView(this._fcComplexEdge, OID, ref mxDocument, out IFeature feature);

                    IComplexEdgeFeature complexEdgeFeature = (IComplexEdgeFeature)feature;
                    // Ricavo la geometria per quel singolo sotto edge nella Complex Edge
                    IGeometry geometry = complexEdgeFeature.GeometryForEID[Convert.ToInt32(EID_Edge)];

                    ElementoLineare toolElemLineare = new ElementoLineare();

                    // Evidenzio e disegno in mappa sottolinea singola sotto-Edge logica della Complex
                    toolElemLineare.EvidenziaFeatureDinamicamente(geometry, activeView, true, null, null, esriSimpleFillStyle.esriSFSSolid);

                    toolElemLineare.EvidenziaFeatureDinamicamente(this._fLayerComplexEdge, Convert.ToInt32(OID), 
                        activeView, false, null, null, esriSimpleFillStyle.esriSFSDiagonalCross);
                    
                    //ToolSelezionaLinea toolSelezionaLinea = new ToolSelezionaLinea();
                    //toolSelezionaLinea.FlashaFeature(feature, ref activeView);

                    // Spengo il tool
                    toolElemLineare.SpegniTool();
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nella selezione listView Complex Edge!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ZoommasuFeatureSelezionata_ListView(IFeatureClass featureClass, string stringOIDFeature, ref IMxDocument mxDocument, 
            out IFeature feature)
        {
            try
            {
                IActiveView activeView = mxDocument.ActiveView;

                IQueryFilter queryFilter = new QueryFilter()
                {
                    WhereClause = $@"OBJECTID = {stringOIDFeature}"
                };

                IFeatureCursor fCursor = featureClass.Search(queryFilter, true);
                feature = fCursor.NextFeature();

                if (feature != null)
                {
                    IGeometry geometriaFeature = feature.ShapeCopy;

                    IBufferConstruction operatoreBuffer = new BufferConstruction();
                    IGeometry geometriaBuffer = operatoreBuffer.Buffer(geometriaFeature, 2);

                    activeView.Extent = geometriaBuffer.Envelope;
                    activeView.Refresh();

                }

                Marshal.ReleaseComObject(fCursor);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sceltaCombobox = this.comboBoxSceltaDownload.SelectedItem.ToString();
        }

        private void listViewSimpleJunction_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(250); 
            
            try
            {
                SelectedListViewItemCollection collection = this.listViewSimpleJunction.SelectedItems;

                if (collection.Count > 0)
                {
                    ListViewItem item = collection[0];

                    var OID = (item.Text.ToString() ?? "(none)");

                    IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                    IActiveView activeView = mxDocument.ActiveView;

                    this.ZoommasuFeatureSelezionata_ListView(this._fcSimpleJunction, OID, ref mxDocument, out IFeature feature);

                    ElementoLineare toolElemLineare = new ElementoLineare();

                    toolElemLineare.EvidenziaFeatureDinamicamente(this._fLayerSimpleJunction, Convert.ToInt32(OID),
                        activeView, true, null, null, esriSimpleFillStyle.esriSFSDiagonalCross);

                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nella selezione listView SimpleJunction!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewSimpleEdge_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(250); 
            
            try
            {
                SelectedListViewItemCollection collection = this.listViewSimpleEdge.SelectedItems;

                if (collection.Count > 0)
                {
                    ListViewItem item = collection[0];

                    var OID = (item.Text.ToString() ?? "(none)");

                    IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                    IActiveView activeView = mxDocument.ActiveView;

                    this.ZoommasuFeatureSelezionata_ListView(this._fcSimpleEdge, OID, ref mxDocument, out IFeature feature);

                    ElementoLineare toolElemLineare = new ElementoLineare();

                    toolElemLineare.EvidenziaFeatureDinamicamente(this._fLayerSimpleEdge, Convert.ToInt32(OID),
                        activeView, true, null, null, esriSimpleFillStyle.esriSFSDiagonalCross);
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nella selezione listView Simple Edge!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Metodi pubblici disponibili per l'esterno
        public bool ValorePresenteInListViewSimpleJunction(string valore)
        {
            bool blnPresente = false;

            if (this.listViewSimpleJunction.Items.Count > 0)
            {
                IEnumerator enumerator = this.listViewSimpleJunction.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    // now empEnumerator.Current is the Employee instance without casting
                    if (enumerator.Current is ListViewItem)
                    {
                        ListViewItem item = enumerator.Current as ListViewItem;
                        string corrente = item.Text;

                        if (corrente.ToLower() == valore.ToLower())
                        {
                            blnPresente = true;
                            break;
                        }
                    }
                }
            }

            return blnPresente;
        }

        public bool ValorePresenteInListViewSimpleEdge(string valore)
        {
            bool blnPresente = false;

            if (this.listViewSimpleEdge.Items.Count > 0)
            {
                IEnumerator enumerator = this.listViewSimpleEdge.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    // now empEnumerator.Current is the Employee instance without casting
                    if (enumerator.Current is ListViewItem)
                    {
                        ListViewItem item = enumerator.Current as ListViewItem;
                        string corrente = item.Text;

                        if (corrente.ToLower() == valore.ToLower())
                        {
                            blnPresente = true;
                            break;
                        }

                    }
                }
            }

            return blnPresente;
        }

        public bool ValorePresenteInListViewComplexEdge(string valore)
        {
            bool blnPresente = false;

            if (this.listViewComplexEdge.Items.Count > 0)
            {
                IEnumerator enumerator = this.listViewComplexEdge.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    // now empEnumerator.Current is the Employee instance without casting
                    if (enumerator.Current is ListViewItem)
                    {
                        ListViewItem item = enumerator.Current as ListViewItem;
                        string corrente = item.SubItems[3].Text;

                        if (corrente.ToLower() == valore.ToLower())
                        {
                            blnPresente = true;
                            break;
                        }

                    }
                }
            }

            return blnPresente;
        }

        public void ValorizzaFcSimpleJunction(IFeatureClass fc)
        {
            this._fcSimpleJunction = fc;
        }

        public void ValorizzaFcSimpleEdge(IFeatureClass fc)
        {
            this._fcSimpleEdge = fc;
        }

        public void ValorizzaFcComplexEdge(IFeatureClass fc)
        {
            this._fcComplexEdge = fc;
        }

        public void ValorizzaFeatureLayerSimpleJunction(IFeatureLayer featureLayer)
        {
            this._fLayerSimpleJunction = featureLayer;
        }

        public void ValorizzaFeatureLayerSimpleEdge(IFeatureLayer featureLayer)
        {
            this._fLayerSimpleEdge = featureLayer;
        }

        public void ValorizzaFeatureLayerComplexEdge(IFeatureLayer featureLayer)
        {
            this._fLayerComplexEdge = featureLayer;
        }
        #endregion

        private void buttonSvuotaSimpleJunction_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.listViewSimpleJunction.Items.Clear();
                this.listViewSimpleJunction.Refresh();
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nello svuotamento della ListView Simple Junction!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSvuotaSimpleEdge_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.listViewSimpleEdge.Items.Clear();
                this.listViewSimpleEdge.Refresh();
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nello svuotamento della ListView Simple Edge!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
