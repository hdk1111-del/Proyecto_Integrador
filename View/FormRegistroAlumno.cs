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

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();

           
            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(nombreCompleto) ||
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

           
            if (usuarios.Any(u => u.Id == id))
            {
                MessageBox.Show("Ya existe un usuario con ese ID/matrícula.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            if (usuarios.Any(u => u.Nombre.Equals(usuario, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ese nombre de usuario ya está en uso.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

       
            var nuevo = new Usuario
            {
                Id = id,
                NombreCompleto = nombreCompleto,
                Nombre = usuario,
                Password = pass,
                Rol = "Student"
            };

            usuarios.Add(nuevo);
            FileManager.GuardarUsuarios(usuarios);

            MessageBox.Show("Alumno registrado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
