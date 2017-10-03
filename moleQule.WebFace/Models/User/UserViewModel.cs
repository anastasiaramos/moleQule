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
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class UserViewModel : ViewModelBase<User, UserInfo>, IViewModel
	{
		#region Attributes

		protected UserBase _base = new UserBase();

		#endregion

        #region Properties

		[HiddenInput]
		public virtual long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long OidEntity { get { return _base.Record.OidEntity; } set { _base.Record.OidEntity = value; } }

		[HiddenInput]
		public long EntityType { get { return _base.Record.EntityType; } set { _base.Record.EntityType = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "ID")]
		public string Code { get { return _base.Record.Code; } set { _base.Record.Code = value; } }

		[Required]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "USERNAME")]
		public string Name { get { return _base.Record.Name; } set { _base.Record.Name = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "EMAIL")]
		public string Email { get { return _base.Record.Email; } set { _base.Record.Email = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "ADMIN")]
		public bool IsAdmin { get { return _base.Record.IsAdmin; } set { _base.Record.IsAdmin = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "SUPER_USER")]
		public bool IsSuperUser { get { return _base.Record.IsSuperUser; } set { _base.Record.IsSuperUser = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "USER")]
		public bool IsUser { get { return _base.IsUser; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "PARTNER")]
		public bool IsPartner { get { return _base.Record.IsPartner; } set { _base.Record.IsPartner = value; } }

		[DisplayFormat(DataFormatString = "{0:0000}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "PIN_CODE")]
		public string Pin { get { return _base.Record.Pin; } set { _base.Record.Pin = value; } }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "BIRTH_DATE")]
		public DateTime BirthDate { get { return _base.Record.BirthDate; } set { _base.Record.BirthDate = value; } }

		[HiddenInput]
		public long Status { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "CREATION_DATE")]
		public DateTime CreationDate { get { return _base.Record.CreationDate; } set { _base.Record.CreationDate = value; } }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "LAST_LOGIN_DATE")]
		public DateTime LastLoginDate { get { return _base.Record.LastLoginDate; } set { _base.Record.LastLoginDate = value; } }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "LAST_PASSWORD_DATE")]
		public DateTime LastPasswordDate { get { return _base.Record.LastPasswordDate; } set { _base.Record.LastPasswordDate = value; } }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "LAST_LOCKED_OUT_DATE")]
		public DateTime LastLockedOutDate { get { return _base.Record.LastLockedOutDate; } set { _base.Record.LastLockedOutDate = value; } }

		public DateTime LastActivityDate { get { return _base.Record.LastActivityDate; } set { _base.Record.LastActivityDate = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "PASSWORD_QUESTION")]
		public string PasswordQuestion { get { return _base.Record.PasswordQuestion; } set { _base.Record.PasswordQuestion = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "PASSWORD_RESPONSE")]
		public string PasswordResponse { get { return _base.Record.PasswordResponse; } set { _base.Record.PasswordResponse = value; } }

		//NO ENLAZADAS
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.EstadoLabel; } set {} }

        [Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "STATUS")]
        public EEstadoItem StatusList { get; set; }

		public ERol Rol { get { return _base.Rol; } }
		
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "CLIENT")]
		public bool IsClient { get { return _base.IsClient; } set { _base.IsClient = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "PROVIDER")]
		public bool IsProvider { get { return _base.IsProvider; } set { } }

		User BusinessObj { get; set; }
		UserInfo ReadOnlyObj { get; set; }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public UserViewModel() { }

		public static UserViewModel New(User source) { return New(source.GetInfo(false)); }
		public static UserViewModel New(UserInfo source)
		{
			UserViewModel obj = new UserViewModel();

			obj.CopyFrom(source);
			obj.LastActivityDate = DateTime.Now;

			return obj;
		}

		public static UserViewModel Get(long oid)
		{
			UserViewModel obj = new UserViewModel();
			UserInfo user = UserInfo.Get(oid, false);
			
			if (user != null)
			{
				obj.CopyFrom(user);
				return obj;
			}
			return null;
		}

		public static void Add(UserViewModel item)
		{
			User newItem = User.New();
			newItem.Licences = Privileges.CreatePerms(newItem);
			item.CopyTo(newItem);
			newItem.Save();
		}
		public static void Edit(UserViewModel source, HttpRequestBase request = null)
		{
			User item = User.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
			source.CopyFrom(item);
		}
		public static void Remove(long oid)
		{
			User.Delete(oid);
		}

		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class UserListViewModel : List<UserViewModel>, IEnumerable<UserViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public UserListViewModel() { }

		public static UserListViewModel Get()
		{
			UserListViewModel list = new UserListViewModel();
			UserList users = UserList.GetList();
			//users.CloseSession();

			//list.SourceList = Users.NewList();

			foreach (UserInfo item in users)
				list.Add(item);

			return list;
		}

        public static UserListViewModel Get(UserList sourceList)
        {
            UserListViewModel list = new UserListViewModel();

            foreach (UserInfo item in sourceList)
                list.Add(UserViewModel.New(item));

            return list;
        }

		/*public new void Add(UserViewModel item) 
		{
			User newItem = SourceList.NewItem();
			newItem.Licences = LicenceMap.CreatePerms(newItem);
			item.CopyTo(newItem); 
		}*/

		public void Add(UserInfo item)
		{
			//SourceList.AddItem(item);
			UserViewModel newItem = UserViewModel.New(item);
			Add(newItem);
		}

		/*public void Save()
		{
			SourceList.OpenNewSession();
			SourceList.BeginTransaction();
			SourceList.Save();
			SourceList.CloseSession();
		}*/

		#endregion
	}
}
