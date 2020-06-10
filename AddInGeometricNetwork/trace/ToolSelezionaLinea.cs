using AddInGeometricNetwork.Globali;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Windows.Forms;


namespace AddInGeometricNetwork
{
    public class ToolSelezionaLinea : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public ToolSelezionaLinea()
        {
            this.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Scopo di questo metodo è di flashare una IFeature di input nella active view!
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="screenDisplay"></param>
        public void FlashaFeature(IFeature feature, ref IActiveView activeView)
        {
            // Accedo all'oggetto screen display
            IScreenDisplay3 screenDisplay = activeView.ScreenDisplay as IScreenDisplay3;

            // Eseguo il flash dell'oggetto in ActiveView...
            IFeatureIdentifyObj feature_identify_obj = new FeatureIdentifyObject
            {
                Feature = feature
            };

            // Creo l'oggetto IIdentifyObj e lo flasho nello screenDisplay
            IIdentifyObj identifyObj = feature_identify_obj as IIdentifyObj;
            identifyObj.Flash((IScreenDisplay)screenDisplay);
        }


        protected override void OnMouseDown(MouseEventArgs arg)
        {
            IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxDocument.ActiveView;

            try
            {
                // Mi occupo di svuotare i Graphics
                activeView.GraphicsContainer.DeleteAllElements();
                if (activeView.Selection != null)
                {
                    if (activeView.Selection.CanClear()) activeView.Selection.Clear();
                }

                IGeometry5 geometria = this.DisegnaRettangoloRubber(ref activeView);
                this.IdentificaLayerLinea(ref activeView, geometria);
                this.FlashaFeature(VariabiliGlobaliClass.featureLineare, ref activeView);
                this.AnalisiTopologiche(VariabiliGlobaliClass.featureLineare, ref activeView);

                // Scelta se zoomare
                if (VariabiliGlobaliClass.blnZoomSuTratta) this.ZoomSuGeometria(VariabiliGlobaliClass.featureLineare, ref activeView);

                // Accendo il bottone del trace della Dockable
                VariabiliGlobaliClass.DockableWindow.AccendiBtnTraceDownstream();
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore Identificazione Layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                activeView.Refresh();
            }
        }

        private void ZoomSuGeometria(IFeature feature, ref IActiveView activeView)
        {
            try
            {
                IGeometry5 geometry = feature.ShapeCopy as IGeometry5;
                IEnvelope envelope = geometry.Envelope;
                activeView.Extent = envelope;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Funzione per disegnare un poligono dato ciò che è 'tracciato' nell'ActiveView
        /// </summary>
        /// <param name="activeView"></param>
        private IGeometry5 DisegnaRettangoloRubber(ref IActiveView activeView)
        {
            IGeometry5 geometria = null;

            try
            {
                // Accedo all'oggetto screen display
                IScreenDisplay3 screenDisplay = activeView.ScreenDisplay as IScreenDisplay3;

                // Definisco lo spazio colore...
                IRgbColor spazioColoreRGB = new RgbColor();
                Random random = new Random();
                spazioColoreRGB.Green = random.Next(0, 255);
                spazioColoreRGB.Blue = random.Next(0, 255);
                spazioColoreRGB.Red = random.Next(0, 255);

                // Definisco le proprietà simboliche...
                ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol
                {
                    Color = spazioColoreRGB,
                    Style = esriSimpleFillStyle.esriSFSDiagonalCross
                };

                // Definisco l'oggetto ISymbol
                ISymbol simbolo = simpleFillSymbol as ISymbol;

                // Disegno il poligono col metodo Rubber Band
                IRubberBand2 rubberBand = new RubberEnvelope() as IRubberBand2;
                geometria = rubberBand.TrackNew((IScreenDisplay)screenDisplay, simbolo) as IGeometry5;

                // Disegno nello screen display
                screenDisplay.StartDrawing(screenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
                screenDisplay.SetSymbol(simbolo);
                screenDisplay.DrawRectangle((IEnvelope2)geometria);
                screenDisplay.FinishDrawing();
            }
            catch (Exception)
            {

                throw;
            }
            
            return geometria;
        }


        private void IdentificaLayerLinea(ref IActiveView activeView, IGeometry5 geometria)
        {
            try
            {
                //VariabiliGlobali.dizNumPosizLayer.Clear();

                // Procedo con la indentifazione dei Layer selezionati in Mappa...
                IMap map = activeView.FocusMap;

                IBufferConstruction operatoreBuffer = new BufferConstruction();

                // Costruisco piccolo buffer sul quale costruire la geometria
                IGeometry5 geometriaBuffer = operatoreBuffer.Buffer(geometria, 0.1) as IGeometry5;

                IEnumLayer enumeratoreLayer = map.Layers;

                ILayer2 layer = enumeratoreLayer.Next() as ILayer2;

                IFeatureLayer2 featureLayer = null;

                string nomeLayer = String.Empty;

                while (layer != null)
                {
                    if (layer.Visible && !(layer is ICompositeLayer) && !(layer is IGroupLayer))
                    {
                        featureLayer = layer as IFeatureLayer2;

                        esriGeometryType tipologiaFeatureLayer = featureLayer.ShapeType;
                        if (tipologiaFeatureLayer == esriGeometryType.esriGeometryPolyline)
                        {
                            IIdentify identifyLayer = layer as IIdentify;
                            // Eseguo il identify in mappa sulla geometria buffer, sul layer in questione
                            IArray array = identifyLayer.Identify(geometriaBuffer);

                            if (array != null)
                            {
                                object obj = array.get_Element(0);
                                IFeatureIdentifyObj fobj = obj as IFeatureIdentifyObj;
                                IRowIdentifyObject irow = fobj as IRowIdentifyObject;

                                // Ricavo la mia Feature interrogata:
                                IFeature fLinea = irow.Row as IFeature;

                                VariabiliGlobaliClass.featureLineare = fLinea;
                                VariabiliGlobaliClass.featureLayerTrace = featureLayer;
                            }
                        }
                    }

                    layer = enumeratoreLayer.Next() as ILayer2;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void AnalisiTopologiche(IFeature feature, ref IActiveView activeView)
        {
            IEdgeFeature edgeTratta = feature as IEdgeFeature;

            IJunctionFeature junctionFeatureFrom = edgeTratta.FromJunctionFeature;
            IJunctionFeature junctionFeatureEnd = edgeTratta.ToJunctionFeature;

            IFeature fJunctionIniziale = junctionFeatureFrom as IFeature;

            VariabiliGlobaliClass.DockableWindow.AggiornaListaJunctionIniziale
                (String.Format("Feature Class: {0}, OID: {1}", this.NomeFC_Feature(fJunctionIniziale), fJunctionIniziale.OID));

            IFeature fJunctionFinale = junctionFeatureEnd as IFeature;

            VariabiliGlobaliClass.DockableWindow.AggiornaListaJunctionFinale
                (String.Format("Feature Class: {0}, OID: {1}", this.NomeFC_Feature(fJunctionFinale), fJunctionFinale.OID));

            #region Parte dedicata a trovare tutte gli oggetti topologicamente connessi
            
            IComplexEdgeFeature complexEdgeFeature = feature as IComplexEdgeFeature;
            ISimpleEdgeFeature simpleEdgeFeature = feature as ISimpleEdgeFeature;
            int contatore = 0;

            VariabiliGlobaliClass.DockableWindow.SvuotaListaJunctionTotale();


            int numeroJunction;
            // Qualora la Edge selezionata sia simple
            if (complexEdgeFeature == null && simpleEdgeFeature != null)
            {
                VariabiliGlobaliClass.DockableWindow.AggiornaListaJunctionTOTALI
                    (String.Format("Feature Class: {0}, OID: {1}", this.NomeFC_Feature(fJunctionIniziale), fJunctionIniziale.OID));

                VariabiliGlobaliClass.DockableWindow.AggiornaListaJunctionTOTALI
                    (String.Format("Feature Class: {0}, OID: {1}", this.NomeFC_Feature(fJunctionFinale), fJunctionFinale.OID));

            }
            else if (complexEdgeFeature != null && simpleEdgeFeature == null)
            {
                numeroJunction = complexEdgeFeature.JunctionFeatureCount;

                while (contatore <= numeroJunction - 1)
                {
                    IJunctionFeature junctionFeature = complexEdgeFeature.JunctionFeature[contatore];
                    // Casto in IFeature
                    IFeature featureTemp = junctionFeature as IFeature;
                    VariabiliGlobaliClass.DockableWindow.AggiornaListaJunctionTOTALI
                        (String.Format("Feature Class: {0}, OID: {1}", this.NomeFC_Feature(featureTemp), featureTemp.OID));
                    contatore++;
                }
            }

            #endregion

            // Mi occupo della collezione geometrie
            IGeometryCollection geometryColl = 
                this.CostruisciCollezioneGeom(fJunctionIniziale, fJunctionFinale);

            contatore = 0;

            while (contatore <= geometryColl.GeometryCount - 1)
            {
                IGeometry geometry = geometryColl.Geometry[contatore];
                this.InserisciGraphics(geometry, ref activeView);
                contatore++;
            }
        }


        private string NomeFC_Feature(IFeature feature)
        {
            string nome;
            try
            {
                IDataset dataset = feature.Class as IDataset;
                nome = dataset.Name;
            }
            catch (Exception)
            {

                throw;
            }
            return nome;
        }

        private IGeometryCollection CostruisciCollezioneGeom(IFeature f1, IFeature f2)
        {
            IGeometryCollection geometryColl = null;

            try
            {
                IGeometry geometryBag = new GeometryBag() as IGeometry;
                geometryColl = geometryBag as IGeometryCollection;
                geometryColl.AddGeometry(f1.ShapeCopy);
                geometryColl.AddGeometry(f2.ShapeCopy);
            }
            catch (Exception)
            {

                throw;
            }

            return geometryColl;
        }

        private void InserisciGraphics(IGeometry geometria, ref IActiveView activeView)
        {
            try
            {
                IScreenDisplay3 screenDisplay = activeView.ScreenDisplay as IScreenDisplay3;

                // Mi occupo ora di scegliere i colori...
                IRgbColor spazioColore = new RgbColor();
                Random randomico = new Random();
                spazioColore.Blue = randomico.Next(255);
                spazioColore.Red = randomico.Next(255);
                spazioColore.Green = randomico.Next(255);

                // Mi occupo ora della simbologia...
                ISimpleMarkerSymbol markerSymbol = new SimpleMarkerSymbol
                {
                    Size = 25,
                    Style = esriSimpleMarkerStyle.esriSMSDiamond,
                    Color = spazioColore
                };

                // Istanzio l'oggetto Marker Element necessario per la creazione dell'oggetto Element...
                IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
                markerElement.Symbol = markerSymbol;

                // Creo e valorizzo l'oggetto Element necessario per l'aggiunta dall'oggetto Graphics...
                IElement elemento = markerElement as IElement;
                elemento.Geometry = geometria;

                // Procedo ora al disegno del Graphics
                IGraphicsContainer contenitoreGraphics = activeView.FocusMap as IGraphicsContainer;
                contenitoreGraphics.AddElement(elemento, 0);
                //activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            }

            catch (Exception)
            {
                throw;
            }
        }


        protected override void OnUpdate()
        {
            Enabled = true;
        }
    }

}
