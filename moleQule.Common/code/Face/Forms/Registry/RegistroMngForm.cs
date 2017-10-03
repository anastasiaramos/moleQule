using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports.Registry;

namespace moleQule.Face.Common
{
	public partial class RegistroMngForm : RegistroMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "RegistroMngForm";
		public static Type Type { get { return typeof(RegistroMngForm); } }
		public override Type EntityType { get { return typeof(Registro); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public Registro _entity;
		public ETipoRegistro _tipo = ETipoRegistro.Todos;

		#endregion
		
		#region Factory Methods

		public RegistroMngForm()
            : this(null) {}
			
		public RegistroMngForm(Form parent)
			: this(false, parent) {}
		
		public RegistroMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}

		public RegistroMngForm(Form parent, ETipoRegistro tipo, string title)
			: this(false, parent, null) 
		{
			Text = title;
			_tipo = tipo;
		}

		public RegistroMngForm(bool is_modal, Form parent, RegistroList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
			Datos.DataSource = RegistroList.NewList().GetSortedList();
            SortProperty = Fecha.DataPropertyName;
            SortDirection = ListSortDirection.Descending;
        }
		
		#endregion
		
		#region Style & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Nombre);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);   
		}

		public override void FormatControls()
		{
            if (Tabla == null) return;
			
			base.FormatControls();
		}
		
		protected override void SetRowFormat(DataGridViewRow row)
        {
            if (row.IsNewRow) return;

            RegistroInfo item = (RegistroInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);

					break;

				case molView.Normal:

					HideAction(molAction.Unlock);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Registro");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            {
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = RegistroList.GetList(_tipo, Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = RegistroList.GetList(_tipo, false);
					break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Registros");
		}

        public override void UpdateList()
        {
            switch (_current_action)
            {
                case molAction.Add:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo(false));
                    if (FilterType == IFilterType.Filter)
                    {
                        RegistroList listA = RegistroList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Lock:
                case molAction.Unlock:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        RegistroList listD = RegistroList.GetList(_filter_results);
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
			RegistroAddForm form = new RegistroAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new RegistroViewForm(ActiveOID, this)); }

        public override void OpenEditForm() 
        {             
            RegistroEditForm form = new RegistroEditForm(ActiveOID, this);
            if (form.Entity != null)
            {
                AddForm(form);
                _entity = form.Entity;
            }
        }

		public override void DeleteAction()
		{
			if (ActiveItem == null) return;

			if (ActiveItem.EEstado != EEstado.Abierto)
			{
				PgMng.ShowInfoException(Resources.Messages.BORRAR_REGISTRO_NO_PERMITIDO);

				_action_result = DialogResult.Ignore;
				return;
			}

            Registro.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void LockAction() { ChangeEstado(EEstado.Closed); }

		public override void UnlockAction() { ChangeEstado(EEstado.Abierto); }

		public void ChangeEstado(EEstado estado)
		{
			try
			{
				_entity = Registro.ChangeEstado(ActiveOID, estado);
				_action_result = DialogResult.OK;
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}

        #endregion

        #region Print

        public override void PrintList()
        {
            RegistryReportMng reportMng = new RegistryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

            RegistryListRpt report = reportMng.GetListReport(RegistroList.GetList(Datos.DataSource as IList<RegistroInfo>));

            ShowReport(report);
        }

		#endregion 
    }

	public partial class RegistroMngBaseForm : Skin06.EntityMngSkinForm<RegistroList, RegistroInfo>
	{
		public RegistroMngBaseForm()
			: this(false, null, null) { }

		public RegistroMngBaseForm(bool isModal, Form parent, RegistroList lista)
			: base(isModal, parent, lista) { }
	}
}
