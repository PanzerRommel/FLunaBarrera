using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MVCBoletas.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            var result = BL.Materia.GetAll();

            if (result.Correct)
            {
                materia.Materias = result.Objects;
            }
            return View(materia);
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            if (IdMateria != null)
            {
                var result = BL.Materia.GetById(IdMateria.Value);
                if (result.Correct)
                {
                    materia = (ML.Materia)result.Object;
                }
            }

            return View(materia);
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            try
            {
                if (materia.IdMateria == 0)
                {
                    var result = BL.Materia.Add(materia);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Registro exitoso";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrió un problema al agregar el registro: " + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    var result = BL.Materia.Update(materia);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Actualización exitosa";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrió un problema al actualizar el registro: " + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
            }
            catch (Exception ex)
            {
                // Loguea o maneja el error según tus necesidades
                ViewBag.Mensaje = "Ocurrió un problema: " + ex.Message;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(int idMateria)
        {
            var result = BL.Materia.Delete(idMateria);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro eliminado exitosamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "Ocurrió un problema al eliminar el registro: " + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }
}
