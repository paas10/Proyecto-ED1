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

            foreach (var item in DataBase.Instance.ArboldeSeries.ObtenerArbol())
            {
                if (item != null)
                {
                    ListaTemporaldePeliculas.Add(item);
                }
            }

            foreach (var item in DataBase.Instance.ArboldeDocumentales.ObtenerArbol())
            {
                if (item != null)
                {
                    ListaTemporaldePeliculas.Add(item);
                }
            }

            foreach (var item in DataBase.Instance.ArboldePeliculas.ObtenerArbol())
            {
                if (item != null)
                {
                    ListaTemporaldePeliculas.Add(item);
                }
            }

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

            //Se manda la lista especial a la vista para poder visualizar la unica pelicula
            Pelicula NuevaPelicula = new Pelicula(URL, Trailer, Nombre,Tipo,Anio,Genero);
            List<Pelicula> ListadePeliculas = new List<Pelicula>();
            ListadePeliculas.Add(NuevaPelicula);
            Session.Add("URL",NuevaPelicula.Trailer);
            return View(ListadePeliculas);
        }

        public ActionResult BuscarPelicula(string Tipo, string Search)
        {
            //Se Crea una lista temporal de usuario para evaluar cual esta logeado
            List<Usuario> ListaTemporaldeUsuarios = new List<Usuario>();
            ListaTemporaldeUsuarios = DataBase.Instance.ArboldeUsuarios.ObtenerArbol();

            List<Pelicula> ListaTemporaldePeliculas = new List<Pelicula>();
            List<Pelicula> ListaTemporaldeSeries = new List<Pelicula>();
            List<Pelicula> ListaTemporaldeDocumentales = new List<Pelicula>();
            List<Pelicula> ListaGeneral = new List<Pelicula>();

            ListaTemporaldePeliculas = DataBase.Instance.ArboldePeliculas.ObtenerArbol();
            ListaTemporaldeSeries = DataBase.Instance.ArboldeSeries.ObtenerArbol();
            ListaTemporaldeDocumentales = DataBase.Instance.ArboldeDocumentales.ObtenerArbol();

            if (Tipo == "Nombre" && Search != null)
            {
                foreach (var item in ListaTemporaldePeliculas)
                {
                    if(item.Nombre == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeSeries)
                {
                    if (item.Nombre == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeDocumentales)
                {
                    if (item.Nombre == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }
            }
            else if (Tipo == "AniodeLanzamiento" && Search != null)
            {
                foreach (var item in ListaTemporaldePeliculas)
                {
                    if (item.AniodeLanzamiento == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeSeries)
                {
                    if (item.AniodeLanzamiento == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeDocumentales)
                {
                    if (item.AniodeLanzamiento == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }
            }
            else if (Tipo == "Genero" && Search != null)
            {
                foreach (var item in ListaTemporaldePeliculas)
                {
                    if (item.Genero == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeSeries)
                {
                    if (item.Genero == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }

                foreach (var item in ListaTemporaldeDocumentales)
                {
                    if (item.Genero == Search)
                    {
                        ListaGeneral.Add(item);
                    }
                }
            }
            else if(ListaGeneral.Count() == 0)
            {
                TempData["msg"] = "<script> alert('El Dato que Buscas no Existe');</script>";
                return View();
            }

               foreach (var item in ListaTemporaldeUsuarios)
               {
                    if (item.Logeado == true)
                    {
                        ViewBag.Message = item.Nombre;
                    }
               }

            return View("MisPeliculas", ListaGeneral);
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
