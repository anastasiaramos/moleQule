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
	public class MonitorViewModel : ViewModelBase<Monitor, MonitorInfo>, IViewModel
	{
		#region Attributes

		protected MonitorBase _base = new MonitorBase();

		#endregion	
	
		#region Properties

		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

        [HiddenInput]
		public long MonitorViewModelID { get { return Oid; } set { Oid = value; } }		
		
		[HiddenInput]
		public long Status { get { return _base.Record.Status; } set { _base.Record.Status = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_TYPE")]
		public long ComponentType { get { return _base.Record.ComponentType; } set { _base.Record.ComponentType= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_SERIAL")]
		public string ComponentSerial { get { return _base.Record.ComponentSerial; } set { _base.Record.ComponentSerial = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_NAME")]
		public string ComponentName { get { return _base.Record.ComponentName; } set { _base.Record.ComponentName= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_IP")]
		public string ComponentIP { get { return _base.Record.ComponentIP; } set { _base.Record.ComponentIP = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_INTERVAL")]
		public long ComponentInterval { get { return _base.Record.ComponentInterval; } set { _base.Record.ComponentInterval= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_STATUS")]
		public long ComponentStatus { get { return _base.Record.ComponentStatus; } set { _base.Record.ComponentStatus= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_TYPE")]
		public long ErrorType { get { return _base.Record.ErrorType; } set { _base.Record.ErrorType= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_LEVEL")]
		public long ErrorLevel { get { return _base.Record.ErrorLevel; } set { _base.Record.ErrorLevel= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DESCRIPTION")]
		public string Description { get { return _base.Record.Description; } set { _base.Record.Description= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "LAST_UPDATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime LastUpdate { get { return _base.Record.LastUpdate; } set { _base.Record.LastUpdate= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_COUNT")]
		public long ErrorCount { get { return _base.Record.ErrorCount; } set { _base.Record.ErrorCount = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "WARNING_COUNT")]
		public long WarningCount { get { return _base.Record.WarningCount; } set { _base.Record.WarningCount = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "NOTIFY")]
		public bool Notify { get { return _base.Record.Notify; } set { _base.Record.Notify = value; } }

		//UNLINKED PROPERTIES
		public virtual EComponentStatus EStatus { get { return _base.EStatus; } set { _base.Record.Status = (long)value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

        public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_STATUS")]
        public virtual string ComponentStatusLabel { get { return _base.ComponentStatusLabel; } set { } }

        public virtual EComponentType EComponentType { get { return _base.EComponentType; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "COMPONENT_TYPE")]
        public virtual string ComponentTypeLabel { get { return _base.ComponentTypeLabel; } set { } }

        public virtual EErrorType EErrorType { get { return _base.EErrorType; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_TYPE")]
        public virtual string ErrorTypeLabel { get { return _base.ErrorTypeLabel; } set { } }

        public virtual EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "ERROR_LEVEL")]
        public virtual string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } set { } }

        public virtual MonitorLineListViewModel Lines { get; set; }
		
		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(Monitor source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(MonitorInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Monitor dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public MonitorViewModel() { }

		public static MonitorViewModel New() 
		{
			MonitorViewModel obj = new MonitorViewModel();
			obj.CopyFrom(MonitorInfo.New());
			return obj;
		}
		public static MonitorViewModel New(Monitor  source) { return New(source.GetInfo(false)); }
		public static MonitorViewModel New(MonitorInfo source)
		{
			MonitorViewModel obj = new MonitorViewModel();
			obj.CopyFrom(source);
			return obj;
		}

        public static MonitorViewModel Get(long oid, bool childs = false)
		{
			MonitorViewModel obj = new MonitorViewModel();
            MonitorInfo monitor = MonitorInfo.Get(oid, childs);

            if (monitor == null) return null;
            
            obj.CopyFrom(monitor);

            if (childs)
                obj.Lines = MonitorLineListViewModel.Get(monitor.LineaRegistros);

			return obj;
		}

        public static MonitorViewModel Get(string componentSerial)
        {
            MonitorViewModel obj = new MonitorViewModel();
            obj.CopyFrom(MonitorInfo.Get(componentSerial, false));
            return obj;
        }

		public static void Add(MonitorViewModel item)
		{
			Monitor newItem = Monitor.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(MonitorViewModel source, HttpRequestBase request = null)
		{
			Monitor item = Monitor.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
			Monitor.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class MonitorListViewModel : List<MonitorViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public MonitorListViewModel() { }

		public static MonitorListViewModel Get()
		{
			MonitorListViewModel list = new MonitorListViewModel();

			MonitorList sourceList = MonitorList.GetList();

			foreach (MonitorInfo item in sourceList)
				list.Add(MonitorViewModel.New(item));

			return list;
		}
		public static MonitorListViewModel Get(MonitorList sourceList)
		{
			MonitorListViewModel list = new MonitorListViewModel();

			foreach (MonitorInfo item in sourceList)
				list.Add(MonitorViewModel.New(item));

			return list;
		}

		#endregion
	}
}
