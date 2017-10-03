using System;
using System.Collections;
using System.Collections.Generic;

namespace moleQule.Library
{
    public class Cache
    {
        protected Hashtable _hash_list = null;
        
        #region Factory Methods

                /// <summary>
        /// Única instancia de la clase PrincipalBase (Singleton)
        /// </summary>
        protected static Cache _main;

        /// <summary>
        /// Unique Cache Class Instance
        /// </summary>
        public static Cache Instance
        {
            get
            {
                return (_main != null) ? _main : new Cache();
            }
        }

        public Cache()
        {
            // Singleton
            _main = this;
            _hash_list = new Hashtable();
        }

        #endregion

        /// <summary>
        /// Guarda en cache una lista. Si ya habia una de ese tipo la sobrescribe
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        public void Save(Type type, object obj)
        {
            if (!_hash_list.ContainsKey(type.GUID))
                _hash_list.Add(type.GUID, obj);
            else
                _hash_list[type.GUID] = obj;
        }

        /// <summary>
        /// Devuelve la cache de un tipo
        /// </summary>
        /// <param name="type"></param>
        public object Get(Type type) { return _hash_list[type.GUID]; }

        /// <summary>
        /// Devuelve la cache de un tipo
        /// </summary>
        /// <param name="type"></param>
        public bool Contains(Type type) { return _hash_list.ContainsKey(type.GUID); }

        /// <summary>
        /// Borra un tipo de la cache
        /// </summary>
        /// <param name="type"></param>
        public void Remove(Type type) { _hash_list.Remove(type.GUID); }

        /// <summary>
        /// Limpia la cache
        /// </summary>
        /// <returns></returns>
        public void Clear() { _hash_list.Clear(); }

    }
}
