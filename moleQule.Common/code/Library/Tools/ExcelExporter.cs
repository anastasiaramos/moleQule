using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;

namespace moleQule.Library.Common
{
    public class ExcelExporter: Library.ExcelExporter, IDisposable
	{
		#region Attributes & Properties

		protected Registro _registro;

		public Registro Registro { get { return _registro; } }

		#endregion

		#region Factory Methods

		public ExcelExporter()
        {
			Init();
        }

		public virtual void Init()
		{
			InitExcelExporter();
		}

        public void Cerrar()
        {
            Close();

			if (_registro != null)
			{
				_registro.Save();
				_registro.CloseSession();
			}
        }

		public string GetConditions(Common.QueryConditions conditions)
		{
			string filtro = string.Empty;

			if (conditions.FechaIni != DateTime.MinValue) filtro += "Fecha Inicial: " + conditions.FechaIni.ToShortDateString() + "; ";
			if (conditions.FechaFin != DateTime.MinValue) filtro += "Fecha Final: " + conditions.FechaFin.ToShortDateString() + "; ";
			filtro += "Estado: " + Common.EnumText<EEstado>.GetLabel(conditions.Estado) + "; ";

			return filtro;
		}

        #endregion

        #region IDisposable

        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                }

                Cerrar();
            }
            disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~ExcelExporter()      
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion

        #region Business Methods

        #endregion        

    }
}
