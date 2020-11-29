using AddInGeometricNetwork.Globali;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AddInGeometricNetwork.engine
{
    class EngineGNValidation
    {
        public List<string> VerificaTopologicalRule(IFeature feature, ref Dictionary<int, Dictionary<int, int>> dizInfoUtili)
        {
            List<string> avvisi = new List<string>();

            dizInfoUtili.Clear();

            try
            {
                //if (feature.FeatureType == esriFeatureType.esriFTComplexEdge)
                //{
                //    IComplexEdgeFeature edgeFeature = (IComplexEdgeFeature)feature;
                //    int numJunctionConnesse = edgeFeature.JunctionFeatureCount;

                //    IFeatureClass fcEdge = (IFeatureClass)feature.Class;

                //    IFeatureDataset featureDataset = fcEdge.FeatureDataset;
                //    IWorkspace workspace = featureDataset.Workspace;
                //    IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;

                //    IValidation2 validation = fcEdge as IValidation2;
                //    IRowSubtypes rowSubtypes = (IRowSubtypes)feature;

                //    int codSottotipo = rowSubtypes.SubtypeCode;

                //    IEnumRule enumRule = validation.RulesBySubtypeCode[codSottotipo];

                //    IRule rule = enumRule.Next();
                //    do
                //    {
                //        if (rule is IEdgeConnectivityRule junctionConnectivityRule)
                //        {
                //            if (numJunctionConnesse > junctionConnectivityRule.JunctionCount)
                //            {
                //                ISubtypes sottotipiFcEdge = (ISubtypes)fcEdge;
                //                string nomeSottotipoEdge = string.Empty;

                //                if (sottotipiFcEdge.HasSubtype)
                //                {
                //                    IEnumSubtype enumSubtypeEdge = sottotipiFcEdge.Subtypes;
                //                    nomeSottotipoEdge = enumSubtypeEdge.Next(out int sottotipoAnalizzatoEdge);

                //                    while (!string.IsNullOrEmpty(nomeSottotipoEdge))
                //                    {
                //                        if (sottotipoAnalizzatoEdge != junctionConnectivityRule.)
                //                        {
                //                            nomeSottotipoEdge = enumSubtypeEdge.Next(out sottotipoAnalizzatoEdge);
                //                        }
                //                        else
                //                        {
                //                            break;
                //                        }
                //                    }
                //                }
                //                else
                //                {
                //                    nomeSottotipoEdge = ((IDataset)fcEdge).Name;
                //                }
                //                avvisi.Add($"Max cardinalità violata per la Edge:{Environment.NewLine}{nomeSottotipoEdge}." +
                //                $"{Environment.NewLine}La max cardinalita' e' {junctionConnectivityRule.EdgeMaximumCardinality}");
                //            }
                //        }

                //        rule = enumRule.Next();
                //    }
                //    while (rule != null);

                //}
                if (feature.FeatureType == esriFeatureType.esriFTSimpleJunction)
                {
                    PuntoGiancaGISClass puntoGiancaGISClass = new PuntoGiancaGISClass
                    {
                        ObjPuntoESRI = feature.ShapeCopy as IPoint
                    };
                    puntoGiancaGISClass.RicavaGN();
                    puntoGiancaGISClass.GN = ((INetworkFeature)feature).GeometricNetwork;
                    puntoGiancaGISClass.Feature = feature;

                    //int EID = this.Punto.RicavaEID(mxdDoc.FocusMap);

                    IFeatureClass featureClass = (IFeatureClass)feature.Class;

                    IFeatureDataset featureDataset = featureClass.FeatureDataset;
                    IWorkspace workspace = featureDataset.Workspace;
                    IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;

                    IValidation2 validation = featureClass as IValidation2;
                    IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
                    
                    int codSottotipo = rowSubtypes.SubtypeCode;

                    IEnumRule enumRule = validation.RulesBySubtypeCode[codSottotipo];

                    IRule rule = enumRule.Next();

                    int maxCardinalitaJunction = -99;

                    ISimpleJunctionFeature simpleJunctionFeature = feature as ISimpleJunctionFeature;

                    Dictionary<int, int> infoAppoggio = new Dictionary<int, int>();

                    string nomeFc = string.Empty;

                    for (int i = 0; i < simpleJunctionFeature.EdgeFeatureCount; i++)
                    {
                        IEdgeFeature edgeTratta = simpleJunctionFeature.get_EdgeFeature(i); // Ricavo la tratta attaccata al punto...

                        // Creo Ifeature tratta attaccata al nodo editato...
                        IFeature featureTratta = (IFeature)edgeTratta;
                        IDataset dataset = (IDataset)featureTratta.Class;
                        nomeFc = dataset.Name;
                        IFeatureClass fcLinea = (IFeatureClass)dataset;

                        if (!infoAppoggio.ContainsKey(fcLinea.ObjectClassID))
                        {
                            infoAppoggio.Add(fcLinea.ObjectClassID, 1);
                        }
                        else
                        {
                            infoAppoggio[fcLinea.ObjectClassID] += 1;
                        }
                    }

                    if (!dizInfoUtili.ContainsKey(featureClass.ObjectClassID))
                        dizInfoUtili.Add(featureClass.ObjectClassID, infoAppoggio);

                    do
                    {
                       if (rule is IJunctionConnectivityRule junctionConnectivityRule)
                        {
                            maxCardinalitaJunction = junctionConnectivityRule.EdgeMaximumCardinality;

                            int sottotipoEdge = junctionConnectivityRule.EdgeSubtypeCode;
                            int junctionSubtype = junctionConnectivityRule.JunctionSubtypeCode;


                            if (maxCardinalitaJunction > 0)
                            {
                                if (dizInfoUtili.ContainsKey(junctionConnectivityRule.JunctionClassID))
                                {
                                    Dictionary<int, int> coppiaValoriEdgeConnessiJunction = dizInfoUtili[junctionConnectivityRule.JunctionClassID];

                                    if (coppiaValoriEdgeConnessiJunction.ContainsKey(junctionConnectivityRule.EdgeClassID)
                                        &&
                                        coppiaValoriEdgeConnessiJunction[junctionConnectivityRule.EdgeClassID] > maxCardinalitaJunction)
                                    {
                                        #region Ricavo info sui sottotipi e sui nomi dei sottotipi interessati
                                        IFeatureClass fcJunction = featureClassContainer.ClassByID[junctionConnectivityRule.JunctionClassID];
                                        IFeatureClass fcEdge = featureClassContainer.ClassByID[junctionConnectivityRule.EdgeClassID];

                                        // Ricavo info sui sottotipi interessati:
                                        ISubtypes sottotipiFcJunction = (ISubtypes)fcJunction;
                                        string nomeSottotipoJunction = string.Empty;

                                        if (sottotipiFcJunction.HasSubtype)
                                        {
                                            IEnumSubtype enumSubtypeJunction = sottotipiFcJunction.Subtypes;
                                            nomeSottotipoJunction = enumSubtypeJunction.Next(out int sottotipoAnalizzatoJunction);

                                            while (!string.IsNullOrEmpty(nomeSottotipoJunction))
                                            {
                                                if (sottotipoAnalizzatoJunction != junctionConnectivityRule.JunctionSubtypeCode)
                                                {
                                                    nomeSottotipoJunction = enumSubtypeJunction.Next(out sottotipoAnalizzatoJunction);
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            nomeSottotipoJunction = ((IDataset)fcJunction).Name;
                                        }

                                        ISubtypes sottotipiFcEdge = (ISubtypes)fcEdge;
                                        string nomeSottotipoEdge = string.Empty;

                                        if (sottotipiFcEdge.HasSubtype)
                                        {
                                            IEnumSubtype enumSubtypeEdge = sottotipiFcEdge.Subtypes;
                                            nomeSottotipoEdge = enumSubtypeEdge.Next(out int sottotipoAnalizzatoEdge);
                                            
                                            while (!string.IsNullOrEmpty(nomeSottotipoEdge))
                                            {
                                                if (sottotipoAnalizzatoEdge != junctionConnectivityRule.EdgeSubtypeCode)
                                                {
                                                    nomeSottotipoEdge = enumSubtypeEdge.Next(out sottotipoAnalizzatoEdge);
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            nomeSottotipoEdge = ((IDataset)fcEdge).Name;
                                        }

                                        #endregion
                                        avvisi.Add($"Max cardinalità violata tra la Junction:{Environment.NewLine}{nomeSottotipoJunction}{Environment.NewLine}e la Edge:{Environment.NewLine}{nomeSottotipoEdge}." +
                                            $"{Environment.NewLine}La max cardinalita' e' {maxCardinalitaJunction}");
                                    }
                                }
                            }
                        }
                        rule = enumRule.Next();
                    }
                    while (rule != null);

                }
            }
            catch (Exception)
            {

                throw;
            }

            return avvisi;
        }
    }
}
