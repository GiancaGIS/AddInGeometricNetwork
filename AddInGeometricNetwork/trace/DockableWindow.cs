using AddInGeometricNetwork.Globali;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace AddInGeometricNetwork
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class DockableWindow : UserControl
    {
        public DockableWindow(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
            VariabiliGlobaliClass.DockableWindow = this;
            this.buttonTrace.Enabled = false;
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
            private DockableWindow m_windowUI;

            public AddinImpl()
            { }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new DockableWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBoxJunctionFinale.Items.Clear();
            this.listBoxJunctionFinale.Refresh();
            this.listBoxJunctionIniziale.Items.Clear();
            this.listBoxJunctionIniziale.Refresh();
            this.listBoxTutteJunction.Items.Clear();
            this.listBoxTutteJunction.Refresh();
        }

        public void AggiornaListaJunctionIniziale(string info)
        {
            this.listBoxJunctionIniziale.Items.Clear();
            this.listBoxJunctionIniziale.Items.Add(info);
            this.listBoxJunctionIniziale.Refresh();
        }

        public void AggiornaListaJunctionFinale(string info)
        {
            this.listBoxJunctionFinale.Items.Clear();
            this.listBoxJunctionFinale.Items.Add(info);
            this.listBoxJunctionFinale.Refresh();
        }

        public void SvuotaListaJunctionTotale()
        {
            this.listBoxTutteJunction.Items.Clear();
            this.listBoxTutteJunction.Refresh();
        }

        public void AggiornaListaJunctionTOTALI(string strInfo)
        {
            this.listBoxTutteJunction.Items.Add(strInfo);
            this.listBoxTutteJunction.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                btnRimuoviGraphicsMappa btnRimuoviGraphicsMappa = new btnRimuoviGraphicsMappa();
                btnRimuoviGraphicsMappa.RimuoviGraphics();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int OID = VariabiliGlobaliClass.featureLineare.OID;
                DialogResult dialogResult =
                    MessageBox.Show($@"Avviare Trace ricorsivo per la Tratta avente ObjectID: {OID}?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    this.listBoxEdge.Items.Clear();
                    this.listBoxJunction.Items.Clear();

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    // Inizializzo le variabili per la progress bar...
                    ITrackCancel trkCancel = null;
                    IProgressDialogFactory prgFact = new ProgressDialogFactoryClass();
                    IStepProgressor stepProgressor = null;
                    IProgressDialog2 progressDialog = null;

                    // Inizializzo le variabili per la status bar di ArcMap...
                    IStatusBar barraStato = ArcMap.Application.StatusBar;

                    IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                    IActiveView activeView = mxDocument.ActiveView;
                    IMap mappa = mxDocument.FocusMap;
                    mappa.ClearSelection();

                    IFeatureSelection featureSelection = VariabiliGlobaliClass.featureLayerTrace as IFeatureSelection;

                    List<int> listaOIDEdge = new List<int>();

                    INetworkFeature networkFeature = VariabiliGlobaliClass.featureLineare as INetworkFeature;
                    INetwork network = networkFeature.GeometricNetwork.Network;
                    int numEdgeNetwork = network.EdgeCount;

                    INetElements netElements = network as INetElements;

                    IForwardStarGEN fStar =
                        (IForwardStarGEN)network.CreateForwardStar(false, null, null, null, null);

                    IEdgeFeature edgeFeature = VariabiliGlobaliClass.featureLineare as IEdgeFeature;

                    int EID_FromJunction_DI_PARTENZA = edgeFeature.FromJunctionEID;

                    Queue<int> junctionDaAnalizzare = new Queue<int>();

                    junctionDaAnalizzare.Enqueue(edgeFeature.ToJunctionEID);
                    this.listBoxJunction.Items.Add(edgeFeature.ToJunctionEID);

                    int contatore = 0;
                    IGeometryCollection geometryColl = null;
                    IGeometry geometryBag = null;

                    // Istanzio un dizionario avente storico del EID delle junction con le edge connesse!
                    // Mi serve per evitare che non si rianalizzino sempre le stesse info ciclicamente!
                    Dictionary<int, int> dizEIDNumEdge = new Dictionary<int, int>();

                    stepProgressor = prgFact.Create(trkCancel, 0);
                    progressDialog = stepProgressor as IProgressDialog2;
                    progressDialog.Description = "Eseguendo Trace Downstream";
                    progressDialog.Title = "Elaborazione massiva in corso...";
                    progressDialog.Animation = esriProgressAnimationTypes.esriProgressSpiral;
                    progressDialog.ShowDialog();

                    stepProgressor.MinRange = 0;
                    stepProgressor.MaxRange = numEdgeNetwork;
                    stepProgressor.StepValue = 1;
                    stepProgressor.Show();

                    barraStato.ShowProgressBar("Eseguendo Trace Downstream...", 0, numEdgeNetwork, 1, true);

                    while (junctionDaAnalizzare.Count > 0)
                    {
                        int EID_analizzato = junctionDaAnalizzare.Dequeue();
                        fStar.FindAdjacent(0, EID_analizzato, out int contaEdge);

                        int UserID = -9999;

                        #region Inizializzo array contenenti le informazioni ottenute sugli EID delle Junction adiacenti e delle Edge adiacenti
                        int[] arrayEIDJunctionAdiacenti = new int[contaEdge];
                        object[] arrayPesiJunction = new object[contaEdge];
                        int[] arrayEIDEdge = new int[contaEdge];
                        object[] arrayPesiEdge = new object[contaEdge];
                        bool[] arrayVersoEdge = new bool[contaEdge]; // array con booleani versi Edge
                        // true se Edge entra nella Junction, false se esce dalla Junction
                        #endregion

                        fStar.QueryAdjacentJunctions(ref arrayEIDJunctionAdiacenti, ref arrayPesiJunction);
                        fStar.QueryAdjacentEdges(ref arrayEIDEdge, ref arrayVersoEdge, ref arrayPesiEdge);

                        // Ora che ho ricavato tutte le EDGE connesse alla Junction finale le aggiungo in coda

                        for (int i = 0; i < arrayEIDJunctionAdiacenti.Length; i++)
                        {
                            if (!(dizEIDNumEdge.ContainsKey(arrayEIDJunctionAdiacenti[i]))
                                && arrayEIDJunctionAdiacenti[i] != EID_analizzato && arrayEIDJunctionAdiacenti[i] != EID_FromJunction_DI_PARTENZA)
                            {
                                if (arrayVersoEdge[i] == false) //Values in the ReverseOrientation array are TRUE if the edge "enters" the junction
                                {
                                    dizEIDNumEdge.Add(arrayEIDJunctionAdiacenti[i], arrayEIDEdge.Length);

                                    junctionDaAnalizzare.Enqueue(arrayEIDJunctionAdiacenti[i]);
                                }
                            }
                        }

                        #region Salvo gli OID delle tratte / edge

                        for (int i = 0; i < arrayEIDEdge.Length; i++)
                        {
                            if (arrayVersoEdge[i] == false)
                            {
                                netElements.QueryIDs(arrayEIDEdge[i], esriElementType.esriETEdge,
                                out int UserClassID, out UserID, out int UserSubID); // USER ID è il ObjectID!

                                listaOIDEdge.Add(UserID);
                                this.listBoxJunction.Items.Add(arrayEIDJunctionAdiacenti[i]);
                            }
                            contatore++;
                        }
                        #endregion


                        #region Paranoia Check --> Se abbiamo superato il numero totale di Edge nella Network, spacco ed esco

                        if (contatore > numEdgeNetwork) break;

                        #endregion

                        stepProgressor.Step();
                    }

                    #region Seleziono tutti gli oggetti della linea.                

                    geometryBag = new GeometryBag() as IGeometry;
                    geometryColl = geometryBag as IGeometryCollection;

                    List<int> listaOIDEdgeBonificata = listaOIDEdge.Distinct().ToList(); // Elimino di eventuali doppioni

                    if (listaOIDEdgeBonificata.Count > 0)
                    {
                        featureSelection.SelectionSet.AddList(listaOIDEdge.Count, ref listaOIDEdgeBonificata.ToArray()[0]);

                        for (int j = 0; j < listaOIDEdgeBonificata.Count; j++)
                        {
                            this.listBoxEdge.Items.Add(listaOIDEdgeBonificata[j]);

                            IQueryFilter2 queryFilter = new QueryFilter() as IQueryFilter2;
                            queryFilter.WhereClause = $@"OBJECTID = {listaOIDEdge[j]}";

                            IFeatureCursor featureCursor =
                                VariabiliGlobaliClass.featureLayerTrace.FeatureClass.Search(queryFilter, false);

                            IFeature feature = featureCursor.NextFeature();

                            geometryColl.AddGeometry(feature.ShapeCopy);

                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor);

                            stepProgressor.Step();
                        }
                    }

                    // Chiudo la Progress Dialog
                    stepProgressor.Message = "Elaborazione terminata con successo!";
                    stepProgressor.Hide();
                    progressDialog.HideDialog();

                    // Chiudo la Barra di Stato
                    barraStato.HideProgressAnimation();
                    barraStato.HideProgressBar();

                    this.listBoxEdge.Refresh();
                    this.listBoxJunction.Refresh();

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

                    activeView.Extent = geometryBag.Envelope;
                    //activeView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, geometryBag.Envelope);
                    activeView.Refresh();
                    #endregion
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nel trace downstream!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBoxEdge.Items.Clear();
            this.listBoxEdge.Refresh();
            this.listBoxJunction.Items.Clear();
            this.listBoxJunction.Refresh();
        }

        public void AccendiBtnTraceDownstream()
        {
            this.buttonTrace.Enabled = true;
            this.buttonTrace.Refresh();
        }

        private void checkBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxZoom.Checked == true)
            {
                VariabiliGlobaliClass.blnZoomSuTratta = true;
            }
            else
            {
                VariabiliGlobaliClass.blnZoomSuTratta = false;
            }
        }
    }
}
