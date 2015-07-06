using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid.Views.Base;

namespace SelectivasEnSucursales.GUIs
{
    public partial class FrmCarnicos : Form
    {
        private TimeSpan tiempoConsultaInicial, tiempoConsultaFinal;
        private string sError;
        private string sArchivoDeEscaneo;
        private List<SegConService.EtiquetasGrid> lstEtiquetas;

        // Creando componentes de impresión.
        PrintingSystem SistemaImpresion = new PrintingSystem();
        PrintableComponentLink ComponenteImpresion = new PrintableComponentLink();

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
                pbCargando.Visible = true;
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
            tiempoConsultaInicial = DateTime.Now.TimeOfDay;
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
                pbCargando.Visible = false;
            }
            else
            {
                MessageBox.Show(sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnConsultar.Enabled = true;
            tiempoConsultaFinal = DateTime.Now.TimeOfDay;
            TimeSpan tiempoTotal = tiempoConsultaFinal - tiempoConsultaInicial;
            lbltiempo.Text = "Tiempo de consulta: " + tiempoTotal.Seconds + " segundos.";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirGrid();
        }
        private void ImprimirGrid()
        {
            /******************************/
            // Creamos el Header
            PageHeaderArea Header = new PageHeaderArea();
            ComponenteImpresion.Images.Add(Image.FromFile(Environment.CurrentDirectory+"\\logomini.png"));
            Header.Content.AddRange(new string[] { "[Image 0]", Properties.Settings.Default.Sucursal, "[Time Printed]" });
            Header.LineAlignment = BrickAlignment.Far;
            /******************************/

            /******************************/
            //Creamos el Footer
            string izquierda = "Paginas: [Page # of Pages #]";
            string centro = "Usuario: [User Name]";
            string derecha = "Fecha: [Date Printed]";
            PageFooterArea Footer = new PageFooterArea();
            Footer.Content.AddRange(new string[] { izquierda, centro, derecha });
            Footer.LineAlignment = BrickAlignment.Near;
            /*****************************/

            /******************************/
            //Agregar el Grid al documento
            ComponenteImpresion.Component = gridEtiquetas;
            //Agregar el header y el footer al documento
            ComponenteImpresion.PageHeaderFooter = new PageHeaderFooter(Header, Footer);
            //Crear el documento
            ComponenteImpresion.CreateDocument(SistemaImpresion);
            //Mostrar la vista previa para imprimir
            ComponenteImpresion.ShowPreviewDialog();
        }
    }
}
