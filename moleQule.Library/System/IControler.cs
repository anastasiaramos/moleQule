using System;
using System.Collections.Generic;

namespace moleQule.Library
{
	public interface IController
	{
#if TRACE
		moleQule.Library.Timer Timer { get; }
#endif
		Dictionary<Type, IModuleDef> Modules { get; }
		Dictionary<Type, Type> RecordEntities { get; }

		void ActivateEntities();
	}
}

