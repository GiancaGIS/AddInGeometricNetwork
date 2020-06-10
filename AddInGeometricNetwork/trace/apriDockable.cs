using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System;
using System.Windows.Forms;


namespace AddInGeometricNetwork
{
    public class apriDockable : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public apriDockable()
        {
        }

        protected override void OnClick()
        {
            try
            {
                UID dockableUID = new UIDClass();
                dockableUID.Value = ThisAddIn.IDs.DockableWindow;

                IDockableWindow dockable = ArcMap.DockableWindowManager.GetDockableWindow(dockableUID);
                
                dockable.Show(true);
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore apertura Dockable Trace!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
