using AddInGeometricNetwork.Globali;
using AddInGeometricNetwork.selezionaElementoGN.raccoglitore;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AddInGeometricNetwork
{
    public class ToolSelezionaElementoGN : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        //protected dockableSelezione.AddinImpl dockable_Punti =
        //                AddIn.FromID<dockableSelezione.AddinImpl>(ThisAddIn.IDs.dockableSelezione);

        protected dockableSelezione avvisiWindow = AddIn.FromID<dockableSelezione.AddinImpl>(ThisAddIn.IDs.dockableSelezione).UI;

        private static int oid = -1;
        private static int newOID = -1;

        private static IFeature featureSelez = null;

        protected IPuntoGiancaGIS Punto
        { get; set; }

        protected ILineaGiancaGIS Polilinea
        { get; set; }


        public ToolSelezionaElementoGN()
        {
            this.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        #region Selezione Feature
        protected void EseguiSelezioneEdge(IMxDocument mxDocument, IActiveView activeView, int xCoord, int yCoord)
        {
            try
            {
                featureSelez = null;

                IMap map = mxDocument.FocusMap;

                IPoint identifyPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(xCoord, yCoord);

                IBufferConstruction operatoreBuffer = new BufferConstruction();

                IGeometry5 geometriaBuffer = operatoreBuffer.Buffer(identifyPoint, 1) as IGeometry5;

                IArea area = (IPolygon)geometriaBuffer as IArea;
                double dblArea = area.Area;

                IEnumLayer enumeratoreLayer = map.Layers;

                ILayer layer = enumeratoreLayer.Next();

                string nomeLayer = string.Empty;

                while (layer != null)
                {
                    nomeLayer = layer.Name;

                    if (layer.Visible && layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;

                        if (featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTSimpleEdge 
                            || featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTComplexEdge)
                        {
                            IIdentify identifyLayer = layer as IIdentify;
                            IArray array = identifyLayer.Identify(geometriaBuffer);

                            if (array != null)
                            {
                                object obj = array.get_Element(0);
                                IFeatureIdentifyObj fobj = obj as IFeatureIdentifyObj;
                                IRowIdentifyObject irow = fobj as IRowIdentifyObject;
                                featureSelez = irow.Row as IFeature;
                                newOID = featureSelez.OID;
                            }
                            else
                            {
                                newOID = -1;
                            }

                            try
                            {
                                if (newOID > -1 && oid != newOID)
                                {
                                    this.EvidenziaFeatureDinamicamente(featureLayer, newOID, activeView, true);
                                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, activeView.Extent);
                                    oid = featureSelez.OID; // Solo ora che ho evidenziato, aggiorno il oid per il confronto

                                    this.Polilinea = new LineaGiancaGISClass() as ILineaGiancaGIS;
                                    this.Polilinea.ObjLineaESRI = featureSelez.ShapeCopy as IPolyline;
                                    this.Polilinea.RicavaGN();
                                    this.Polilinea.GN = ((INetworkFeature)featureSelez).GeometricNetwork;
                                    this.Polilinea.Feature = featureSelez;
                                    this.Polilinea.FLayer = featureLayer;

                                    switch (featureLayer.FeatureClass.FeatureType)
                                    {
                                        case esriFeatureType.esriFTSimpleEdge:
                                            this.Polilinea.IsComplexEdge = false;
                                            VariabiliGlobaliClass.DockableSelezione.ValorizzaFeatureLayerSimpleEdge(featureLayer);
                                            break;
                                        case esriFeatureType.esriFTComplexEdge:
                                            this.Polilinea.IsComplexEdge = true;
                                            VariabiliGlobaliClass.DockableSelezione.ValorizzaFeatureLayerComplexEdge(featureLayer);
                                            break;
                                    }
                                }
                            }

                            catch (Exception errore)
                            {
                                string err = errore.Message;
                            }
                        }
                    }

                    layer = enumeratoreLayer.Next();
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "ERRORE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void EseguiSelezioneJunction(IMxDocument mxDocument, IActiveView activeView, int xCoord, int yCoord)
        {
            try
            {
                featureSelez = null;

                IMap map = mxDocument.FocusMap;

                IPoint identifyPoint = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(xCoord, yCoord);

                IBufferConstruction operatoreBuffer = new BufferConstruction();

                IGeometry5 geometriaBuffer = operatoreBuffer.Buffer(identifyPoint, 1) as IGeometry5;

                IArea area = (IPolygon)geometriaBuffer as IArea;
                double dblArea = area.Area;

                IEnumLayer enumeratoreLayer = map.Layers;

                ILayer layer = enumeratoreLayer.Next();

                string nomeLayer = string.Empty;

                while (layer != null)
                {
                    nomeLayer = layer.Name;

                    if (layer.Visible && layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;

                        if (featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTSimpleJunction)
                        {
                            IIdentify identifyLayer = layer as IIdentify;
                            IArray array = identifyLayer.Identify(geometriaBuffer);

                            if (array != null)
                            {
                                object obj = array.get_Element(0);
                                IFeatureIdentifyObj fobj = obj as IFeatureIdentifyObj;
                                IRowIdentifyObject irow = fobj as IRowIdentifyObject;
                                featureSelez = irow.Row as IFeature;
                                newOID = featureSelez.OID;
                            }
                            else
                            {
                                newOID = -1;
                            }

                            try
                            {
                                if (newOID > -1 && oid != newOID)
                                {
                                    this.EvidenziaFeatureDinamicamente(featureLayer, newOID, activeView, true);
                                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, activeView.Extent);
                                    oid = featureSelez.OID; // Solo ora che ho evidenziato, aggiorno il oid per il confronto

                                    PuntoGiancaGISClass puntoGiancaGISClass = new PuntoGiancaGISClass();
                                    
                                    this.Punto = puntoGiancaGISClass as IPuntoGiancaGIS;
                                    this.Punto.ObjPuntoESRI = featureSelez.ShapeCopy as IPoint;
                                    this.Punto.RicavaGN();
                                    this.Punto.GN = ((INetworkFeature)featureSelez).GeometricNetwork;
                                    this.Punto.Feature = featureSelez;

                                    switch (featureLayer.FeatureClass.FeatureType)
                                    {
                                        case esriFeatureType.esriFTSimpleJunction:
                                            this.Punto.IsSimpleJunction = true;
                                            VariabiliGlobaliClass.DockableSelezione.ValorizzaFeatureLayerSimpleJunction(featureLayer);
                                            break;
                                        default:
                                            this.Punto.IsSimpleJunction = false;
                                            break;
                                    }
                                }
                            }

                            catch (Exception errore)
                            {
                                string err = errore.Message;
                            }
                        }
                    }

                    layer = enumeratoreLayer.Next();
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "ERRORE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Metodi per Graphics

        protected internal void EvidenziaFeatureDinamicamente(IGeometry geometriaFeature, IActiveView activeView,
            bool blnEliminaGraphicsEsistenti,
            esriSimpleMarkerStyle? stilePunto = esriSimpleMarkerStyle.esriSMSCircle,
            esriSimpleLineStyle? stileLinea = esriSimpleLineStyle.esriSLSSolid,
            esriSimpleFillStyle? stilePoligono = esriSimpleFillStyle.esriSFSForwardDiagonal)
        {
            try
            {
                IScreenDisplay3 screenDisplay = activeView.ScreenDisplay as IScreenDisplay3;

                if (geometriaFeature.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline poliLinea = geometriaFeature as IPolyline;
                }
                else if (geometriaFeature.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IPolygon poligono = geometriaFeature as IPolygon;
                }
                else if (geometriaFeature.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IPoint punto = geometriaFeature as IPoint;
                }

                IBufferConstruction operatoreBuffer = new BufferConstruction();

                IGeometry geometriaBuffer = operatoreBuffer.Buffer(geometriaFeature, 2);

                //ITopologicalOperator5 operatoreTopologico = geometriaFeature as ITopologicalOperator5;
                //IGeometry5 geometriaBuffer = operatoreTopologico.Buffer(1000) as IGeometry5;

                IRgbColor spazioColoreRGB = new RgbColor();
                Random random = new Random();
                spazioColoreRGB.Green = random.Next(0, 255);
                spazioColoreRGB.Blue = random.Next(0, 255);
                spazioColoreRGB.Red = random.Next(0, 255);

                this.AggiungiGraphicInMappa((ArcMap.Application.Document as IMxDocument).FocusMap, geometriaBuffer, spazioColoreRGB, 
                    spazioColoreRGB, blnEliminaGraphicsEsistenti, stilePunto, stileLinea, stilePoligono);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected internal void EvidenziaFeatureDinamicamente(IFeatureLayer layer, int featureOID, IActiveView activeView,
            bool blnEliminaGraphicsEsistenti,
            esriSimpleMarkerStyle? stilePunto = esriSimpleMarkerStyle.esriSMSCircle,
            esriSimpleLineStyle? stileLinea = esriSimpleLineStyle.esriSLSSolid,
            esriSimpleFillStyle? stilePoligono = esriSimpleFillStyle.esriSFSForwardDiagonal)
        {
            try
            {
                IScreenDisplay3 screenDisplay = activeView.ScreenDisplay as IScreenDisplay3;

                IFeatureClass featureClass = layer.FeatureClass;
                IFeature feature = featureClass.GetFeature(featureOID);
                IGeometry geometriaFeature = feature.ShapeCopy;

                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline poliLinea = geometriaFeature as IPolyline;
                }
                else if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IPolygon poligono = geometriaFeature as IPolygon;
                }
                else if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IPoint punto = geometriaFeature as IPoint;
                }

                IBufferConstruction operatoreBuffer = new BufferConstruction();

                IGeometry geometriaBuffer = operatoreBuffer.Buffer(geometriaFeature, 2);

                //ITopologicalOperator5 operatoreTopologico = geometriaFeature as ITopologicalOperator5;
                //IGeometry5 geometriaBuffer = operatoreTopologico.Buffer(1000) as IGeometry5;

                IRgbColor spazioColoreRGB = new RgbColor();
                Random random = new Random();
                spazioColoreRGB.Green = random.Next(0, 255);
                spazioColoreRGB.Blue = random.Next(0, 255);
                spazioColoreRGB.Red = random.Next(0, 255);

                this.AggiungiGraphicInMappa((ArcMap.Application.Document as IMxDocument).FocusMap, geometriaBuffer, spazioColoreRGB, 
                    spazioColoreRGB, blnEliminaGraphicsEsistenti, stilePunto, stileLinea, stilePoligono);
            }
            catch (Exception)
            {

                throw;
            }

        }

        ///<summary>Draw a specified graphic on the map using the supplied colors.</summary>
        ///      
        ///<param name="map">An IMap interface.</param>
        ///<param name="geometry">An IGeometry interface. It can be of the geometry type: esriGeometryPoint, esriGeometryPolyline, or esriGeometryPolygon.</param>
        ///<param name="rgbColor">An IRgbColor interface. The color to draw the geometry.</param>
        ///<param name="outlineRgbColor">An IRgbColor interface. For those geometry's with an outline it will be this color.</param>
        ///      
        ///<remarks>Calling this function will not automatically make the graphics appear in the map area. Refresh the map area after after calling this function with Methods like IActiveView.Refresh or IActiveView.PartialRefresh.</remarks>
        protected internal void AggiungiGraphicInMappa(IMap map, IGeometry geometry, IRgbColor rgbColor, IRgbColor outlineRgbColor, 
            bool blnEliminaGraphicsEsistenti, 
            esriSimpleMarkerStyle? stilePunto = esriSimpleMarkerStyle.esriSMSCircle, 
            esriSimpleLineStyle? stileLinea = esriSimpleLineStyle.esriSLSSolid,
            esriSimpleFillStyle? stilePoligono = esriSimpleFillStyle.esriSFSForwardDiagonal)
        {
            ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer = (ESRI.ArcGIS.Carto.IGraphicsContainer)map;
            
            if (blnEliminaGraphicsEsistenti)
                graphicsContainer.DeleteAllElements();

            ESRI.ArcGIS.Carto.IElement element = null;

            if (geometry.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
            {
                // Marker symbols
                ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol() as ISimpleMarkerSymbol;
                simpleMarkerSymbol.Color = rgbColor;
                simpleMarkerSymbol.Outline = true;
                simpleMarkerSymbol.OutlineColor = outlineRgbColor;
                simpleMarkerSymbol.Size = 15;
                if (stilePunto.HasValue)
                    simpleMarkerSymbol.Style = stilePunto.Value;

                ESRI.ArcGIS.Carto.IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
                markerElement.Symbol = simpleMarkerSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)markerElement;
            }
            else if (geometry.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
            {
                //  Line elements
                ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbol() as ISimpleLineSymbol;
                simpleLineSymbol.Color = rgbColor;
                if (stileLinea.HasValue)
                    simpleLineSymbol.Style = stileLinea.Value;
                simpleLineSymbol.Width = 5;

                ESRI.ArcGIS.Carto.ILineElement lineElement = new ESRI.ArcGIS.Carto.LineElement() as ILineElement;
                lineElement.Symbol = simpleLineSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)lineElement;
            }
            else if (geometry.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                // Polygon elements
                ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbol() as ISimpleFillSymbol;
                simpleFillSymbol.Color = rgbColor;
                if (stilePoligono.HasValue)
                    simpleFillSymbol.Style = stilePoligono.Value;
                ESRI.ArcGIS.Carto.IFillShapeElement fillShapeElement = new ESRI.ArcGIS.Carto.PolygonElement() as IFillShapeElement;
                fillShapeElement.Symbol = simpleFillSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)fillShapeElement;
            }
            if (element != null)
            {
                element.Geometry = geometry;
                graphicsContainer.AddElement(element, 0);
            }
        }

        #endregion

        protected override void OnUpdate()
        { }
    }

    public class ElementoLineare : ToolSelezionaElementoGN
    {
        private RaccogliInfoPerListView _spazzolaInfo = null;

        public ElementoLineare()
        {
            this.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        /// <summary>
        /// Scopo dell'AddIn è quello di impostare la manina (Pan) di ArcMap e spegnere il current tool
        /// </summary>
        public void SpegniTool()
        {
            UID panUID = new UIDClass
            {
                Value = VariabiliGlobaliClass.UID_PAN_ARCMAP
            }; // UID della manina (pan) di ArcMap

            ICommandItem commandItemPan = ArcMap.Application.Document.CommandBars.Find(panUID);

            IApplication application = ArcMap.Application;
            application.CurrentTool = commandItemPan;
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            try
            {
                IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxDocument.ActiveView;

                if (base.Polilinea.ObjLineaESRI != null)
                {
                    ToolSelezionaLinea toolSelezionaLinea = new ToolSelezionaLinea();
                    toolSelezionaLinea.FlashaFeature(base.Polilinea.Feature, ref activeView);

                    // Inizializzo le variabili per la status bar di ArcMap...
                    IStatusBar barraStato = ArcMap.Application.StatusBar;
                    barraStato.ProgressAnimation.Animation = esriAnimations.esriAnimationDrawing;

                    #region Simple Edge
                    if (!base.Polilinea.IsComplexEdge && base.Polilinea.Feature is ISimpleEdgeFeature) // SSE è una simple edge
                    {
                        _spazzolaInfo = RaccogliInfoPerListView.Instance();

                        avvisiWindow.InitLog(_spazzolaInfo);

                        VariabiliGlobaliClass.DockableSelezione.ValorizzaFcSimpleEdge((IFeatureClass)this.Polilinea.Feature.Class);

                        ISimpleEdgeFeature simpleEdgeFeature = base.Polilinea.Feature as ISimpleEdgeFeature;

                        int EID = simpleEdgeFeature.EID;

                        // convert the EID to a feature class ID, feature ID, and sub ID
                        INetElements netElements = base.Polilinea.GN.Network as INetElements;
                        netElements.QueryIDs(EID, esriElementType.esriETJunction, out int FCID, out int FID, out int subID);

                        Dictionary<string, long> diz = this.RicavaOIDJunctionConnesse(base.Polilinea.Feature, subID, ref mxDocument);

                        if (diz["from"] == -99)
                        {
                            MessageBox.Show($@"La Junction iniziale non è stata caricata in mappa per la tratta con ObjectID = {base.Polilinea.Feature.OID}", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (diz["to"] == -99)
                        {
                            MessageBox.Show($@"La Junction finale non è stata caricata in mappa per la tratta con ObjectID = {base.Polilinea.Feature.OID}", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        if (!VariabiliGlobaliClass.DockableSelezione.ValorePresenteInListViewSimpleEdge(base.Polilinea.Feature.OID.ToString()))
                            _spazzolaInfo.AggiungiInfoSimpleEdge(base.Polilinea.Feature.OID, FCID, EID,
                                diz["from"], diz["to"]);

                        barraStato.Message[0] = $@"EID ultimo oggetto selezionato = {EID}";
                    }
                    #endregion

                    #region Complex Edge
                    if (base.Polilinea.IsComplexEdge) // SSE è una simple edge
                    {
                        _spazzolaInfo = RaccogliInfoPerListView.Instance();

                        avvisiWindow.InitLog(_spazzolaInfo);

                        VariabiliGlobaliClass.DockableSelezione.ValorizzaFcComplexEdge((IFeatureClass)base.Polilinea.Feature.Class);

                        // convert the EID to a feature class ID, feature ID, and sub ID
                        INetElements netElements = this.Polilinea.GN.Network as INetElements;

                        List<int> listaEIDs = this.RicavaListaEIDsComplexEdgeFeatureInfo(base.Polilinea.Feature, netElements);

                        for (int contatore = 0; contatore < listaEIDs.Count; contatore++)
                        {
                            netElements.QueryIDs(listaEIDs[contatore], esriElementType.esriETEdge, out int FCID, out int FID, out int subID);

                            Dictionary<string, long> diz = this.RicavaOIDJunctionConnesse(this.Polilinea.Feature, subID, ref mxDocument);

                            if (!VariabiliGlobaliClass.DockableSelezione.ValorePresenteInListViewComplexEdge(listaEIDs[contatore].ToString()))
                                _spazzolaInfo.AggiungiInfoComplexEdge(base.Polilinea.Feature.OID, FCID, listaEIDs[contatore], subID,
                                    diz["from"], diz["to"]);

                            barraStato.Message[0] = $@"EID ultimo oggetto selezionato = {listaEIDs[contatore]}";
                        }

                    }
                    #endregion
                }

            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            }
            finally
            { }
        }

        private Dictionary<string, long> RicavaOIDJunctionConnesse(IFeature feature, int subID, ref IMxDocument mxDocument)
        {
            Dictionary<string, long> diz = new Dictionary<string, long>();

            int FCID, FID, subID_ex;

            try
            {
                if (feature is IEdgeFeature edgeFeature)
                {
                    IPolyline polyline = edgeFeature.GeometryForEdgeElement[subID] as IPolyline;

                    #region Punto Iniziale
                    IPoint puntoInziale = polyline.FromPoint;

                    IPointToEID pointToEID = new PointToEID() as IPointToEID;
                    pointToEID.GeometricNetwork = this.Polilinea.GN;
                    pointToEID.SourceMap = mxDocument.FocusMap;
                    pointToEID.SnapTolerance = 10;     //  set a snap tolerance of 10 map units
                    pointToEID.GetNearestJunction(puntoInziale, out int EID, out IPoint outPoint);

                    // convert the EID to a feature class ID, feature ID, and sub ID
                    INetElements netElements = this.Polilinea.GN.Network as INetElements;

                    if (EID == 0)
                    {
                        diz.Add("from", -99);
                    }
                    else
                    {
                        netElements.QueryIDs(EID, esriElementType.esriETJunction, out FCID, out FID, out subID_ex);
                        diz.Add("from", FID);
                    }

                    #endregion

                    #region Punto Finale
                    IPoint puntoFinale = polyline.ToPoint;
                    pointToEID = new PointToEID() as IPointToEID;
                    pointToEID.GeometricNetwork = this.Polilinea.GN;
                    pointToEID.SourceMap = mxDocument.FocusMap;
                    pointToEID.SnapTolerance = 10;     //  set a snap tolerance of 10 map units
                    pointToEID.GetNearestJunction(puntoFinale, out EID, out outPoint);

                    if (EID == 0)
                    {
                        diz.Add("to", -99);
                    }
                    else
                    {
                        netElements.QueryIDs(EID, esriElementType.esriETJunction, out FCID, out FID, out subID_ex);
                        diz.Add("to", FID);
                    }
                    #endregion
                }
            }
            catch (Exception)
            {

                throw;
            }

            return diz;
        }

        private List<int> RicavaListaEIDsComplexEdgeFeatureInfo(IFeature feature, INetElements netElements)
        {
            List<int> listaEIDs = new List<int>();

            try
            {
                if (feature is IComplexEdgeFeature complexEdgeFeature)
                {
                    IEnumNetEID enumNetEID = netElements.GetEIDs(feature.Class.ObjectClassID, feature.OID, esriElementType.esriETEdge);

                    int EID = enumNetEID.Next();

                    while (EID > 0)
                    {
                        if (netElements.IsValidElement(EID, esriElementType.esriETEdge))
                            listaEIDs.Add(EID);

                        EID = enumNetEID.Next();
                    }
                }
            }
            catch (Exception)
            {
            }
            return listaEIDs;
        }

        protected override void OnMouseMove(MouseEventArgs arg)
        {
            try
            {
                IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxDocument.ActiveView;

                this.EseguiSelezioneEdge(mxDocument, activeView, arg.X, arg.Y);
                
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            }
            finally
            { }
        }
    }

    public class ElementoPuntuale : ToolSelezionaElementoGN
    {
        private RaccogliInfoPerListView _spazzolaInfo = null;

        public ElementoPuntuale()
        {
            this.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        public void SpegniTool()
        {
            this.OnDeactivate();
        }
        protected override void OnMouseDown(MouseEventArgs arg)
        {
            try
            {
                IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxDocument.ActiveView;

                // Inizializzo le variabili per la status bar di ArcMap...
                IStatusBar barraStato = ArcMap.Application.StatusBar;
                barraStato.ProgressAnimation.Animation = esriAnimations.esriAnimationDrawing;

                if (base.Punto.ObjPuntoESRI != null)
                {
                    ToolSelezionaLinea toolSelezionaLinea = new ToolSelezionaLinea();
                    toolSelezionaLinea.FlashaFeature(base.Punto.Feature, ref activeView);

                    VariabiliGlobaliClass.DockableSelezione.ValorizzaFcSimpleJunction((IFeatureClass)base.Punto.Feature.Class);

                    _spazzolaInfo = RaccogliInfoPerListView.Instance();

                    avvisiWindow.InitLog(_spazzolaInfo);

                    // find the nearest junction element to this Point

                    IPointToEID pointToEID = new PointToEID() as IPointToEID;
                    pointToEID.GeometricNetwork = this.Punto.GN;
                    pointToEID.SourceMap = mxDocument.FocusMap;
                    pointToEID.SnapTolerance = 10;     //  set a snap tolerance of 10 map units
                    pointToEID.GetNearestJunction(base.Punto.ObjPuntoESRI, out int EID, out IPoint outPoint);

                    // Casto l'interfaccia nella classe che la implementa, per accedere alle coordinate.
                    PuntoGiancaGISClass puntoGiancaGISClass = base.Punto as PuntoGiancaGISClass;
                    double X_Coord = puntoGiancaGISClass.CoordX();
                    double Y_Coord = puntoGiancaGISClass.CoordY();

                    double longitudine = puntoGiancaGISClass.Longitudine();
                    double latitudine = puntoGiancaGISClass.Latitudine();

                    // convert the EID to a feature class ID, feature ID, and sub ID
                    INetElements netElements = base.Punto.GN.Network as INetElements;
                    netElements.QueryIDs(EID, esriElementType.esriETJunction, out int FCID, out int FID, out int subID);

                    if (!VariabiliGlobaliClass.DockableSelezione.ValorePresenteInListViewSimpleJunction(base.Punto.Feature.OID.ToString()))
                        _spazzolaInfo.AggiungiInfoSimpleJunction(this.Punto.Feature.OID, FCID, EID, X_Coord, Y_Coord, longitudine, latitudine);
                    

                    barraStato.Message[0] = $@"EID ultimo oggetto selezionato = {EID}";
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            }
            finally
            { }
        }

        protected override void OnMouseMove(MouseEventArgs arg)
        {
            try
            {
                IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxDocument.ActiveView;

                this.EseguiSelezioneJunction(mxDocument, activeView, arg.X, arg.Y);
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            }
            finally
            { }
        }
    }

}
