using System;
using System.Windows.Forms;
using Proyecto_Integrador.Controller;
using Proyecto_Integrador.Model;
using Proyecto_Integrador.View;

namespace Proyecto_Integrador
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

           
            AuthController.SeedUsuarios();
        }
        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            using (var frm = new FormRegistroAlumno())
            {
                frm.ShowDialog(this);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            string nombre = txtUsuario.Text.Trim();
            string pass = txtContrasena.Text.Trim();

            var usuario = AuthController.Login(nombre, pass);

            if (usuario == null)
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }


       
            Form siguienteForm;

            if (usuario.Rol == "Teacher")
            {
                siguienteForm = new FormProfesor(usuario);
            }
            else
            {
                siguienteForm = new FormEstudiante(usuario);
            }

            
            siguienteForm.FormClosed += (_, __) => this.Show();

            siguienteForm.Show();
            this.Hide();
        }
    }
}
