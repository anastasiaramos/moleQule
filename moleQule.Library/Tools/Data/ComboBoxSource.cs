using System;
using System.Collections.Generic;
using System.Text;

namespace moleQule.Library
{
    public interface IComboBoxSource
    {
        long Oid { get; }
        string Value { get; }
    }

    //clase para rellenar los combos
    public class ComboBoxSource
    {
        private string _texto = string.Empty;
        private long _oid = 0;
        private long _oid_ajeno = 0;
        private long _tipo = 0;

        public string Texto 
        {
			get
			{
            	return _texto;
			}
			set
			{
				if (_texto != null && !_texto.Equals(value))
				{
					_texto = value;
				}
			}
        }
        public virtual long Oid
		{
			get
			{
            	return _oid;
			}
			set
			{
				if (!_oid.Equals(value))
				{
					_oid = value;
				}
			}
		}
        public virtual long OidAjeno
        {
            get
            {
                return _oid_ajeno;
            }
            set
            {
                if (!_oid_ajeno.Equals(value))
                {
                    _oid_ajeno = value;
                }
            }
        }
        public virtual long Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                if (!_tipo.Equals(value))
                {
                    _tipo = value;
                }
            }
        }

        public ComboBoxSource(long oid, string texto)
        {
            this.Oid = oid;
            this.Texto = texto;
        }

        public ComboBoxSource()
        {
            
        }    
	}

    public class ComboBoxSourceList : List<ComboBoxSource>
    {
        private ComboBoxSourceList _childs = null;

		/// <summary>
		/// Lista de cache de sublistas de hijos filtradas por OID
		/// </summary>
		private List<ComboBoxSourceList> _cache_childs_by_oid = new List<ComboBoxSourceList>();

        public ComboBoxSourceList Childs
        {
            get { return _childs; }
            set { _childs = value; }
        }

        public ComboBoxSourceList() {}

        protected void AddEmptyItem()
        {
            this.Add(new ComboBoxSource());
        }

		/// <summary>
		/// Busca una lista filtrada en la cache
		/// </summary>
		/// <param name="oid"></param>
		/// <returns></returns>
		private ComboBoxSourceList GetCacheFilteredList(long oid)
		{
			foreach (ComboBoxSourceList cbList in _cache_childs_by_oid)
			{
				//cbList[0] es EmptyItem, por eso preguntamos por cbList[1]
				if (cbList.Count > 1 && cbList[1].OidAjeno == oid) return cbList; 
			}

			return null;
		}

        public ComboBoxSourceList GetFilteredChilds(long oid)
        {
			// Miramos en caché si ya está la lista creada
			ComboBoxSourceList lista = GetCacheFilteredList(oid);
		
			if (lista != null)
				return lista;
			else
				lista = new ComboBoxSourceList();

            lista.AddEmptyItem();

            if (oid == 0) return lista;

            if (Childs != null)
            {
                foreach (ComboBoxSource item in Childs)
                {
                    if (item.OidAjeno == oid)
                    {
                        lista.Add(item);
                    }
                }
            }
			
			_cache_childs_by_oid.Add(lista);

            return lista;
        }

        public ComboBoxSource Buscar(long oid)
        {
            foreach (ComboBoxSource item in this)
            {
                if (item.Oid == oid)
                    return item;
            }
            return null;
        }

        public ComboBoxSource Buscar(long oid, long tipo)
        {
            foreach (ComboBoxSource item in this)
            {
                if (item.Oid == oid && item.Tipo == tipo)
                    return item;
            }
            return null;
        }

        public ComboBoxSource Buscar(long oid, long tipo, string cadena)
        {
            foreach (ComboBoxSource item in this)
            {
                if (item.Oid == oid && item.Tipo == tipo && item.Texto.Contains(cadena))
                    return item;
            }
            return null;
        }

        public ComboBoxSource Buscar(string cadena)
        {
            foreach (ComboBoxSource item in this)
            {
                if (item.Texto == cadena)
                    return item;
            }
            return null;
        }

        public static ComboBoxSource Get(object source, long oid)
        {
            ComboBoxSourceList list = source as ComboBoxSourceList;

            if (list == null) return null;

            return list.Buscar(oid);
        }

        public void Remove(long oid)
        {
            Remove(Buscar(oid));
        }
    }

    public class HComboBoxSourceList : ComboBoxSourceList
    {
        public HComboBoxSourceList() { }

        public HComboBoxSourceList(UserList lista)
        {
            AddEmptyItem();

            foreach (UserInfo item in lista)
            {
                ComboBoxSource combo = new ComboBoxSource();

                combo.Texto = item.Name;
                combo.Oid = item.Oid;
                combo.OidAjeno = 0;

                this.Add(combo);
            }
        }
    }
}
