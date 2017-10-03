using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

using moleQule.Library;

namespace moleQule.WebFace.Common
{
	public partial class ServiceBaseEx : moleQule.Library.ServiceBase
	{
		#region Attibutes & Properties

		protected bool Monitor { get; set; }
		
		protected string ApiKey { get; set; }
		protected string SecretKey { get; set; }
		protected string ComponentName { get; set; }
		protected int MonitorPollInterval { get; set; }

		#endregion

		#region Factory Methods

		public ServiceBaseEx(string name, int pollInterval)
			: base(name, pollInterval)
		{
			InitializeComponent();
			Monitor = true;
		}

		protected override void Close()
		{
			base.Close();
			if (Monitor) MonitorMng.Instance.Close();
		}

		protected override void Initialize()
		{
			base.Initialize();
			StartStatusMonitor();
		}

		protected void SetMonitorParams(string serialKey,
										string apiKey, 
										string apiSecretKey,
										string componentName,
										int pollInterval)
		{
			SerialNumber = serialKey;
			ApiKey = apiKey;
			SecretKey = apiSecretKey;
			ComponentName = componentName;
			MonitorPollInterval = pollInterval;
			Monitor = (pollInterval > 0);
		}
		
		#endregion

		#region Business Methods

		protected void StartStatusMonitor()
		{
			try
			{
				if (Monitor)
				{
					MyLogger.LogText("MONITOR ENABLED", "ServiceBase::StartStatusMonitor");

					MonitorMng.Instance.ComponentApiKey = ApiKey;
					MonitorMng.Instance.ComponentSecretKey = SecretKey;
					MonitorMng.Instance.ComponentSerial = SerialNumber;
					MonitorMng.Instance.ComponentName = ComponentName;
					MonitorMng.Instance.ComponentType = EComponentType.WinService;
					MonitorMng.Instance.ComponentStatus = EComponentStatus.OK;
					MonitorMng.Instance.ComponentInterval = MonitorPollInterval;

					MonitorMng.Instance.StartPoll(MonitorPollInterval);
				}
				else
					MyLogger.LogText("MONITOR DISABLED", "ServiceBase::StartStatusMonitor");
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::StartStatusMonitor");
			}
		}

		#endregion
	}
}