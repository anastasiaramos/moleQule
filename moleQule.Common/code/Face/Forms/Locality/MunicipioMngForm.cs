using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports;
using moleQule.Face;

namespace moleQule.Face.Common
{
	public partial class MunicipioMngForm : MunicipioMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "MunicipioMngForm";
		public static Type Type { get { return typeof(MunicipioMngForm); } }
		public override Type EntityType { get { return typeof(Municipio); } }
	
		protected override int BarSteps { get { return base.BarSteps + 4; } }

		public Municipio _entity;

		#endregion

		#region Factory Methods

		protected MunicipioMngForm()
			: this(null) { }

		public MunicipioMngForm(Form parent)
			: this(false, parent) { }

		public MunicipioMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public MunicipioMngForm(bool isModal, Form parent, MunicipioList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = MunicipioList.NewList().GetSortedList();
			SortProperty = Municipio.DataPropertyName;
		}

		#endregion

		#region Style & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Localidad.Tag = 0.5;
			Municipio.Tag = 0.5;

			cols.Add(Localidad);
			cols.Add(Municipio);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			/*if (row.IsNewRow) return;
			MunicipioInfo item = (MunicipioInfo)row.DataBoundItem;*/
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Copy);

					break;

				case molView.Normal:

					ShowAction(molAction.Copy);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Municipio");

			long oid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = MunicipioList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Municipios");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo());
					if (FilterType == IFilterType.Filter)
					{
						MunicipioList listA = MunicipioList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo());
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
						MunicipioList listD = MunicipioList.GetList(_filter_results);
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
			MunicipioAddForm form = new MunicipioAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new MunicipioViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			MunicipioEditForm form = new MunicipioEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void CopyObjectAction(long oid)
		{
			MunicipioAddForm form = new MunicipioAddForm(Library.Common.Municipio.CloneAsNew(ActiveItem), this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteObject(long oid)
		{
			Library.Common.Municipio.Delete(oid);
			_action_result = DialogResult.OK;
		}

		#endregion
	}

	public partial class MunicipioMngBaseForm : Skin06.EntityMngSkinForm<MunicipioList, MunicipioInfo>
	{
		public MunicipioMngBaseForm()
			: this(false, null, null) { }

		public MunicipioMngBaseForm(bool isModal, Form parent, MunicipioList lista)
			: base(isModal, parent, lista) { }
	}
}
