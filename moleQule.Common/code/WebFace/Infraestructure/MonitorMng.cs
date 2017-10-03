using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;
using System.Threading;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.WebFace.Common.Models;
using moleQule.WebFace.Properties;

namespace moleQule.WebFace.Common
{
	public class MonitorMng
	{
		#region Properties

		int _componentInterval = 300;
		HttpStatusCode _responseCode = HttpStatusCode.OK;

		public Guid TimerGuid { get; set; }
		public string ComponentApiKey { get; set; }
		public string ComponentSecretKey { get; set; }
		public string ComponentName { get; set; }
		public EComponentType ComponentType { get; set; }
		public string ComponentSerial { get; set; }
		public EComponentStatus ComponentStatus { get; set; }
		public int ComponentInterval { get { return _componentInterval * 1000; } set { _componentInterval = value; } }

		public DateTime LastUpdate { get; set; }

		public delegate void DlgMonitorController();
		public DlgMonitorController MonitorController { get; set; }

		public HttpStatusCode ResposeStatusCode { get { return _responseCode; } }

		protected string MonitorControllerURL { get { return Properties.Settings.Default.MONITOR_CONTROLLER_PATH; } }
		protected string MonitorURL { get { return SettingsMng.Instance.GetApiBaseUrl() + MonitorControllerURL; } }

		#endregion

		#region Factory Methods

		private static MonitorMng _singleton = null;

		public static MonitorMng Instance { get { return _singleton != null ? _singleton : new MonitorMng(); } }

		public MonitorMng()
		{
			_singleton = this;

			ComponentStatus = EComponentStatus.OK;

			LastUpdate = DateTime.Now;
		}

		public void Close()
		{
			StopPoll();
		}

		public void StartPoll(int pollInterval) { StartPoll(pollInterval, false); }
		public void StartPoll(int pollInterval, bool waitResponse)
		{
			MyLogger.LogText("Monitor Timer Started: Tick every " + pollInterval + "s", "MonitorMng::StartPoll");
			MyLogger.LogText("Monitor Host: " + SettingsMng.Instance.GetApiBaseUrl(), "MonitorMng::StartPoll");
			TimerGuid = TimerMng.Instance.InitTimer(ETimerType.System, MonitorStatusTimerController, pollInterval);
			
			//Initial Poll
			UpdateMonitorStatus(waitResponse);
		}

		public void StopPoll()
		{
			MyLogger.LogText("Monitor Timer Stopped", "MonitorMng::StopPoll");
			TimerMng.Instance.CloseTimer(TimerGuid);
		}

		public void RestartPoll()
		{
			MyLogger.LogText("Monitor Timer Restarted");
			TimerMng.Instance.ResetTimer(TimerGuid);
		}

		#endregion

		#region POST

		public void PutMonitor()
		{
			HttpClient client = new HttpClient();

			client.BaseAddress = new Uri(SettingsMng.Instance.GetApiBaseUrl());

			// Add an Accept header for JSON format.
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			FillToken(client);

			// Create a new monitor
			MonitorViewModel monitor = MonitorViewModel.New();

			monitor.ComponentSerial = ComponentSerial;
			monitor.ComponentType = (long)ComponentType;
			monitor.ComponentName = ComponentName;
			monitor.ComponentStatus = (long)EComponentStatus.OK;
			monitor.ComponentInterval = ComponentInterval;

			Uri gizmoUri = null;

			// Update the monitor
            HttpResponseMessage response = null;
			string responsephrase = string.Empty;
            int count = 0;
            do
            {
				MyLogger.LogText("CALL TO " + MonitorURL + monitor.ComponentSerial + " (attempt " + (count + 1) + ")", "MonitorMng::PutMonitor");

				response = client.PutAsJsonAsync(MonitorControllerURL + monitor.ComponentSerial, monitor).Result;

				if (response != null)
				{
					_responseCode = response.StatusCode;
					responsephrase = response.ReasonPhrase;
				}
				else
				{
					_responseCode = HttpStatusCode.Unused;
					responsephrase = "RESPONSE IS NULL";
				}

				count++;

				MyLogger.LogText(String.Format("{0} ({1})", _responseCode, responsephrase), "MonitorMng::PutMonitor");

				//Si el servidor no responde esperamos 10s para el siguiente intento
				if ((int)_responseCode == 404) Thread.Sleep(10000); 
            }
            while (!response.IsSuccessStatusCode && count < 5);				

			if (response.IsSuccessStatusCode)
			{
				gizmoUri = response.Headers.Location;
				ComponentStatus = EComponentStatus.OK;
				LastUpdate = DateTime.Now;
			}
			else
				ComponentStatus = EComponentStatus.ERROR;

			if (MonitorController != null) MonitorController();
		}

		#endregion

		#region Business Methods

		private void FillToken(HttpClient client)
		{
			client.DefaultRequestHeaders.Add("apiKey", ComponentApiKey);
			client.DefaultRequestHeaders.Add("salt", TokenMng.GetSalt());
			client.DefaultRequestHeaders.Add("token",
												TokenMng.GetToken(
														client.DefaultRequestHeaders.GetValues("apiKey").First(),
														ComponentSecretKey,
														client.DefaultRequestHeaders.GetValues("salt").First()
												)
											);
		}

		public EComponentStatus UpdateMonitorStatus() { return UpdateMonitorStatus(false); }
		public EComponentStatus UpdateMonitorStatus(bool waitResponse)
		{
			Thread monitorThread = new Thread(PutMonitor);
			monitorThread.Start();

			if (waitResponse)
			{
				while (monitorThread.IsAlive);
				return ComponentStatus;
			}

			return EComponentStatus.UNAVAILABLE;			
		}

		public void MonitorStatusTimerController(TTimer timer)
		{
			UpdateMonitorStatus();
		}

		#endregion
	}
}
