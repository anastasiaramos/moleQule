using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO.Ports;
using System.Threading;

using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class SerialPortMng
	{
		#region Attributes & Properties

		SerialPort SerialPort { get; set; }
		Thread PortReaderThread { get; set; }
		PortMngStatus Status { get; set; }

		PortByteHandlerDelegate ByteHandlerDelegate { get; set; }
		PortLineHandlerDelegate LineHandlerDelegate { get; set; }

		#endregion

		#region Factory Methods

		/// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
        /// </summary>
        protected static SerialPortMng _main;

        /// <summary>
        /// Unique Controler Class Instance
        /// </summary>
        public static SerialPortMng Instance { get { return (_main != null) ? _main : new SerialPortMng(); } }
        
        /// <summary>
        /// Constructor 
        /// </summary>
		protected SerialPortMng()
        {
            // Singleton
            _main = this;

			PortReaderThread = null;
			Status = PortMngStatus.Stop;
        }

		public void Close() 
		{
			StopRead();

			_main = null;
			SerialPort = null;
		}

		protected void SerialPortConfig(ESerialDevice device)
		{
			switch (device)
			{
				case ESerialDevice.K3Scales:

					if (SerialPort == null) SerialPort = new SerialPort("COM3");

					SerialPort.DataBits = 8;
					SerialPort.BaudRate = 9600;
					SerialPort.Parity = Parity.None;
					SerialPort.StopBits = StopBits.One;
					SerialPort.NewLine = ((char)3).ToString();
					SerialPort.ReadTimeout = 10000;

					break;
			}

			MyLogger.LogText("Port " + SerialPort.PortName + " CONFIGURED", "SerialPortMng::SerialPortConfig");
			MyLogger.LogText("	  DataBits: " + SerialPort.DataBits);
			MyLogger.LogText("	  BaudRate: " + SerialPort.BaudRate);
			MyLogger.LogText("	  Parity: " + SerialPort.Parity);
			MyLogger.LogText("	  StopBits: " + SerialPort.StopBits);
			MyLogger.LogText("	  NewLine: " + SerialPort.NewLine);
		}

		#endregion

		#region Business Methods

		public delegate void PortReaderDelegate();
		public delegate void PortByteHandlerDelegate(byte[] line);
		public delegate void PortLineHandlerDelegate(string line);

		protected void PortByteReader()
		{
			if (SerialPort == null) return;

			Status = PortMngStatus.Reading;

			SerialPort.Open();
			MyLogger.LogText("Port " + SerialPort.PortName + " OPEN", "SerialPortMng::PortByteReader");

			byte[] line = new byte[14];

			//Bucle de sincronizacion con el puerto
			while (true)
			{
				SerialPort.Read(line, 0, 1);
				//MyLogger.LogText("Leido caracter de sincronizacion '" + (char)line[0] + "'");

				if ((char)line[0] == (char)3)
				{
					line = new byte[14];

					string value = string.Empty;

					while (SerialPort.BytesToRead < line.Length) { }

					SerialPort.Read(line, 0, line.Length);

					for (int i = 0; i < line.Length; i++)
						value += line[i].ToString() + " | ";
						
					//MyLogger.LogText("Leida linea de sincronizacion '" + value + "'");

					if ((char)line[0] == 2 && line[13] == 3)
					{
						ByteHandlerDelegate(line);
						break;
					}
				}
			}

			while (Status == PortMngStatus.Reading)
			{
				if (SerialPort.BytesToRead < line.Length) continue;

				SerialPort.Read(line, 0, line.Length);

				string value = string.Empty;

				for (int i = 0; i < line.Length; i++)
					value += (char)line[i] + " | ";

				//MyLogger.LogText("Leida linea '" + value + "'");

				if (line[0] == 2 && line[13] == 3)
				{
					ByteHandlerDelegate(line);
				}
			}
		}

		protected void PortLineReader()
		{
			try
			{
				if (SerialPort == null) return;

				Status = PortMngStatus.Reading;

				MyLogger.LogText("Opening port " + SerialPort.PortName + " ...", "SerialPortMng::PortLineReader");
				SerialPort.Open();
				MyLogger.LogText("Port " + SerialPort.PortName + " OPEN", "SerialPortMng::PortLineReader");

				if (Status == PortMngStatus.Reading)
					MyLogger.LogText("Listening Port " + SerialPort.PortName, "SerialPortMng::PortLineReader");

				string value = string.Empty;

				//Port Reading Loop
				while (Status == PortMngStatus.Reading)
				{
					try
					{
						value = SerialPort.ReadLine();
					}
					catch (TimeoutException)
					{
						value = string.Empty;
						MyLogger.LogText("Port read Timeout", "SerialPortMng::PortLineReader");
					}

					if (!string.IsNullOrEmpty(value)) LineHandlerDelegate(value);
				}

				MyLogger.LogText("Stop listening Port " + SerialPort.PortName, "SerialPortMng::PortLineReader");
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "SerialPortMng::PortLineReader");
			}
		}

		public void StartRead(ESerialDevice device, PortLineHandlerDelegate handler)
		{
			if (Status == PortMngStatus.Reading)
			{
				MyLogger.LogText("Serial Port Manager is reading. Please stop it first", "SerialPortMng::StartRead");
				throw new iQException("Serial Port Manager is reading. Please stop it first", "SerialPortMng::StartRead");
			}

			SerialPortConfig(device);

			LineHandlerDelegate = handler;

			Status = PortMngStatus.Reading;

            PortReaderThread = new Thread(PortLineReader);
            PortReaderThread.Start();
		}

		public void StartRead(ESerialDevice device, PortByteHandlerDelegate handler)
		{
			if (Status == PortMngStatus.Reading)
			{
				MyLogger.LogText("Serial Port Manager is reading. Please stop it first", "SerialPortMng::StartRead");
				throw new iQException("Serial Port Manager is reading. Please stop it first", "SerialPortMng::StartRead");
			}

			SerialPortConfig(device);

			ByteHandlerDelegate = handler;

			Status = PortMngStatus.Reading;

			PortReaderThread = new Thread(PortByteReader);
			PortReaderThread.Start();
		}

		public void StopRead()
		{
			try
			{
				Status = PortMngStatus.Stop;

				if (PortReaderThread == null) return;
								
				while (PortReaderThread.IsAlive) continue;

				if (SerialPort != null) SerialPort.Close();

				Status = PortMngStatus.Stop;
			}
			catch (Exception ex)
			{
				MyLogger.LogException(ex, "SerialPortMng::StopRead");
			}
			finally
			{
				MyLogger.LogText("Port " + SerialPort.PortName + " CLOSED", "SerialPortMng::StopRead");

				PortReaderThread = null;				
				SerialPort = null;
			}
		}

		#endregion

		#region Events

		#endregion
	}

	#region Enums

	public enum ESerialDevice { K3Scales = 1 }

	public enum PortMngStatus { Reading = 1, Stop = 2, Waiting = 3 }
 
	#endregion
}
