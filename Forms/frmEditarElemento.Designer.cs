namespace LubricentroVelezV2.Forms
{
    partial class frmEditarElemento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditarElemento));
            lblCampo1 = new Label();
            lblCampo2 = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            txtCampo1 = new TextBox();
            txtCampo2 = new TextBox();
            SuspendLayout();
            // 
            // lblCampo1
            // 
            lblCampo1.AutoSize = true;
            lblCampo1.Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            lblCampo1.Location = new Point(25, 33);
            lblCampo1.Name = "lblCampo1";
            lblCampo1.Size = new Size(73, 16);
            lblCampo1.TabIndex = 1;
            lblCampo1.Text = "Label1";
            // 
            // lblCampo2
            // 
            lblCampo2.AutoSize = true;
            lblCampo2.Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            lblCampo2.Location = new Point(25, 113);
            lblCampo2.Name = "lblCampo2";
            lblCampo2.Size = new Size(73, 16);
            lblCampo2.TabIndex = 2;
            lblCampo2.Text = "Label2";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = SystemColors.ButtonFace;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Font = new Font("Lucida Console", 9.75F);
            btnGuardar.Image = (Image)resources.GetObject("btnGuardar.Image");
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(86, 174);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(106, 40);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.MiddleRight;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = SystemColors.ButtonFace;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Font = new Font("Lucida Console", 9.75F);
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(198, 174);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(106, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtCampo1
            // 
            txtCampo1.Font = new Font("Arial", 11.25F);
            txtCampo1.Location = new Point(25, 52);
            txtCampo1.Name = "txtCampo1";
            txtCampo1.Size = new Size(363, 25);
            txtCampo1.TabIndex = 0;
            // 
            // txtCampo2
            // 
            txtCampo2.Font = new Font("Arial", 11.25F);
            txtCampo2.Location = new Point(25, 132);
            txtCampo2.Name = "txtCampo2";
            txtCampo2.Size = new Size(363, 25);
            txtCampo2.TabIndex = 1;
            // 
            // frmEditarElemento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 226);
            Controls.Add(txtCampo2);
            Controls.Add(txtCampo1);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(lblCampo2);
            Controls.Add(lblCampo1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmEditarElemento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmElemento";
            Load += frmEditarElemento_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCampo1;
        private Label lblCampo2;
        private Button btnGuardar;
        private Button btnCancelar;
        private TextBox txtCampo1;
        private TextBox txtCampo2;
    }
}