using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace TuProyecto.Controllers
{
    public class GrupoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Grupo grupo = new ML.Grupo(); // instancia para las propiedades
            ML.Result result = BL.Grupo.GetAll();

            if (result.Correct)
            {
                grupo.Grupos = result.Objects;
                return View(grupo);
            }
            return View(grupo);
        }
    }
}
