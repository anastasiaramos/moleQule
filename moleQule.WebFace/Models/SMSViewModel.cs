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

namespace moleQule.WebFace.Models
{
	public interface ISMSViewModel
	{
		string Text { get; set; }
		long NoticeType { get; set; }
		string PhoneNumber { get; set; }
	}

	/// <summary>
	/// ErrorViewModel
	/// </summary>
	[Serializable()]
	public class SMSViewModel : ISMSViewModel
	{
		#region Properties

		protected SMSParams _sms_params = new SMSParams();
		protected long _noticeType = 0;

		#endregion

        #region Properties

		[Display(ResourceType = typeof(Resources.Labels), Name = "TEXT")]
		public string Text { get { return _sms_params.Text; } set {_sms_params.Text = value;} }

		public long NoticeType { get { return _noticeType; } set { _noticeType = value; } }

		public string PhoneNumber { get; set; }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public SMSViewModel() { }

		public static SMSViewModel New(string text, long noticeType)
		{
			SMSViewModel obj = new SMSViewModel()
			{
				Text = text,
				NoticeType = noticeType
			};

			return obj;
		}
		public static SMSViewModel New(Exception ex, long noticeType) 
		{
			return New(iQExceptionHandler.GetAllMessages(ex, true), noticeType);
		}

		#endregion
	}
}
