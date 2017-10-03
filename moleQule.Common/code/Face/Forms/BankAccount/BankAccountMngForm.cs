using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports.BankAccount;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Common
{
	public partial class BankAccountMngForm : BankAccountMngBaseForm
    {
        #region Attributes & Properties

		public const string ID = "BankAccountMngForm";
		public static Type Type { get { return typeof(BankAccountMngForm); } }
        public override Type EntityType { get { return typeof(BankAccount); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        public BankAccount _entity;

		#endregion
		
		#region Factory Methods

		public BankAccountMngForm()
            : this(null) {}
			
		public BankAccountMngForm(Form parent)
			: this(false, parent) {}
		
		public BankAccountMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

        public BankAccountMngForm(bool isModal, Form parent, BankAccountList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
            Datos.DataSource = BankAccountList.NewList().GetSortedList();			
			SortProperty = Entidad.DataPropertyName;
        }
		
		#endregion
	
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() {}

		#endregion

		#region Layout
		
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Entidad.Tag = 0.2;
            Observaciones.Tag = 0.8;

            cols.Add(Entidad);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
            if (Tabla == null) return;

            base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Resources.Labels.MOVIMIENTOS_TI, Properties.Resources.apunte_bancario);
		}
	
		protected override void SetRowFormat(DataGridViewRow row)
        {
            if (row.IsNewRow) return;
            
			BankAccountInfo item = (BankAccountInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			if (item.EEstado != EEstado.Baja)
			{
				row.Cells[Saldo.Name].Style.ForeColor = (item.Saldo > 0) ? Face.Common.ControlTools.Instance.AbiertoStyle.ForeColor : Color.Red;
			}
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
					HideAction(molAction.Delete);
                    HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);
					ShowAction(molAction.Print);
					HideAction(molAction.Delete);
                    ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "CuentaBancaria");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = BankAccountList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de CuentaBancarias");
		}

        public override void UpdateList()
        {
            switch (_current_action)
            {
                case molAction.Add:
				case molAction.Copy:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo(false));
                    if (FilterType == IFilterType.Filter)
                    {
                        BankAccountList listA = BankAccountList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
				case molAction.Lock:
                case molAction.Unlock:
				case molAction.ChangeStateAnulado:
                    if (_entity == null) return;
                    if (_entity.Oid != ActiveItem.Oid)
                        ActiveItem.CopyFrom(_entity.CuentasAsociadas.GetItem(ActiveItem.Oid));
                    else
                        ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        BankAccountList listD = BankAccountList.GetList(_filter_results);
                        listD.RemoveItem(ActiveOID);
                        _filter_results = listD.GetSortedList();
                    }
                    break;
            }

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
        }	

		#endregion

        #region Actions

        public override void OpenAddForm()
        {
			BankAccountAddForm form = new BankAccountAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			switch (ActiveItem.ETipoCuenta)
			{
				case ETipoCuenta.ComercioExterior:
                case ETipoCuenta.FondoInversion:
					AddForm(new BankAccountViewForm(ActiveItem.OidCuentaAsociada, this));
					break;

				default:
					AddForm(new BankAccountViewForm(ActiveOID, this));
					break;
			}
		}

        public override void OpenEditForm() 
        {  
			switch (ActiveItem.ETipoCuenta)
			{
				case ETipoCuenta.ComercioExterior:
                case ETipoCuenta.FondoInversion:
					{
						BankAccountEditForm form = new BankAccountEditForm(ActiveItem.OidCuentaAsociada, this);
						if (form.Entity != null)
						{
							AddForm(form);
							_entity = form.Entity;
						}
					}
					break;

				default:
					{
						BankAccountEditForm form = new BankAccountEditForm(ActiveOID, this);
						if (form.Entity != null)
						{
							AddForm(form);
							_entity = form.Entity;
						}
					}
					break;
			}
		}

		public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
					AddForm(form);
				}
			}
		}

		public override void CustomAction1() { ShowApuntesAction(); }

		public void ShowApuntesAction()
		{
			BankAccountInfo cuenta = BankAccountInfo.Get(ActiveOID, false);

			Assembly assembly = Assembly.Load("moleQule.Face.Invoice");
            Type type = assembly.GetType("moleQule.Face.Invoice.BankLineMngForm");

            EntityMngBaseForm form = (EntityMngBaseForm)type.InvokeMember("BankLineMngForm", BindingFlags.CreateInstance, null, null, new object[3] { true, this, cuenta });
			form.ShowDialog();
		}

		#endregion

		#region Print

		public override void PrintList() 
		{
			
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

            BankAccountReportMng reportMng = new BankAccountReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);			
			ReportClass report = reportMng.GetListReport(List);
			
			PgMng.FillUp();
			
			ShowReport(report);
			
		}

		#endregion
    }

    public partial class BankAccountMngBaseForm : Skin06.EntityMngSkinForm<BankAccountList, BankAccountInfo>
	{
		public BankAccountMngBaseForm()
			: this(false, null, null) { }

        public BankAccountMngBaseForm(bool isModal, Form parent, BankAccountList lista)
			: base(isModal, parent, lista) { }
	}
}
