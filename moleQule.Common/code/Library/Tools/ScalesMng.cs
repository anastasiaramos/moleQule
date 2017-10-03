using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Timers;

using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class ScalesMng
	{
		#region Attributes & Properties

		public struct TLine
		{
			public byte[] Value;
			public string Line;
			public DateTime Date;
		}

		EStatus Status = EStatus.OK;
		ESerialDevice SerialDevice { get; set; }

		TLine LastLine;

		#endregion

		#region Factory Methods

		/// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
        /// </summary>
        protected static ScalesMng _main;

        /// <summary>
        /// Unique Controler Class Instance
        /// </summary>
        public static ScalesMng Instance { get { return (_main != null) ? _main : new ScalesMng(); } }
        
        /// <summary>
        /// Constructor 
        /// </summary>
		protected ScalesMng()
        {
            // Singleton
            _main = this;

			SerialDevice = ESerialDevice.K3Scales;

			LastLine = new TLine { Line = string.Empty };
        }

		#endregion

		#region Business Methods

		public void CopyFromLine(Pesaje item, TLine line)
		{
            try
            {
                switch (SerialDevice)
                {
                    case ESerialDevice.K3Scales:
                        {
                            try { item.Neto = Convert.ToDecimal(line.Line.Substring(2, 8)); }
                            catch { item.Neto = 0; }

                            item.Fecha = line.Date;
                            item.Tara = 0;
                            item.Bruto = item.Tara + item.Neto;
                            item.Observaciones = EnumText<ESerialDevice>.GetLabel(ESerialDevice.K3Scales) + " | " + line.Line;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "CALLER", "ScalesMng::CopyFromLine");
            }
		}

		private bool IsDistinctLine(string line)
		{
            try
            {
                return (LastLine.Line != line);
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::IsDistinctLine:: line = " + line);
            }
            return true;
		}
		private bool IsStableLine(string line)
		{
            try
            {
                return (line.Substring(1, 1) == "A"); //((char)0x04).ToString());
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::IsStableLine:: line = " + line);
            }
            return true;
		}
        private bool IsNegativeLine(string line)
        {
            try
            {
                return (line.Substring(2, 1) == "-"); //((char)0x04).ToString());
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::IsNegativeLine:: line = " + line);
                return true;
            }
        }
        private bool IsValidWeigth(string line)
        {
            try
            {
                if (IsNegativeLine(line))
                    return false;

                decimal value = Convert.ToDecimal(line.Substring(2, 8));
                return value > 5;
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::IsValidWeigth:: line = " + line);
                return true;
            }
        }

		private bool IsZeroLine(string line)
		{
            try
            {
                return line.Substring(1, 1).Equals(""); //((char)0x08).ToString());
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::IsZeroLine:: line = " + line);
            }
            return true;
		}

		public bool ZeroLine(string line)
		{
			try
			{
                if (IsNegativeLine(line))
                    return false;

				decimal value = Convert.ToDecimal(line.Substring(2, 8));
				return value == 0;
            }
            catch (Exception ex)
            {
                MyLogger.LogText(ex.Message + ex.StackTrace, "ScalesMng::ZeroLine:: line = " + line);
            }
            return true;
		}

		public void ScaleListeningStart()
		{
            SerialPortMng.Instance.StartRead(SerialDevice, new SerialPortMng.PortLineHandlerDelegate(SaveScaleLine));
        }

		public void ScaleListeningStart(ESerialDevice device)
		{
			SerialDevice = device;
			ScaleListeningStart();
		}

		public void ScaleListeningStop()
		{
			try
			{
				SaveScaleLine();

				SerialPortMng.Instance.Close();

				_main = null;
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ScalesMng::ScaleListeningStop");
			}
		}		

		public void SaveScaleByteLine(byte[] line)
		{
			/*if (!ZeroLine(line))
			{
				ScalesLines.Add(new TLine { Value = line, Date = DateTime.Now });
				return;
			}

			if (ScalesLines.Count > 1)
			{
				SaveScaleLine(ScalesLines[ScalesLines.Count - 1]);
				ScalesLines.Clear();
			}*/
		}

		public void SaveScaleLine(string line)
		{
			try
			{
				if (IsNegativeLine(line))
					return;

				if (ZeroLine(line))
				{
					if (LastLine.Line != string.Empty)
					{
						Status = EStatus.Working;
						SaveScaleLine();
					}

					LastLine.Line = string.Empty;
					return;
				}

				if (!IsStableLine(line)) return;
				if (!IsValidWeigth(line)) return;
				if (!IsDistinctLine(line)) return;

				LastLine.Line = line;
				LastLine.Date = DateTime.Now;
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ScalesMng::SaveScaleLine");
			}
		}

		public void SaveScaleLine()
		{
			try
			{
				if (Status != EStatus.Working) return;
				if (LastLine.Line == string.Empty) return;

				Pesajes pesajes = Pesajes.NewList();

				Pesaje item = pesajes.NewItem();
				CopyFromLine(item, LastLine);

				pesajes.OpenNewSession();
				pesajes.BeginTransaction();
				pesajes.Save();
				pesajes.CloseSession();

				MyLogger.LogText("Line saved: " + LastLine.Line, "ScalesMng::SaveScaleLine");

				Status = EStatus.Closed;
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "ScalesMng::SaveScaleLine");
				Status = EStatus.Error;
			}
		}

		#endregion
	}
}
