using System;

namespace Proyecto_Integrador.Model
{
    public class Calificacion
    {
        public string SimulacionId { get; set; } = "";   
        public string Profesor { get; set; } = "";       
        public double Nota { get; set; }                 
        public string Comentario { get; set; } = "";    
        public DateTime Fecha { get; set; } = DateTime.Now; 


    }
}
