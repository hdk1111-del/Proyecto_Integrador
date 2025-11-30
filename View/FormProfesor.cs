using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Proyecto_Integrador.Controller;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.View
{
    public partial class FormProfesor : Form
    {
        private readonly Usuario _usuario;

        private List<Simulacion> _todas = new();
        private List<Calificacion> _califs = new();

        private ComboBox cmbAlumnos;
        private Label lblAlumno;

        public FormProfesor(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Text = $"Panel del Profesor – {_usuario.Nombre}";
            StartPosition = FormStartPosition.CenterScreen;

            txtComentario.ReadOnly = true;

            dgvSimulaciones.AutoGenerateColumns = true;
            dgvSimulaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSimulaciones.MultiSelect = false;
            dgvSimulaciones.ReadOnly = true;

            AsegurarComboAlumnos();
            ConfigurarChart();

            dgvSimulaciones.SelectionChanged += (_, __) => MostrarSeleccion();
            dgvSimulaciones.DataBindingComplete += dgvSimulaciones_DataBindingComplete;

            btnBuscar.Click += (_, __) => AplicarFiltros();
            btnRefrescar.Click += (_, __) =>
            {
                txtFiltroUsuario.Clear();
                cmbAlumnos.SelectedIndex = 0;
                AplicarFiltros();
            };

            cmbAlumnos.SelectedIndexChanged += (_, __) => AplicarFiltros();
            txtFiltroUsuario.TextChanged += (_, __) => AplicarFiltros();

            btnCalificar.Click += btnCalificar_Click;

            CargarSimulaciones();
        }

        private void ConfigurarChart()
        {
            var area = chartDetalle.ChartAreas["main"];
            chartDetalle.Legends.Clear();

            chartDetalle.AntiAliasing = AntiAliasingStyles.All;
            chartDetalle.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            area.AxisX.LabelStyle.Format = "0.###";
            area.AxisY.LabelStyle.Format = "0.###";
        }

        private void AsegurarComboAlumnos()
        {
            var existente = this.Controls.Find("cmbAlumnos", true).FirstOrDefault() as ComboBox;
            if (existente != null)
            {
                cmbAlumnos = existente;
                return;
            }

            var top = this.Controls.Find("panelTop", true).FirstOrDefault() as Panel;
            if (top == null)
            {
                top = new Panel { Name = "panelTop", Dock = DockStyle.Top, Height = 50 };
                Controls.Add(top);
                top.BringToFront();
            }

            lblAlumno = new Label
            {
                Name = "lblAlumno",
                Text = "Alumno:",
                AutoSize = true,
                Location = new Point(10, 15)
            };
            top.Controls.Add(lblAlumno);

            cmbAlumnos = new ComboBox
            {
                Name = "cmbAlumnos",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 220,
                Location = new Point(lblAlumno.Right + 6, 10)
            };
            top.Controls.Add(cmbAlumnos);

            if (this.Controls.Find("txtFiltroUsuario", true).FirstOrDefault() is TextBox txtFiltro)
            {
                txtFiltro.Left = cmbAlumnos.Right + 12;
            }
            if (this.Controls.Find("btnBuscar", true).FirstOrDefault() is Button btnBuscar)
            {
                btnBuscar.Left = (this.Controls.Find("txtFiltroUsuario", true).FirstOrDefault() as TextBox)?.Right + 8 ?? (cmbAlumnos.Right + 12);
            }
            if (this.Controls.Find("btnRefrescar", true).FirstOrDefault() is Button btnRefrescar)
            {
                btnRefrescar.Left = (this.Controls.Find("btnBuscar", true).FirstOrDefault() as Button)?.Right + 8 ?? (cmbAlumnos.Right + 100);
            }
        }

        private void CargarSimulaciones()
        {
            _todas = FileManager.LeerSimulaciones();
            _califs = FileManager.LeerCalificaciones();

            var alumnos = _todas
                .Select(s => s.Usuario ?? "")
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Distinct()
                .OrderBy(u => u)
                .ToList();

            alumnos.Insert(0, "(Todos)");
            cmbAlumnos.DataSource = alumnos;

            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            var elegido = cmbAlumnos?.SelectedItem as string ?? "(Todos)";
            var texto = (txtFiltroUsuario?.Text ?? "").Trim();

            IEnumerable<Simulacion> lista = _todas;

            if (elegido != "(Todos)")
                lista = lista.Where(s => string.Equals(s.Usuario, elegido, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(texto))
                lista = lista.Where(s => (s.Usuario ?? "").Contains(texto, StringComparison.OrdinalIgnoreCase));

            var result = lista.OrderByDescending(s => s.Fecha).ToList();

            dgvSimulaciones.DataSource = new BindingList<Simulacion>(result);
            lblResumen.Text = $"{result.Count} simulación(es) encontradas.";

            AjustarColumnas();
        }

        private void AjustarColumnas()
        {
            if (dgvSimulaciones.Columns["Id"] != null)
                dgvSimulaciones.Columns["Id"].Visible = false;

            if (dgvSimulaciones.Columns["IdAlumno"] != null)
                dgvSimulaciones.Columns["IdAlumno"].HeaderText = "Matrícula";

            if (dgvSimulaciones.Columns["Usuario"] != null)
                dgvSimulaciones.Columns["Usuario"].HeaderText = "Usuario";

            if (dgvSimulaciones.Columns["NombreCompleto"] != null)
                dgvSimulaciones.Columns["NombreCompleto"].HeaderText = "Nombre completo";
        }

        private bool EstaCalificada(Simulacion s)
            => _califs.Any(c => c.SimulacionId == s.Id);

        private void dgvSimulaciones_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!dgvSimulaciones.Columns.Cast<DataGridViewColumn>().Any(c => c.Name == "Estado"))
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "Estado",
                    ReadOnly = true
                };
                dgvSimulaciones.Columns.Add(col);
            }

            foreach (DataGridViewRow row in dgvSimulaciones.Rows)
            {
                if (row.DataBoundItem is not Simulacion sim) continue;

                bool calif = EstaCalificada(sim);
                row.Cells["Estado"].Value = calif ? "Calificada" : "Pendiente";

                if (calif)
                {
                    row.DefaultCellStyle.BackColor = Color.Honeydew;
                    row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dgvSimulaciones.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvSimulaciones.DefaultCellStyle.ForeColor;
                }
            }
        }

        private void MostrarSeleccion()
        {
            var sim = dgvSimulaciones.CurrentRow?.DataBoundItem as Simulacion;
            if (sim == null) return;

            lblResumen.Text = Resumen(sim);
            DibujarSimulacion(sim);

            txtComentario.Text = sim.ComentarioEstudiante ?? "";

            var nombre = string.IsNullOrWhiteSpace(sim.NombreCompleto)
                ? sim.Usuario
                : sim.NombreCompleto;

            lblComentario.Text = $"Comentario de {nombre}:";

            var calif = _califs.FirstOrDefault(c => c.SimulacionId == sim.Id);
            if (calif != null)
                txtNota.Text = calif.Nota.ToString("0.0#");
            else
                txtNota.Clear();
        }

        private static string Resumen(Simulacion s)
        {
            return s.TipoCircuito == "RC"
                ? $"{s.Usuario} | RC | R={s.R}Ω, C={s.C}F, V={s.V}, V0={s.V0}, Modo={s.Modo}"
                : $"{s.Usuario} | RL | R={s.R}Ω, L={s.L}H, V={s.V}, Modo={s.Modo}";
        }

        private void DibujarSimulacion(Simulacion s)
        {
            var area = chartDetalle.ChartAreas["main"];
            var serie = chartDetalle.Series["Simulacion"];

            serie.Points.Clear();

            
            serie.ChartType = SeriesChartType.Spline;
            serie.BorderWidth = 3;
            serie.MarkerStyle = MarkerStyle.Circle;
            serie.MarkerSize = 5;

            if (s.TipoCircuito == "RC")
            {
                serie.Color = Color.SteelBlue;
                serie.BorderColor = Color.SteelBlue;
            }
            else
            {
                serie.Color = Color.IndianRed;
                serie.BorderColor = Color.IndianRed;
            }

            double tau = s.TipoCircuito == "RC"
                ? s.R * s.C
                : s.L / s.R;

            if (tau <= 0)
            {
                MessageBox.Show("Los parámetros dan una constante de tiempo no válida (τ <= 0).");
                return;
            }

            double tMaxGrafica = Math.Min(s.TiempoMaximo, 5.0 * tau);
            if (tMaxGrafica <= 0) tMaxGrafica = s.TiempoMaximo;
            if (tMaxGrafica <= 0) tMaxGrafica = tau * 5;

            int nPuntos = 300;
            double dt = tMaxGrafica / nPuntos;

            double maxValor = 0.0;

            for (double time = 0.0; time <= tMaxGrafica + dt / 2.0; time += dt)
            {
                double valor;

                if (s.TipoCircuito == "RC")
                {
                    valor = s.Modo == "Descarga"
                        ? s.V0 * Math.Exp(-time / tau)
                        : s.V * (1 - Math.Exp(-time / tau));
                }
                else
                {
                    valor = s.Modo == "Apagado"
                        ? (s.V / s.R) * Math.Exp(-time / tau)
                        : (s.V / s.R) * (1 - Math.Exp(-time / tau));
                }

                if (valor > maxValor) maxValor = valor;

                serie.Points.AddXY(time, valor);
            }

            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = s.TipoCircuito == "RC" ? "Voltaje (V)" : "Corriente (A)";

            area.BackColor = Color.White;
            area.BackSecondaryColor = (s.TipoCircuito == "RC")
                ? Color.FromArgb(230, 240, 255)
                : Color.FromArgb(255, 235, 235);
            area.BackGradientStyle = GradientStyle.TopBottom;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = tMaxGrafica;
            area.AxisX.Interval = tMaxGrafica / 5.0;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = (maxValor > 0) ? maxValor * 1.1 : 1;

            area.AxisX.StripLines.Clear();
            if (tau <= tMaxGrafica) AgregarLinea(area, tau, "τ");
            if (2 * tau <= tMaxGrafica) AgregarLinea(area, 2 * tau, "2τ");
            if (3 * tau <= tMaxGrafica) AgregarLinea(area, 3 * tau, "3τ");
        }

        private static void AgregarLinea(ChartArea area, double x, string etiqueta)
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

        private void btnCalificar_Click(object? sender, EventArgs e)
        {
            var sim = dgvSimulaciones.CurrentRow?.DataBoundItem as Simulacion;
            if (sim == null)
            {
                MessageBox.Show("Selecciona una simulación.");
                return;
            }

            if (!double.TryParse(txtNota.Text, out var nota) || nota < 0 || nota > 5)
            {
                MessageBox.Show("Ingresa una nota válida entre 0 y 5.");
                return;
            }

            var califs = FileManager.LeerCalificaciones();

            var existente = califs.FirstOrDefault(c => c.SimulacionId == sim.Id);

            if (existente == null)
            {
                califs.Add(new Calificacion
                {
                    SimulacionId = sim.Id,
                    Profesor = _usuario.Nombre,
                    Nota = nota,
                    Comentario = sim.ComentarioEstudiante ?? "",
                    Fecha = DateTime.Now
                });
            }
            else
            {
                existente.Nota = nota;
                existente.Comentario = sim.ComentarioEstudiante ?? "";
                existente.Fecha = DateTime.Now;
            }

            FileManager.GuardarCalificaciones(califs);

            _califs = FileManager.LeerCalificaciones();
            AplicarFiltros();

            MessageBox.Show("Calificación guardada.");
        }

        private void cmbAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lblComentario_Click(object sender, EventArgs e)
        {
        }
    }
}
