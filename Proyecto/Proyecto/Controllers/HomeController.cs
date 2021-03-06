﻿using Librería_de_Clases;
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
            //Se crea un nuevo Usuario
            Usuario Nuevo = new Usuario();
            //Se crea una lista temporal de usuarios y una lista de usuarios para poder pasarla posteriormente.
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            List<Usuario> ListadeUsuarios = new List<Usuario>();

            //a la lista temporal de usuarios se le asignan los usuarios que posee el arbol.
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            //Se busca el usuario que esta logeado y es el que se envia a la vista.
            foreach (var item in ListaTemporaldeUsuarios)
            {
                if(item.Logeado == true)
                {
                    Nuevo = item;
                    ViewBag.Message = Nuevo.Username;
                    ListadeUsuarios.Add(Nuevo);
                }
            }
            return View(ListadeUsuarios);
        }

        public ActionResult ForgetPassword(FormCollection collection)
        {
            string nombre = collection["Nombre"];
            //Se crea una lista temporal de usuarios
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            //se evalua cual es la contraseña que coincide y se asigna el nuevo valor
            foreach (var item in ListaTemporaldeUsuarios)
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
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            //Se deslogea al usuario que coincide con las especificaciones
            foreach (var item in ListaTemporaldeUsuarios)
            {
                if (item.Logeado == true)
                {
                    item.Logeado = false;
                }
            }
            return View("Index");
        }

        public ActionResult RegistrarUsuarios(FormCollection collection)
        {
            if (collection["Nombre"] != null)
            {
                Usuario Nuevo = new Usuario();
                Nuevo.Nombre = collection["Nombre"];
                Nuevo.Apellido = collection["Apellido"];
                Nuevo.Edad = Convert.ToInt32(collection["Edad"]);
                Nuevo.Username = collection["Username"];
                Nuevo.Password = collection["Password"];
                Nuevo.Logeado = true;

                DataBase.Instance.ArboldeUsuarios.Insertar(Nuevo);
                TempData["msg"] = "<script> alert('Usuario insertado con éxito');</script>";
            }
            return View();
        }

        public ActionResult MenudeDecision()
        {
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            //Se evalua la decision y se envia el nombre de usuario para mostrarlo en las vistas
            foreach (var item in ListaTemporaldeUsuarios)
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
            //Se inserta el nuevo usuario al Árbol de usuarios
            if ((collection["Username"] == "Admin" || collection["Username"] == "admin") && (collection["Password"] == "Admin" || collection["Password"] == "admin"))
            {
                ViewBag.Message = "Admin";
                Usuario UsuarioNuevo = new Usuario();
                UsuarioNuevo.Nombre = collection["Username"];
                UsuarioNuevo.Password = collection["Password"];
                UsuarioNuevo.Logeado = true;
                DataBase.Instance.ArboldeUsuarios.Insertar(UsuarioNuevo);
                return View("MenudeDecision");
      
            }
            else if(collection["Username"]!= "Admin")
            {
                List<Usuario> ListadeUsuarios = new List<Usuario>();
                ListadeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();
                foreach (var item in ListadeUsuarios)
                {
                    if (item.Username == collection["Username"] && item.Password == collection["Password"])
                    {
                        ViewBag.Message = item.Username;
                        return View("UsuarioDecision");
                    }
                }
                TempData["msg1"] = "<script> alert('El Usuario o La Contraseña es Incorrecta');</script>";
                return View();
            }
            else
            {
                //Si no es correcto se envia un mensaje de Error
                TempData["msg1"] = "<script> alert('El Usuario o La Contraseña es Incorrecta');</script>";
                return View();
            }
            
        }


        public ActionResult UploadFile()
        {
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            //Se crea una lista temporal de usuarios para ver si esta logeada o no
            foreach (var item in ListaTemporaldeUsuarios)
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
            //Se valida el .json
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

            //Se deserealiza el objeto por medio de .json obteniendo una lista de peliculas
            var ListadePeliculasGeneral = JsonConvert.DeserializeObject<List<Pelicula>>(Dato);

            //Se insertan las peliculas en el arbol y se clasifican
            foreach (var item in ListadePeliculasGeneral)
            {
                if(item.Tipo == "Pelicula")
                {
                    DataBase.Instance.ArboldePeliculas.Insertar(item);
                }
                else if (item.Tipo == "Serie")
                {
                    DataBase.Instance.ArboldeSeries.Insertar(item);
                }
                else if (item.Tipo == "Documental")
                {
                    DataBase.Instance.ArboldeDocumentales.Insertar(item);
                }
            }

            //Se crea una lista temporal de usuarios para identificar cual esta logeado
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            foreach (var item in ListaTemporaldeUsuarios)
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