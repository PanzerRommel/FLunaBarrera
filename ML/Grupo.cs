using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public string Nombregrupo { get; set; }
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; }

        public List<object> Grupos { get; set; }
    }
}
