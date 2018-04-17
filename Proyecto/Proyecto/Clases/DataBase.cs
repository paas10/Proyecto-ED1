using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Librería_de_Clases;
using Proyecto.Models;
using Proyecto.Clases;

namespace Proyecto.Clases
{
    public class DataBase
    {
        private static DataBase instance;
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataBase();
                return instance;
            }
        }

        public Arbol2_3<Pelicula> ArboldePeliculas;
        public List<string> ArchivoTexto;
        public List<Pelicula> ListadePrueba;
        public List<Usuario> ListadePruebaUser;

        public DataBase()
        {
            ArboldePeliculas = new Arbol2_3<Pelicula>();
            ArchivoTexto = new List<string>();
            ListadePrueba = new List<Pelicula>();
            ListadePruebaUser = new List<Usuario>();
        }
    }
}