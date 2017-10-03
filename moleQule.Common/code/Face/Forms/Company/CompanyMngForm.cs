using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using Csla;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Common
{
	public partial class CompanyMngForm : CompanyMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "CompanyMngForm";
		public static Type Type { get { return typeof(CompanyMngForm); } }
		public override Type EntityType { get { return typeof(Company); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

		public Company _entity;

		public override ISchemaInfo ActiveISchema { get { return CompanyInfo.GetISchemaInfo(ActiveOID); } }

		#endregion

		#region Factory Methods

		public CompanyMngForm()
			: this(null) { }

		public CompanyMngForm(Form parent)
			: this(false, parent, null) { }

		public CompanyMngForm(bool isModal, Form parent, CompanyList list = null)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseÃ±o y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = CompanyList.NewList().GetSortedList();
			SortProperty = Codigo.DataPropertyName;
		}

		#endregion

		#region Autorizacion

        protected override void ActivateAction(molAction action, bool state)
        {
            if (EntityType == null) return;

            switch (action)
            {
                case molAction.ChangeStateContabilizado:

                    if ((AppContext.User != null) && (state))
                        base.ActivateAction(action, Library.AutorizationRulesControler.CanEditObject(Library.Common.Resources.SecureItems.EMPRESA));
                    else
                        base.ActivateAction(action, state);

                    break;

                default:
                    base.ActivateAction(action, state);
                    break;
            }
        }

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.8;
			Municipio.Tag = 0.2;

			cols.Add(Nombre);
			cols.Add(Municipio);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

			//EmpresaInfo item = (EmpresaInfo)row.DataBoundItem;

			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
                case molView.Select:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Empresa");

			// Guardamos la configuración actual del listado
			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = CompanyList.GetList(AppContext.User.GetInfo(), false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Empresas");
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
						CompanyList listA = CompanyList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
				case molAction.ChangeStateAnulado:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						CompanyList listD = CompanyList.GetList(_filter_results);
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

		public override void LoadSchema() { base.LoadSchema(); }
		public override void LoadSchema(ISchemaInfo schema) 
		{ 
			base.LoadSchema(schema);

			//Company currency configuration
			CompanyInfo company = CompanyInfo.Get(AppContext.ActiveSchema.Oid, false);
			company.SetCurrency();
		}

		public override void OpenAddForm()
		{
#if DEMO
			PgMng.ShowInfoException(Resources.Messages.ENTERPRISE_MAX_LIMITED_NOTICE);				
			_action_result = DialogResult.Ignore;
#else
			if (List.Count == Properties.Settings.Default.ENTERPRISE_MAX_LIMITED)
			{
				PgMng.ShowInfoException(Resources.Messages.ENTERPRISE_MAX_LIMITED_NOTICE);
				_action_result = DialogResult.Ignore;
			}
			else
			{
				CompanyAddForm form = new CompanyAddForm(this);
				AddForm(form);
				_entity = form.Entity;
			}
#endif
		}

		public override void OpenViewForm() { AddForm(new CompanyViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			CompanyEditForm form = new CompanyEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Company.Delete(ActiveOID);
			_action_result = DialogResult.OK;
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

        protected override void DefaultAction() { MarkDefaultSchema(); }
        
		public override void MarkDefaultSchema()
		{
			if (List == null) return;

            SettingsMng.Instance.SetDefaultSchema(ActiveOID);
            SettingsMng.Instance.UserSettings.Save();
			List.SetPrincipal(ActiveOID);

			Datos.ResetBindings(false);
            LoadSchema();
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			/*
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			
			EmpresaReportMng reportMng = new EmpresaReportMng(AppContext.ActiveSchema);
			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);			
			EmpresaListRpt report = reportMng.GetListReport(List);
			
			PgMng.FillUp();
			
			ShowReport(report);
			*/
		}

		#endregion
	}

	public partial class CompanyMngBaseForm : Skin06.SchemaMngSkinForm<CompanyList, CompanyInfo>
	{
		public CompanyMngBaseForm()
			: this(false, null, null) { }

		public CompanyMngBaseForm(bool isModal, Form parent, CompanyList list)
			: base(isModal, parent, CompanyMngForm.Type, list) { }
	}
}
