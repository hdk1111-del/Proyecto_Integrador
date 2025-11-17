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
                Paso = double.Parse(txtDt.Text)
            };
        }

        private void DibujarSimulacion(Simulacion s)
        {
            chartSim.Series.Clear();
            var serie = new Series("Simulación")
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "main",
                BorderWidth = 2
            };

            double tau;
            List<double> t = new();
            List<double> y = new();

            if (s.TipoCircuito == "RC")
            {
                tau = s.R * s.C;
                for (double time = 0; time <= s.TiempoMaximo; time += s.Paso)
                {
                    double value = s.Modo == "Descarga"
                        ? s.V0 * Math.Exp(-time / tau)
                        : s.V * (1 - Math.Exp(-time / tau));
                    t.Add(time);
                    y.Add(value);
                }
            }
            else
            {
                tau = s.L / s.R;
                for (double time = 0; time <= s.TiempoMaximo; time += s.Paso)
                {
                    double value = s.Modo == "Apagado"
                        ? (s.V / s.R) * Math.Exp(-time / tau)
                        : (s.V / s.R) * (1 - Math.Exp(-time / tau));
                    t.Add(time);
                    y.Add(value);
                }
            }

            for (int i = 0; i < t.Count; i++)
                serie.Points.AddXY(t[i], y[i]);

            chartSim.Series.Add(serie);

            var area = chartSim.ChartAreas["main"];
            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = s.TipoCircuito == "RC" ? "Voltaje (V)" : "Corriente (A)";
            area.AxisX.StripLines.Clear();

            tau = s.TipoCircuito == "RC" ? s.R * s.C : s.L / s.R;
            AgregarLineaVertical(area, tau, "τ");
            AgregarLineaVertical(area, 2 * tau, "2τ");
            AgregarLineaVertical(area, 3 * tau, "3τ");

            
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
