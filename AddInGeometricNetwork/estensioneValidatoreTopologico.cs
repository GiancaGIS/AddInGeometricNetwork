using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using AddInGeometricNetwork.Globali;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.esriSystem;
using AddInGeometricNetwork.engine;


namespace AddInGeometricNetwork
{
    public class estensioneValidatoreTopologico : ESRI.ArcGIS.Desktop.AddIns.Extension
    {
        // Creo gli oggetti editor e eventi_editing che utilizzerò per creare gli eventi on CreateFeature
        private IEditor editor;
        private IEditEvents_Event eventi_editing = null;

        private static estensioneValidatoreTopologico s_extension;

        private Dictionary<int, Dictionary<int, int>> dizInfoUtili = new Dictionary<int, Dictionary<int, int>>();

        private int EIDoggettoAnalizzato = -99;

        public estensioneValidatoreTopologico()
        {
        }

        protected IPuntoGiancaGIS Punto
        { get; set; }

        protected override void OnStartup()
        {
            s_extension = this;
            editor = LeggiEditorGIS(ArcMap.Application);
        }
        protected override void OnShutdown()
        {
        }

        public static IEditor3 LeggiEditorGIS(IApplication processoArcMap)
        {
            try
            {
                UID extensionID = new UIDClass
                {
                    Value = "esriEditor.Editor"
                };
                IExtension editExtension = processoArcMap.FindExtensionByCLSID(extensionID);
                IEditor3 editorGIS = editExtension as IEditor3;
                return editorGIS;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Questa funzione viene richiamata automaticamente, ogni volta che l'utente attiva o disattiva l'Extension
        /// nell'apposito menù di ArcMap!
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override bool OnSetState(ExtensionState state)
        {
            try
            {
                this.State = state;

                if (state == ExtensionState.Enabled) // Se l'estensione è abilitata allora leggi il config e aggancia gli eventi
                {
                    AperturaEventiEditing();
                    StatoEstensione.estensioneAttiva = true; // Salvo l'info in questo bool, che verrà poi sfruttato anche dal Bottone della Toolbar
                }
                else // Altrimenti deinizializza gli eventi!
                {
                    ChiusuraEventiEditing();
                    StatoEstensione.estensioneAttiva = false;
                }
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore aggancio eventi editing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return base.OnSetState(state);
        }


        private void AperturaEventiEditing()
        {
            try
            {
                eventi_editing = (IEditEvents_Event)editor;
                eventi_editing.OnCreateFeature += new IEditEvents_OnCreateFeatureEventHandler(Ascolta_Evento_OnCreateFeature);
                eventi_editing.OnSelectionChanged += new IEditEvents_OnSelectionChangedEventHandler(Ascolta_Evento_OnSelectionChanged);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Ascolta_Evento_OnSelectionChanged()
        {
            try
            {
                IDocument processoArcMap = ArcMap.Application.Document;
                IMxDocument mxdDoc = processoArcMap as IMxDocument;
                IMap mappa = mxdDoc.FocusMap;

                int intOggettiSelezionati = mappa.SelectionCount;

                if (intOggettiSelezionati > 1)
                {
                    return;
                }

                else if (intOggettiSelezionati == 0)
                {
                    return;
                }

                else
                {
                    ISelection selezione = mappa.FeatureSelection;
                    IEnumFeature enumSelezioneFeature = selezione as IEnumFeature;
                    IFeature fOggettoSelezionato = enumSelezioneFeature.Next();

                    int EID_onTheFly = 0;

                    if (fOggettoSelezionato is ISimpleEdgeFeature simpleEdgeFeature)
                    {
                        EID_onTheFly = simpleEdgeFeature.EID;
                    }
                    else if (fOggettoSelezionato is ISimpleJunctionFeature simpleJunctionFeature)
                    {
                        EID_onTheFly = simpleJunctionFeature.EID;
                    }

                    if (EID_onTheFly != this.EIDoggettoAnalizzato)
                    {

                        EngineGNValidation engineGNValidation = new EngineGNValidation();
                        List<string> listaErroriTopologici = engineGNValidation.VerificaTopologicalRule(fOggettoSelezionato, ref dizInfoUtili);

                        if (listaErroriTopologici.Count > 0)
                            MessageBox.Show(String.Join(" ", listaErroriTopologici), "Violazione regola topologica Geometric Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Ascolta_Evento_OnCreateFeature(IObject obj)
        {
            try
            {
                IFeature feature = (IFeature)obj;

                EngineGNValidation engineGNValidation = new EngineGNValidation();
                List<string> listaErroriTopologici = engineGNValidation.VerificaTopologicalRule(feature, ref dizInfoUtili);

                if (listaErroriTopologici.Count > 0)
                    MessageBox.Show(String.Join(" ", listaErroriTopologici));

                if (feature is ISimpleEdgeFeature simpleEdgeFeature)
                {
                    this.EIDoggettoAnalizzato = simpleEdgeFeature.EID;
                }
                else if (feature is ISimpleJunctionFeature simpleJunctionFeature)
                {
                    this.EIDoggettoAnalizzato = simpleJunctionFeature.EID;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Questa funzione, una volta richiamata mi permette di distruggere tutti gli eventi inizializzati
        /// </summary>
        private void ChiusuraEventiEditing()
        {
            try
            {
                if (eventi_editing != null) // OVVIAMENTE, distruggo l'evento onChangeFeature & Co., SSE prima è stato generato
                // l'oggetto eventi_editing! Ad esempio al primo avvio di ArcMap eventi_editing è ancora null!
                {
                    //eventi_editing.OnChangeFeature -= new IEditEvents_OnChangeFeatureEventHandler(Ascolta_Evento_OnChangeFeature);
                    eventi_editing.OnCreateFeature -= new IEditEvents_OnCreateFeatureEventHandler(Ascolta_Evento_OnCreateFeature);
                    //eventi_editing.OnDeleteFeature -= new IEditEvents_OnDeleteFeatureEventHandler(Ascolta_Evento_OnDeleteFeature);
                    eventi_editing.OnSelectionChanged -= new IEditEvents_OnSelectionChangedEventHandler(Ascolta_Evento_OnSelectionChanged);
                    eventi_editing = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore nella chiusura degli eventi di Editing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
