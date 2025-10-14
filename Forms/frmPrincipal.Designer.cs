namespace LubricentroVelezV2
{
    partial class frmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            btnNuevaOrden = new Button();
            btnNuevoAceite = new Button();
            btnNuevoAditivo = new Button();
            txtBuscar = new TextBox();
            dgvOrdenes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).BeginInit();
            SuspendLayout();
            // 
            // btnNuevaOrden
            // 
            btnNuevaOrden.BackColor = SystemColors.ActiveCaption;
            btnNuevaOrden.Cursor = Cursors.Hand;
            btnNuevaOrden.Font = new Font("Lucida Console", 9.75F);
            btnNuevaOrden.Image = (Image)resources.GetObject("btnNuevaOrden.Image");
            btnNuevaOrden.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevaOrden.Location = new Point(12, 12);
            btnNuevaOrden.Name = "btnNuevaOrden";
            btnNuevaOrden.Size = new Size(130, 40);
            btnNuevaOrden.TabIndex = 0;
            btnNuevaOrden.Text = "Nueva Orden";
            btnNuevaOrden.TextAlign = ContentAlignment.MiddleRight;
            btnNuevaOrden.UseVisualStyleBackColor = false;
            // 
            // btnNuevoAceite
            // 
            btnNuevoAceite.BackColor = SystemColors.ActiveCaption;
            btnNuevoAceite.Cursor = Cursors.Hand;
            btnNuevoAceite.Font = new Font("Lucida Console", 9.75F);
            btnNuevoAceite.Image = (Image)resources.GetObject("btnNuevoAceite.Image");
            btnNuevoAceite.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoAceite.Location = new Point(164, 12);
            btnNuevoAceite.Name = "btnNuevoAceite";
            btnNuevoAceite.Size = new Size(106, 40);
            btnNuevoAceite.TabIndex = 1;
            btnNuevoAceite.Text = "Aceites";
            btnNuevoAceite.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoAceite.UseVisualStyleBackColor = false;
            btnNuevoAceite.Click += btnNuevoAceite_Click;
            // 
            // btnNuevoAditivo
            // 
            btnNuevoAditivo.BackColor = SystemColors.ActiveCaption;
            btnNuevoAditivo.Cursor = Cursors.Hand;
            btnNuevoAditivo.Font = new Font("Lucida Console", 9.75F);
            btnNuevoAditivo.Image = (Image)resources.GetObject("btnNuevoAditivo.Image");
            btnNuevoAditivo.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevoAditivo.Location = new Point(294, 12);
            btnNuevoAditivo.Name = "btnNuevoAditivo";
            btnNuevoAditivo.Size = new Size(106, 40);
            btnNuevoAditivo.TabIndex = 2;
            btnNuevoAditivo.Text = "Aditivos";
            btnNuevoAditivo.TextAlign = ContentAlignment.MiddleRight;
            btnNuevoAditivo.UseVisualStyleBackColor = false;
            btnNuevoAditivo.Click += btnNuevoAditivo_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Arial", 11.25F);
            txtBuscar.Location = new Point(12, 73);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(170, 25);
            txtBuscar.TabIndex = 3;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvOrdenes
            // 
            dgvOrdenes.AllowUserToAddRows = false;
            dgvOrdenes.AllowUserToDeleteRows = false;
            dgvOrdenes.AllowUserToOrderColumns = true;
            dgvOrdenes.AllowUserToResizeRows = false;
            dgvOrdenes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenes.BackgroundColor = SystemColors.AppWorkspace;
            dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenes.GridColor = SystemColors.Control;
            dgvOrdenes.Location = new Point(12, 113);
            dgvOrdenes.MultiSelect = false;
            dgvOrdenes.Name = "dgvOrdenes";
            dgvOrdenes.ReadOnly = true;
            dgvOrdenes.RowHeadersVisible = false;
            dgvOrdenes.ScrollBars = ScrollBars.Vertical;
            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.Size = new Size(776, 325);
            dgvOrdenes.TabIndex = 4;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvOrdenes);
            Controls.Add(txtBuscar);
            Controls.Add(btnNuevoAditivo);
            Controls.Add(btnNuevoAceite);
            Controls.Add(btnNuevaOrden);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmPrincipal";
            Text = "Lubricentro Velez";
            WindowState = FormWindowState.Maximized;
            Load += frmPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNuevaOrden;
        private Button btnNuevoAceite;
        private Button btnNuevoAditivo;
        private TextBox txtBuscar;
        private DataGridView dgvOrdenes;
    }
}
