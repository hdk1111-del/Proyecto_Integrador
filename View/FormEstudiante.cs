using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Proyecto_Integrador.Model;
using Proyecto_Integrador.Controller;

namespace Proyecto_Integrador.View
{
    public partial class FormEstudiante : Form
    {
        private readonly Usuario _usuario;
        private Label lblTau;
        private Label lblBienvenida;

        public FormEstudiante(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

         
            ConfigurarChart();

         
            lblBienvenida = new Label
            {
                Text = $"Bienvenido, {_usuario.NombreCompleto}  ({_usuario.Id})",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 10)
            };
            panelParams.Controls.Add(lblBienvenida);

          
            lblTau = new Label
            {
                Text = "τ = ",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true
            };
            panelParams.Controls.Add(lblTau);
            lblTau.Location = new Point(txtDt.Left, txtDt.Bottom + 10);

            
            cmbTipo.Items.AddRange(new[] { "RC", "RL" });
            cmbTipo.SelectedIndex = 0;
            ConfigurarModoSegunTipo();

            
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            btnSimular.Click += btnSimular_Click;
            btnGuardar.Click += btnGuardar_Click;

          
            btnVerCorrienteRC.Click += btnVerCorrienteRC_Click;
        }

        private void ConfigurarChart()
        {
            var area = chartSim.ChartAreas["main"];
            chartSim.Legends.Clear();

            chartSim.AntiAliasing = AntiAliasingStyles.All;
            chartSim.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            area.AxisX.LabelStyle.Format = "0.###";
            area.AxisY.LabelStyle.Format = "0.###";
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigurarModoSegunTipo();
        }
        private void ConfigurarModoSegunTipo()
        {
            cmbModo.Items.Clear();

            if (cmbTipo.Text == "RC")
            {
                cmbModo.Items.AddRange(new[] { "Carga", "Descarga" });
                cmbModo.SelectedIndex = 0;

                txtC.Enabled = true;
                txtL.Enabled = false;

                
                btnVerCorrienteRC.Enabled = true;
            }
            else  
            {
                cmbModo.Items.AddRange(new[] { "Encendido", "Apagado" });
                cmbModo.SelectedIndex = 0;

                txtC.Enabled = false;
                txtL.Enabled = true;

            
                btnVerCorrienteRC.Enabled = false;
            }
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            try
            {
                var sim = LeerFormulario();
                DibujarSimulacion(sim);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al simular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var sims = FileManager.LeerSimulaciones();
                var sim = LeerFormulario();
                sims.Add(sim);
                FileManager.GuardarSimulaciones(sims);
                MessageBox.Show("Simulación guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Simulacion LeerFormulario()
        {
            return new Simulacion
            {
                Usuario = _usuario.Nombre,
                NombreCompleto = _usuario.NombreCompleto,
                IdAlumno = _usuario.Id,
                TipoCircuito = cmbTipo.Text,
                Modo = cmbModo.Text,
                R = double.Parse(txtR.Text),
                C = double.TryParse(txtC.Text, out var cVal) ? cVal : 0,
                L = double.TryParse(txtL.Text, out var lVal) ? lVal : 0,
                V = double.Parse(txtV.Text),
                V0 = double.TryParse(txtV0.Text, out var v0Val) ? v0Val : 0,
                TiempoMaximo = double.Parse(txtTmax.Text),
                Paso = double.Parse(txtDt.Text),

                ComentarioEstudiante = txtComentarioEstudiante.Text
            };
        }

        private void DibujarSimulacion(Simulacion s)
        {
            var serie = chartSim.Series["Simulacion"];
            serie.Points.Clear();

            
            serie.ChartType = SeriesChartType.Spline;
            serie.BorderWidth = 3;
            serie.MarkerStyle = MarkerStyle.Circle;
            serie.MarkerSize = 4;

    
            if (s.TipoCircuito == "RC")
                serie.Color = Color.SteelBlue;
            else
                serie.Color = Color.IndianRed;

         
            double tau = s.TipoCircuito == "RC"
                ? s.R * s.C
                : s.L / s.R;

            if (tau <= 0)
            {
                MessageBox.Show("Los parámetros producen una constante de tiempo inválida.");
                return;
            }

            
            double tMaxGrafica = Math.Min(s.TiempoMaximo, 5 * tau);
            if (tMaxGrafica <= 0) tMaxGrafica = s.TiempoMaximo;
            if (tMaxGrafica <= 0) tMaxGrafica = 5 * tau;

       
            int n = 300;
            double dt = tMaxGrafica / n;

            double maxY = 0;

            for (double t = 0; t <= tMaxGrafica + dt / 2.0; t += dt)
            {
                double y;

                if (s.TipoCircuito == "RC")
                {
                    y = s.Modo == "Descarga"
                        ? s.V0 * Math.Exp(-t / tau)
                        : s.V * (1 - Math.Exp(-t / tau));
                }
                else 
                {
                    y = s.Modo == "Apagado"
                        ? (s.V / s.R) * Math.Exp(-t / tau)
                        : (s.V / s.R) * (1 - Math.Exp(-t / tau));
                }

                if (y > maxY) maxY = y;
                serie.Points.AddXY(t, y);
            }

            if (maxY <= 0) maxY = 1;

            
            var area = chartSim.ChartAreas["main"];

            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = s.TipoCircuito == "RC" ? "Voltaje (V)" : "Corriente (A)";

           
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = tMaxGrafica;
            area.AxisX.Interval = tMaxGrafica / 5.0;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = maxY * 1.1; 

            area.BackColor = Color.White;
            area.BackSecondaryColor = s.TipoCircuito == "RC"
                ? Color.FromArgb(230, 240, 255)   // azul suave
                : Color.FromArgb(255, 235, 235);  // rojo suave
            area.BackGradientStyle = GradientStyle.TopBottom;

        
            area.AxisX.StripLines.Clear();

           
            if (tau <= tMaxGrafica) AgregarLineaVertical(area, tau, "τ");
            if (2 * tau <= tMaxGrafica) AgregarLineaVertical(area, 2 * tau, "2τ");
            if (3 * tau <= tMaxGrafica) AgregarLineaVertical(area, 3 * tau, "3τ");

            lblTau.Text = $"τ = {tau:0.###} s";
        }


        private void btnVerCorrienteRC_Click(object sender, EventArgs e)
        {
            try
            {
                var sim = LeerFormulario();

                if (sim.TipoCircuito != "RC")
                {
                    MessageBox.Show(
                        "La gráfica de corriente solo aplica para circuitos RC.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DibujarCorrienteRC(sim);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al graficar la corriente: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DibujarCorrienteRC(Simulacion s)
        {
            // Aseguramos que es RC
            if (s.TipoCircuito != "RC")
                return;

            var area = chartSim.ChartAreas["main"];
            var serie = chartSim.Series["Simulacion"];

          
            serie.Points.Clear();

            
            serie.ChartType = SeriesChartType.Spline;
            serie.BorderWidth = 3;
            serie.MarkerStyle = MarkerStyle.Circle;
            serie.MarkerSize = 4;
            serie.Color = Color.SteelBlue;

            // 1️⃣ Constante de tiempo τ = R · C
            double tau = s.R * s.C;
            if (tau <= 0)
            {
                MessageBox.Show("Los parámetros dan una constante de tiempo no válida (τ ≤ 0).");
                return;
            }

           
            double tMaxGrafica = Math.Min(s.TiempoMaximo, 5.0 * tau);
            if (tMaxGrafica <= 0) tMaxGrafica = s.TiempoMaximo;
            if (tMaxGrafica <= 0) tMaxGrafica = 5.0 * tau;

           
            int nPuntos = 300;
            double dt = tMaxGrafica / nPuntos;

            double maxAbs = 0.0;

       
            for (double time = 0.0; time <= tMaxGrafica + dt / 2.0; time += dt)
            {
                double corriente;

                if (s.Modo == "Descarga")
                {
                    // Descarga: I(t) = -(V0/R) * e^(-t/RC)
                    corriente = -(s.V0 / s.R) * Math.Exp(-time / tau);
                }
                else 
                {
               
                    corriente = (s.V / s.R) * Math.Exp(-time / tau);
                }

                if (Math.Abs(corriente) > maxAbs)
                    maxAbs = Math.Abs(corriente);

                serie.Points.AddXY(time, corriente);
            }

            if (maxAbs <= 0) maxAbs = 1e-6;

            
            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = "Corriente (A)";

       
            area.BackColor = Color.White;
            area.BackSecondaryColor = Color.FromArgb(230, 240, 255);
            area.BackGradientStyle = GradientStyle.TopBottom;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;


            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = tMaxGrafica;
            area.AxisX.Interval = tMaxGrafica / 5.0;

            
            area.AxisY.Minimum = -maxAbs * 1.1;
            area.AxisY.Maximum = maxAbs * 1.1;

        
            area.AxisX.StripLines.Clear();
            if (tau <= tMaxGrafica) AgregarLineaVertical(area, tau, "τ");
            if (2.0 * tau <= tMaxGrafica) AgregarLineaVertical(area, 2.0 * tau, "2τ");
            if (3.0 * tau <= tMaxGrafica) AgregarLineaVertical(area, 3.0 * tau, "3τ");

            lblTau.Text = $"τ = {tau:0.###} s (corriente RC)";
        }

 

        private static void AgregarLineaVertical(ChartArea area, double x, string etiqueta)
        {
            var strip = new StripLine
            {
                IntervalOffset = x,
                StripWidth = 0.001,
                BackColor = Color.FromArgb(70, Color.Gray),
                Text = etiqueta
            };

            area.AxisX.StripLines.Add(strip);
        }
    }
}
