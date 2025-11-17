using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Proyecto_Integrador.Model;

namespace Proyecto_Integrador.Controller
{
    public static class FileManager
    {
        private static readonly string BasePath = Path.Combine(Application.StartupPath, "Data");

        static FileManager()
        {
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }

        private static string UsuariosPath => Path.Combine(BasePath, "usuarios.txt");
        private static string SimulacionesPath => Path.Combine(BasePath, "simulaciones.txt");
        private static string CalificacionesPath => Path.Combine(BasePath, "calificaciones.txt");

    

        public static List<Usuario> LeerUsuarios()
        {
            if (!File.Exists(UsuariosPath)) return new List<Usuario>();

            string json = File.ReadAllText(UsuariosPath);
            return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
        }

        public static void GuardarUsuarios(List<Usuario> usuarios)
        {
            string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(UsuariosPath, json);
        }

     
        public static List<Simulacion> LeerSimulaciones()
        {
            if (!File.Exists(SimulacionesPath)) return new List<Simulacion>();

            string json = File.ReadAllText(SimulacionesPath);
            return JsonSerializer.Deserialize<List<Simulacion>>(json) ?? new List<Simulacion>();
        }

        public static void GuardarSimulaciones(List<Simulacion> sims)
        {
            string json = JsonSerializer.Serialize(sims, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(SimulacionesPath, json);
        }

     

        public static List<Calificacion> LeerCalificaciones()
        {
            if (!File.Exists(CalificacionesPath)) return new List<Calificacion>();

            string json = File.ReadAllText(CalificacionesPath);
            return JsonSerializer.Deserialize<List<Calificacion>>(json) ?? new List<Calificacion>();
        }

        public static void GuardarCalificaciones(List<Calificacion> califs)
        {
            string json = JsonSerializer.Serialize(califs, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(CalificacionesPath, json);
        }
    }
}
