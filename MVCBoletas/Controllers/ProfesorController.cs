using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBoletas.Controllers
{
    public class ProfesorController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Profesor profesor = new ML.Profesor();
            var result = BL.Profesor.GetAll();

            if (result.Correct)
            {
                profesor.Profesores = result.Objects;
            }

            return View(profesor);
        }

        [HttpGet]
        public ActionResult Form(int? IdProfesor)
        {
            ML.Profesor profesor = new ML.Profesor();

            if (IdProfesor != null)
            {
                var result = BL.Profesor.GetById(IdProfesor.Value);
                if (result.Correct)
                {
                    profesor = (ML.Profesor)result.Object;
                }
            }

            return View(profesor);
        }

        [HttpPost]
        public ActionResult Form(ML.Profesor profesor)
        {
            try
            {
                if (profesor.IdProfesor == 0)
                {
                    var result = BL.Profesor.Add(profesor);

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
                    var result = BL.Profesor.Update(profesor);

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
        public ActionResult Delete(int idProfesor)
        {
            var result = BL.Profesor.Delete(idProfesor);

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
