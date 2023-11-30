using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; }
        public ML.Alumno Alumno { get; set; }
        public ML.Grupo Grupo { get; set; }
        public ML.Materia Materia { get; set; }
        public ML.Profesor Profesor { get; set; }
        public decimal Resultado { get; set; }
        public List<object> Calificaciones { get; set; }
    }
}
