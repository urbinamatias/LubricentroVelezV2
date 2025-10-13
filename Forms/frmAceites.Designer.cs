namespace LubricentroVelezV2.Forms
{
    partial class frmAceites
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAceites));
            lblAceites = new Label();
            btnAñadir = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            dgvAceites = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvAceites).BeginInit();
            SuspendLayout();
            // 
            // lblAceites
            // 
            lblAceites.AutoSize = true;
            lblAceites.Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            lblAceites.Location = new Point(29, 33);
            lblAceites.Name = "lblAceites";
            lblAceites.Size = new Size(84, 16);
            lblAceites.TabIndex = 0;
            lblAceites.Text = "Aceites";
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
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = SystemColors.ActiveCaption;
            btnEliminar.Cursor = Cursors.Hand;
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
            // 
            // dgvAceites
            // 
            dgvAceites.AllowUserToAddRows = false;
            dgvAceites.AllowUserToDeleteRows = false;
            dgvAceites.AllowUserToResizeColumns = false;
            dgvAceites.AllowUserToResizeRows = false;
            dgvAceites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAceites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAceites.BackgroundColor = SystemColors.AppWorkspace;
            dgvAceites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAceites.GridColor = SystemColors.Control;
            dgvAceites.Location = new Point(29, 61);
            dgvAceites.MultiSelect = false;
            dgvAceites.Name = "dgvAceites";
            dgvAceites.ReadOnly = true;
            dgvAceites.RowHeadersVisible = false;
            dgvAceites.ScrollBars = ScrollBars.Vertical;
            dgvAceites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAceites.Size = new Size(283, 325);
            dgvAceites.TabIndex = 5;
            // 
            // frmAceites
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 453);
            Controls.Add(dgvAceites);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAñadir);
            Controls.Add(lblAceites);
            MaximizeBox = false;
            Name = "frmAceites";
            Text = "Aceites";
            Load += frmAceites_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAceites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAceites;
        private Button btnAñadir;
        private Button btnEditar;
        private Button btnEliminar;
        private DataGridView dgvAceites;
    }
}