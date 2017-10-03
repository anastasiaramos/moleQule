using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports.Registry;
using moleQule.Library.CslaEx;

namespace moleQule.Face.Common
{
	public partial class LineaRegistroMngForm : LineaRegistroMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "LineaRegistroMngForm";
		public static Type Type { get { return typeof(LineaRegistroMngForm); } }
		public override Type EntityType { get { return typeof(LineaRegistro); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public LineaRegistro _entity;
		public ETipoRegistro _tipo = ETipoRegistro.Todos;
		
		#endregion
		
		#region Factory Methods

		public LineaRegistroMngForm()
            : this(null) {}
			
		public LineaRegistroMngForm(Form parent)
			: this(false, parent) {}
		
		public LineaRegistroMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}

		public LineaRegistroMngForm(Form parent, ETipoRegistro tipo, string title)
			: this(false, parent, null) 
		{
			Text = title;
			_tipo = tipo;
		}

		public LineaRegistroMngForm(bool is_modal, Form parent, LineaRegistroList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla);
			Datos.DataSource = LineaRegistroList.NewList().GetSortedList();
			SortProperty = IDCompuesto.DataPropertyName;
        }
		
		#endregion

		#region Style & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

            switch (_tipo)
            {
                case ETipoRegistro.Fomento:

                    Descripcion.Visible = false;
                    CodigoEntidad.Visible = false;

                    Expediente.Tag = 0.3;
                    Producto.Tag = 0.3;
                    Observaciones.Tag = 0.4;

                    cols.Add(Expediente);
                    cols.Add(Producto);
                    cols.Add(Observaciones);

                    break;

                default:

                    LineaFomento.Visible = false;
                    Expediente.Visible = false;
                    Producto.Visible = false;
                    Subvencion.Visible = false;
                    FechaConocimiento.Visible = false;

                    Descripcion.Tag = 0.4;
                    Observaciones.Tag = 0.6;

                    cols.Add(Descripcion);
                    cols.Add(Observaciones);

                    break;
            }

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

            LineaRegistroInfo item = (LineaRegistroInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
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
					HideAction(molAction.Delete);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Delete);

					break;
			}
		}
		
		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "LineaRegistro");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            {
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = LineaRegistroList.GetList(_tipo, Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = LineaRegistroList.GetList(_tipo, false);
					break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de LineaRegistros");
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
                        LineaRegistroList listA = LineaRegistroList.GetList(_filter_results);
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
                        LineaRegistroList listD = LineaRegistroList.GetList(_filter_results);
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

        #region Print

		public override void PrintList() 
		{
            RegistryReportMng reportMng = new RegistryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

            ReportClass report = null;

            switch(_tipo)
            {
                case ETipoRegistro.Fomento:
                        report = reportMng.GetListFomentoReport(LineaRegistroList.GetList(Datos.DataSource as IList<LineaRegistroInfo>));
                    break;

                default:
                        report = reportMng.GetListReport(LineaRegistroList.GetList(Datos.DataSource as IList<LineaRegistroInfo>));
                    break;
            }

            ShowReport(report);
		}

		#endregion
    }

	public partial class LineaRegistroMngBaseForm : Skin06.EntityMngSkinForm<LineaRegistroList, LineaRegistroInfo>
	{
		public LineaRegistroMngBaseForm()
			: this(false, null, null) { }

		public LineaRegistroMngBaseForm(bool isModal, Form parent, LineaRegistroList lista)
			: base(isModal, parent, lista) { }
	}

}
