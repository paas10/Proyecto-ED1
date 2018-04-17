using Proyecto.Clases;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }
            if (DataBase.Instance.ListadePrueba == null)
            {
                TempData["msg"] = "<script> alert('No Tienes Peliculas Agregadas, Porfavor Agrega Peliculas Antes para ver tu Inicio');</script>";
                return RedirectToAction("MenudeDecision","Home");
            }
            else
            {
                return View(DataBase.Instance.ListadePrueba);
            }
        }

        public ActionResult VerPelicula(string URL, string Nombre, string Tipo, string Genero, string Anio)
        {
            foreach (var item in DataBase.Instance.ListadePruebaUser)
            {
                if (item.Logeado == true)
                {
                    ViewBag.Message = item.Nombre;
                }
            }

            Pelicula NuevaPelicula = new Pelicula(URL, Nombre,Tipo,Anio,Genero);
            List<Pelicula> ListadePeliculas = new List<Pelicula>();
            ListadePeliculas.Add(NuevaPelicula);
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
