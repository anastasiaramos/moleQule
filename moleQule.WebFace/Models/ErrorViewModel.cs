using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.WebFace;
using moleQule.WebFace.Resources;

namespace moleQule.WebFace.Models
{
	public interface IErrorViewModel
	{
		long HttpCode { get; set; }
		iQExceptionCode molCode { get; set; }
		long Code { get; set; }
		string Message { get; set; }
		string SystemMessage { get; set; }
		string Title { get; set; }
	}

	/// <summary>
	/// ErrorViewModel
	/// </summary>
	[Serializable()]
	public class ErrorViewModel : IErrorViewModel
	{
        #region Properties

		public long HttpCode { get; set; }

		public iQExceptionCode molCode { get; set; }

		[Display(ResourceType = typeof(Labels), Name = "ERROR_CODE")]
		public long Code { get { return (long)molCode; } set { molCode = (iQExceptionCode)value; } }

		[Display(ResourceType = typeof(Labels), Name = "MESSAGE")]
		public string Message { get; set; }

		[Display(ResourceType = typeof(Labels), Name = "SYSTEM_MESSAGE")]
		public string SystemMessage { get; set; }

		public string Title { get; set; }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public ErrorViewModel() { }

		public static ErrorViewModel New(Exception ex) 
		{
			ex = ex ?? new iQException(Errors.NULL_EXCEPTION);

			long httpcode = 500;
			httpcode = (ex is HttpException) ? ((HttpException)ex).GetHttpCode() : httpcode;
			httpcode = (ex is HttpCompileException) ? 000 : httpcode;

			try
			{
				iQExceptionHandler.TreatException(ex);
			}
			catch (iQException iQex)
			{
				return New(	httpcode,
							iQex.Code,
							iQex.Message,
							iQex.SysMessage);
			}

			return null;
		}
		public static ErrorViewModel New(long httpCode, iQExceptionCode errorCode, string message, string systemMessage)
		{
			ErrorViewModel obj = new ErrorViewModel();

			obj.HttpCode = httpCode;
			obj.molCode = errorCode;
			obj.Message = string.IsNullOrEmpty(message) ? GetMessage(httpCode) : message;
			obj.SystemMessage = systemMessage;
			obj.Title = GetTitle(httpCode);

			return obj;
		}

		public static string GetTitle(long httpCode)
		{
			switch (httpCode)
			{
				case 400: return Errors.ERROR_400_TITLE;
				case 401: return Errors.ERROR_401_TITLE;
				case 403: return Errors.ERROR_403_TITLE;
				case 404: return Errors.ERROR_404_TITLE;
				default: return Errors.ERROR_000_TITLE;
			}
		}

		public static string GetMessage(long httpCode)
		{
			switch (httpCode)
			{
				case 400: return Errors.ERROR_400;
				case 401: return Errors.ERROR_401;
				case 403: return Errors.ERROR_403;
				case 404: return Errors.ERROR_404;
				default: return Errors.ERROR_000;
			}
		}

		#endregion
	}
}
