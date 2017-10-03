using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;

using moleQule.Library;

using moleQule.Face;

namespace moleQule.Face
{
    public partial class UserMngForm : UserMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "UserMngForm";
		public static Type Type { get { return typeof(UserMngForm); } }
        public override Type EntityType { get { return typeof(User); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        public User _entity;

		#endregion
		
		#region Factory Methods

		public UserMngForm() 
			: this(null) {}
			
		public UserMngForm(Form parent)
			: this(false, parent) {}
		
		public UserMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) {}

		public UserMngForm(bool isModal, Form parent, UserList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = UserList.NewList().GetSortedList();			
			SortProperty = Nombre.DataPropertyName;
        }
		
		#endregion
	
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() {}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			DefaultSchema.Tag = 0.7;

			cols.Add(Nombre);
			cols.Add(DefaultSchema);

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
            
			UserInfo item = (UserInfo)row.DataBoundItem;
			
			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (ViewMode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Copy);
					HideAction(molAction.View);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Copy);
					HideAction(molAction.View);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);

					break;
			}
		}
		
		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "User");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = UserList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Users");
		}
		
        public override void UpdateList()
        {
            /*switch (_current_action)
            {
                case molAction.Add:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo(false));
                    if (FilterType == IFilterType.Filter)
                    {
                        UserList listA = UserList.GetList(_filter_results);
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
                        UserList listD = UserList.GetList(_filter_results);
                        listD.RemoveItem(ActiveOID);
                        _filter_results = listD.GetSortedList();
                    }
                    break;
            }*/

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
        }	
		
		#endregion

        #region Actions

		#endregion
    }

	public partial class UserMngBaseForm : Skin06.EntityMngSkinForm<UserList, UserInfo>
	{
		public UserMngBaseForm() 
			: this(false, null, null) {}

		public UserMngBaseForm(bool isModal, Form parent, UserList lista)
			: base(isModal, parent, lista) {}
	}
}
