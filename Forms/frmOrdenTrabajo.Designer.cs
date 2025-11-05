namespace LubricentroVelezV2.Forms
{
    partial class frmOrdenTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenTrabajo));
            grpVehiculo = new GroupBox();
            SuspendLayout();
            // 
            // grpVehiculo
            // 
            grpVehiculo.Font = new Font("Lucida Console", 12F, FontStyle.Bold);
            grpVehiculo.Location = new Point(12, 12);
            grpVehiculo.Name = "grpVehiculo";
            grpVehiculo.Size = new Size(200, 100);
            grpVehiculo.TabIndex = 0;
            grpVehiculo.TabStop = false;
            grpVehiculo.Text = "Vehículo";
            // 
            // frmOrdenTrabajo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(739, 411);
            Controls.Add(grpVehiculo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmOrdenTrabajo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Orden";
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpVehiculo;
    }
}