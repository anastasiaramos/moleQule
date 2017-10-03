using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Common.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class MonitorLineViewModel : ViewModelBase<MonitorLine, MonitorLineInfo>, IViewModel
	{
		#region Attributes

		protected MonitorLineBase _base = new MonitorLineBase();

		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long MonitorLineViewModelID { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long OidMonitor { get { return _base.Record.OidMonitor; } set { _base.Record.OidMonitor = value; } }

		[HiddenInput]
		public long Status { get { return _base.Record.Status; } set { _base.Record.Status = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get { return _base.Record.Date; } set { _base.Record.Date = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_IP")]
		public string ComponentIP { get { return _base.Record.ComponentIP; } set { _base.Record.ComponentIP = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_INTERVAL")]
		public long ComponentInterval { get { return _base.Record.ComponentInterval; } set { _base.Record.ComponentInterval = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_STATUS")]
		public long ComponentStatus { get { return _base.Record.ComponentStatus; } set { _base.Record.ComponentStatus = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_LEVEL")]
		public long ErrorLevel { get { return _base.Record.ErrorLevel; } set { _base.Record.ErrorLevel = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DESCRIPTION")]
		public string Description { get { return _base.Record.Description; } set { _base.Record.Description = value; } }

		//FOREIGN PROPERTIES
		[HiddenInput]
		public virtual EComponentStatus EStatus { get { return _base.EStatus; } set { _base.Record.Status = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public string StatusLabel { get { return _base.StatusLabel; } set {} }

        public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_STATUS")]
        public virtual string ComponentStatusLabel { get { return _base.ComponentStatusLabel; } set { } }

        public virtual EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_LEVEL")]
        public virtual string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_INTERVAL_SEC")]
		public long ComponentIntervalSec { get { return _base.Record.ComponentInterval / 1000; } set { _base.Record.ComponentInterval = value * 1000; } }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(MonitorLine source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(MonitorLineInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(MonitorLine dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public MonitorLineViewModel() { }

		public static MonitorLineViewModel New() 
		{
			MonitorLineViewModel obj = new MonitorLineViewModel();
			obj.CopyFrom(MonitorLineInfo.New());
			return obj;
		}
		public static MonitorLineViewModel New(MonitorLine  source) { return New(source.GetInfo(false)); }
		public static MonitorLineViewModel New(MonitorLineInfo source)
		{
			MonitorLineViewModel obj = new MonitorLineViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static MonitorLineViewModel Get(long oid)
		{
			MonitorLineViewModel obj = new MonitorLineViewModel();
			obj.CopyFrom(MonitorLineInfo.Get(oid, false));
			return obj;
		}

		public static void Add(MonitorLineViewModel item)
		{
			MonitorLine newItem = MonitorLine.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(MonitorLineViewModel source)
		{
			MonitorLine item = MonitorLine.Get(source.Oid);
			source.CopyTo(item);
			item.Save();
		}
		public static void Remove(long oid)
		{
			MonitorLine.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class MonitorLineListViewModel : List<MonitorLineViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public MonitorLineListViewModel() { }

		public static MonitorLineListViewModel Get()
		{
			MonitorLineListViewModel list = new MonitorLineListViewModel();

			MonitorLineList sourceList = MonitorLineList.GetList();

			foreach (MonitorLineInfo item in sourceList)
				list.Add(MonitorLineViewModel.New(item));

			return list;
		}
		public static MonitorLineListViewModel Get(MonitorLineList sourceList)
		{
			MonitorLineListViewModel list = new MonitorLineListViewModel();

			foreach (MonitorLineInfo item in sourceList)
				list.Add(MonitorLineViewModel.New(item));

			return list;
		}

		#endregion
	}
}
