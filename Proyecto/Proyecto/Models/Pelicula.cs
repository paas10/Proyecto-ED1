using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Proyecto.Models
{
    public class Pelicula : IComparable
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Tipo del Film es Requerido")]
        public string Tipo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre de La Pelicula es Requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Año de Lazamiento de La Pelicula es Requerido")]
        public int AñodeLanzamiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Genero de la Pelicula es Requerido")]
        public string Genero { get; set; }

        public Pelicula(string Tipo, string Nombre, int AñodeLanzamiento, string Genero)
        {
            this.Tipo = Tipo;
            this.Nombre = Nombre;
            this.AñodeLanzamiento = AñodeLanzamiento;
            this.Genero = Genero;
        }


        public Pelicula()
        {
            this.Tipo = null;
            this.Nombre = null;
            this.AñodeLanzamiento = Convert.ToInt32(DateTime.Now.Year);
            this.Genero = Genero;
        }

        public int CompareByNombredePelicula(Pelicula pelicula)
        {
            return Nombre.CompareTo(pelicula.Nombre);
        }

        public int CompareByAño(Pelicula pelicula)
        {
            return AñodeLanzamiento.CompareTo(pelicula.AñodeLanzamiento);
        }

        public int CompareByGenero(Pelicula pelicula)
        {
            return Genero.CompareTo(pelicula.Genero);
        }

        public int CompareTo(object obj)
        {
            int res;

            try
            {
                Pelicula pelicula = obj as Pelicula;

                res = CompareByAño(pelicula);

                if (res != 0)
                    return res;
                else
                    res = CompareByNombredePelicula(pelicula);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public delegate int Comparar(Pelicula Pelicula);

        public int CompareTo(Pelicula pelicula, Comparar criterio)
        {
            return criterio(pelicula);
        }
    }
}
