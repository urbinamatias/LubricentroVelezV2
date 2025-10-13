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
        private readonly IOrdenesService _service;
        private List<Aceites> _aceites = new List<Aceites>();
        public frmAceites(IOrdenesService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void frmAceites_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            _aceites = await _service.GetAceitesAsync();
            dgvAceites.DataSource = _aceites;
            dgvAceites.Columns["IdAceite"].Visible = false;
            dgvAceites.Columns["OrdenesTrabajos"].Visible = false;
            this.Cursor = Cursors.Default;
        }
    }
}
