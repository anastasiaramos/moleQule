
namespace moleQule.Library
{
    /// <summary>
    /// MAIN ROOT Editable Business Object Interface
	/// Each Application MUST contain a MAIN ROOT OBJECT with this interface.
	/// You can use the "Schema" default main object or define your own application ISchema main object.
	/// In this case, the new main object in the application must be a clon of Schema object with its 
	/// own specifications.
    /// </summary>
	public interface ISchema 
	{
		long Oid { get;	}
		string Code { get; }
		string Name { get; }
        bool Principal { get; set; }
        bool UseDefaultReports { get; set; }

		string SchemaCode { get; }

		/// <summary>
		/// Devuelve el siguiente código de Schema.
		/// </summary>
		/// <returns>Código de N cifras</returns>
		//string GetNewCode();

		/// <summary>
		/// Devuelve el siguiente Serial de Schema.
		/// </summary>
		/// <returns>Código de N cifras</returns>
		//Int64 GetNewSerial();

		ISchema IGet(long oid);
		void IDelete(long oid);
    }
	
}
