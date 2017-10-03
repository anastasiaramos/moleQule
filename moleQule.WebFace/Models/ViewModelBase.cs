using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using moleQule.Library.CslaEx; 

namespace moleQule.WebFace.Models
{
    [Serializable()]
	public abstract class ViewModelBase<T, C>
	{
		#region Attributes

		#endregion

		#region Properties

		#endregion

		#region Business Objects

		public void CopyFrom(T sourceObj)
		{
			if (sourceObj == null) return;

			foreach (PropertyInfo item in this.GetType().GetProperties())
			{
				if (item != null)
				{
					try
					{
						PropertyInfo source = typeof(T).GetProperty(item.Name);
						if (source != null) item.SetValue(this, source.GetValue(sourceObj, null), null);
					}
					catch { }
				}
			}
		}

		public void CopyFrom(C sourceObj)
		{
			if (sourceObj == null) return;

			foreach (PropertyInfo item in this.GetType().GetProperties())
			{
				if (item != null)
				{
					try
					{
						PropertyInfo source = typeof(C).GetProperty(item.Name);
						if (source != null) item.SetValue(this, source.GetValue(sourceObj, null), null);
					}
					catch { }
				}
			}
		}

		public virtual void CopyTo(T destObj, HttpRequestBase request = null)
		{
			foreach (PropertyInfo item in this.GetType().GetProperties())
			{
				if (item == null) continue;
				if (item.Name == "Oid") continue;
				if (request != null  && request[item.Name] == null) continue;
				
				try
				{
					PropertyInfo dest = typeof(T).GetProperty(item.Name);
					if (dest != null) dest.SetValue(destObj, item.GetValue(this, null), null);
				}
				catch { }
			}
		}

		#endregion
	}
}
