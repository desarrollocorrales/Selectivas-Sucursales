using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SelectivasEnSucursales.GUIs
{
    public partial class FrmConfiguracionCarnicos : Form
    {
        public FrmConfiguracionCarnicos()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                webPreview.Url = new Uri(txbUrl.Text);
                webPreview.Update();
            }
            catch 
            {
                MessageBox.Show("Error en la URL...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmConfiguracionCarnicos_Load(object sender, EventArgs e)
        {
            txbUrl.Text = Properties.Settings.Default.URLWebService;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.URLWebService = txbUrl.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("La URL ha sido guardada con éxito!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
