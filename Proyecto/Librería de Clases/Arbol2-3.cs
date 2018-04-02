﻿using System;
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

        public Arbol2_3<T> Buscar(T value, Arbol2_3<T> raiz)
        {
            if (Raiz == null)
            {
                return null;
            }
            else if (raiz.Valor.CompareTo(value) == 0)
            {
                return raiz;
            }
            else if (raiz.Valor.CompareTo(value) == -1)
            {
                return Buscar(value, raiz.HijoDerecho);
            }
            else
            {
                return Buscar(value, raiz.HijoIzquierdo);
            }
        }

        public int Balancear(Arbol2_3<T> x)
        {
            if (x == null)
                return -1;
            else
                return x.FactorBalanceo;
        }

        //Rotación Simple Izquierda
        public Arbol2_3<T> RotacionIzquierda(Arbol2_3<T> Nodo)
        {
            Arbol2_3<T> Auxiliar = Nodo.HijoIzquierdo;
            Nodo.HijoIzquierdo = Auxiliar.HijoDerecho;

            Auxiliar.HijoDerecho = Nodo;
            //Obtiene el mayor factor de equilibrio de sus hijos.
            Nodo.FactorBalanceo = Math.Max(Balancear(Nodo.HijoIzquierdo), Balancear(Nodo.HijoDerecho) + 1);
            Auxiliar.FactorBalanceo = Math.Max(Balancear(Auxiliar.HijoIzquierdo), Balancear(Auxiliar.HijoDerecho)) + 1;
            return Auxiliar;
        }
        //Rotación Simple Derecha
        public Arbol2_3<T> RotacionDerecha(Arbol2_3<T> Nodo)
        {
            Arbol2_3<T> Auxiliar = Nodo.HijoDerecho;
            Nodo.HijoDerecho = Auxiliar.HijoIzquierdo;

            Auxiliar.HijoIzquierdo = Nodo;
            //Obtiene el mayor factor de equilibrio de sus hijos.
            Nodo.FactorBalanceo = Math.Max(Balancear(Nodo.HijoIzquierdo), Balancear(Nodo.HijoDerecho)) + 1;
            Auxiliar.FactorBalanceo = Math.Max(Balancear(Auxiliar.HijoIzquierdo), Balancear(Auxiliar.HijoDerecho)) + 1;
            return Auxiliar;
        }

        //Rotación Doble Izquierda
        public Arbol2_3<T> RotacionDobleIzquierda(Arbol2_3<T> Nodo)
        {
            Arbol2_3<T> Auxiliar;
            Nodo.HijoIzquierdo = RotacionDerecha(Nodo.HijoIzquierdo);
            Auxiliar = RotacionIzquierda(Nodo);
            return Auxiliar;
        }
        //Rotación Doble Derecha
        public Arbol2_3<T> RotacionDobleDerecha(Arbol2_3<T> Nodo)
        {
            Arbol2_3<T> Auxiliar;
            Nodo.HijoDerecho = RotacionIzquierda(Nodo.HijoDerecho);
            Auxiliar = RotacionDerecha(Nodo);
            return Auxiliar;
        }

        //Metodo InsertarHijos
        public Arbol2_3<T> InsertarHijos(Arbol2_3<T> Nuevo, Arbol2_3<T> SubArbol)
        {
            Arbol2_3<T> NuevoPadre = SubArbol;
            if (Nuevo.Valor.CompareTo(SubArbol.Valor) == -1)
            {
                if (SubArbol.HijoIzquierdo == null)
                {
                    SubArbol.HijoIzquierdo = Nuevo;
                }
                else
                {
                    SubArbol.HijoIzquierdo = InsertarHijos(Nuevo, SubArbol.HijoIzquierdo);
                    if (Balancear(SubArbol.HijoIzquierdo) - Balancear(SubArbol.HijoDerecho) == 2)
                    {
                        if (Nuevo.Valor.CompareTo(SubArbol.HijoIzquierdo.Valor) == -1)
                        {
                            NuevoPadre = RotacionIzquierda(SubArbol);
                        }
                        else
                        {
                            NuevoPadre = RotacionDobleIzquierda(SubArbol);
                        }
                    }
                }


            }
            else if (Nuevo.Valor.CompareTo(SubArbol.Valor) == 1)
            {
                if (SubArbol.HijoDerecho == null)
                {
                    SubArbol.HijoDerecho = Nuevo;
                }
                else
                {
                    SubArbol.HijoDerecho = InsertarHijos(Nuevo, SubArbol.HijoDerecho);
                    if (Balancear(SubArbol.HijoDerecho) - Balancear(SubArbol.HijoIzquierdo) == 2)
                    {
                        if (Nuevo.Valor.CompareTo(SubArbol.HijoDerecho.Valor) == 1)
                        {
                            NuevoPadre = RotacionDerecha(SubArbol);
                        }
                        else
                        {
                            NuevoPadre = RotacionDobleDerecha(SubArbol);
                        }
                    }
                }
            }
            else
            {
                //Nodo con un factor de ordenamiento ya existente
            }


            //Actualizando Factor Equilibrio
            if (SubArbol.HijoIzquierdo == null && SubArbol.HijoDerecho != null)
            {
                SubArbol.FactorBalanceo = SubArbol.HijoDerecho.FactorBalanceo + 1;
            }
            else if (SubArbol.HijoDerecho == null && SubArbol.HijoIzquierdo != null)
            {
                SubArbol.FactorBalanceo = SubArbol.HijoIzquierdo.FactorBalanceo + 1;
            }
            else
            {
                SubArbol.FactorBalanceo = Math.Max(Balancear(SubArbol.HijoIzquierdo), Balancear(SubArbol.HijoDerecho)) + 1;
            }
            return NuevoPadre;
        }
        public void Insertar(T value)
        {
            Arbol2_3<T> Nuevo = new Arbol2_3<T>(value);

            if (Raiz == null)
                Raiz = Nuevo;
            else
                Raiz = InsertarHijos(Nuevo, Raiz);

        }

        private Arbol2_3<T> Reemplazar(Arbol2_3<T> NodoAEliminar)
        {
            Arbol2_3<T> remplazoPadre = NodoAEliminar;
            Arbol2_3<T> reemplazo = NodoAEliminar;
            Arbol2_3<T> auxiliar = NodoAEliminar.HijoDerecho;
            while (auxiliar != null)
            {
                remplazoPadre = reemplazo;
                reemplazo = auxiliar;
                auxiliar = auxiliar.HijoIzquierdo;
            }
            if (reemplazo != NodoAEliminar.HijoDerecho)
            {
                remplazoPadre.HijoIzquierdo = reemplazo.HijoDerecho;
                reemplazo.HijoDerecho = NodoAEliminar.HijoDerecho;
            }
            return reemplazo;
        }

        Arbol2_3<T> nodoE, nodoP;
        public Arbol2_3<T> Eliminar(T valorEliminar, ref Arbol2_3<T> Raiz)
        {

            if (Raiz != null)
            {
                if (Raiz.Valor.CompareTo(valorEliminar) == -1)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.HijoIzquierdo);
                }
                else
                {
                    if (Raiz.Valor.CompareTo(valorEliminar) == 1)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.HijoIzquierdo);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar
                        Arbol2_3<T> NodoEliminar = Raiz;
                        if (NodoEliminar.HijoDerecho == null)
                        {
                            Raiz = NodoEliminar.HijoIzquierdo;
                            if (Balancear(nodoE.HijoIzquierdo) - Balancear(nodoE.HijoDerecho) == 2)
                            {
                                if (nodoE.Valor.CompareTo(valorEliminar) == 1)
                                    nodoP = RotacionIzquierda(nodoE);
                                else
                                    nodoE = RotacionDerecha(nodoE);
                            }
                            if (Balancear(nodoE.HijoDerecho) - Balancear(nodoE.HijoIzquierdo) == 2)
                            {
                                if (nodoE.HijoDerecho.Valor.CompareTo(valorEliminar) == -1)
                                    nodoE = RotacionDerecha(nodoE);
                                else
                                    nodoE = RotacionDobleDerecha(nodoE);
                                nodoP = RotacionDerecha(nodoE);
                            }
                        }
                        else
                        {
                            if (NodoEliminar.HijoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.HijoDerecho;
                            }
                            else
                            {
                                if (Balancear(Raiz.HijoIzquierdo) - Balancear(Raiz.HijoDerecho) > 0)
                                {
                                    Arbol2_3<T> AuxiliarNodo = null;
                                    Arbol2_3<T> Auxiliar = Raiz.HijoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.HijoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.HijoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.Valor = Auxiliar.Valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.HijoDerecho = Auxiliar.HijoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.HijoIzquierdo = Auxiliar.HijoIzquierdo;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso
                                }
                                else
                                {
                                    if (Balancear(Raiz.HijoDerecho) - Balancear(Raiz.HijoIzquierdo) > 0)
                                    {
                                        Arbol2_3<T> AuxiliarNodo = null;
                                        Arbol2_3<T> Auxiliar = Raiz.HijoDerecho;
                                        bool Bandera = false;
                                        while (Auxiliar.HijoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.HijoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.Valor = Auxiliar.Valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.HijoIzquierdo = Auxiliar.HijoIzquierdo;
                                        }
                                        else
                                        {
                                            Raiz.HijoDerecho = Auxiliar.HijoDerecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Balancear(Raiz.HijoDerecho) - Balancear(Raiz.HijoIzquierdo) == 0)
                                        {
                                            Arbol2_3<T> AuxiliarNodo = null;
                                            Arbol2_3<T> Auxiliar = Raiz.HijoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.HijoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.HijoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.Valor = Auxiliar.Valor;
                                            NodoEliminar = Auxiliar;
                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.HijoDerecho = Auxiliar.HijoDerecho;
                                            }
                                            else
                                            {
                                                Raiz.HijoIzquierdo = Auxiliar.HijoIzquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new System.InvalidOperationException("Nodo inexistente en el arbol");
            }
            return nodoP;
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
