using System;
using System.Collections.Generic;

namespace moleQule.Library
{
    /// <summary>
	/// Agente Acreedor
	/// </summary>
	public interface IModuleDef
	{
		string Name { get; }
		Type Type { get; }
		Type[] Mappings { get; }

		void GetEntities(Dictionary<Type, Type> RecordEntities);
	}
}

