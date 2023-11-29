using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P.Controllers
{
    public class UsuarioController : Controllers
    {
        // GET: Usuario
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            ML.Result result = BL.Usuario.GetAll(usuario);
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        public ActionResult Form(int? IdUsuario)
        {

            return View();
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {


                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddEF(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha completado el registro";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error" + result.ErrorMessage;
                    }
                }
                else
                {
                    ML.Result result = BL.Usuario.AddEF(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha completado la actulización";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error" + result.ErrorMessage;
                    }
                }

            }
            else
            {
                return PartialView(usuario);
            }
            return PartialView(usuario);
        }



        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {

            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            if (result.Correct)
            {

                ViewBag.Mensaje = "El usuarios se Elimino Satisfactoriamente";
            }

            else
            {
                ViewBag.Mensaje = "El usuario no se elimino" + result.ErrorMessage;
            }


            return View("Modal");
        }






    }
}