using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Librería_de_Clases
{
    class Pelicula : IComparable
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Tipo del Film es Requerido")]
        public string Tipo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre de La Pelicula es Requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Año de Lazamiento de La Pelicula es Requerido")]
        public DateTime AñodeLanzamiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Genero de la Pelicula es Requerido")]
        public string Genero { get; set; }

        public Pelicula(string Tipo, string Nombre, DateTime AñodeLanzamiento, string Genero)
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
            this.AñodeLanzamiento = DateTime.Now;
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

        //Modificar, ver si implementar o no lo del Codigo PK.
        public int CompareTo(object obj)
        {
            try
            {
                Pelicula pelicula = obj as Pelicula;

                /*if (pelicula.codigoPK == 1)
                    return CompareByNombredePelicula(pelicula);
                else if (pelicula.codigoPK == 2)
                {
                    return CompareByAño(pelicula);
                }   
                else
                {
                    return CompareByGenero(pelicula);
                }   */
                return 0;
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
