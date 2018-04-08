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
        private Nodo2_3<T> RaizSecundaria;
        private bool TrabajarSobreSecundaria = false;
        public int nElementos;

        public Arbol2_3()
        {
            this.Raiz = null;
            this.nElementos = 0;
        }

        public void Insertar(T vNuevo)
        {
            this.RaizSecundaria = null;

            nElementos += 1;
            Insertar(vNuevo, ref Raiz);

            if (TrabajarSobreSecundaria == true)
            {
                Raiz = RaizSecundaria;
                TrabajarSobreSecundaria = false;
            }
                
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
                        nHijoIzquierdo.Padre = nPadre;
                        nHijoDerecho.Padre = nPadre;

                        nPadre.Hijos[0] = nHijoIzquierdo;
                        nPadre.Hijos[2] = nHijoDerecho;

                        int cont = 0;
                        ContarLLaves(nPadre, ref cont);

                        if (nElementos == cont)
                        {
                            Raiz = nPadre;
                        }
                        else
                        {
                            TrabajarSobreSecundaria = true;
                            RaizSecundaria = nPadre;
                        }
                            
                    }
                    // si si existe papá y tiene espacio el valor se mete en él y se reorganizan los hijos.
                    else 
                    {
                        InsertarAca(ref nAuxiliar.Padre, nAuxiliar.Elementos[0]);

                        // Si en el nodo que se encuentra es un hijo izquierdo se acomodan la nueva distribucion de los hijos dependiendo de este criterio
                        // Es exactamente lo mismo con las demás opciones.
                        if (nAuxiliar.PosicionHijo == "Hijo Izquierdo")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = Raiz.Hijos[1];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(vNuevo);
                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(nAuxiliar.Elementos[1]);
                            Nodo2_3<T> nHijoDerecho;

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoDerecho = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoDerecho = nAuxiliar.Padre.Hijos[2];

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {

                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoDerecho;
                            }
                            
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Central")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoDerecho;
                            }
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Derecho")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[0].Hijos[2] = Raiz.Hijos[1];
                            }

                            Nodo2_3<T> nHijoIzquierdo = nAuxiliar.Padre.Hijos[1];

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[0];

                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(vNuevo);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoCentral;
                                RaizSecundaria.Hijos[2].Hijos[2] = nHijoDerecho;
                            }

                        }

                    }
                }
                else if (nAuxiliar.Elementos[0].CompareTo(vNuevo) == -1 && nAuxiliar.Elementos[1].CompareTo(vNuevo) == 1)
                {
                    // Subir vNuevo

                    // Si el papá del actual es nulo, tiene que crearse un nuevo nodo que sea papi para el acutual.
                    if (nAuxiliar.Padre == null)
                    {
                        Nodo2_3<T> nPadre = new Nodo2_3<T>(vNuevo);
                        Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                        Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                        nHijoIzquierdo.Padre = nPadre;
                        nHijoDerecho.Padre = nPadre;

                        nPadre.Hijos[0] = nHijoIzquierdo;
                        nPadre.Hijos[2] = nHijoDerecho;

                        int cont = 0;
                        ContarLLaves(nPadre, ref cont);

                        if (nElementos == cont)
                        {
                            Raiz = nPadre;
                        }
                        else
                        {
                            TrabajarSobreSecundaria = true;
                            RaizSecundaria = nPadre;
                        }

                    }
                    // si si existe papá y tiene espacio el valor se mete en él y se reorganizan los hijos.
                    else
                    {
                        InsertarAca(ref nAuxiliar.Padre, vNuevo);

                        // Si en el nodo que se encuentra es un hijo izquierdo se acomodan la nueva distribucion de los hijos dependiendo de este criterio
                        // Es exactamente lo mismo con las demás opciones.
                        if (nAuxiliar.PosicionHijo == "Hijo Izquierdo")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = Raiz.Hijos[1];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(nAuxiliar.Elementos[1]);
                            Nodo2_3<T> nHijoDerecho;

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoDerecho = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoDerecho = nAuxiliar.Padre.Hijos[2];

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoDerecho;
                            }
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Central")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoDerecho;
                            }
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Derecho")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[0].Hijos[2] = Raiz.Hijos[1];
                            }

                            Nodo2_3<T> nHijoIzquierdo;

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[0];

                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(nAuxiliar.Elementos[1]);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoCentral;
                                RaizSecundaria.Hijos[2].Hijos[2] = nHijoDerecho;
                            }
                            
                        }

                    }
                }
                else
                {
                    // Subir nAuxiliar.Elementos[1]

                    // Si el papá del actual es nulo, tiene que crearse un nuevo nodo que sea papi para el acutual.
                    if (nAuxiliar.Padre == null)
                    {
                        Nodo2_3<T> nPadre = new Nodo2_3<T>(nAuxiliar.Elementos[1]);
                        Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                        Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(vNuevo);
                        nHijoIzquierdo.Padre = nPadre;
                        nHijoDerecho.Padre = nPadre;

                        nPadre.Hijos[0] = nHijoIzquierdo;
                        nPadre.Hijos[2] = nHijoDerecho;

                        int cont = 0;
                        ContarLLaves(nPadre, ref cont);

                        if (nElementos == cont)
                        {
                            Raiz = nPadre;
                        }
                        else
                        {
                            TrabajarSobreSecundaria = true;
                            RaizSecundaria = nPadre;
                        }
                    }
                    // si si existe papá y tiene espacio el valor se mete en él y se reorganizan los hijos.
                    else
                    {
                        InsertarAca(ref nAuxiliar.Padre, nAuxiliar.Elementos[1]);

                        // Si en el nodo que se encuentra es un hijo izquierdo se acomodan la nueva distribucion de los hijos dependiendo de este criterio
                        // Es exactamente lo mismo con las demás opciones.
                        if (nAuxiliar.PosicionHijo == "Hijo Izquierdo")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = Raiz.Hijos[1];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(vNuevo);
                            Nodo2_3<T> nHijoDerecho;

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoDerecho = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoDerecho = nAuxiliar.Padre.Hijos[2];

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;


                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoDerecho;
                            }
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Central")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[2].Hijos[2] = Raiz.Hijos[2];
                            }

                            Nodo2_3<T> nHijoIzquierdo = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(vNuevo);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                RaizSecundaria.Hijos[0].Hijos[2] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[0].Hijos[1] = nHijoIzquierdo;
                                RaizSecundaria.Hijos[1].Hijos[0] = nHijoDerecho;
                            }
                        }
                        else if (nAuxiliar.PosicionHijo == "Hijo Derecho")
                        {
                            if (TrabajarSobreSecundaria == true)
                            {
                                RaizSecundaria.Hijos[0].Hijos[0] = Raiz.Hijos[0];
                                RaizSecundaria.Hijos[0].Hijos[2] = Raiz.Hijos[1];
                            }

                            // Hermano adyacente
                            Nodo2_3<T> nHijoIzquierdo;

                            if (nAuxiliar.Padre.Hijos[1] != null)
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[1];
                            else
                                nHijoIzquierdo = nAuxiliar.Padre.Hijos[0];

                            Nodo2_3<T> nHijoCentral = new Nodo2_3<T>(nAuxiliar.Elementos[0]);
                            Nodo2_3<T> nHijoDerecho = new Nodo2_3<T>(vNuevo);

                            nHijoIzquierdo.Padre = nAuxiliar.Padre;
                            nHijoCentral.Padre = nAuxiliar.Padre;
                            nHijoDerecho.Padre = nAuxiliar.Padre;

                            if (TrabajarSobreSecundaria == false)
                            {
                                nAuxiliar.Padre.Hijos[0] = nHijoIzquierdo;
                                nAuxiliar.Padre.Hijos[1] = nHijoCentral;
                                nAuxiliar.Padre.Hijos[2] = nHijoDerecho;
                            }
                            else
                            {
                                RaizSecundaria.Hijos[2].Hijos[0] = nHijoCentral;
                                RaizSecundaria.Hijos[2].Hijos[2] = nHijoDerecho;
                            }
                            
                        }

                    }
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

        private void ContarLLaves(Nodo2_3<T> Aux, ref int Elements)
        {
            if (Aux != null)
            {
                ContarLLaves(Aux.Hijos[0], ref Elements);
                if (Aux.Elementos[0] != null)
                    Elements++;
                ContarLLaves(Aux.Hijos[1], ref Elements);
                if (Aux.Elementos[1] != null)
                    Elements++;
                ContarLLaves(Aux.Hijos[2], ref Elements);
            }
        }


    }
}
