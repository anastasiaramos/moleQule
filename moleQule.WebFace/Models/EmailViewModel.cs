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
	public interface IEmailViewModel
	{
		string Body { get; set; }
		long NoticeType { get; set; }
		string RecipientName { get; set; }
	}

	/// <summary>
	/// ErrorViewModel
	/// </summary>
	[Serializable()]
	public class EmailViewModel : IEmailViewModel
	{
		#region Attributes

		protected MailParams _mailParams = new MailParams();
		protected long _noticeType = 0;
        protected DateTime _date = DateTime.Now;

		#endregion

        #region Properties

		[Display(ResourceType = typeof(Resources.Labels), Name = "BODY")]
		public string Body { get { return _mailParams.Body; } set {_mailParams.Body = value;} }

		public long NoticeType { get { return _noticeType; } set { _noticeType = value; } }

        public string RecipientName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get { return _date; } set { _date = value; } }
		
		public string HtmlBody { get { return Body.Replace(System.Environment.NewLine, "<br>"); } }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public EmailViewModel() { }

		public static EmailViewModel New(string body, long noticeType)
		{
			EmailViewModel obj = new EmailViewModel()
			{
				Body = body,
				NoticeType = noticeType
			};

			return obj;
		}
		public static EmailViewModel New(Exception ex, long noticeType) 
		{
			return New(iQExceptionHandler.GetAllMessages(ex, true), noticeType);
		}

		#endregion
	}
}
