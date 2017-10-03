using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Hipatia;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Common.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class CompanyViewModel : ViewModelBase<Company, CompanyInfo>, IViewModel
	{
		#region Attributes

		protected CompanyBase _base = new CompanyBase();

		public ContactoEmpresaList _company_contacts;		
		
		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }		
		
		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "ID")]
		public string Code { get { return _base.Record.Code; } set { _base.Record.Code = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "NAME")]
		public string Name { get { return _base.Record.Name; } set { _base.Record.Name = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "VAT_NUMBER")]
		public string VatNumber { get { return _base.Record.VatNumber; } set { _base.Record.VatNumber = value; } }

		/*[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "TIPO_ID")]
		public long TipoId { get { return _base.Record.TipoId; } set { _base.Record.TipoId = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "CTA_COTIZACION")]
		public string CtaCotizacion { get { return _base.Record.CtaCotizacion; } set { _base.Record.CtaCotizacion = value; } }*/

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "ADDRESS")]
		public string Direccion { get { return _base.Record.Direccion; } set { _base.Record.Direccion = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "CITY")]
		public string Municipio { get { return _base.Record.Municipio; } set { _base.Record.Municipio = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "ZIP_CODE")]
		public string CodPostal { get { return _base.Record.CodPostal; } set { _base.Record.CodPostal = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "STATE")]
		public string Provincia { get { return _base.Record.Provincia; } set { _base.Record.Provincia = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "PHONE_NUMBER")]
		public string Telefonos { get { return _base.Record.Telefonos; } set { _base.Record.Telefonos = value; } }

		/*[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "FAX")]
		public string Fax { get { return _base.Record.Fax; } set { _base.Record.Fax = value; } }*/

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "URL")]
		public string Url { get { return _base.Record.Url; } set { _base.Record.Url = value; } }

		[DataType(DataType.EmailAddress)]
		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "EMAIL")]
		public string Email { get { return _base.Record.Email; } set { _base.Record.Email = value; } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "MANAGER")]
		public string Responsable { get { return _base.Record.Responsable; } set { _base.Record.Responsable = value; } }

		/*[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "LOGO")]
		public string Logo { get { return _base.Record.Logo; } set { _base.Record.Logo = value; } }*/

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "BANK_ACCOUNT")]
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } set { _base.Record.CuentaBancaria = value; } }

		/*[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "P_IRPF")]
		public Decimal PIrpf { get { return _base.Record.PIrpf; } set { _base.Record.PIrpf = value; } }*/

		public ContactoEmpresaList Contacts { get { return _company_contacts; } }
		
		//UNLINKED PROPERTIES
		public virtual long Status { get { return (long)_base.EStatus; } set { /*_base.Record.Status = (long)value;*/ } }

		public virtual EEstado EStatus { get { return _base.EStatus; } set { /*_base.Record.Status = (long)value;*/ } }

		[Display(ResourceType = typeof(Library.Common.Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }
		
		#endregion
		
		#region Business Methods

        public new void CopyFrom(Company source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
        public new void CopyFrom(CompanyInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
        public new void CopyTo(Company dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public CompanyViewModel() { }

		public static CompanyViewModel New() 
		{
			CompanyViewModel obj = new CompanyViewModel();
            obj.CopyFrom(CompanyInfo.New());
			return obj;
		}
        public static CompanyViewModel New(Company source) { return New(source.GetInfo(false)); }
        public static CompanyViewModel New(CompanyInfo source)
		{
			CompanyViewModel obj = new CompanyViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static CompanyViewModel Get(long oid)
		{
			CompanyViewModel obj = new CompanyViewModel();
            obj.CopyFrom(CompanyInfo.Get(oid, false));
			return obj;
		}

		public static void Add(CompanyViewModel item)
		{
            Company newItem = Company.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(CompanyViewModel source, HttpRequestBase request = null)
		{
            Company item = Company.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
            Company.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class CompanyListViewModel : List<CompanyViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public CompanyListViewModel() { }

		public static CompanyListViewModel Get()
		{
			CompanyListViewModel list = new CompanyListViewModel();

            CompanyList sourceList = CompanyList.GetList();

            foreach (CompanyInfo item in sourceList)
				list.Add(CompanyViewModel.New(item));

			return list;
		}
        public static CompanyListViewModel Get(CompanyList sourceList)
		{
			CompanyListViewModel list = new CompanyListViewModel();

            foreach (CompanyInfo item in sourceList)
				list.Add(CompanyViewModel.New(item));

			return list;
		}

		#endregion
	}
}
