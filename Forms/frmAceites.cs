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
    public partial class frmAceites : Form
    {
        public event EventHandler? DataChanged;
        private readonly IOrdenesService _service;
        private List<Aceites> _aceites = new List<Aceites>();
        public frmAceites(IOrdenesService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void frmAceites_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadAceitesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadAceitesAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            _aceites = await _service.GetAceitesAsync();
            dgvAceites.DataSource = _aceites;
            dgvAceites.Columns["IdAceite"].Visible = false;
            dgvAceites.Columns["OrdenesTrabajos"].Visible = false;
            this.Cursor = Cursors.Default;
        }

        private async void btnAñadir_Click(object sender, EventArgs e)
        {
            try
            {
                using var frm = new frmEditarElemento(_service, "Aceite");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadAceitesAsync();
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
            if (dgvAceites.CurrentRow == null) return;

            var seleccionado = dgvAceites.CurrentRow.DataBoundItem as Aceites;
            if (seleccionado == null) return;

            try
            {
                using var frm = new frmEditarElemento(_service, "Aceite", seleccionado);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadAceitesAsync();
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
            if (dgvAceites.CurrentRow == null) return;

            var seleccionado = dgvAceites.CurrentRow.DataBoundItem as Aceites;
            if (seleccionado == null) return;

            if (MessageBox.Show($"¿Seguro que desea eliminar '{seleccionado.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAceiteAsync(seleccionado.IdAceite);
                    await LoadAceitesAsync();
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
