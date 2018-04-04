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
                nAuxiliar.Elementos[0] = vNuevo;
            }
            // Si es un nodo hoja se puede insertar el primer o segundo valor del nodo.
            else if (nAuxiliar.EsHoja == true)
            {
                return InsertarAca(nAuxiliar, vNuevo);
            }
            // Si la siguiente condicion se cumple quiere decir que solo hay opción de hijo izquierdo o derecho.
            else if (nAuxiliar.Elementos[1] == null)
            {
                // El valor nuevo es MENOR que la llave padre, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijos[0]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(nAuxiliar, valorTemp);

                }
                // El valor nuevo es MAYOR que la llave padre, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijos[2]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(nAuxiliar, valorTemp);

                }

            }
            // Si la siguiente condicion se cumple quiere decir que hay opción hijo izquierdo, derecho o central.
            else if (nAuxiliar.Elementos[0] != null && nAuxiliar.Elementos[1] != null)
            {
                // El valor nuevo es MENOR que la llave padre izquierda, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijos[0]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(nAuxiliar, valorTemp);
                }
                // El valor nuevo es MAYOR que la llave padre izquierda y MENOR que la llave padre derecha, se debe dirigir al hijo central.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijos[1]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(nAuxiliar, valorTemp);
                }
                // El valor nuevo es MAYOR que la llave padre derecha, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Elementos[1].CompareTo(vNuevo) == -1)
                {
                    T valorTemp = Insertar(vNuevo, nAuxiliar.Hijos[2]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(nAuxiliar, valorTemp);
                }
            }

            return default(T);
        }

        public T InsertarAca(Nodo2_3<T> nAuxiliar, T vNuevo)
        {
            // Si el nodo únicamente tiene un valor, se inserta el segundo donde corresponde
            if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1] == null)
            {
                nAuxiliar.Elementos[1] = vNuevo;
            }
            else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1 && nAuxiliar.Elementos[1] == null)
            {
                T valorAuxiliar = nAuxiliar.Elementos[0];
                nAuxiliar.Elementos[1] = valorAuxiliar;
                nAuxiliar.Elementos[0] = vNuevo;
            }
            // Si el nodo tiene dos valores, se ordenan los valores para subir el valor central.
            if (nAuxiliar.Elementos[0] != null && nAuxiliar.Elementos[1] != null)
            {
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    // subir Raiz.Valor[0]

                    T valorSubir = nAuxiliar.Elementos[0];
                    nAuxiliar.Elementos[0] = vNuevo;

                    return valorSubir;
                }
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1].CompareTo(vNuevo) == 1)
                {
                    // Subir vNuevo
                }
                else
                {
                    // Subir Raiz.Valor[1]
                }
            }

            return default(T); 
        }

        // Verifica y corrije si algún hijo se encuentra en una posicion equivocada.
        public void CorregirHijos(Nodo2_3<T> nAuxiliar)
        {
            //ESTA CUESTIÓN ESTÁ EN PROCESO
            if (nAuxiliar.Elementos[1] == null)
            {
                //if (nAuxiliar.Valor[0].CompareTo(nAuxiliar.Hijo[0].Valor[0]) -1)
            }
        }













        public List<T> ObtenerArbol()
        {
            List<T> Partidos = new List<T>();
            InOrder(Raiz, ref Partidos);

            return Partidos;
        }

        private void InOrder(Nodo2_3<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                InOrder(Aux.Hijos[0], ref Elements);
                Elements.Add(Aux.Elementos[0]);
                InOrder(Aux.Hijos[1], ref Elements);
                Elements.Add(Aux.Elementos[1]);
                InOrder(Aux.Hijos[2], ref Elements);
            }
        }


    }
}
