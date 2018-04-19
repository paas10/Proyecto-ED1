using Librería_de_Clases;
using Newtonsoft.Json;
using Proyecto.Clases;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MiUsuario()
        {
            Usuario Nuevo = new Usuario();
            List <Usuario> ListadeUsuarios = new List<Usuario>();
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if(item.Logeado == true)
                {
                    Nuevo = item;
                    ListadeUsuarios.Add(Nuevo);
                }
            }
            return View(ListadeUsuarios);
        }

        public ActionResult ForgetPassword(FormCollection collection)
        {
            string nombre = collection["Nombre"];
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if(item.Nombre == collection["Nombre"])
                {
                    item.Password = collection["Password"];
                }
            }
            return View();
        }

        public ActionResult CerrarSesion()
        {
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    item.Logeado = false;
                }
            }
            return View("Index");
        }

        public ActionResult RegistrarUsuarios()
        {
            return View();
        }

        public ActionResult MenudeDecision()
        {
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }

            return View();
         
        }

        public ActionResult UsuarioDecision()
        {
            return View();
        }


        public ActionResult Login(FormCollection collection)
        {
            if (collection["Username"] == "Admin" && collection["Password"] == "Admin")
            {
                ViewBag.Message = "Admin";
                Usuario UsuarioNuevo = new Usuario();
                UsuarioNuevo.Nombre = collection["Username"];
                UsuarioNuevo.Password = collection["Password"];
                UsuarioNuevo.Logeado = true;
                DataBase.Instance.ListadePruebaUser.Add(UsuarioNuevo);
                if(collection["Username"] == "Admin")
                {
                    return View("MenudeDecision");
                }
                else
                {
                    return View("UsuarioDecision");
                }
            }
            else
            {
                TempData["msg1"] = "<script> alert('El Usuario o La Contraseña es Incorrecta');</script>";
                return View();
            }
        }


        public ActionResult UploadFile()
        {
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Aca se hace el Ingreso por medio de Archivo de Texto, ya que el Boton de Result esta Linkeado.
        public ActionResult UploadFile(HttpPostedFileBase file, int? Tipo)
        {
            if (Path.GetExtension(file.FileName) != ".json")
            {
                //Aca se debe de Agregar una Vista de Error, o de Datos No Cargados
                TempData["msg"] = "<script> alert('Error El Archivo Cargado No es de Tipo Json');</script>";
                return RedirectToAction("Error", "Shared");
            }

            Stream Direccion = file.InputStream;
            //Se lee el Archivo que se subio, por medio del Lector

            StreamReader Lector = new StreamReader(Direccion, System.Text.Encoding.UTF8);
            //El Archivo se lee en una linea para luego ingresarlo

            string Dato = "";
            Dato = Lector.ReadToEnd();

            var ListadePeliculas = JsonConvert.DeserializeObject<List<Pelicula>>(Dato);
            DataBase.Instance.ListadePrueba = ListadePeliculas;

            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }

            return View("MenudeDecision");
        }
    }
}