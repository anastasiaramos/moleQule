using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace moleQule.Library.Tools
{
	public class EntityJsonConverter : JavaScriptConverter
	{

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			esEntity entity = Activator.CreateInstance(type) as esEntity;

			if (entity != null)
			{
				List<string> propertyNames = (from esColumnMetadata c in entity.es.Meta.Columns select c.PropertyName).ToList();
				var properties = dictionary.Where(e => propertyNames.Contains(e.Key)).ToDictionary(e => e.Key, e => e.Value);
				var extraColumns = dictionary.Where(e => !propertyNames.Contains(e.Key)).ToDictionary(e => e.Key, e => e.Value);
				entity.SetProperties(properties);
				entity.SetExtraColumns(extraColumns);
			}
			return entity;
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();

			var entity = obj as esEntity;

			if (entity != null)
			{
				foreach (esColumnMetadata c in entity.es.Meta.Columns)
				{
					result.Add(c.PropertyName, entity.GetColumn(c.Name));
				}
				result = result.Concat(entity.GetExtraColumns()).ToDictionary(e => e.Key, e => e.Value);
			}
			return result;
		}

		private List<Type> _supportedTypes = new List<Type>() { typeof(esEntity) };

		public override IEnumerable<Type> SupportedTypes
		{
			get { return _supportedTypes; }
		}
	}
}

