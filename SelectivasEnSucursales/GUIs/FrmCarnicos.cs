using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SelectivasEnSucursales.GUIs
{
    public partial class FrmCarnicos : Form
    {
        private string sError;
        private string sArchivoDeEscaneo;
        private List<SegConService.EtiquetasGrid> lstEtiquetas;

        public FrmCarnicos()
        {
            InitializeComponent();
        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {
            new FrmConfiguracionCarnicos().ShowDialog();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultarEtiquetas();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private string obtenerListaDeEtiquetetasDeEscaneo()
        {
            //Leer el archivo de escaneo
            string ArrayEtiquetas = string.Empty;

            try
            {                
                if (sArchivoDeEscaneo != null)
                {
                    StreamReader sr = new StreamReader(sArchivoDeEscaneo);
                    ArrayEtiquetas = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return ArrayEtiquetas;
        }
        private void ConsultarEtiquetas()
        {
            btnConsultar.Enabled = false;
            bgwConsulta.RunWorkerAsync();
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {            
            obtenerArchivoDeEscaneo();
        }
        private void obtenerArchivoDeEscaneo()
        {
            DialogResult DR = ofdEscaneo.ShowDialog();

            if (DR == DialogResult.OK)
            {
                sArchivoDeEscaneo = ofdEscaneo.FileName;
                txbRutaArchivoEscaneo.Text = ofdEscaneo.FileName;
            }
        }

        private void ShowException(Exception ex)
        {
            string TipoException = ex.GetType().ToString();
            StringBuilder SB = new StringBuilder();
            if (ex.InnerException != null)
                SB.AppendLine(ex.InnerException.Message);
            else
                SB.AppendLine(ex.Message);

            MessageBox.Show(SB.ToString(), TipoException, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gvEtiquetas_EndGrouping(object sender, EventArgs e)
        {
            //Expandir los grupos
            (sender as DevExpress.XtraGrid.Views.Grid.GridView).ExpandAllGroups();
        }

        private void bgwConsulta_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sEtiquetas = obtenerListaDeEtiquetetasDeEscaneo();

                SegConService.ServicioWeb Servicio = new SegConService.ServicioWeb();
                Servicio.Url = Properties.Settings.Default.URLWebService;

                lstEtiquetas = Servicio.ConsultarEtiquetas(sEtiquetas).ToList();
                sError = string.Empty;
            }
            catch (Exception ex)
            {
                sError = "Ocurrio un error con el Servicio Web: " + Environment.NewLine + ex.Message;
            }
        }
        private void bgwConsulta_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sError.Equals(string.Empty))
            {
                gridEtiquetas.DataSource = lstEtiquetas;
                gvEtiquetas.BestFitColumns();
            }
            else
            {
                MessageBox.Show(sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnConsultar.Enabled = true;
        }
    }
}
