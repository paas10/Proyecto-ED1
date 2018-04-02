using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librería_de_Clases
{
    public class Nodo2_3<T>
    {
        public T[] Valor = new T[2];
        public Nodo2_3<T>[] Hijo = new Nodo2_3<T>[3];

        public bool EsHoja
        {
            get
            {
                return Hijo[0] == null && Hijo[1] == null && Hijo[2] == null;
            }
        }

        public Nodo2_3(T valor)
        {
            this.Valor[0] = valor;
            for (int i = 0; i < 3; i++)
            {
                Hijo[i] = null;
            }
        }
    }
}

