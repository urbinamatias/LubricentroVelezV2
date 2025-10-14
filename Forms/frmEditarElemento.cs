using LubricentroVelezV2.Models;
using LubricentroVelezV2.Services.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LubricentroVelezV2.Forms
{
    public partial class frmEditarElemento : Form
    {
        private readonly IOrdenesService _service;
        private readonly string _tipo; // "Aceite" o "Aditivo"
        private readonly object? _elemento; // Aceites o Aditivos
        private bool _esEdicion;

        public frmEditarElemento(IOrdenesService service, string tipo, object? elemento = null)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _service = service;
            _tipo = tipo;
            _elemento = elemento;
        }
        private void frmEditarElemento_Load(object sender, EventArgs e)
        {
            if (_tipo == "Aceite")
            {
                Text = "Nuevo Aceite";
                lblCampo1.Text = "Nombre:";
                lblCampo2.Text = "Marca:";
                txtCampo2.Visible = true;
            }
            else
            {
                Text = "Nuevo Aditivo";
                lblCampo1.Text = "Nombre:";
                lblCampo2.Visible = false;
                txtCampo2.Visible = false;
            }

            if (_elemento != null)
            {
                _esEdicion = true;

                if (_tipo == "Aceite" && _elemento is Aceites a)
                {
                    Text = "Editar Aceite";
                    txtCampo1.Text = a.Nombre;
                    txtCampo2.Text = a.Marca;
                }
                else if (_tipo == "Aditivo" && _elemento is Aditivos ad)
                {
                    Text = "Editar Aditivo";
                    txtCampo1.Text = ad.Nombre;
                }
            }
            else
            {
                _esEdicion = false;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCampo1.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.");
                return;
            }

            if (_tipo == "Aceite")
            {
                var aceite = _esEdicion && _elemento is Aceites existente
                    ? existente
                    : new Aceites();

                aceite.Nombre = txtCampo1.Text.Trim();
                aceite.Marca = txtCampo2.Text.Trim();

                if (_esEdicion)
                    await _service.UpdateAceiteAsync(aceite);
                else
                    await _service.AddAceiteAsync(aceite);
            }
            else
            {
                var aditivo = _esEdicion && _elemento is Aditivos existente
                    ? existente
                    : new Aditivos();

                aditivo.Nombre = txtCampo1.Text.Trim();

                if (_esEdicion)
                    await _service.UpdateAditivoAsync(aditivo);
                else
                    await _service.AddAditivoAsync(aditivo);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar? Los cambios no guardados se perderán.", "Confirmar Cancelación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
