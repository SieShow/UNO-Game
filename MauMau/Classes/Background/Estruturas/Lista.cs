﻿using MauMau.Classes.Exceptions;
using System;
using System.Collections;

namespace MauMau.Classes.Background.Estruturas
{
    /// <summary>
    /// Lista de elementos do tipo T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lista<T> : IEnumerator, IEnumerable
    {
        private Elemento prim, ult;
        private int count;
        private int actualIndexPosition = -1;
        public int Count { get { return this.count; } }
        /// <summary>
        /// Retorna o elemento referente ao ponteiro
        /// </summary>
        public object Current
        {
            get
            {
                T val = this[actualIndexPosition];
                if (val == null) throw new InvalidOperationException();
                else return val;
            }
        }
        public Lista()
        {
            this.prim = new Elemento(null, -1);
            this.ult = this.prim;
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                T val = (T)this.GetByIndex(index);
                if (val == null) throw new InvalidIndexException(this.ToString() + " line 41");
                return val;
            }
            set
            {
                Elemento val = this.GetElementoByIndex(index);
                val.SetDado(value);
            }
        }
        /// <summary>
        /// Retorna um objeto pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private object GetByIndex(int val)
        {
            Elemento aux = prim;
            if (val >= this.count || val < 0) throw new InvalidIndexException(this.ToString() + ", line 56");
            else if (aux == null) throw new NullReferenceException(this.ToString() + ", line 57");
            while (aux != null && val != aux.GetIndex())
            {
                aux = aux.Prox;
            }
            return aux.GetDado();
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private Elemento GetElementoByIndex(int val)
        {
            Elemento aux = this.prim;
            if (val > this.count || val < 0) throw new IndexOutOfRangeException(this.ToString() + ", line 72");
            else if (aux == null) throw new NullReferenceException(this.ToString() + ", line 73");

            while (val != aux.GetIndex()) aux = aux.Prox;
            return aux;
        }
        /// <summary>
        /// Através de um objeto, verifica se o mesmo existe na lista
        /// </summary>
        /// <param name="dado"></param>
        /// <returns></returns>
        private Elemento GetElementOnList(object dado)
        {
            Elemento aux = this.prim.Prox;
            while (aux.GetDado() != dado || aux == null) aux = aux.Prox;
            return aux;
        }
        /// <summary>
        /// Retorna o primeiro elemento na lista
        /// </summary>
        /// <returns></returns>
        public T Primeiro()
        {
            return (T)this.prim.Prox.GetDado();
        }
        /// <summary>
        /// Acrescenta o contador de elementos da lista
        /// </summary>
        protected void ElementAdded()
        {
            count++;
        }
        /// <summary>
        /// Decrescenta o contador de elementos da lista
        /// </summary>
        protected void ElementDeleted()
        {
            count--;
        }
        /// <summary>
        /// "Recria" a lista
        /// </summary>
        private void Rebuild()
        {
            if (this.prim == null)
            {
                object dad = null;
                this.prim = new Elemento(dad, 0);
                this.ult = prim;
            }
        }
        /// <summary>
        /// Adiciona um elemento na lista
        /// </summary>
        /// <param name="el"></param>
        public virtual void Add(object el)
        {
            Elemento aux = new Elemento(el, this.count);
            this.ult.Prox = aux;
            this.ult = aux;
            this.ElementAdded();
        }

        /// <summary>
        /// Remove o primeiro elemento da lista
        /// </summary>
        /// <returns></returns>
        public virtual T RemoveFirst()
        {
            if (this.prim.Prox != null)
            {
                Elemento aux = this.prim;
                Elemento auxret = aux.Prox;
                aux.Prox = auxret.Prox;
                if (auxret == this.ult) this.ult = aux;
                else auxret.Prox = null;

                this.ElementDeleted();
                this.Rebuild();
                this.RefactoreIndex();
                return (T)auxret.GetDado();
            }
            else return default(T);
        }
        /// <summary>
        /// Procura um elemento 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T Find(T obj)
        {
            Elemento aux = this.prim;
            while (aux != null)
            {
                if (aux.GetDado().Equals(obj))
                {
                    return (T)aux.GetDado();
                }
                aux = aux.Prox;
            }
            return default(T);
        }
        /// <summary>
        /// Limpa totalmente a lista
        /// </summary>
        public void Clear()
        {
            Elemento aux = this.prim;
            Elemento aux2;
            while (aux != null)
            {
                aux2 = aux;
                aux = null;
                aux = aux2.Prox;
            }
        }
        /// <summary>
        /// Setta para o proximo elmeento da lista
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            actualIndexPosition++;
            return (actualIndexPosition < this.count);
        }
        /// <summary>
        /// Retorna o ponteiro para o primeiro elemento da lista
        /// </summary>
        public void Reset()
        {
            this.actualIndexPosition = -1;
        }
        /// <summary>
        /// Pega o elemento
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new internClass(this);
        }
        /// <summary>
        /// Classe interna para foreach
        /// </summary>
        private sealed class internClass : IEnumerator
        {
            private Lista<T> val;
            private int indexactual = -1;

            public internClass(Lista<T> val)
            {
                this.val = val;
            }
            /// <summary>
            /// Pega o elemento atual
            /// </summary>
            public object Current
            {
                get
                {
                    return this.val[indexactual];
                }
            }
            /// <summary>
            /// Move para o proximo elemento da lista
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                indexactual++;
                return (indexactual < this.val.count);
            }
            /// <summary>
            /// Reseta o ponteiro de posição
            /// </summary>
            public void Reset()
            {
                indexactual = -1;
            }
        }
        /// <summary>
        /// Retorna o index de um elemento
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetIndexOf(object obj)
        {
            Elemento aux = prim;
            while (aux != null && aux.GetDado() != obj)
            {
                aux = aux.Prox;
            }
            return aux.GetIndex();
        }
        /// <summary>
        /// Remove um elemento da lista
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T Remover(object obj)
        {
            Elemento aux = this.prim;
            while ((aux.Prox != null) && (!aux.Prox.GetDado().Equals(obj)))
            {
                aux = aux.Prox;
            }
            if (aux.Prox != null)
            {
                Elemento auxret = aux.Prox;
                aux.Prox = auxret.Prox;
                if (auxret == this.ult) this.ult = aux;
                else auxret.Prox = null;

                this.ElementDeleted();
                this.Rebuild();
                this.RefactoreIndex();
                return (T)auxret.GetDado();
            }
            else return default(T);
        }
        /// <summary>
        /// Remove um elemento localizado em uma posição fornecida
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T RemoveAt(int index)
        {
            Elemento aux = this.prim;
            for (int i = 0; i < index; i++)
            {
                if (aux.Prox != null) aux = aux.Prox;
            }

            Elemento auxret = aux.Prox;
            aux.Prox = auxret.Prox;
            if (auxret == this.ult) this.ult = aux;
            else auxret.Prox = null;

            this.ElementDeleted();
            this.Rebuild();
            this.RefactoreIndex();
            return (T)auxret.GetDado();
        }
        /// <summary>
        /// Reajusta os indeces dos elementos da lista
        /// </summary>
        private void RefactoreIndex()
        {
            Elemento aux = this.prim;
            while (aux != null)
            {
                if (null != aux.Prox && aux.GetIndex() != aux.Prox.GetIndex() - 1)
                {
                    aux = aux.Prox;
                    while (aux != null)
                    {
                        aux.SetIndex(aux.GetIndex() - 1);
                        aux = aux.Prox;
                    }
                    return;
                }
                aux = aux.Prox;
            }
        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(object dadofrom, object dadoto)
        {

        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(object dadofrom, int dadoto)
        {

        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(int dadofrom, object dadoto)
        {
            this.TreatIndexException(dadofrom);
            this.TreatElementException(dadoto);

            object aux = this.GetElementoByIndex(dadofrom);
            ((Elemento)dadoto).SetDado(this.GetElementoByIndex(dadofrom).GetDado());
            this.GetElementoByIndex(dadofrom).SetDado(aux);
        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(int dadofrom, int dadoto)
        {
            this.TreatIndexException(dadofrom);
            this.TreatIndexException(dadoto);

            object aux = this.GetByIndex(dadoto);
            this.GetElementoByIndex(dadoto).SetDado(this.GetElementoByIndex(dadofrom).GetDado());
            this.GetElementoByIndex(dadofrom).SetDado(aux);
        }
        /// <summary>
        /// Embaralha a posição de todos os elementos da lista
        /// </summary>
        /// <param name="RAM"></param>
        public void SwitchAll(Random RAM)
        {
            if (this.count > 1)
            {
                for (int i = 0; i < this.count; i++)
                {
                    int newplace = RAM.Next(0, this.count - 1);
                    object obj = this[i];
                    this[i] = this[newplace];
                    this[newplace] = (T)obj;
                }
            }
        }
        /// <summary>
        /// Verifica se ocorrerá uma exceção do tipo IndexOutOfRangeException
        /// </summary>
        /// <param name="forsearch"></param>
        private void TreatIndexException(int forsearch)
        {
            if (forsearch < 0 || forsearch > this.count) throw new IndexOutOfRangeException();
        }
        /// <summary>
        /// Verifica se ocorrerá uma exceção do tipo NullReferenceException
        /// </summary>
        /// <param name="el"></param>
        private void TreatElementException(Elemento el)
        {
            Elemento aux;
            if (el == null) throw new ArgumentNullException();
            else if ((aux = GetElementOnList(el.GetDado())) == null) throw new NullReferenceException();
        }
        /// <summary>
        /// Verifica se ocorrerá uma exceção do tipo NullReferenceException
        /// </summary>
        /// <param name="obj"></param>
        private void TreatElementException(object obj)
        {
            Elemento aux;
            if (obj == null) throw new ArgumentNullException();
            else if ((aux = this.GetElementOnList(obj)) == null) throw new NullReferenceException();
        }
        /// <summary>
        /// Retorna o elemento na ultima posição da lista
        /// </summary>
        /// <returns></returns>
        public T GetElementInLastIndex()
        {
            return (T)this.GetByIndex(this.count);
        }
    }
}
