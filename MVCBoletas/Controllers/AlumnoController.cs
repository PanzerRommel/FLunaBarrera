using System;
using System.Collections.Generic;
using System.Web.Mvc;
     // Asegúrate de reemplazar "YourNamespace" con el namespace correcto

namespace YourNamespace.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            var result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if (IdAlumno != null)
            {
                var result = BL.Alumno.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                }
            }

            return View(alumno);
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            try
            {
                if (alumno.IdAlumno == 0)
                {
                    var result = BL.Alumno.Add(alumno);

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
                    var result = BL.Alumno.Update(alumno);

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
        public ActionResult Delete(int? IdAlumno)
        {
            

            var result = BL.Alumno.Delete(IdAlumno.Value);

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
