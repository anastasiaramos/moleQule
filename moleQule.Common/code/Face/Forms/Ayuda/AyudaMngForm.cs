using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports;
using moleQule.Library.CslaEx;
using moleQule.Face;

namespace moleQule.Face.Common
{
	public partial class AyudaMngForm : AyudaMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "AyudaMngForm";
		public static Type Type { get { return typeof(AyudaMngForm); } }
		public override Type EntityType { get { return typeof(Ayuda); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public Ayuda _entity;

		#endregion
		
		#region Factory Methods

		public AyudaMngForm()
            : this(null) {}
			
		public AyudaMngForm(Form parent)
			: this(false, parent) {}

		public AyudaMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public AyudaMngForm(Form parent, string title)
			: this(false, parent, null) 
		{
			Text = title;
		}

		public AyudaMngForm(bool isModal, Form parent, AyudaList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla); 
			Datos.DataSource = AyudaList.NewList().GetSortedList();			
			SortProperty = Codigo.DataPropertyName;
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

            AyudaInfo item = (AyudaInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Print);
					HideAction(molAction.Unlock);

					break;

				case molView.Normal:

					HideAction(molAction.Print);
					HideAction(molAction.Unlock);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Ayuda");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            {
				case EntityMngFormTypeData.Default:
					List = AyudaList.GetList(false);
					break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Ayudas");
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
                        AyudaList listA = AyudaList.GetList(_filter_results);
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
                        AyudaList listD = AyudaList.GetList(_filter_results);
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
			AyudaAddForm form = new AyudaAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenViewForm()
		{			
			AddForm(new AyudaViewForm(ActiveOID, this));
		}

        public override void OpenEditForm() 
        {             
            AyudaEditForm form = new AyudaEditForm(ActiveOID, this);
            if (form.Entity != null)
            {
                AddForm(form);
                _entity = form.Entity;
            }
        }

		public override void DeleteAction()
		{
			if (ActiveItem == null) return;

            Ayuda.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void LockAction()
		{
			ChangeEstado(EEstado.Baja);
		}

		public override void UnlockAction()
		{
			ChangeEstado(EEstado.Abierto);
		}

		public void ChangeEstado(EEstado estado)
		{
			try
			{
				_entity = Ayuda.ChangeEstado(ActiveOID, estado);
				_action_result = DialogResult.OK;
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}		

		public override void PrintList() 
		{
			/*AyudaReportMng reportMng = new AyudaReportMng(AppContext.ActiveSchema);
			
			AyudaListRpt report = reportMng.GetListReport(List);
			
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
								moleQule.Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}*/
		}

		#endregion
    }

	public partial class AyudaMngBaseForm : Skin06.EntityMngSkinForm<AyudaList, AyudaInfo>
	{
		public AyudaMngBaseForm()
			: this(false, null, null) { }

		public AyudaMngBaseForm(bool isModal, Form parent, AyudaList lista)
			: base(isModal, parent, lista) { }
	}
}
