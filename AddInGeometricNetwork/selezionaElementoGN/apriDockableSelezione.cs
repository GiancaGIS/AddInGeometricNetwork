using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Windows.Forms;

namespace AddInGeometricNetwork
{
    public class apriDockableSelezione : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public apriDockableSelezione()
        {
        }

        protected override void OnClick()
        {
            try
            {
                UID dockableUID = new UIDClass();
                dockableUID.Value = ThisAddIn.IDs.dockableSelezione;

                IDockableWindow dockable = ArcMap.DockableWindowManager.GetDockableWindow(dockableUID);

                dockable.Show(true);
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore apertura Dockable Selezione!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
