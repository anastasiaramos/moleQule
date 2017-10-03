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
    /// <summary>
	/// Agente Acreedor
	/// </summary>
    public interface ITransactionPayment
	{
        long Oid { get; }
		long OidExpediente { get; }
		string FechaAsignacion { get; set;}
		decimal Total { get; }
		decimal TotalPagado { get; set; }
		decimal Pendiente { get; set; }
		decimal PendienteAsignar { get; set; }
		decimal Asignado { get; set; }
		decimal Acumulado { get; set; }
		string Vinculado { get; set; }
		string NFactura { get; }
		ETipoAcreedor ETipoAcreedor { get; }

		//ETipoPago ETipoPago { get; set; }

		/*ProductoProveedores ProductoProveedores { get; }
		Pagos Pagos { get; }

        string CuentaAsociada { get; }
        string Impuesto { get; }
        decimal PImpuesto { get; }

        int SessionCode { get; }
        ISession Session();

        void BeginEdit();
        void ApplyEdit();
        void CancelEdit();
        void CloseSession();
        IAcreedor IClone();
        IAcreedor ISave();
		IAcreedorInfo IGetInfo();*/
	}
}