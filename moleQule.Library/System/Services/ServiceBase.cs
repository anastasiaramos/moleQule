using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace moleQule.Library
{
	public partial class ServiceBase : System.ServiceProcess.ServiceBase
	{
		#region Attibutes & Properties

		public Guid TimerGuid { get; set; }

		protected ELogLevel LogLevel { get; set; }
		protected string SerialNumber { get; set; }
		protected int PollInterval { get; set; }
		protected bool Running { get; set; }
		protected Dictionary<string, ICronJob> CronJobs { get; set; }

		protected List<ISchemaInfo> Schemas = new List<ISchemaInfo>();

		#endregion

		#region Factory Methods

		public ServiceBase(string name, int pollInterval)
		{
			InitializeComponent();
#if DEBUG
			LogLevel = ELogLevel.ALL;
#else
			LogLevel = ELogLevel.WARN;
#endif
			ServiceName = name;
			PollInterval = pollInterval;

			MyLogger.LogStart(String.Format("{0} Service Started", ServiceName), "ServiceBase::ServiceBase");

			LoadCronJobs();

			Running = false;
		}

		protected virtual void Close() 
		{
			TimerMng.Instance.CloseTimer(TimerGuid);
		}

		protected virtual void Initialize() 
		{
			MyLogger.LogText(String.Format("{0} SCHEMAS FOUND", (Schemas == null) ? 0 : Schemas.Count), "ServiceBase::OnStart");

			StartTimer();
		}

		protected virtual void LoadCronJobs()
		{
			try
			{
				foreach (KeyValuePair<string, ICronJob> item in CronJobs)
				{
					if (item.Value.Status == EComponentStatus.DISABLED)
						MyLogger.LogText(string.Format("{0} is DISABLED", item.Key), "ServiceBase::LoadCronjobs");
					else
						MyLogger.LogText(string.Format("{0} is ENABLED EVERY {1}s", item.Key, item.Value.Interval), "ServiceBase::LoadCronjobs");
				}
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::LoadCronJobs");
			}
		}

		protected virtual bool LoadSchema(ISchemaInfo schema)
		{
			try
			{
				if (AppContext.Principal == null) Login();

				AppContext.Principal.ChangeUserSchema(schema, true);

				return (AppContext.Principal != null);
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::LoadSchema");
				if (AppContext.Principal != null) AppContext.Principal.Close();

				return false;
			}
		}

		protected virtual void Login()
		{
			try
			{
				if (AppContext.Principal != null) AppContext.Principal.Logout();
				PrincipalBase.Login();

				MyLogger.LogText(	string.Format("CONNECTED TO {0}:{1} AS USER {2}",
									SettingsMng.Instance.GetActiveServer(),				
									SettingsMng.Instance.GetDBName(),
									SettingsMng.Instance.GetDBUser()),
									"Service::Login");
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::Login");
				if (AppContext.Principal != null) AppContext.Principal.Close();
			}
		}

		#endregion

		#region Business Methods

		protected virtual void Execute()
		{
			try
			{
				//To avoid overlapped executions
				if (Running)
				{
					MyLogger.LogText("LAST EXECUTION IS STILL BUSY. NOT EXECUTED TO AVOID OVERLAPPING", "ServiceBase::Execute");
					return;
				}

				Running = true;

				foreach (ISchemaInfo item in Schemas)
				{
					MyLogger.LogText(String.Format("PROCESSING SCHEMA '{0}'...", item.Name), "ServiceBase::Execute");

					if (!LoadSchema(item))
					{
						MyLogger.LogText(String.Format("ERROR LOADING SCHEMA {0}", item.Name), "ServiceBase::Execute");
						Running = false;
						return;
					}

					foreach (KeyValuePair<string, ICronJob> cronjob in CronJobs)
					{
						Thread cronThread = ExecuteTask(cronjob.Value, item);
						while (cronThread != null && cronThread.IsAlive) { };
					}
				}

				Running = false;
			}
			catch (Exception ex)
			{
				Running = false;
				MyLogger.LogException(ex, "ServiceBase::Execute");

				Login();
			}
		}

		protected Thread ExecuteTask(ICronJob cronjob, object parameter)
		{
			if (cronjob == null) return null;

			try
			{
				switch (cronjob.CheckStatus((ISchemaInfo)parameter))
				{
					case EComponentStatus.DISABLED:
						{
							if (LogLevel == ELogLevel.ALL)
								MyLogger.LogText(string.Format("{0} DISABLED", cronjob.ID), "ServiceBase::ExecuteTask");
							return null;
						}

					case EComponentStatus.WORKING:
						{
							if (LogLevel == ELogLevel.ALL || LogLevel == ELogLevel.WARN)
								MyLogger.LogText(string.Format("{0} STILL BUSY", cronjob.ID), "ServiceBase::ExecuteTask");
							return null;
						}

					case EComponentStatus.UNAVAILABLE:
						{
							if (LogLevel == ELogLevel.ALL) 
								MyLogger.LogText(string.Format("{0} OUT OF DATE ", cronjob.ID), "ServiceBase::ExecuteTask");
							return null;
						}

					case EComponentStatus.READY:
						{
							if (LogLevel == ELogLevel.ALL || LogLevel == ELogLevel.WARN)
								MyLogger.LogText(string.Format("{0} STARTED ", cronjob.ID), "ServiceBase::ExecuteTask");
#if DEBUG
							cronjob.Run(parameter);
							return null;
#else
                            Thread taskthread = new Thread(new ParameterizedThreadStart(cronjob.Run));
							taskthread.Start(parameter);
							return taskthread;
#endif							
						}
				}

				return null;
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::ExecuteTask");
				return null;
			}
		}

		protected void StartTimer()
		{
			try
			{
				TimerGuid = TimerMng.Instance.InitTimer(ETimerType.System, ServiceTimerController, PollInterval);

				MyLogger.LogText(string.Format("Service Timer Started. TICK EVERY {0}s", PollInterval), "Service::StartTimer");
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::StartTimer");
			}
		}

		#endregion

		#region Events

		protected override void OnStart(string[] args)
		{
			try
			{
				Initialize();
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::OnStart");
				Dispose();
			}
		}

		protected override void OnStop()
		{
			try
			{
				Close();
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ServiceBase::OnStop");
			}
			finally
			{
				MyLogger.LogText("Service Stopped", "ServiceBase::OnStop");
			}
		}

		public virtual void ServiceTimerController(TTimer timer)
		{
			Execute();
		}

		#endregion

#if DEBUG
        #region Debug

        public virtual void DebugExecute()
		{
			Initialize();
						 
            //if (Schemas.Count > 0) LoadSchema(Schemas[0]);

			Execute();
		}

		#endregion
#endif
	}
}