using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Threading;

namespace moleQule.Library
{
	public class TimerMng
	{
		#region Enum

		public delegate void TickController(TTimer tTimer);

		#endregion
		
		#region Properties

		private bool IsLogEnabled { get; set; }

		Dictionary<Guid, TTimer> TickTimers { get; set; }

		public Stopwatch StopWatch { get; set; }

		#endregion

		#region Factory Methods

		private static TimerMng _singleton = null;

		public static TimerMng Instance { get { return _singleton != null ? _singleton : new TimerMng(); } }

		public TimerMng()
		{
			_singleton = this;

			string setting = System.Configuration.ConfigurationManager.AppSettings["LogEnabled"];

			bool _isLogEnabled = false;
			bool.TryParse(setting, out _isLogEnabled); 

			IsLogEnabled = _isLogEnabled;

			TickTimers = new Dictionary<Guid, TTimer>();
			StopWatch = new Stopwatch();
		}

		public void Close()
		{
			/*foreach (KeyValuePair<Guid,TTimer> item in TickTimers)
				CloseTimer((Guid)item.Key);

			TickTimers.Clear();*/
		}
		
		public Guid InitTimer(ETimerType timerType, TickController controller, int interval)
		{
			return InitTimer(timerType, controller, interval, Guid.NewGuid());
		}
		public Guid InitTimer(ETimerType timerType, TickController controller, int interval, Guid guid)
		{
			switch (timerType)
			{
				case ETimerType.System:
					{
						System.Timers.Timer wTimer = new System.Timers.Timer();
						wTimer.Interval = interval * 1000;
						wTimer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);

						TickTimers.Add(guid, new TTimer { Guid = guid, Type = timerType, Timer = wTimer, Controller = controller });

						wTimer.Start();
					}
					break;

				case ETimerType.Form:
					{
						System.Windows.Forms.Timer wTimer = new System.Windows.Forms.Timer();
						wTimer.Interval = interval * 1000;
						wTimer.Tick += new EventHandler(Timer_Tick);

						TickTimers.Add(guid, new TTimer { Guid = guid, Type = timerType, Timer = wTimer, Controller = controller });

						wTimer.Start();
					}
					break;
			}

			return guid;
		}

		public TTimer CloseTimer(Guid id)
		{
			TTimer tTimer = new TTimer();

			if (!TickTimers.ContainsKey(id)) return tTimer;

			tTimer = (TTimer)TickTimers[id];
			tTimer.CloseTimer();
			
			TickTimers.Remove(id);

			return tTimer;
		}

		public void ResetTimer(Guid id)
		{
			TTimer tTimer = CloseTimer(id);
			InitTimer(tTimer.Type, tTimer.Controller, (tTimer.Interval / 1000), id);			
		}

		#endregion

		#region Events

		//System.Timers.Timer EventHandler
		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				TTimer timer = TickTimers.Values.Single(item =>
										(((TTimer)item).Type == ETimerType.System) && 
										((System.Timers.Timer)((TTimer)item).Timer).Equals((System.Timers.Timer)sender));

				timer.Controller(timer);
			}
			catch (Exception ex)
			{
				if (IsLogEnabled) MyLogger.LogText(ex.Message);
			}
		}

		//System.Windows.Forms.Timer EventHandler
		private void Timer_Tick(object sender, EventArgs e)
		{
			try
			{
				TTimer timer = TickTimers.Values.Single(item => 
										(((TTimer)item).Type == ETimerType.Form) &&				
										((System.Windows.Forms.Timer)((TTimer)item).Timer).Equals((System.Windows.Forms.Timer)sender)
					);

				timer.Controller(timer);
			}
			catch (Exception ex)
			{
				if (IsLogEnabled) MyLogger.LogText(ex.Message);
			}
		}

		#endregion
	}

	public enum ETimerType { System, Form }

	public class TTimer
	{
		public Guid Guid;
		public ETimerType Type;
		public object Timer;
		public TimerMng.TickController Controller;
		public int Interval 
		{
			get 
			{
				switch (Type)
				{
					case ETimerType.System: return (int)((System.Timers.Timer)Timer).Interval;
					case ETimerType.Form: return ((System.Windows.Forms.Timer)Timer).Interval;
					default: return 0;
				}
			}

			set
			{
				switch (Type)
				{
					case ETimerType.System: ((System.Timers.Timer)Timer).Interval = (double)value; break;
					case ETimerType.Form: ((System.Windows.Forms.Timer)Timer).Interval = value; break;
				}
			}
		}

		public void CloseTimer()
		{
			switch (Type)
			{
				case ETimerType.System:
					((System.Timers.Timer)Timer).Stop();
					((System.Timers.Timer)Timer).Dispose();

					break;

				case ETimerType.Form:
					((System.Windows.Forms.Timer)Timer).Stop();
					((System.Windows.Forms.Timer)Timer).Dispose();

					break;
			}
		}
	}
}
