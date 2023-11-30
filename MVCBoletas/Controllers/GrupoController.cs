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
        public ActionResult Form(int? IdGrupo)
        {
            ML.Grupo grupo = new ML.Grupo();

            if (IdGrupo.HasValue)
            {
                var result = BL.Grupo.GetById(IdGrupo.Value);

                if (result.Correct)
                {
                    grupo = result.Objects.FirstOrDefault() as ML.Grupo ?? new ML.Grupo();
                }
                else
                {
                    // Manejo de error o redirección en caso de fallo
                    ViewBag.ErrorMessage = result.ErrorMessage; // Puedes utilizar ViewBag para mostrar mensajes de error en la vista
                    return View("Error"); // Reemplaza "Error" con el nombre de tu vista de error
                }
            }

            return View(grupo);
        }

        [HttpPost]
        public ActionResult Form(ML.Grupo grupo)
        {
            try
            {
                if (grupo.IdGrupo == 0)
                {
                    var result = BL.Grupo.Add(grupo);

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
                    var result = BL.Grupo.Update(grupo);

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
        [HttpPost]
        public ActionResult Delete(int IdGrupo)
        {
            try
            {
                var result = BL.Grupo.Delete(IdGrupo);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Eliminación exitosa";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al eliminar el registro: " + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            catch (Exception ex)
            {
                // Loguea o maneja el error según tus necesidades
                ViewBag.Mensaje = "Ocurrió un problema: " + ex.Message;
                return PartialView("Modal");
            }
        }
    }
}
