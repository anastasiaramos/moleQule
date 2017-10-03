
namespace moleQule.Library
{
	/// <summary>
	/// MAIN ROOT ReadOnly Business Object Interface.
	/// </summary>
	public interface ISchemaInfo
	{
		long Oid { get; set; }
		string Code { get; set; }
		string Name { get; }
        bool Principal { get; set; }
        bool UseDefaultReports { get; }

		string SchemaCode { get; }

		ISchemaInfo IGet(long oid);
		ISchema IGetSchema(long oid);
        System.Byte[] GetImage();
	}

}
