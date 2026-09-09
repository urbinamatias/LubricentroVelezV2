using LubricentroVelezV2.Models;
using LubricentroVelezV2.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LubricentroVelezV2.Forms
{
    public partial class frmAditivos : Form
    {
        public event EventHandler? DataChanged;
        private readonly IOrdenesService _service;
        private List<Aditivos> _aditivos = new List<Aditivos>();
        public frmAditivos(IOrdenesService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void frmAditivos_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadAditivosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadAditivosAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            _aditivos = await _service.GetAditivosAsync();
            dgvAditivos.DataSource = _aditivos;
            dgvAditivos.Columns["IdAditivo"].Visible = false;
            dgvAditivos.Columns["OrdenesTrabajos"].Visible = false;
            this.Cursor = Cursors.Default;
        }

        private async void btnAñadir_Click(object sender, EventArgs e)
        {
            try
            {
                using var frm = new frmEditarElemento(_service, "Aditivo");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadAditivosAsync();
                    DataChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAditivos.CurrentRow == null) return;

            var seleccionado = dgvAditivos.CurrentRow.DataBoundItem as Aditivos;
            if (seleccionado == null) return;

            try
            {
                using var frm = new frmEditarElemento(_service, "Aditivo", seleccionado);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadAditivosAsync();
                    DataChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAditivos.CurrentRow == null) return;

            var seleccionado = dgvAditivos.CurrentRow.DataBoundItem as Aditivos;
            if (seleccionado == null) return;

            if (MessageBox.Show($"¿Seguro que desea eliminar '{seleccionado.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAditivoAsync(seleccionado.IdAditivo);
                    await LoadAditivosAsync();
                    DataChanged?.Invoke(this, EventArgs.Empty);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
