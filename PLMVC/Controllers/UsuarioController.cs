using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            ML.Result result = BL.Usuario.GetAll ();
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
           


                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.Add(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha completado el registro";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error" + result.Message;
                    }
                }
                else
                {
                    ML.Result result = BL.Usuario.Add(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Se ha completado la actulización";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error" + result.Message;
                    }
                }

            
            return PartialView(usuario);
        }



        [HttpGet]
        public ActionResult Delete(ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.Delete(usuario);
            if (result.Correct)
            {

                ViewBag.Mensaje = "El usuario se Elimino ";
            }

            else
            {
                ViewBag.Mensaje = "El usuario no se elimino" + result.Message;
            }


            return View("Modal");
        }





    }
}