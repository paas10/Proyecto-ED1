using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Librería_de_Clases
{
    public class Arbol2_3 <T> where T : IComparable
    {

        public Nodo2_3<T> Raiz;

        public Arbol2_3()
        {
            this.Raiz = null;
        }

        public void Insertar(T vNuevo)
        {
            Insertar(vNuevo, ref Raiz);
        }

        private T Insertar(T vNuevo, ref Nodo2_3<T> nAuxiliar)
        {
            // Si el nodo esta vacío (raiz inicialmente) únicamente se inserta el valor en el nodo.
            if (nAuxiliar == null)
            {
                Nodo2_3<T> nNuevo = new Nodo2_3<T>(vNuevo);
                nAuxiliar = nNuevo;
            }
            // Si es un nodo hoja se puede insertar el primer o segundo valor del nodo.
            else if (nAuxiliar.EsHoja == true)
            {
                return InsertarAca(ref nAuxiliar, vNuevo);
            }
            // Si la siguiente condicion se cumple quiere decir que solo hay opción de hijo izquierdo o derecho.
            else if (nAuxiliar.Elementos[1] == null)
            {
                // El valor nuevo es MENOR que la llave padre, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, ref nAuxiliar.Hijos[0]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(ref nAuxiliar, valorTemp);

                }
                // El valor nuevo es MAYOR que la llave padre, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1)
                {
                    T valorTemp = Insertar(vNuevo, ref nAuxiliar.Hijos[2]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(ref nAuxiliar, valorTemp);

                }

            }
            // Si la siguiente condicion se cumple quiere decir que hay opción hijo izquierdo, derecho o central.
            else if (nAuxiliar.Elementos[0] != null && nAuxiliar.Elementos[1] != null)
            {
                // El valor nuevo es MENOR que la llave padre izquierda, se debe dirigir al hijo izquierdo.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, ref nAuxiliar.Hijos[0]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(ref nAuxiliar, valorTemp);
                }
                // El valor nuevo es MAYOR que la llave padre izquierda y MENOR que la llave padre derecha, se debe dirigir al hijo central.
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1].CompareTo(vNuevo) == 1)
                {
                    T valorTemp = Insertar(vNuevo, ref nAuxiliar.Hijos[1]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(ref nAuxiliar, valorTemp);
                }
                // El valor nuevo es MAYOR que la llave padre derecha, se debe dirigir al hijo derecho.
                else if (nAuxiliar.Elementos[1].CompareTo(vNuevo) == -1)
                {
                    T valorTemp = Insertar(vNuevo, ref nAuxiliar.Hijos[2]);

                    // INSERTAR ACÁ
                    if (valorTemp != null)
                        InsertarAca(ref nAuxiliar, valorTemp);
                }
            }

            return default(T);
        }

        private T InsertarAca(ref Nodo2_3<T> nAuxiliar, T vNuevo)
        {
            // Si el nodo únicamente tiene un valor, se inserta el segundo donde corresponde
            if (nAuxiliar.Elementos[1] == null)
            {
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1)
                {
                    nAuxiliar.Elementos[1] = vNuevo;
                }
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    T valorAuxiliar = nAuxiliar.Elementos[0];
                    nAuxiliar.Elementos[1] = valorAuxiliar;
                    nAuxiliar.Elementos[0] = vNuevo;
                }
            }
            // Si el nodo tiene dos valores, se ordenan los valores para subir el valor central.
            // Por deduccion, si el elemento derecho no está vacío... el nodo está lleno.
            else 
            {
                if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == 1)
                {
                    // subir nAuxiliar.Elementos[0]

                    // Si el papá del actual es nulo, tiene que crearse un nuevo nodo que sea papi para el acutual.
                    if (nAuxiliar.Padre == null)
                    {
                        Nodo2_3<T> nPadre = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                        Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(vNuevo);
                        Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                        nPadre.Hijos[0] = nHijoIzquierdo;
                        nPadre.Hijos[2] = nHijoDerecho;
                    }
                    // si si existe papá y tiene espacio el valor se mete en él y se reorganizan los hijos.
                    else 
                    {
                        InsertarAca(ref nAuxiliar.Padre, nAuxiliar.Elementos[0]);
                        // REVISAR EN ESTA POSICION COMO QUEDARÍAN LOS HIJOS

                        Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(vNuevo);
                        Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                        nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                        nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                    }
                }
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1].CompareTo(vNuevo) == 1)
                {
                    // Subir vNuevo
                }
                else
                {
                    // Subir nAuxiliar.Elementos[1]
                }
            }

            return default(T); 
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
