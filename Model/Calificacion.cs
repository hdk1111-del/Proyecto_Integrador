using System;

namespace Proyecto_Integrador.Model
{
    public class Calificacion
    {
        public string SimulacionId { get; set; } = "";   // Id de la simulación
        public string Profesor { get; set; } = "";       // Usuario del profesor
        public double Nota { get; set; }                 // Nota numérica

        // Ahora este comentario es el comentario del ESTUDIANTE asociado a la simulación
        public string Comentario { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
