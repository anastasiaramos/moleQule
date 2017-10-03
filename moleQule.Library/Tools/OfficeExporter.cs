using System;
using System.Collections.Generic;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Windows.Forms;

namespace moleQule.Library
{
    public class OfficeExporter
    {
        #region Attributes & Properties

        protected const int MAX_VERSION = 14;

        #endregion

        #region Factory Methods
       
        #endregion
    }

	public class ExcelExporter : OfficeExporter
	{
		#region Attributes & Properties

		static Excel.Application _o_excel = null;

		#endregion

		#region Factory Methods

		public static Excel.Application InitExcelExporter()
		{
			if (_o_excel != null) return _o_excel;

			int version = MAX_VERSION;

			// comprobar si se encuentra instalada la versión de Word
			RegistryKey runK = null;
			string entryName = string.Empty;

			while ((runK == null) && (version >= 9))
			{
				//Windows Vista/XP/7
				entryName = String.Format(Resources.Paths.APP_REG32 + Resources.Paths.OFFICE_REG32_W7,
											version.ToString() + ".0",
											"Excel");
				runK = Registry.LocalMachine.OpenSubKey(entryName, false);

				//Windows Server 2008
				if (runK == null)
				{
					entryName = String.Format(Resources.Paths.APP_REG32 + Resources.Paths.OFFICE_REG32_WS2008,
												version.ToString() + ".0",
												"Excel");
					runK = Registry.LocalMachine.OpenSubKey(entryName, false);
				}

				version--;
			}

			if ((runK != null) && (runK.GetValue("Path", "").ToString() != ""))
			{
				_o_excel = new Excel.Application();
				_o_excel.Visible = true;

				// Verifica si el Excel Application Object se construyó satisfactoriamente
				if (_o_excel == null)
				{
					throw new Exception(String.Format(Resources.Errors.OFFICE_INIT, "Microsoft Excel"));
				}
			}
			else
			{
				throw new Exception(String.Format(Resources.Errors.OFFICE_NOT_INSTALLED, "Microsoft Excel"));
			}

			// Limpia cualquier otra referencia a Excel
			GC.Collect();

			return _o_excel;
		}

		public static Excel.Workbook NewWorkbook()
		{
			return (_o_excel != null) ? (Excel.Workbook)(_o_excel.Workbooks.Add(Missing.Value)) : null;
		}

		public static void Close()
		{
			if (_o_excel != null)
			{
				_o_excel.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(_o_excel);
				_o_excel = null;
			}

			GC.Collect();
		}

		#endregion
	}
	
	public class WordExporter : OfficeExporter
	{
		#region Attributes & Properties

		static Word.Application _o_word = null;

		#endregion

		#region Factory Methods

		public static Word.Application InitWordExporter()
		{
			if (_o_word != null) return _o_word;

			int version = 12;

			// comprobar si se encuentra instalada la versión de Word
			RegistryKey runK = Registry.LocalMachine.OpenSubKey(@"Software\\Microsoft\\Office\\" + version.ToString() + ".0\\Word\\InstallRoot", false);

			while ((runK == null) && (version >= 9))
			{
				version--;
				// comprobar si se encuentra instalada la versión de Word
				runK = Registry.LocalMachine.OpenSubKey(@"Software\\Microsoft\\Office\\" + version.ToString() + ".0\\Word\\InstallRoot", false);
			}

			if ((runK != null) && (runK.GetValue("Path", "").ToString() != ""))
			{
				_o_word = new Word.Application();
				_o_word.Visible = true;

				// Verifica si el Word Application Object se construyó satisfactoriamente
				if (_o_word == null)
				{
					throw new Exception("Error al inicializar Word");
				}
			}
			else
			{
				throw new Exception("No se ha encontrado una versión válida de Microsoft Word instalado en el sistema");
			}

			// Limpia cualquier otra referencia a Excel
			GC.Collect();

            //_o_word = new Word.Application();
            //_o_word.Visible = true;

            //// Verifica si el Word Application Object se construyó satisfactoriamente
            //if (_o_word == null)
            //{
            //    throw new Exception("Error al inicializar Word");
            //}

			return _o_word;
		}

		public static Word.Document NewDocument()
		{
			object oMissing = System.Reflection.Missing.Value;
			object oEndOfDoc = "\\endofdoc"; 

			return (_o_word != null) ? _o_word.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing) : null;
		}

		public static void Close()
		{
			if (_o_word != null)
			{
				//_o_word.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(_o_word);
				_o_word = null;
			}

			GC.Collect();
		}

		#endregion
	}
}
