using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
	public interface IEntity
	{
		long Oid { get; }
		long EntityType { get; }

		int SessionCode { get; }
		ISession Session();
	}

	public interface IEntityBase
	{
        long Oid { get; }
        long Serial { get; }
        string Codigo { get; }
        EEstado EEstado { get; set; }
        DateTime FechaReferencia { get; }

        int SessionCode { get; }
        ISession Session();

        IEntityBase ICloneAsNew();
        void ICopyValues(IEntityBase source);
        void DifferentYearChecks();
        void DifferentYearTasks(IEntityBase oldItem);
        void SameYearTasks(IEntityBase newItem);
        void IEntityBaseSave(object parent);
	}
}