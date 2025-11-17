using System;

namespace Proyecto_Integrador.Model
{
    public class Simulacion
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();

    
        public string Usuario { get; set; } = "";         
        public string NombreCompleto { get; set; } = "";  
        public string IdAlumno { get; set; } = "";        

       
        public string TipoCircuito { get; set; } = "";    
        public string Modo { get; set; } = "";            

        public double R { get; set; }     
        public double C { get; set; }    
        public double L { get; set; }     
        public double V { get; set; }     
        public double V0 { get; set; }   
        public double TiempoMaximo { get; set; }   
        public double Paso { get; set; }
        // 👇 NUEVO: comentario que escribe el alumno sobre esta simulación
        public string ComentarioEstudiante { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
