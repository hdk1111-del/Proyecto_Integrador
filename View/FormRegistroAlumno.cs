using System;
using System.Linq;
using System.Windows.Forms;
using Proyecto_Integrador.Controller;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.View
{
    public partial class FormRegistroAlumno : Form
    {
        public FormRegistroAlumno()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            btnRegistrar.Click += btnRegistrar_Click;
        }
        private static string GenerarNuevoId(List<Usuario> usuarios)
        {
            int max = 0;

            foreach (var u in usuarios)
            {
                if (u.Rol == "Student" && !string.IsNullOrWhiteSpace(u.Id))
                {
                    string numPart = u.Id;
                    int i = 0;
                    while (i < numPart.Length && !char.IsDigit(numPart[i]))
                        i++;

                    if (i < numPart.Length)
                    {
                        numPart = numPart.Substring(i);
                        if (int.TryParse(numPart, out int n) && n > max)
                            max = n;
                    }
                }
            }

            int siguiente = max + 1;
            return $"E{siguiente:000}";  // → E001, E002, E003...
        }
        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreCompleto) ||
                string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(confirmar))
            {
                MessageBox.Show("Completa todos los campos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pass != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var usuarios = FileManager.LeerUsuarios();

            if (usuarios.Any(u => u.Nombre.Equals(usuario, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ese nombre de usuario ya está en uso.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ⭐ GENERAR ID AUTOMÁTICO ⭐
            string nuevoId = GenerarNuevoId(usuarios);

            // Lo asignamos al textbox aunque esté oculto (no pasa nada)
            txtId.Text = nuevoId;

            // Crear el usuario
            var nuevo = new Usuario
            {
                Id = nuevoId,
                NombreCompleto = nombreCompleto,
                Nombre = usuario,
                Password = pass,
                Rol = "Student"
            };

            usuarios.Add(nuevo);
            FileManager.GuardarUsuarios(usuarios);

            MessageBox.Show($"Alumno registrado correctamente.\nID asignado: {nuevoId}",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
