using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.Controller
{
    public static class AuthController
    {
 
        public static void SeedUsuarios()
        {
         
            var usuarios = FileManager.LeerUsuarios();
            bool cambios = false;

   
            if (!usuarios.Any(u => u.Nombre.Equals("profesor", StringComparison.OrdinalIgnoreCase)))
            {
                usuarios.Add(new Usuario
                {
                    Id = "P001",
                    NombreCompleto = "Profesor Default",
                    Nombre = "profesor",
                    Password = "123",
                    Rol = "Teacher"
                });
                cambios = true;
            }

            
            if (!usuarios.Any(u => u.Nombre.Equals("estudiante", StringComparison.OrdinalIgnoreCase)))
            {
                usuarios.Add(new Usuario
                {
                    Id = "E001",
                    NombreCompleto = "Estudiante Demo",
                    Nombre = "estudiante",
                    Password = "123",
                    Rol = "Student"
                });
                cambios = true;
            }

            if (cambios)
            {
                FileManager.GuardarUsuarios(usuarios);
            }
        }

      
        public static Usuario? Login(string nombre, string password)
        {
            var usuarios = FileManager.LeerUsuarios();

            return usuarios.FirstOrDefault(u =>
                u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}
