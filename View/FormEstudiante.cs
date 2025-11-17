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
        private Usuario _usuario;
        private Panel panelChart;
        private Chart chartSim;
        private Label lblTau;
        private Label lblBienvenida;

        public FormEstudiante(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            // Crear panel y chart (no desaparece nunca)
            CrearChartPermanente();

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
        }

        private void CrearChartPermanente()
        {
            panelChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            Controls.Add(panelChart);
            panelChart.BringToFront();

            chartSim = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            var area = new ChartArea("main");
            chartSim.ChartAreas.Add(area);
            chartSim.Legends.Clear();

            // Mejora visual general
            chartSim.AntiAliasing = AntiAliasingStyles.All;
            chartSim.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            area.AxisX.LabelStyle.Format = "0.###";
            area.AxisY.LabelStyle.Format = "0.###";

            panelChart.Controls.Add(chartSim);
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
            }
            else
            {
                cmbModo.Items.AddRange(new[] { "Encendido", "Apagado" });
                cmbModo.SelectedIndex = 0;
                txtC.Enabled = false;
                txtL.Enabled = true;
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
            chartSim.Series.Clear();

            var serie = new Series("Simulación")
            {
                ChartType = SeriesChartType.Spline,   // curva suave
                ChartArea = "main",
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 4,
                IsValueShownAsLabel = false
            };

            // Color distinto según el tipo de circuito
            if (s.TipoCircuito == "RC")
            {
                serie.Color = Color.SteelBlue;
                serie.BorderColor = Color.SteelBlue;
            }
            else // RL
            {
                serie.Color = Color.IndianRed;
                serie.BorderColor = Color.IndianRed;
            }

            // 1️⃣ Constante de tiempo τ
            double tau = s.TipoCircuito == "RC"
                ? s.R * s.C
                : s.L / s.R;

            if (tau <= 0)
            {
                MessageBox.Show("Los parámetros dan una constante de tiempo no válida (τ <= 0).");
                return;
            }

            // 2️⃣ Rango de tiempo a graficar: hasta 5·τ o tmax, lo que sea menor
            double tMaxGrafica = Math.Min(s.TiempoMaximo, 5.0 * tau);
            if (tMaxGrafica <= 0) tMaxGrafica = s.TiempoMaximo;
            if (tMaxGrafica <= 0) tMaxGrafica = tau * 5; // último fallback

            // 3️⃣ Número de puntos y paso de simulación SOLO para la gráfica
            int nPuntos = 300;
            double dt = tMaxGrafica / nPuntos;

            // 4️⃣ Generar puntos directamente en la serie
            for (double time = 0.0; time <= tMaxGrafica + dt / 2.0; time += dt)
            {
                double valor;

                if (s.TipoCircuito == "RC")
                {
                    valor = s.Modo == "Descarga"
                        ? s.V0 * Math.Exp(-time / tau)
                        : s.V * (1 - Math.Exp(-time / tau));
                }
                else // RL
                {
                    valor = s.Modo == "Apagado"
                        ? (s.V / s.R) * Math.Exp(-time / tau)
                        : (s.V / s.R) * (1 - Math.Exp(-time / tau));
                }

                serie.Points.AddXY(time, valor);
            }

            chartSim.Series.Add(serie);

            // 🔧 Configuración de ejes
            var area = chartSim.ChartAreas["main"];
            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = s.TipoCircuito == "RC" ? "Voltaje (V)" : "Corriente (A)";

            // Fondo con degradado según tipo
            area.BackColor = Color.White;
            area.BackSecondaryColor = (s.TipoCircuito == "RC")
                ? Color.FromArgb(230, 240, 255)   // azul clarito
                : Color.FromArgb(255, 235, 235);  // rojo clarito
            area.BackGradientStyle = GradientStyle.TopBottom;

            // Rejilla suave
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            // 👉 Zoom al rango útil [0, tMaxGrafica]
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = tMaxGrafica;
            area.AxisX.Interval = tMaxGrafica / 5.0;

            // Escala del eje Y para que no se “aplane”
            double valorFinal = (s.TipoCircuito == "RC") ? s.V : (s.V / s.R);
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = valorFinal * 1.1; // 10% más arriba del valor final

            // Líneas τ, 2τ, 3τ solo si caben en el rango mostrado
            area.AxisX.StripLines.Clear();
            if (tau <= tMaxGrafica) AgregarLineaVertical(area, tau, "τ");
            if (2 * tau <= tMaxGrafica) AgregarLineaVertical(area, 2 * tau, "2τ");
            if (3 * tau <= tMaxGrafica) AgregarLineaVertical(area, 3 * tau, "3τ");

            lblTau.Text = $"τ = {tau:0.###} s";
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
