namespace Proyecto_Integrador.View
{
    partial class FormEstudiante
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
            panelParams = new Panel();
            btnGuardar = new Button();
            btnSimular = new Button();
            txtDt = new TextBox();
            lblDt = new Label();
            txtTmax = new TextBox();
            lblTmax = new Label();
            txtV0 = new TextBox();
            lblV0 = new Label();
            txtV = new TextBox();
            lblV = new Label();
            txtL = new TextBox();
            lblL = new Label();
            txtC = new TextBox();
            lblC = new Label();
            txtR = new TextBox();
            lblR = new Label();
            cmbModo = new ComboBox();
            lblModo = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            panelParams.SuspendLayout();
            SuspendLayout();
            // 
            // panelParams
            // 
            panelParams.Controls.Add(btnGuardar);
            panelParams.Controls.Add(btnSimular);
            panelParams.Controls.Add(txtDt);
            panelParams.Controls.Add(lblDt);
            panelParams.Controls.Add(txtTmax);
            panelParams.Controls.Add(lblTmax);
            panelParams.Controls.Add(txtV0);
            panelParams.Controls.Add(lblV0);
            panelParams.Controls.Add(txtV);
            panelParams.Controls.Add(lblV);
            panelParams.Controls.Add(txtL);
            panelParams.Controls.Add(lblL);
            panelParams.Controls.Add(txtC);
            panelParams.Controls.Add(lblC);
            panelParams.Controls.Add(txtR);
            panelParams.Controls.Add(lblR);
            panelParams.Controls.Add(cmbModo);
            panelParams.Controls.Add(lblModo);
            panelParams.Controls.Add(cmbTipo);
            panelParams.Controls.Add(lblTipo);
            panelParams.Dock = DockStyle.Left;
            panelParams.Location = new Point(0, 0);
            panelParams.Name = "panelParams";
            panelParams.Size = new Size(327, 571);
            panelParams.TabIndex = 0;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(172, 461);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(112, 34);
            btnGuardar.TabIndex = 19;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnSimular
            // 
            btnSimular.Location = new Point(44, 461);
            btnSimular.Name = "btnSimular";
            btnSimular.Size = new Size(112, 34);
            btnSimular.TabIndex = 18;
            btnSimular.Text = "Simular";
            btnSimular.UseVisualStyleBackColor = true;
            // 
            // txtDt
            // 
            txtDt.Location = new Point(177, 381);
            txtDt.Name = "txtDt";
            txtDt.Size = new Size(116, 31);
            txtDt.TabIndex = 17;
            txtDt.Text = "0.01";
            // 
            // lblDt
            // 
            lblDt.AutoSize = true;
            lblDt.Location = new Point(3, 381);
            lblDt.Name = "lblDt";
            lblDt.Size = new Size(163, 25);
            lblDt.TabIndex = 16;
            lblDt.Text = "Paso de tiempo (s):";
            // 
            // txtTmax
            // 
            txtTmax.Location = new Point(177, 329);
            txtTmax.Name = "txtTmax";
            txtTmax.Size = new Size(111, 31);
            txtTmax.TabIndex = 15;
            txtTmax.Text = "5";
            // 
            // lblTmax
            // 
            lblTmax.AutoSize = true;
            lblTmax.Location = new Point(3, 329);
            lblTmax.Name = "lblTmax";
            lblTmax.Size = new Size(168, 25);
            lblTmax.TabIndex = 14;
            lblTmax.Text = "Tiempo máximo (s):";
            // 
            // txtV0
            // 
            txtV0.Location = new Point(172, 283);
            txtV0.Name = "txtV0";
            txtV0.Size = new Size(121, 31);
            txtV0.TabIndex = 13;
            txtV0.Text = "0";
            // 
            // lblV0
            // 
            lblV0.AutoSize = true;
            lblV0.Location = new Point(3, 286);
            lblV0.Name = "lblV0";
            lblV0.Size = new Size(170, 25);
            lblV0.TabIndex = 12;
            lblV0.Text = "Condición inicial (V):";
            // 
            // txtV
            // 
            txtV.Location = new Point(157, 237);
            txtV.Name = "txtV";
            txtV.Size = new Size(126, 31);
            txtV.TabIndex = 11;
            txtV.Text = "5";
            // 
            // lblV
            // 
            lblV.AutoSize = true;
            lblV.Location = new Point(44, 240);
            lblV.Name = "lblV";
            lblV.Size = new Size(95, 25);
            lblV.TabIndex = 10;
            lblV.Text = "Voltaje (V):";
            // 
            // txtL
            // 
            txtL.Location = new Point(151, 187);
            txtL.Name = "txtL";
            txtL.Size = new Size(150, 31);
            txtL.TabIndex = 9;
            txtL.Text = "0.5";
            // 
            // lblL
            // 
            lblL.AutoSize = true;
            lblL.Location = new Point(10, 187);
            lblL.Name = "lblL";
            lblL.Size = new Size(134, 25);
            lblL.TabIndex = 8;
            lblL.Text = "Inductancia (H):";
            // 
            // txtC
            // 
            txtC.Location = new Point(151, 143);
            txtC.Name = "txtC";
            txtC.Size = new Size(150, 31);
            txtC.TabIndex = 7;
            txtC.Text = "0.00047";
            // 
            // lblC
            // 
            lblC.AutoSize = true;
            lblC.Location = new Point(10, 143);
            lblC.Name = "lblC";
            lblC.Size = new Size(138, 25);
            lblC.TabIndex = 6;
            lblC.Text = "Capacitancia (F):";
            // 
            // txtR
            // 
            txtR.Location = new Point(112, 12);
            txtR.Name = "txtR";
            txtR.Size = new Size(208, 31);
            txtR.TabIndex = 5;
            txtR.Text = "1000";
            // 
            // lblR
            // 
            lblR.AutoSize = true;
            lblR.Location = new Point(9, 104);
            lblR.Name = "lblR";
            lblR.Size = new Size(130, 25);
            lblR.TabIndex = 4;
            lblR.Text = "Resistencia (Ω):";
            // 
            // cmbModo
            // 
            cmbModo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModo.FormattingEnabled = true;
            cmbModo.Location = new Point(138, 53);
            cmbModo.Name = "cmbModo";
            cmbModo.Size = new Size(182, 33);
            cmbModo.TabIndex = 3;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Location = new Point(44, 56);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(65, 25);
            lblModo.TabIndex = 2;
            lblModo.Text = "Modo:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(138, 96);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(182, 33);
            cmbTipo.TabIndex = 1;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(3, 15);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(113, 25);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "Circuito tipo:";
            // 
            // FormEstudiante
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 571);
            Controls.Add(panelParams);
            Name = "FormEstudiante";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulaciones – Estudiante";
            panelParams.ResumeLayout(false);
            panelParams.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelParams;
        private Label lblModo;
        private ComboBox cmbTipo;
        private Label lblTipo;
        private Label lblC;
        private TextBox txtR;
        private Label lblR;
        private ComboBox cmbModo;
        private Label lblL;
        private TextBox txtC;
        private TextBox txtV;
        private Label lblV;
        private TextBox txtL;
        private TextBox txtTmax;
        private Label lblTmax;
        private TextBox txtV0;
        private Label lblV0;
        private Button btnGuardar;
        private Button btnSimular;
        private TextBox txtDt;
        private Label lblDt;
    }
}