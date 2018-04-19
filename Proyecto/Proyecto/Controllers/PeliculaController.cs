using Proyecto.Clases;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Librería_de_Clases;

namespace Proyecto.Controllers
{
    public class PeliculaController : Controller
    {

        public void imprimirArchivo()
        {

            StreamWriter escritor = new StreamWriter(@"C:\Users\Admin\Desktop\Bitacora.txt");

            foreach (var linea in DataBase.Instance.ArchivoTexto)
            {
                escritor.WriteLine(linea);
            }
            escritor.Close();
        }

            // GET: Pelicula
        public ActionResult MisPeliculas(string NombreUsuario)
        {
            //Se crean listas temporales de usuarios y peliculas para usarlas despues
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            List<Pelicula> ListaTemporaldePeliculas = new List<Pelicula>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();
            ListaTemporaldePeliculas = DataBase.Instance.ArboldePeliculas.ObtenerArbol();

            //se evalua si el usuario esta logeado
            foreach (var item in ListaTemporaldeUsuarios)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }
            if (ListaTemporaldePeliculas == null)
            {
                //Se envia un mensaje de error si el usuario no tiene peliculas agregadas
                TempData["msg"] = "<script> alert('No Tienes Peliculas Agregadas, Porfavor Agrega Peliculas Antes para ver tu Inicio');</script>";
                return RedirectToAction("MenudeDecision","Home");
            }
            else
            {
                //Se envia la lista temporal de peliculas a la vista
                return View(ListaTemporaldePeliculas);
            }
        }

        [ValidateInput(false)]
        public ActionResult VerPelicula(string URL, string Trailer, string Nombre, string Tipo, string Genero, string Anio)
        {
            //Se Crea una lista temporal de usuario para evaluar cual esta logeado
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            foreach (var item in ListaTemporaldeUsuarios)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }

            Pelicula NuevaPelicula = new Pelicula(URL, Trailer, Nombre,Tipo,Anio,Genero);
            List<Pelicula> ListadePeliculas = new List<Pelicula>();
            ListadePeliculas.Add(NuevaPelicula);
            Session.Add("URL",NuevaPelicula.Trailer);
            return View(ListadePeliculas);
        }


            // GET: Pelicula/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pelicula/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pelicula/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            
            try
            {
                /*Pelicula nuevaPelicula = new Pelicula(collection["Tipo"], collection["Nombre"], Convert.ToInt32(collection["AñodeLanzamiento"]),
                    collection["Genero"]);*/

                DataBase.Instance.ArchivoTexto.Add("INSERCION");
                DataBase.Instance.ArchivoTexto.Add("\tTipo de pelicula: " + collection["Tipo"]);
                DataBase.Instance.ArchivoTexto.Add("\tNombre de la pelicula: " + collection["Nombre"]);
                DataBase.Instance.ArchivoTexto.Add("\tAño de lanzamiento: " + collection["AñodeLanzamiento"]);
                DataBase.Instance.ArchivoTexto.Add("\tGenero: " + collection["Genero"]);
                DataBase.Instance.ArchivoTexto.Add("");


                imprimirArchivo();

                /*DataBase.Instance.ArboldePeliculas.Insertar(nuevaPelicula);*/

                return RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pelicula/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pelicula/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pelicula/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pelicula/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
