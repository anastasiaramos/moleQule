using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace moleQule.Library
{
    public class Timer
    {
		#region Enum

		public delegate void TickController();

		public enum ETickTimer { Poll, Download, Standard }
	
		public struct TTickTimer
		{
			public System.Windows.Forms.Timer Timer;
			public TickController Controller;
		}

		struct TCrono
		{
			public TimeSpan crono;
			public TimeSpan from_start;
			public string label;
		}

		#endregion

        TimeSpan _start;
        TimeSpan _end;
        TimeSpan _initial;
        
		List<TCrono> Intervals { get; set; }
		Dictionary<ETickTimer, TTickTimer> TickTimers { get; set; }

		/// <summary>
		/// Única instancia de la clase (Singleton Pattern)
		/// </summary>
		internal static Timer _singleton = null;

		/// <summary>
		/// Devuelve la instancia única
		/// </summary>
		public static Timer Instance { get { return _singleton != null ? _singleton : new Timer(); } }

        public Timer()
        {
			// Singleton
			_singleton = this;

			Intervals = new List<TCrono>();

			TickTimers = new Dictionary<ETickTimer, TTickTimer>();

            Start();
        }

		#region RecordTimer

		public void Start()
        {
            _initial = DateTime.Now.TimeOfDay;
            _start = _initial;
        }

		public void Stop()
		{
			Intervals.Clear();
		}

        public void Reset()
        {
			Intervals.Clear();
            Start();
        }

        public void Record(string label)
        {
            _end = DateTime.Now.TimeOfDay;
            TCrono crono;
			crono.label = label;
			crono.crono = _end - _start;
            crono.from_start = _end - _initial;
			Intervals.Add(crono);
            _start = DateTime.Now.TimeOfDay;
        }

        public string GetCronos()
        {
            string intervalos = string.Empty;
            int index = 1;

			foreach (TCrono t in Intervals)
            {
                intervalos += "Step " + index.ToString("00") + ": " + t.label + "; Parcial = " + t.crono.Minutes.ToString() + ":" + t.crono.Seconds.ToString("00") + ":" + t.crono.Milliseconds.ToString("000") + "; Total = " + t.from_start.Minutes.ToString() + ":" + t.from_start.Seconds.ToString("00") + ":" + t.from_start.Milliseconds.ToString("000") + System.Environment.NewLine;
                index++;
            }

			if (Intervals.Count == 0) intervalos = "Sin intervalos";

            return intervalos;
        }

		public int GetIntervals()
		{
			return (Intervals != null) ? Intervals.Count : 0;
		}

        /// <summary>
        /// Lanza una excepcion con la información recogida en el Timer
        /// </summary>
        public void ShowCronos() { throw new iQDebugException(this.GetCronos()); }

		#endregion

		#region TickTimer

		public void InitTickTimer(ETickTimer eTimer, TickController tickController, double interval)
		{
			System.Windows.Forms.Timer wTimer = new System.Windows.Forms.Timer();
			wTimer.Tick += new System.EventHandler(Timer_Tick);
			wTimer.Interval = (int)(interval * 1000);

			TickTimers.Add(eTimer, new TTickTimer { Timer = wTimer, Controller = tickController });

			wTimer.Enabled = true;
			wTimer.Start();
		}

		public TTickTimer CloseTickTimer(ETickTimer timer)
		{
			TTickTimer tTimer = (TTickTimer)TickTimers[timer];
			tTimer.Timer.Stop();
			tTimer.Timer.Dispose();
			TickTimers.Remove(timer);

			return tTimer;
		}

		public void ResetTimer(ETickTimer timer)
		{
			TTickTimer tTimer = CloseTickTimer(timer);
			InitTickTimer(timer, tTimer.Controller, tTimer.Timer.Interval);
		}

		#endregion

		#region Events

		private void Timer_Tick(object sender, EventArgs e)
		{
			KeyValuePair<ETickTimer, TTickTimer> entry = TickTimers.First(item => ((TTickTimer)item.Value).Timer == (System.Windows.Forms.Timer)sender);
			((TTickTimer)(entry.Value)).Controller();
		}

		#endregion
	}
}
