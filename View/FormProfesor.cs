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

        private Chart chartDetalle;
        private ComboBox cmbAlumnos;
        private Label lblAlumno;

        public FormProfesor(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Text = $"Panel del Profesor – {_usuario.Nombre}";
            StartPosition = FormStartPosition.CenterScreen;

            // El profesor SOLO LEE el comentario
            txtComentario.ReadOnly = true;

            dgvSimulaciones.AutoGenerateColumns = true;
            dgvSimulaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSimulaciones.MultiSelect = false;
            dgvSimulaciones.ReadOnly = true;
            dgvSimulaciones.AutoGenerateColumns = true;

            AsegurarComboAlumnos();
            CrearChart();

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

        private void CrearChart()
        {
            var right = this.Controls.Find("panelRight", true).FirstOrDefault() as Panel;
            if (right == null)
            {
                right = new Panel { Name = "panelRight", Dock = DockStyle.Fill, BackColor = Color.White };
                Controls.Add(right);
                right.BringToFront();
            }

            chartDetalle = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            var area = new ChartArea("main");
            chartDetalle.ChartAreas.Add(area);
            chartDetalle.Legends.Clear();

            // Mejora visual general
            chartDetalle.AntiAliasing = AntiAliasingStyles.All;
            chartDetalle.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            area.AxisX.LabelStyle.Format = "0.###";
            area.AxisY.LabelStyle.Format = "0.###";

            right.Controls.Add(chartDetalle);
            chartDetalle.BringToFront();
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
            // Ocultar el Id de la SIMULACIÓN (Guid)
            if (dgvSimulaciones.Columns["Id"] != null)
            {
                dgvSimulaciones.Columns["Id"].Visible = false;
            }

            // Renombrar la columna del alumno
            if (dgvSimulaciones.Columns["IdAlumno"] != null)
            {
                dgvSimulaciones.Columns["IdAlumno"].HeaderText = "Matrícula";
            }

            if (dgvSimulaciones.Columns["Usuario"] != null)
            {
                dgvSimulaciones.Columns["Usuario"].HeaderText = "Usuario";
            }

            if (dgvSimulaciones.Columns["NombreCompleto"] != null)
            {
                dgvSimulaciones.Columns["NombreCompleto"].HeaderText = "Nombre completo";
            }
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

            // Comentario que escribió el estudiante
            txtComentario.Text = sim.ComentarioEstudiante ?? "";

            // Cambiar el texto del label para que muestre el nombre del alumno
            var nombre = string.IsNullOrWhiteSpace(sim.NombreCompleto)
                ? sim.Usuario
                : sim.NombreCompleto;

            lblComentario.Text = $"Comentario de {nombre}:";

            // Cargar la nota si ya existe
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
            chartDetalle.Series.Clear();

            var serie = new Series("Simulación")
            {
                ChartType = SeriesChartType.Spline,   // curva suave
                ChartArea = "main",
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 5,
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
            if (tMaxGrafica <= 0) tMaxGrafica = tau * 5; // fallback

            // 3️⃣ Número de puntos y paso solo para la gráfica
            int nPuntos = 300;
            double dt = tMaxGrafica / nPuntos;

            double maxValor = 0.0;

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

                if (valor > maxValor) maxValor = valor;

                serie.Points.AddXY(time, valor);
            }

            chartDetalle.Series.Add(serie);

            var area = chartDetalle.ChartAreas["main"];
            area.AxisX.Title = "Tiempo (s)";
            area.AxisY.Title = s.TipoCircuito == "RC" ? "Voltaje (V)" : "Corriente (A)";

            // Fondo con degradado (distinto tono según tipo)
            area.BackColor = Color.White;
            area.BackSecondaryColor = (s.TipoCircuito == "RC")
                ? Color.FromArgb(230, 240, 255)   // azul clarito
                : Color.FromArgb(255, 235, 235);  // rojo clarito
            area.BackGradientStyle = GradientStyle.TopBottom;

            // Rejilla más suave
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            // 👉 Zoom al rango útil [0, tMaxGrafica]
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = tMaxGrafica;
            area.AxisX.Interval = tMaxGrafica / 5.0;

            // Escala de eje Y para que no se aplaste
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = (maxValor > 0) ? maxValor * 1.1 : 1;

            // Limpiamos las líneas verticales anteriores (τ, 2τ, 3τ)
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

            // Si ya existía calificación para esta simulación, la actualizamos
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
