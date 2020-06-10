using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System;
using System.Windows.Forms;


namespace AddInGeometricNetwork
{
    public class btnRimuoviGraphicsMappa : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public btnRimuoviGraphicsMappa()
        {
            this.Enabled = true;
        }

        protected override void OnClick()
        {
            try
            {
                this.RimuoviGraphics();
            }
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message, "Errore nella pulizia dei Graphics dalla ActiveView!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RimuoviGraphics()
        {
            try
            {
                IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxDocument.ActiveView;

                // Mi occupo di svuotare i Graphics
                activeView.GraphicsContainer.DeleteAllElements();
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, activeView.Extent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
