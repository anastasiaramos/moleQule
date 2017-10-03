using System;
using System.Collections;
using System.Collections.Generic;

namespace moleQule.Library
{
	[Serializable()]
	public class HashOidList
	{
		public Hashtable List = new Hashtable();
		public string Oids = "0";

		public List<long> ToList()
		{
			List<long> list = new List<long>();

			foreach (long item in List.Values)
				list.Add(item);

			return list;
		}

		public void Add(long oid)
		{
			if (List[oid] == null)
			{
				Oids += "," + oid;
				List.Add(oid, oid);
			}
		}

		public void Clear()
		{
			Oids = "0";
			List.Clear();
		}
	}
}
