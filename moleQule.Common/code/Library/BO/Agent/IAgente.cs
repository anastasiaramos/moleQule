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
    /// <summary>
	/// Agente 
	/// </summary>
	public interface IAgente
	{
		string ID { get; set; }
		long TipoId { get; set; }
		string Nombre { get; set; }
		string Alias { get; set; }
	}

    public interface IAgenteInfo
    {
		string ID { get; }
		long TipoId { get; }
		string Nombre { get; }
		string Alias { get; }
	}

	/// <summary>
	/// Agente Base
	/// </summary>
	public class AgenteBase
	{
		#region Business Methods

		public static string GetTipoIDMask(ETipoID tipo)
		{
			switch (tipo)
			{
				case ETipoID.CIF:
					return Resources.Labels.CIF_MASK;

				case ETipoID.DNI:
				case ETipoID.NIF:
					return Resources.Labels.NIF_MASK;

				case ETipoID.NIE:
					return Resources.Labels.NIE_MASK;

				case ETipoID.OTROS:
					return string.Empty;
			}

			return string.Empty;
		}

		public static void ValidateInput(ETipoID tipo, string field, string value)
		{
			switch (tipo)
			{
				case ETipoID.CIF:
					Validator.ValidateCIF(field, value);
					break;

				case ETipoID.NIF:
				case ETipoID.DNI:
					Validator.ValidateNIF(field, value);
					break;

				case ETipoID.NIE:
					Validator.ValidateNIE(field, value);
					break;
			}
		}

		#endregion
	}

}

