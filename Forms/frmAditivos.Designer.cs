namespace LubricentroVelezV2.Forms
{
    partial class frmAditivos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAditivos));
            lblAditivos = new Label();
            dgvAditivos = new DataGridView();
            btnAñadir = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAditivos).BeginInit();
            SuspendLayout();
            // 
            // lblAditivos
            // 
            lblAditivos.AutoSize = true;
            lblAditivos.Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            lblAditivos.Location = new Point(29, 33);
            lblAditivos.Name = "lblAditivos";
            lblAditivos.Size = new Size(95, 16);
            lblAditivos.TabIndex = 0;
            lblAditivos.Text = "Aditivos";
            // 
            // dgvAditivos
            // 
            dgvAditivos.AllowUserToAddRows = false;
            dgvAditivos.AllowUserToDeleteRows = false;
            dgvAditivos.AllowUserToResizeColumns = false;
            dgvAditivos.AllowUserToResizeRows = false;
            dgvAditivos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAditivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAditivos.BackgroundColor = SystemColors.AppWorkspace;
            dgvAditivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAditivos.GridColor = SystemColors.Control;
            dgvAditivos.Location = new Point(29, 61);
            dgvAditivos.MultiSelect = false;
            dgvAditivos.Name = "dgvAditivos";
            dgvAditivos.ReadOnly = true;
            dgvAditivos.RowHeadersVisible = false;
            dgvAditivos.ScrollBars = ScrollBars.Vertical;
            dgvAditivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAditivos.Size = new Size(283, 325);
            dgvAditivos.TabIndex = 1;
            // 
            // btnAñadir
            // 
            btnAñadir.BackColor = SystemColors.ActiveCaption;
            btnAñadir.Cursor = Cursors.Hand;
            btnAñadir.Font = new Font("Lucida Console", 9.75F);
            btnAñadir.Image = (Image)resources.GetObject("btnAñadir.Image");
            btnAñadir.ImageAlign = ContentAlignment.MiddleLeft;
            btnAñadir.Location = new Point(12, 401);
            btnAñadir.Name = "btnAñadir";
            btnAñadir.Size = new Size(106, 40);
            btnAñadir.TabIndex = 2;
            btnAñadir.Text = "Añadir";
            btnAñadir.TextAlign = ContentAlignment.MiddleRight;
            btnAñadir.UseVisualStyleBackColor = false;
            btnAñadir.Click += btnAñadir_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = SystemColors.ActiveCaption;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.Font = new Font("Lucida Console", 9.75F);
            btnEditar.Image = (Image)resources.GetObject("btnEditar.Image");
            btnEditar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditar.Location = new Point(124, 401);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(106, 40);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.TextAlign = ContentAlignment.MiddleRight;
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Font = new Font("Lucida Console", 9.75F);
            btnEliminar.Image = (Image)resources.GetObject("btnEliminar.Image");
            btnEliminar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminar.Location = new Point(236, 401);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(106, 40);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextAlign = ContentAlignment.MiddleRight;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // frmAditivos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 453);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAñadir);
            Controls.Add(dgvAditivos);
            Controls.Add(lblAditivos);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmAditivos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aditivos";
            Load += frmAditivos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAditivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAditivos;
        private DataGridView dgvAditivos;
        private Button btnAñadir;
        private Button btnEditar;
        private Button btnEliminar;
    }
}