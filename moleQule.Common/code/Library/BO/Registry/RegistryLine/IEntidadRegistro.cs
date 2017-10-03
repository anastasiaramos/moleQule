using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Common
{
	public interface IEntidadRegistro
	{
		long Oid { get; set; }
		string Codigo { get; set; }
		ETipoEntidad ETipoEntidad { get; }
		string DescripcionRegistro { get; }
		EEstado EEstado { get; set; }

		void CloseSession();
		IEntidadRegistro ISave();
		IEntidadRegistro IGet(long oid, bool childs);
		void Update(Registro parent);	
	}

	public interface IEntidadRegistroInfo
    {
		long Oid { get; }
		string Codigo { get;}
		ETipoEntidad ETipoEntidad { get; }
		string DescripcionRegistro { get; }
	}

	public interface IEntidadRegistroList
	{
		IEntidadRegistro IGetItem(long oid);

		void CloseSession();
		IEntidadRegistro ISave();
		void Update(Registro parent);	
	}

	public struct TEntidadRegistroBase
	{
		public bool Active { get { return Table != string.Empty; } }
		public ETipoEntidad ETipoEntidad;
		public string Table;
		public Type Type;
		public Type ListType;
	}

	[Serializable()]
	public class TEntidadRegistroList
	{
		public ETipoEntidad ETipoEntidad;
		public Type ListType;
		public List<long> Oids;
		public IEntidadRegistroList List;
	}
}

