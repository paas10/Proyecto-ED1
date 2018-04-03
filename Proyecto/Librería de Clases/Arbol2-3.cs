using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Librería_de_Clases
{
    class Arbol2_3 <T> where T : IComparable
    {

        public Nodo2_3<T> Raiz;

        public Arbol2_3()
        {
            this.Raiz = null;
        }

        public void Insertar(T vNuevo)
        {
            Insertar(vNuevo, Raiz);
        }

        private T Insertar(T vNuevo, Nodo2_3<T> nAuxiliar)
        {
            // Si el nodo esta vacío (raiz inicialmente) únicamente se inserta el valor en el nodo.
            if (nAuxiliar == null)
            {
                nAuxiliar.Valor[0] = vNuevo;
            }
            // Si es un nodo hoja se puede insertar el primer o segundo valor del nodo.
            else if (nAuxiliar.EsHoja == true)
            {
                InsertarAca(nAuxiliar, vNuevo);
            }
            // Si la siguiente condicion se cumple quiere decir que solo hay opción de hijo izquierdo o derecho.
            else if (nAuxiliar.Valor[1] == null)
            {
                // El valor nuevo es MENOR que la llave padre, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Valor[0].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijo[0]);
                    // INSERTAR ACÁ
                }
                // El valor nuevo es MAYOR que la llave padre, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Valor[0].CompareTo(vNuevo) == -1)
                    Insertar(vNuevo, nAuxiliar.Hijo[2]);

            }
            // Si la siguiente condicion se cumple quiere decir que hay opción hijo izquierdo, derecho o central.
            else if (nAuxiliar.Valor[0] != null && nAuxiliar.Valor[1] != null)
            {
                // El valor nuevo es MENOR que la llave padre izquierda, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Valor[0].CompareTo(vNuevo) == 1)
                    Insertar(vNuevo, nAuxiliar.Hijo[0]);
                // El valor nuevo es MAYOR que la llave padre izquierda y MENOR que la llave padre derecha, se debe dirigir al hijo central.
                if (nAuxiliar.Valor[0].CompareTo(vNuevo) == -1 && nAuxiliar.Valor[1].CompareTo(vNuevo) == 1)
                    Insertar(vNuevo, nAuxiliar.Hijo[1]);
                // El valor nuevo es MAYOR que la llave padre derecha, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Valor[1].CompareTo(vNuevo) == -1)
                    Insertar(vNuevo, nAuxiliar.Hijo[2]);
            }
        }



        public T InsertarAca(Nodo2_3<T> nAuxiliar, T vNuevo)
        {
            // Si el nodo únicamente tiene un valor, se inserta el segundo donde corresponde
            if (nAuxiliar.Valor[0].CompareTo(vNuevo) == -1 && nAuxiliar.Valor[1] == null)
            {
                nAuxiliar.Valor[1] = vNuevo;
            }
            else if (nAuxiliar.Valor[0].CompareTo(vNuevo) == 1 && nAuxiliar.Valor[1] == null)
            {
                T valorAuxiliar = nAuxiliar.Valor[0];
                nAuxiliar.Valor[1] = valorAuxiliar;
                nAuxiliar.Valor[0] = vNuevo;
            }
            // Si el nodo tiene dos valores, se ordenan los valores para subir el valor central.
            if (nAuxiliar.Valor[0] != null && nAuxiliar.Valor[1] != null)
            {
                if (nAuxiliar.Valor[0].CompareTo(vNuevo) == 1)
                {
                    // subir Raiz.Valor[0]

                    T valorSubir = nAuxiliar.Valor[0];
                    nAuxiliar.Valor[0] = vNuevo;

                    return valorSubir;
                }
                else if (nAuxiliar.Valor[0].CompareTo(vNuevo) == -1 && nAuxiliar.Valor[1].CompareTo(vNuevo) == 1)
                {
                    // Subir vNuevo
                }
                else
                {
                    // Subir Raiz.Valor[1]
                }
            }
        }
























        public List<T> ObtenerArbol()
        {
            List<T> Partidos = new List<T>();
            InOrder(Raiz, ref Partidos);

            return Partidos;
        }

        private void PreOrder(Arbol2_3<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                Elements.Add(Aux.Valor);
                PreOrder(Aux.HijoIzquierdo, ref Elements);
                PreOrder(Aux.HijoDerecho, ref Elements);
            }
        }
        private void InOrder(Arbol2_3<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                InOrder(Aux.HijoIzquierdo, ref Elements);
                Elements.Add(Aux.Valor);
                InOrder(Aux.HijoDerecho, ref Elements);
            }
        }
        private void PostOrder(Arbol2_3<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                PostOrder(Aux.HijoIzquierdo, ref Elements);
                PostOrder(Aux.HijoDerecho, ref Elements);
                Elements.Add(Aux.Valor);
            }
        }

        public List<T> Orders(string Order)
        {
            List<T> Elements = new List<T>();
            switch (Order)
            {
                case "PreOrder":
                    PreOrder(Raiz, ref Elements);
                    break;
                case "InOrder":
                    InOrder(Raiz, ref Elements);
                    break;
                case "PostOrder":
                    PostOrder(Raiz, ref Elements);
                    break;
            }

            return Elements;
        }

    }
}
