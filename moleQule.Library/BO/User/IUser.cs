using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
	/// Agente Acreedor
	/// </summary>
	public interface IUser
	{
		long OidUser { get; set; }
		string Username { get; set; }
		EEstadoItem EUserStatus { get; set; }
		string UserStatusLabel { get; }
		DateTime CreationDate { get; set; }
		DateTime LastLoginDate { get; set; }
	}
}

