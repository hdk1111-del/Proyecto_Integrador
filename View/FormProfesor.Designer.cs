namespace Proyecto_Integrador.View
{
    partial class FormProfesor
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelTop = new Panel();
            btnRefrescar = new Button();
            btnBuscar = new Button();
            txtFiltroUsuario = new TextBox();
            lblBuscar = new Label();
            cmbAlumnos = new ComboBox();
            lblAlumno = new Label();
            dgvSimulaciones = new DataGridView();
            panelRight = new Panel();
            chartDetalle = new System.Windows.Forms.DataVisualization.Charting.Chart();
            grpCalificar = new GroupBox();
            btnCalificar = new Button();
            txtComentario = new TextBox();
            lblComentario = new Label();
            txtNota = new TextBox();
            lblNota = new Label();
            lblResumen = new Label();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSimulaciones).BeginInit();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDetalle).BeginInit();
            grpCalificar.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnRefrescar);
            panelTop.Controls.Add(btnBuscar);
            panelTop.Controls.Add(txtFiltroUsuario);
            panelTop.Controls.Add(lblBuscar);
            panelTop.Controls.Add(cmbAlumnos);
            panelTop.Controls.Add(lblAlumno);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.MaximumSize = new Size(0, 50);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1078, 50);
            panelTop.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(746, 15);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(100, 32);
            btnRefrescar.TabIndex = 4;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(640, 15);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(90, 32);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtFiltroUsuario
            // 
            txtFiltroUsuario.Location = new Point(434, 15);
            txtFiltroUsuario.Name = "txtFiltroUsuario";
            txtFiltroUsuario.Size = new Size(200, 31);
            txtFiltroUsuario.TabIndex = 2;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(351, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(67, 25);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // cmbAlumnos
            // 
            cmbAlumnos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumnos.FormattingEnabled = true;
            cmbAlumnos.Location = new Point(84, 12);
            cmbAlumnos.Name = "cmbAlumnos";
            cmbAlumnos.Size = new Size(220, 33);
            cmbAlumnos.TabIndex = 1;
            cmbAlumnos.SelectedIndexChanged += cmbAlumnos_SelectedIndexChanged;
            // 
            // lblAlumno
            // 
            lblAlumno.AutoSize = true;
            lblAlumno.Location = new Point(10, 15);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Size = new Size(79, 25);
            lblAlumno.TabIndex = 0;
            lblAlumno.Text = "Alumno:";
            // 
            // dgvSimulaciones
            // 
            dgvSimulaciones.AllowUserToAddRows = false;
            dgvSimulaciones.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dgvSimulaciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSimulaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSimulaciones.Dock = DockStyle.Left;
            dgvSimulaciones.Location = new Point(0, 50);
            dgvSimulaciones.MaximumSize = new Size(540, 0);
            dgvSimulaciones.MultiSelect = false;
            dgvSimulaciones.Name = "dgvSimulaciones";
            dgvSimulaciones.ReadOnly = true;
            dgvSimulaciones.RowHeadersWidth = 62;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dgvSimulaciones.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvSimulaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSimulaciones.Size = new Size(540, 544);
            dgvSimulaciones.TabIndex = 1;
            dgvSimulaciones.DataBindingComplete += dgvSimulaciones_DataBindingComplete;
            // 
            // panelRight
            // 
            panelRight.BackColor = SystemColors.Info;
            panelRight.Controls.Add(chartDetalle);
            panelRight.Controls.Add(grpCalificar);
            panelRight.Controls.Add(lblResumen);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(540, 50);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(538, 544);
            panelRight.TabIndex = 2;
            // 
            // chartDetalle
            // 
            chartArea1.Name = "main";
            chartDetalle.ChartAreas.Add(chartArea1);
            chartDetalle.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartDetalle.Legends.Add(legend1);
            chartDetalle.Location = new Point(0, 0);
            chartDetalle.Name = "chartDetalle";
            series1.BorderWidth = 3;
            series1.ChartArea = "main";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Simulacion";
            chartDetalle.Series.Add(series1);
            chartDetalle.Size = new Size(538, 372);
            chartDetalle.TabIndex = 2;
            chartDetalle.Text = "chart1";
            // 
            // grpCalificar
            // 
            grpCalificar.Controls.Add(btnCalificar);
            grpCalificar.Controls.Add(txtComentario);
            grpCalificar.Controls.Add(lblComentario);
            grpCalificar.Controls.Add(txtNota);
            grpCalificar.Controls.Add(lblNota);
            grpCalificar.Dock = DockStyle.Bottom;
            grpCalificar.Location = new Point(0, 372);
            grpCalificar.Name = "grpCalificar";
            grpCalificar.Size = new Size(538, 150);
            grpCalificar.TabIndex = 1;
            grpCalificar.TabStop = false;
            grpCalificar.Text = "Calificar";
            // 
            // btnCalificar
            // 
            btnCalificar.Location = new Point(66, 93);
            btnCalificar.Name = "btnCalificar";
            btnCalificar.Size = new Size(184, 42);
            btnCalificar.TabIndex = 4;
            btnCalificar.Text = "Guardar calificación";
            btnCalificar.UseVisualStyleBackColor = true;
            btnCalificar.Click += btnCalificar_Click;
            // 
            // txtComentario
            // 
            txtComentario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtComentario.Location = new Point(293, 58);
            txtComentario.Multiline = true;
            txtComentario.Name = "txtComentario";
            txtComentario.ScrollBars = ScrollBars.Vertical;
            txtComentario.Size = new Size(200, 86);
            txtComentario.TabIndex = 3;
            // 
            // lblComentario
            // 
            lblComentario.AutoSize = true;
            lblComentario.Location = new Point(206, 30);
            lblComentario.Name = "lblComentario";
            lblComentario.Size = new Size(109, 25);
            lblComentario.TabIndex = 2;
            lblComentario.Text = "Comentario:";
            lblComentario.Click += lblComentario_Click;
            // 
            // txtNota
            // 
            txtNota.Location = new Point(110, 26);
            txtNota.Name = "txtNota";
            txtNota.Size = new Size(80, 31);
            txtNota.TabIndex = 1;
            // 
            // lblNota
            // 
            lblNota.AutoSize = true;
            lblNota.Location = new Point(15, 30);
            lblNota.Name = "lblNota";
            lblNota.Size = new Size(99, 25);
            lblNota.TabIndex = 0;
            lblNota.Text = "Nota (0–5):";
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Dock = DockStyle.Bottom;
            lblResumen.Location = new Point(0, 522);
            lblResumen.MaximumSize = new Size(0, 22);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 22);
            lblResumen.TabIndex = 0;
            lblResumen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormProfesor
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 594);
            Controls.Add(panelRight);
            Controls.Add(dgvSimulaciones);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormProfesor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel del Profesor";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSimulaciones).EndInit();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDetalle).EndInit();
            grpCalificar.ResumeLayout(false);
            grpCalificar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblBuscar;
        private Button btnRefrescar;
        private Button btnBuscar;
        private TextBox txtFiltroUsuario;
        private DataGridView dgvSimulaciones;
        private Panel panelRight;
        private Label lblResumen;
        private GroupBox grpCalificar;
        private Label lblNota;
        private Label lblComentario;
        private TextBox txtNota;
        private TextBox txtComentario;
        private Button btnCalificar;
      
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDetalle;
    }
}