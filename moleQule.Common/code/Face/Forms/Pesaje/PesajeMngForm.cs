using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using Csla;
using moleQule.Library.CslaEx;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports;
using moleQule.Face;

namespace moleQule.Face.Common
{
	public partial class PesajeMngForm : PesajeMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "PesajeMngForm";
		public static Type Type { get { return typeof(PesajeMngForm); } }
		public override Type EntityType { get { return typeof(Pesaje); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }
				
		public Pesaje _entity;

		#endregion
		
		#region Factory Methods

		protected PesajeMngForm()
            : this(null) {}
			
		public PesajeMngForm(Form parent)
			: this(false, parent) {}
		
		public PesajeMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public PesajeMngForm(bool isModal, Form parent, PesajeList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
			Datos.DataSource = PesajeList.NewList().GetSortedList();			
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
        }
		
		#endregion
	
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() {}

		#endregion

		#region Style & Format
		
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

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
            
			//PesajeInfo item = (PesajeInfo)row.DataBoundItem;
			
			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Print);
					HideAction(molAction.Delete);

					break;

				case molView.Normal:
					
					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Print);
					HideAction(molAction.Delete);

					break;
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Pesaje");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = PesajeList.GetList(DateTime.Today.AddDays(-7), DateTime.Today, false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Pesajes");
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
                        PesajeList listA = PesajeList.GetList(_filter_results);
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
                        PesajeList listD = PesajeList.GetList(_filter_results);
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
		}

		public override void OpenViewForm()
		{
		}

        public override void OpenEditForm() 
        {  
		}

		#endregion

		#region Print

		public override void PrintList() 
		{
			/*
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			
			PesajeReportMng reportMng = new PesajeReportMng(AppContext.ActiveSchema);
			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);			
			PesajeListRpt report = reportMng.GetListReport(List);
			
			PgMng.FillUp();
			
			ShowReport(report);
			*/
		}

		#endregion
    }

	public partial class PesajeMngBaseForm : Skin06.EntityMngSkinForm<PesajeList, PesajeInfo>
	{
		public PesajeMngBaseForm()
			: this(false, null, null) { }

		public PesajeMngBaseForm(bool isModal, Form parent, PesajeList lista)
			: base(isModal, parent, lista) { }
	}
}
