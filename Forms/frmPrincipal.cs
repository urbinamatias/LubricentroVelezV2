using LubricentroVelezV2.DTOs;
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
        }

        private async void frmPrincipal_Load(object sender, EventArgs e)
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
            var data = (List<OrdenesTrabajoDTO>)_bindingSource.DataSource;

            if (_lastSortedColumn == columnName)
                _sortAscending = !_sortAscending;
            else
                _sortAscending = true;

            _lastSortedColumn = columnName;

            if (_sortAscending)
                dgvOrdenes.DataSource = data.OrderBy(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
            else
                dgvOrdenes.DataSource = data.OrderByDescending(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
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

                // restaurar vista original
                _bindingSource.DataSource = _ordenes;
            }
        }
        private void txtBuscar_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholder || txtBuscar.ForeColor == Color.Gray)
            {
                _bindingSource.DataSource = _ordenes;
                return;
            }
            if (txtBuscar.ForeColor == Color.Gray && txtBuscar.Text == placeholder)
            {
                _bindingSource.DataSource = _ordenes;
                return;
            }

            string texto = txtBuscar.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(texto))
            {
                // restaura la lista original y su orden
                _bindingSource.DataSource = _ordenes;
                return;
            }

            // filtrado seguro: chequea que Patente no sea null
            var filtradas = _ordenes
                .Where(o => !string.IsNullOrEmpty(o.Patente) && o.Patente.ToUpper().Contains(texto))
                .OrderByDescending(o => o.IdOt) // opcional: mantener orden descendente por IdOt
                .ToList();

            _bindingSource.DataSource = filtradas;
        }
    }
}
