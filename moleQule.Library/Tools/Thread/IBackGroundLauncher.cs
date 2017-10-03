using System;
using System.ComponentModel;

namespace moleQule.Library
{
    /// <summary>
    /// Resultado de la operacion en segundo plano
    /// </summary>
    public enum BGResult
    {
        OK = 0,
        Error = 1,
        Working = 2,
        Cancelled = 3
    }

    public enum BGAction 
    { 
        Default = 0, 
        Update = 1, 
        Backup = 2 
    }
    
    /// <summary>
    /// Interfaz para cualquier formulario que requiera ejecutar código en segundo plano
    /// </summary>
    public interface IBackGroundLauncher
	{
        /// <summary>
        /// Flag que indica que ha finalizado el trabajo en segundo plano
        /// </summary>
        bool Finished { get; set;}

        /// <summary>
        /// Resultado del proceso
        /// </summary>
        BGResult Result { get; set;}

        /// <summary>
		/// Funcion que contiene el codigo a ejecutar en primer plano
		/// </summary>
		void ForeGroundJob();

		/// <summary>
		/// Funcion que contiene el codigo a ejecutar en segundo plano
		/// </summary>
		void BackGroundJob();

        /// <summary>
        /// Funcion que contiene el codigo a ejecutar en segundo plano
        /// </summary>
        void BackGroundJob(BackgroundWorker bk);

	}
}
