using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Forms;
using LubricentroVelezV2.Services.Implementation;
using System.ComponentModel;
using System.Windows.Forms;

namespace LubricentroVelezV2
{
    public partial class frmPrincipal : Form
    {
        private readonly OrdenesService _service;
        private string _lastSortedColumn = "";
        private bool _sortAscending = true;
        private List<OrdenesTrabajoDTO> _ordenes = new List<OrdenesTrabajoDTO>();
        private BindingSource _bindingSource = new BindingSource();
        private const string placeholder = "Patente...";
        private ListSortDirection _lastOrderDirection = ListSortDirection.Ascending;

        public frmPrincipal(OrdenesService service)
        {
            InitializeComponent();
            _service = service;
            dgvOrdenes.ColumnHeaderMouseClick += dgvOrdenes_ColumnHeaderMouseClick;
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null, dgvOrdenes, new object[] { true });

            dgvOrdenes.DataSource = _bindingSource;
            txtBuscar.GotFocus += txtBuscar_GotFocus;
            txtBuscar.LostFocus += txtBuscar_LostFocus;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            txtBuscar.KeyPress += txtBuscar_KeyPress;

            btnNuevaOrden.Click += btnNuevaOrden_Click;
            dgvOrdenes.CellDoubleClick += dgvOrdenes_CellDoubleClick;
        }

        private async void frmPrincipal_Load(object sender, EventArgs e)
        {
            await LoadOrdenesAsync();
        }

        private async Task LoadOrdenesAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            _ordenes = await _service.FillGridAsync();
            _bindingSource.DataSource = _ordenes;
            dgvOrdenes.Columns["IdOt"].HeaderText = "N° Orden";
            dgvOrdenes.Columns["Fecha"].HeaderText = "Fecha";
            dgvOrdenes.Columns["Patente"].HeaderText = "Patente";
            dgvOrdenes.Columns["Kilometraje"].HeaderText = "Kilometraje";
            dgvOrdenes.Columns["Propietario"].HeaderText = "Propietario";
            dgvOrdenes.Columns["Aceite"].HeaderText = "Aceite";
            dgvOrdenes.Columns["Aditivo"].HeaderText = "Aditivo";
            this.Cursor = Cursors.Default;

            SetPlaceHolder();
        }

        private void dgvOrdenes_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgvOrdenes.Columns[e.ColumnIndex].DataPropertyName;

            if (_lastSortedColumn == columnName)
            {
                _lastOrderDirection = _lastOrderDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                _lastSortedColumn = columnName;
                _lastOrderDirection = ListSortDirection.Ascending;
            }

            AplicarOrden();
        }
        private void AplicarOrden()
        {
            if (string.IsNullOrEmpty(_lastSortedColumn))
                return;

            var listaActual = ((List<OrdenesTrabajoDTO>)_bindingSource.DataSource);

            if (_lastOrderDirection == ListSortDirection.Ascending)
                _bindingSource.DataSource = listaActual
                    .OrderBy(o => o.GetType().GetProperty(_lastSortedColumn)?.GetValue(o))
                    .ToList();
            else
                _bindingSource.DataSource = listaActual
                    .OrderByDescending(o => o.GetType().GetProperty(_lastSortedColumn)?.GetValue(o))
                    .ToList();
        }

        private void SetPlaceHolder()
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.ForeColor = SystemColors.GrayText;
                txtBuscar.Text = placeholder;
            }
        }
        private void txtBuscar_GotFocus(object? sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholder)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }
        private void txtBuscar_LostFocus(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.ForeColor = Color.Gray;
                txtBuscar.Text = placeholder;

                _bindingSource.DataSource = _ordenes;

                if (!string.IsNullOrEmpty(_lastSortedColumn))
                    AplicarOrden();
            }
        }
        private void txtBuscar_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.ForeColor == Color.Gray || string.IsNullOrWhiteSpace(txtBuscar.Text) || txtBuscar.Text == placeholder)
            {
                if (_bindingSource.DataSource != _ordenes)
                    _bindingSource.DataSource = _ordenes;
                if (!string.IsNullOrEmpty(_lastSortedColumn))
                    AplicarOrden();

                return;
            }

            string texto = txtBuscar.Text.Trim().ToUpper();

            var filtradas = _ordenes
                .Where(o => !string.IsNullOrEmpty(o.Patente) && o.Patente.ToUpper().Contains(texto))
                .OrderByDescending(o => o.IdOt)
                .ToList();

            _bindingSource.DataSource = filtradas;
            if (!string.IsNullOrEmpty(_lastSortedColumn))
                AplicarOrden();
        }
        private void btnNuevaOrden_Click(object? sender, EventArgs e)
        {
            // Abre frmOrdenTrabajo en modo Nueva Orden
            frmOrdenTrabajo frmOrden = new(_service);
            frmOrden.DataChanged += async (s, ev) => await LoadOrdenesAsync();
            frmOrden.ShowDialog();
        }

        private void dgvOrdenes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar clic en el encabezado

            // Obtener el IdOt de la fila seleccionada
            var selectedRow = dgvOrdenes.Rows[e.RowIndex].DataBoundItem as OrdenesTrabajoDTO;

            if (selectedRow != null)
            {
                int idOt = selectedRow.IdOt;

                // Abre frmOrdenTrabajo en modo Ver/Editar
                frmOrdenTrabajo frmOrden = new(_service, idOt);
                frmOrden.DataChanged += async (s, ev) => await LoadOrdenesAsync();
                frmOrden.ShowDialog();
            }
        }

        private void btnNuevoAceite_Click(object sender, EventArgs e)
        {
            frmAceites frmAceites = new(_service);
            frmAceites.DataChanged += async (s, ev) => await LoadOrdenesAsync();
            frmAceites.ShowDialog();
        }

        private void btnNuevoAditivo_Click(object sender, EventArgs e)
        {
            frmAditivos frmAditivos = new(_service);
            frmAditivos.DataChanged += async (s, ev) => await LoadOrdenesAsync();
            frmAditivos.ShowDialog();
        }
    }
}
