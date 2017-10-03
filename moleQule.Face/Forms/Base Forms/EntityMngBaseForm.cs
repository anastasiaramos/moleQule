using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;
using moleQule.Library;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos de ese Tipo
	/// </summary>
	public partial class EntityMngBaseForm : moleQule.Face.ChildForm, IBackGroundLauncher
	{
		#region Attributes & Properties

        /// <summary>
        /// Nº de pasos para la barra de progreso
        /// </summary>
        protected override int BarSteps { get { return base.BarSteps; } }

        /// <summary>
        /// DataGrid con el listado
        /// </summary>
        protected DataGridView TablaBase;

        /// <summary>
        /// Forma de asociar los datos al formulario
        /// </summary>
		EntityMngFormTypeData _data_type = EntityMngFormTypeData.Default;

        /// <summary>
        /// Lista de objetos del formulario
        /// </summary>
        protected object _item_list;

        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        protected object _selected;

        protected long _selectedOid = 0;

        /// <summary>
        /// Columna de orden
        /// </summary>
        protected string _sort_property = "Codigo";

        /// <summary>
        /// Direccion de ordenacion
        /// </summary>
        protected ListSortDirection _sort_direction = ListSortDirection.Ascending;

		/// <summary>
		/// Mantiene una lista de formularios de tipo DriverEntityForm
		/// activos 
		/// </summary>
        protected IList<EntityDriverForm> _list_active_form;

        protected Operation _operation = Operation.Contains;
        protected object _search_value = string.Empty;
        protected object _second_search_value = string.Empty;
		
		protected bool NeedsOrderList { get; set; }
		protected bool NeedsRefreshList { get; set; }

		//Estas deben sobreescribirse como SortedBindingList<object>
        protected object _sorted_list = null;
        protected object _filter_results = null;
        protected object _search_results = null;
		//Estas deben sobreescribirse como List<object>
		protected object _last_results = null;

		protected IFilterProperty _filter_property = IFilterProperty.All;
        protected IFilterType _filter_type = IFilterType.None;
        protected string _filter_keys = string.Empty;
		public List<FilterItem> _filter_list = new List<FilterItem>();
        protected DataGridViewColumn _active_column = null;

        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        public EntityMngFormTypeData DataType { get { return _data_type; } }

        /// <summary>
        /// Lista de objetos del formulario
        /// </summary>
        public object List { get { return _item_list; } }

		/// <summary>
		/// Devuelve el OID del objeto activo seleccionado de la tabla
		/// </summary>
		/// <returns></returns>
		public virtual long ActiveOID { get { return -1; } }
        
        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        public object Selected { get { return _selected; } }

		public virtual string SortProperty
		{
			get { return _sort_property; }
			set { _sort_property = value; }
		}
		public virtual ListSortDirection SortDirection
		{
			get { return _sort_direction; }
			set { _sort_direction = value; }
		}

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public virtual long ActiveFoundOID { get { return -1; } }

        public object FilteredList { get { throw new iQImplementationException("FilteredList"); } }
        public object SortedList { get { throw new iQImplementationException("SortedList"); } }

		public IFilterProperty FilterProperty { get { return _filter_property; } }
        public IFilterType FilterType { get { return _filter_type; } }
		public List<FilterItem> FilterList { get { return _filter_list; } set { _filter_list = value; } }
		public String FilterValues 
		{ 
			get 
			{
				string filter_values = string.Empty;
				foreach (FilterItem item in FilterList)
					if (item.Active) filter_values += item.Text;

				return filter_values;
			} 
		}

		public DataGridViewCellStyle BasicStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle HideStyle = new DataGridViewCellStyle();

		#endregion

        #region Factory Methods

        public EntityMngBaseForm()
            : this(false, null) {}

		public EntityMngBaseForm(bool isModal)
			: this(isModal, null) { }

		public EntityMngBaseForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public EntityMngBaseForm(bool isModal, Form parent, object list)
			: base(isModal, parent)
        {
            InitializeComponent();

            _list_active_form = new List<EntityDriverForm>();

			ViewMode = molView.Normal;

			_item_list = list;
            _data_type = (_item_list == null) ? EntityMngFormTypeData.Default : EntityMngFormTypeData.ByParameter;           

			NeedsOrderList = true;

			try
			{
                _show_colors = SettingsMng.Instance.GetFormatGridsSetting();
			}
			catch { }
        }

		public override void InitForm()
		{
#if TRACE
			PgMng.Record("EMngBaseForm::InitForm INI");
#endif
			//IDE Design compatibility
			if (TablaBase == null) return;

			ApplyAuthorizationRules();
			RefreshSecondaryData();
			RefreshList();
			FormatForm();
#if TRACE
			PgMng.Record("EMngBaseForm::InitForm END");
#endif
		}

		protected virtual void SetMainDataGridView(DataGridView grid)
		{
			TablaBase = grid;

			/*this.TablaBase.DoubleClick += new System.EventHandler(this.TablaBase_DoubleClick);
			this.TablaBase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablaBase_CellClick);
			this.TablaBase.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TablaBase_ColumnHeaderMouseClick);
			this.TablaBase.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TablaBase_RowPrePaint);
			this.TablaBase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TablaBase_KeyPress);*/
		}

		#endregion

        #region IBackGroundLauncher

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new void BackGroundJob(BackgroundWorker bk)
        {
            try
            {
                switch (_back_job)
                {
                    case BackJob.RefreshList:
                        BkRefreshList(bk);
                        break;

                    default:
                        base.BackGroundJob(bk);
                        break;
                }
            }
            catch (Exception ex)
            {
                CancelBackGroundJob();
                MessageBox.Show(ex.Message);
            }
        }

        private void BkRefreshList(BackgroundWorker bk)
        {
            try
            {
                RefreshMainData();
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
				PgMng.ShowInfoException(ex);
            }
        }

        #endregion

        #region Layout & Format

		public override bool ActivateForm()
		{
			if (EntityType == null)
				return true;

			if (!(bool)EntityType.InvokeMember("CanGetObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null))
			{
				PgMng.FillUp();
				PgMng.ShowInfoException(Resources.Messages.NO_GET_PERMISSION);
				return false;
			}

			return true;
		}

		protected override void FormatForm()
		{
#if TRACE
			PgMng.Record("EMngBaseForm::FormatForm INI");
#endif
			SetView();
			base.FormatForm();
#if TRACE
			PgMng.Record("EMngBaseForm::FormatForm END");
#endif
		}

		public override void FormatControls()
		{
			base.FormatControls();

			ControlTools.Instance.CopyBasicStyle(BasicStyle);
			ControlTools.Instance.CopyBasicStyle(HideStyle);
			HideStyle.ForeColor = Color.Transparent;
			HideStyle.SelectionForeColor = Color.Transparent;
		}

        /// <summary>
        /// Da formato a los controles para la vista de SelectForm
        /// </summary>
        protected virtual void SetSelectView() { SetView(molView.Select); }

        protected ListSortDirection GetGridSortDirection(DataGridView grid)
        {
			if (grid == null) return ListSortDirection.Ascending;

			return (grid.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }

		protected string GetGridSortColumn(DataGridView grid)
		{
			if (grid == null) return string.Empty;
			return (grid.SortedColumn != null) ? grid.SortedColumn.Name : _sort_property;
		}

        protected string GetGridSortProperty(DataGridView grid)
        {
			if (grid == null) return string.Empty;
            return (grid.SortedColumn != null) ? grid.SortedColumn.DataPropertyName : _sort_property;
        }

		protected virtual void SetColumnActive(DataGridViewColumn col)
		{
			_active_column = col;
			ControlsMng.MarkGridColumn(TablaBase, _active_column, ControlTools.Instance.HeaderSelectedStyle);
		}

		protected void OrderByColumn(DataGridViewColumn col, ListSortDirection direction) { OrderByColumn(col, direction, false); }
		protected virtual void OrderByColumn(DataGridViewColumn col, ListSortDirection sortDirection, bool respect_current)
		{
#if TRACE
			PgMng.Record("EMngBase::OrderByColumn - INI");
#endif
			SortProperty = col.DataPropertyName;
			SortDirection = sortDirection;

			ControlsMng.OrderByColumn(TablaBase, col, sortDirection, respect_current);
			SetColumnActive(col);
#if TRACE
			PgMng.Record("EMngBase::OrderByColumn - END");
#endif
		}

        protected virtual void SetRowFormat(DataGridViewRow row) { }

        protected virtual void SetGridFormat() { SetGridFormat(null); }
        protected virtual void SetGridFormat(Control control)
        {
			if (!_show_colors) return;
#if TRACE
			if (PgMng != null) PgMng.Record("EMngBase::SetGridFormat - INI");
#endif
			foreach (DataGridViewRow row in TablaBase.Rows)
			{
				if (row.Index > 50) return;
				SetRowFormat(row);
			}
#if TRACE
			if (PgMng != null) PgMng.Record("EMngBase::SetGridFormat - END");
#endif
        }

        /// <summary>
        /// Maximiza el tamaño del formulario al total del area cliente.
        /// Si utilizamos el Maximize lo aplica a todos los formularios abiertos
        /// </summary>
        protected override void MaximizeForm() 
        {
            SuspendLayout();
            base.MaximizeForm();
            ResumeLayout();
        }

		protected virtual void ShowNoResultsPanel(bool show)
		{
#if TRACE
			PgMng.Record("EMngBaseForm::ShowNoResultsPanel - INI");
#endif
			ProgressBK_Panel.Visible = show;
			
			Title_LB.Visible = !show;
			Animation.Visible = !show;
			//ProgressInfo_PB.Visible = !show;
			//CancelBkJob_BT.Visible = !show;
			ProgressMsg_LB.Top = (Progress_Panel.Height - ProgressMsg_LB.Height) / 2;
			ProgressMsg_LB.Text = Resources.Messages.NO_RESULTS.ToUpper();
#if TRACE
			PgMng.Record("EMngBaseForm::ShowNoResultsPanel - INI");
#endif
		}

        #endregion

		#region Source

        /// <summary>Refresca el origen de datos del formulario
        /// <returns>void</returns>
        /// </summary>
        public void ReloadData() { RefreshList(); }

		/// <summary>Refresca la lista despues de realizar alguna operación
		/// <returns>void</returns>
		/// </summary>
		protected void RefreshList()
		{
			try
			{
				_back_job = BackJob.RefreshList;
				//Parche porque no muestra la barra bien al inicio
				if (PgMng != null) PgMng.Grow(string.Empty, "EMngBaseForm::RefreshList INI");
				if (PgMng != null) PgMng.StartBackJob(this);
				RefreshSources();
#if TRACE
				PgMng.Record("EMngBaseForm::RefreshList END");
#endif
			}
			catch (Exception ex)
			{
				if (PgMng != null)
				{
					PgMng.FillUp();
					PgMng.ShowErrorException(ex);
				}
				else
					ProgressInfoMng.ShowException(ex);
			}
		}

		public void RefreshFormsData()
		{
			PgMng.Reset(BarSteps, 1, Resources.Messages.REFRESHING_LIST, this);
#if TRACE
            PgMng.Record("EMngBaseForm::RefreshFormsData INI");
#endif			
			RefreshList();
			PgMng.FillUp();
#if TRACE
            PgMng.Record("EMngBaseForm::RefreshFormsData END");
#endif
		}

        protected virtual void SetMainList(object list) { SetMainList(list, true); }
        protected virtual void SetMainList(object list, bool order) { throw new iQImplementationException("SetMainList"); }

		protected void SetMainList(object list, string sortProperty, ListSortDirection sortDirection, bool order)
		{
#if TRACE
			PgMng.Record("EMngBaseForm::SetMainList INI");
#endif
			Datos.DataSource = list;
#if TRACE
			PgMng.Record("EMngBaseForm::SetMainList::Datos.DataSource = list");
#endif
			if (order)
				OrderByColumn(ControlsMng.GetColumn(TablaBase, sortProperty), sortDirection, true);
#if TRACE
			PgMng.Record("EMngBaseForm::SetMainList END");
#endif
		}

        /// <summary>
        /// Actualiza la lista de objetos
        /// </summary>
        public virtual void UpdateList() { throw new iQImplementationException("UpdateList"); }

		/// <summary>Selecciona un elemento de la tabla
		/// </summary>
		/// <param name="oid">Identificar del elemento</param>
		protected virtual void Select(long oid) { throw new iQImplementationException("Select"); }

        #endregion

        #region Business Methods

		protected virtual Type GetColumnType(string column_name)
		{
			return TablaBase.Columns[column_name] != null ? TablaBase.Columns[column_name].ValueType : null;
		}

		protected virtual string GetColumnProperty(string column_name)
		{
			return TablaBase.Columns[column_name] != null ? TablaBase.Columns[column_name].DataPropertyName : null;
		}

        protected FCriteria GetCriteria(string column_name, object value, object secondValue, Operation operation)
        {
            try
            {
                switch (GetColumnType(column_name).ToString())
                {
                    case "System.DateTime":
                        {
                            if (Convert.ToString(secondValue) == string.Empty)
                                return new FCriteria<DateTime>(GetColumnProperty(column_name), Convert.ToDateTime(value), operation);
                            else
                                return new FCriteria<DateTime>(GetColumnProperty(column_name), Convert.ToDateTime(value), Convert.ToDateTime(secondValue), operation);
                        }

                    case "System.String":
                        return new FCriteria<String>(GetColumnProperty(column_name), Convert.ToString(value), operation);

                    case "System.Int32":
                        {
                            if (Convert.ToString(secondValue) == string.Empty)
                                return new FCriteria<int>(GetColumnProperty(column_name), Convert.ToInt32(value), operation);
                            else
                                return new FCriteria<int>(GetColumnProperty(column_name), Convert.ToInt32(value), Convert.ToInt32(secondValue), operation);
                        }

                    case "System.Int64":
                        {
                            if (Convert.ToString(secondValue) == string.Empty)
                                return new FCriteria<long>(GetColumnProperty(column_name), Convert.ToInt64(value), operation);
                            else
                                return new FCriteria<long>(GetColumnProperty(column_name), Convert.ToInt64(value), Convert.ToInt64(secondValue), operation);
                        }

                    case "System.Decimal":
                        {
                            if (Convert.ToString(secondValue) == string.Empty)
                                return new FCriteria<Decimal>(GetColumnProperty(column_name), Convert.ToDecimal(value), operation);
                            else
                                return new FCriteria<Decimal>(GetColumnProperty(column_name), Convert.ToDecimal(value), Convert.ToDecimal(secondValue), operation);
                        }

                    case "System.Double":
                        {
                            if (Convert.ToString(secondValue) == string.Empty)
                                return new FCriteria<double>(GetColumnProperty(column_name), Convert.ToDouble(value), operation);
                            else
                                return new FCriteria<double>(GetColumnProperty(column_name), Convert.ToDouble(value), Convert.ToDouble(secondValue), operation);
                        }

                    case "System.Boolean":
                        return new FCriteria<bool>(GetColumnProperty(column_name), Convert.ToBoolean(value), operation);

                    default:
                        return new FCriteria<String>(GetColumnProperty(column_name), Convert.ToString(value), operation);
                }
            }
            catch
            {
                return new FCriteria<String>(GetColumnProperty(column_name), Convert.ToString(value), operation);
            }
        }

        #endregion

		#region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (action)
			{
                case molAction.Add:
                        OpenAddForm();
                    break;

				case molAction.ChangeStateContabilizado:

					if (this.Datos.Count == 0) return;
					ChangeStateAction(EEstadoItem.Contabilizado);

					break;

				case molAction.ChangeStateAnulado:
					
					if (this.Datos.Count == 0) return;
                    
                    if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
                        return;

					ChangeStateAction(EEstadoItem.Anulado);
					
					break;

				case molAction.ChangeStateEmitido:

					if (this.Datos.Count == 0) return;
					ChangeStateAction(EEstadoItem.Emitido);
					
					break;

				case molAction.Close:

					NeedsRefreshList = false;
					_action_result = DialogResult.Cancel;
					Cerrar();
					break;

				case molAction.Copy:

					if (this.Datos.Count == 0) return;
					CopyObjectAction(ActiveOID);
					
					break;

				case molAction.CustomAction1:

					if (this.Datos.Count == 0) return;
					CustomAction1();

					break;

				case molAction.CustomAction2:

					if (this.Datos.Count == 0) return;
					CustomAction2();
					break;

				case molAction.CustomAction3:

					if (this.Datos.Count == 0) return;
					CustomAction3();

					break;

				case molAction.CustomAction4:

					if (this.Datos.Count == 0) return;
					CustomAction4();

					break;

				case molAction.Default:

					Status = EStatus.OK;

					NeedsOrderList = false;
					NeedsRefreshList = false;

					DefaultAction();

					break;

				case molAction.Delete:

					NeedsOrderList = false;

					if (this.Datos.Count == 0) return;

					if (ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
					{
						try
						{
							DeleteObject(ActiveOID);
						}
						catch
						{
							PgMng.Reset(3, 1, Face.Resources.Messages.DELETING, this);

							long oid = ActiveOID;

							DeleteAction();
							PgMng.Grow(string.Empty, "EMngBaseForm::ExecuteAction::Delete");

							//Se eliminan todos los formularios de ese objeto
							foreach (EntityDriverForm form in _list_active_form)
							{
								if (form is ItemMngBaseForm)
								{
									if (((ItemMngBaseForm)form).Oid == oid)
									{
										form.Dispose();
										break;
									}
								}
							}

							PgMng.FillUp();
						}
					}
					else
						_action_result = DialogResult.Ignore;

					break;

				case molAction.Edit:

					if (this.Datos.Count == 0) return;
					OpenEditForm();
#if TRACE
					PgMng.Record(String.Format("EMngBaseForm::ExecuteAction {0} INI", action.ToString()));
#endif
					break;

				case molAction.EmailLink:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
					EmailLinkAction();

					break;

				case molAction.EmailPDF:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;					
					EmailPDFAction();

					break;

				case molAction.ExportPDF:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
					ExportPDFAction();
					
					break;

				case molAction.FilterOn:

					NeedsOrderList = false;
					
					if (this.Datos.Count == 0) return;
					FilterValueAction();

					break;

				case molAction.FilterGlobal:
						
					NeedsOrderList = false;
					FilterGlobalAction();
					
					break;

				case molAction.FilterAll:

					NeedsOrderList = false;
					FilterAllAction();
					
					break;

				case molAction.FilterOff:

					FilterOffAction();
					break;

                case molAction.Lock:

					if (this.Datos.Count == 0) return;
					LockAction();
                    
                    break;

                case molAction.Print:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
                    PrintList();
                    
                    break;

                case molAction.PrintDetail:
						
					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
					PrintDetailAction();
                    
                    break;

				case molAction.PrintListQR:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
					PrintQRAction();
					
					break;

				case molAction.Refresh:

					PgMng.Reset(4, 1, Resources.Messages.LOADING_DATA, this);

					NeedsOrderList = true;
					NeedsRefreshList = true;

					RefreshAction();
					break;

                case molAction.Select:

					NeedsOrderList = false;
					NeedsRefreshList = false;

                    if (this.Datos.Count > 0)
                    {
                        _action_result = DialogResult.OK;
                        SelectObject();                            
                    }
					else
						_action_result = DialogResult.Cancel;

					DialogResult = _action_result;
                    break;

                case molAction.SelectAll:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
                    
                    _action_result = DialogResult.OK;
                    SelectAll();
					DialogResult = _action_result;
                                
                    break;

				case molAction.ShowDocuments:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;
					ShowDocumentsAction();

					break;

				case molAction.Unlock:

					if (this.Datos.Count == 0) return;
					UnlockAction();
					
					break;

				case molAction.View:

					NeedsOrderList = false;
					NeedsRefreshList = false;

					if (this.Datos.Count == 0) return;					
					OpenViewForm();
					
					break;

				default:
					base.DoExecuteAction(action);
					break;
			}
		}

        public override void ExecuteAction(molAction action, bool nested)
        {
            try
            {
#if TRACE		
				PgMng.Record(String.Format("EMngBaseForm::ExecuteAction {0} INI", action.ToString()));
#endif
				if (!nested)
				{
					if (Status == EStatus.Working) return;

					Status = EStatus.Working;

					_current_action = action;

					_action_result = DialogResult.Ignore;

					NeedsRefreshList = true;
					NeedsOrderList = true;
				}

				DoExecuteAction(action);

				if ((_action_result == DialogResult.OK) && NeedsRefreshList)
				{
#if TRACE
					PgMng.Record("EMngBaseForm::UpdateList INI");
#endif
					//Cada acción decide si se ordena o no
					UpdateList();
#if TRACE
					PgMng.Record("EMngBaseForm::UpdateList END");
#endif
				}
#if TRACE
				PgMng.Record(String.Format("EMngBaseForm::ExecuteAction {0} END", action.ToString()));
#endif				
            }
			catch (iQLockException)
			{
				PgMng.ShowInfoException(Resources.Messages.LOCK_ERROR);
			}
			catch (System.Security.SecurityException ex)
			{
				PgMng.ShowErrorException(ex.Message);
			}
            catch (Exception ex)
            {
				PgMng.ShowErrorException(ex);
            }
            finally
            {
				Status = EStatus.OK;
				PgMng.FillUp();
			}
        }

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Edit); }

		/// <summary>Abre el formulario para añadir item
		/// <returns>void</returns>
		/// </summary>
		public virtual void OpenAddForm() { throw new iQImplementationException("OpenAddForm"); }

		/// <summary>Abre el formulario en modo lectura
		/// <returns>void</returns>
		/// </summary>
		public virtual void OpenViewForm() { throw new iQImplementationException("OpenViewForm"); }

		/// <summary>Abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void OpenEditForm() { throw new iQImplementationException("OpenEditForm"); }

		/// <summary>Duplica un objeto y abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void CopyObjectAction(long oid) { throw new iQImplementationException("CopyObjectAction"); }

		/// <summary>DEPRECATED. Borrar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void DeleteObject(long oid) { throw new iQImplementationException("DeleteObject"); }

		/// <summary>Borrar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void DeleteAction() { throw new iQImplementationException("DeleteAction"); }

		/// <summary>
		/// Cambia el estado de un registro
		/// </summary>
		public virtual void ChangeStateAction(EEstadoItem estado) { throw new iQImplementationException("ChangeStateAction"); }

        /// <summary>
        /// Bloquea un registro
        /// </summary>
        public virtual void LockAction() { throw new iQImplementationException("LockAction"); }

		/// <summary>
		/// Refresca la lista
		/// </summary>
		public virtual void RefreshAction() { throw new iQImplementationException("RefreshAction"); }

        /// <summary>
        /// Desbloquea un registro
        /// </summary>
        public virtual void UnlockAction() { throw new iQImplementationException("UnlockAction"); }

		/// <summary>Duplica un objeto y abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void DuplicateObject(long oid) { throw new iQImplementationException("DEPRECATED::DuplicateObject"); }

		/// <summary>Abre el formulario para encontrar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void OpenLocalizeForm() { throw new iQImplementationException("OpenLocalizeForm"); }

		public virtual void CustomAction1() { throw new iQImplementationException("CustomAction1"); }
		public virtual void CustomAction2() { throw new iQImplementationException("CustomAction2"); }
		public virtual void CustomAction3() { throw new iQImplementationException("CustomAction3"); }
		public virtual void CustomAction4() { throw new iQImplementationException("CustomAction4"); }

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public virtual void PrintList() { throw new iQImplementationException("PrintList"); }

        /// <summary>Imprime el detalle del objeto seleccionado
        /// <returns>void</returns>
        /// </summary>
        public virtual void PrintDetailAction() { throw new iQImplementationException("PrintDetailAction"); }

		/// <summary>Imprime el listado con QR Codes
		/// <returns>void</returns>
		/// </summary>
		public virtual void PrintQRAction() { throw new iQImplementationException("PrintQRAction"); }

		/// <summary>
		/// Exporta a PDF el objeto seleccionado
		/// </summary>
		public virtual void ExportPDFAction() { throw new iQImplementationException("ExportPDFAction"); }

		/// <summary>
		/// Envía por email el fichero pdf del objeto seleccionado
		/// </summary>
		public virtual void EmailPDFAction() { throw new iQImplementationException("EmailPDFAction"); }

		/// <summary>
		/// Envía por email el link del objeto seleccionado
		/// </summary>
		public virtual void EmailLinkAction() { throw new iQImplementationException("EmailLinkAction"); }

        public virtual void FilterValueAction()
        {
			_filter_property = IFilterProperty.ByParamenter;
			//_operation = Operation.Equal;
			FilterItems(_search_value, _second_search_value);
        }

		/// <summary>
		/// Filtra por todos los campos visibles en el grid
		/// </summary>
		public virtual void FilterGlobalAction()
		{
#if TRACE
			AppControllerBase.AppControler.Timer.Record("EMngBaseForm::FilterGlobalAction - INI");
#endif
			_filter_property = IFilterProperty.All;
			_operation = Operation.Contains;

			try
			{
				FilterItem fItem = new FilterItem();
				fItem.Value = _filter_keys;
				fItem.Operation = Operation.Contains;
				fItem.FilterProperty = IFilterProperty.All;

				if (DoFilter(fItem))
				{
					SetFilter(true);
					_filter_type = IFilterType.Filter;
				}
#if TRACE
				AppControllerBase.AppControler.Timer.Record("EMngBaseForm::FilterGlobalAction - INI");
#endif
			}
			catch (SystemException ex)
			{
				PgMng.ShowInfoException(ex);
			}
			finally
			{
				ShowNoResultsPanel(DatosSearch.Count == 0);
			}
		}

		/// <summary>
		/// Aplica todos los filtros acumulados
		/// </summary>
		public virtual void FilterAllAction()
		{
			try
			{
#if TRACE
				PgMng.Record("EMngBaseForm::FilterAllAction INI");
#endif
				_filter_property = IFilterProperty.ByList;

				List<FilterItem> filterListCopy = new List<FilterItem>();

				foreach (FilterItem item in FilterList)
					filterListCopy.Add(item);

				FilterOffAction();

				FilterList = filterListCopy;
				foreach (FilterItem fItem in FilterList)
				{
					if (fItem.Active)
					{
						_operation = fItem.Operation;

						if (DoFilter(fItem))
						{
							SetFilter(true);
							_filter_type = IFilterType.Filter;
						}
					}
					else
						AddFilterItem(fItem);
				}

				if (_filter_type == IFilterType.None)
					SetFilter(false);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				ShowNoResultsPanel(DatosSearch.Count == 0);
#if TRACE
				PgMng.Record("EMngBaseForm::FilterAllAction END");
#endif
			}
		}

		public virtual void FilterOffAction()
		{
#if TRACE
			PgMng.Record("EMngBaseForm::FilterOffAction INI");
#endif
			_filter_type = IFilterType.None;
			_filter_keys = string.Empty;
			SetFilter(false);
			FilterList.Clear();
			DatosSearch.DataSource = Datos.DataSource;

			ShowNoResultsPanel(DatosSearch.Count == 0);
#if TRACE
			PgMng.Record("EMngBaseForm::FilterOffAction END");
#endif
		}

        /// <summary>Selecciona el registro activo
        /// <returns>void</returns>
        /// </summary>
        public virtual void SelectObject() 
        { 
            _selected = Datos.Current;
        }

        /// <summary>Selecciona el registro activo
        /// <returns>void</returns>
        /// </summary>
        public virtual void SelectAll()
        {
            _selected = Datos.DataSource;
        }

		public virtual void ShowDocumentsAction() { throw new iQImplementationException("ShowDocumentsAction"); }

		public virtual void CloseAction() { ExecuteAction(molAction.Close); }

		/// <summary>Cierra el formulario
		/// <returns>void</returns>
		/// </summary>
		public virtual void Cerrar()
		{
			if (_list_active_form.Count == 1)
			{
                foreach (EntityDriverForm form in _list_active_form)
				{
					if (form is LocalizeForm)
					{
						form.CloseForm -= new EventHandler(CloseForm);
						form.Close(); 
						this.Close();
					}
					else
					{
						if (DialogResult.Yes == ProgressInfoMng.ShowQuestion(Resources.Messages.CURRENT_EDITION_CLOSE))
							this.Close();
					}
				}
			}
			else
			{
				if (_list_active_form.Count <= 0)
					this.Close();
				else
				{
					if (DialogResult.Yes == ProgressInfoMng.ShowQuestion(Resources.Messages.CURRENT_EDITION_CLOSE))
						this.Close();
				}
			}
		}

		#endregion

        #region Find & Filter

        /// <summary>
        /// Lanza un evento al realizar una búsqueda
        /// </summary>
        protected void FindItems(object value)
        {
            if (value.ToString() == string.Empty)
            {
                PgMng.ShowInfoException(Resources.Messages.NO_SELECTED);
            }
            else
            {
                try
                {
                    if (DoFind(value))
                    {
                        Select(ActiveFoundOID);
                        _filter_type = IFilterType.Search;
                    }
                }
                catch (SystemException ex)
                {
					MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));
                }
            }
        }

        /// <summary>
        /// Lanza un evento al realizar un filtrado
        /// </summary>
        protected void FilterItems(object value, object secondValue = null)
        {
            if (value.ToString() == string.Empty)
            {
				FilterOffAction();
				return;
            }
            else
            {
				try
				{
					if (DoFilter(value, secondValue))
					{
						SetFilter(true);
						_filter_type = IFilterType.Filter;
					}
				}
				catch (SystemException ex)
				{
					PgMng.ShowErrorException(ex);
				}
				finally
				{
					ShowNoResultsPanel(DatosSearch.Count == 0);
				}
            }
        }

        /// <summary>
        /// Lanza un evento al realizar un filtrado
        /// </summary>
        protected void FilterByFirst(string value)
        {
            _operation = Operation.StartsWith;
            FilterItems(value);
        }

        protected virtual void FilterByKey(string key)
        {
            DataGridViewColumn column = ControlsMng.GetCurrentColumn(TablaBase);

			switch (key)
			{
				case "\b":
					if (_filter_keys.Length >= 1)
						_filter_keys = (_active_column != column) ? string.Empty : _filter_keys.Substring(0, _filter_keys.Length - 1);
					else
						_filter_keys = string.Empty;
					break;

				default:
					_filter_keys = (_active_column != column) ? string.Empty : _filter_keys + key;
					break;
			}

            _active_column = column;

            if (column.ValueType != null)
            {
                if (column.ValueType.Name == "Int32") return;
                if (column.ValueType.Name == "Int64") return;
                if (column.ValueType.Name == "Single") return;
                if (column.ValueType.Name == "Double") return;

                int numItems = Datos.Count;

				FilterList.Clear();
                _operation = Operation.Contains;
                FilterItems(_filter_keys);
            }
        }

        /// <summary>
        /// Implementan la lógica de búsqueda o filtrado
        /// </summary>
        /// <param name="list">lista de resultados</param>
        /// <returns></returns>
        /// <remarks>Es necesario reemplazarla en cada clase heredera</remarks>
        protected IList Localize(object value, string property) { throw new iQImplementationException("Localize"); }
		protected virtual IList Localize(FilterItem fItem) { throw new iQImplementationException("Localize"); }
        protected IList LocalizeByFirst(string value, string property) { throw new iQImplementationException("LocalizeByFirst"); }

        protected virtual bool DoFind(object value) { throw new iQImplementationException("DoFind"); }
        protected virtual bool DoFilter(object value, object secondValue = null) { throw new iQImplementationException("DoFilter"); }
		protected virtual bool DoFilter(FilterItem fItem) { throw new iQImplementationException("DoFilter"); }

        protected virtual bool DoFilterByFirst(string value, string column_name) { throw new iQImplementationException("DoFilterByFirst"); }
        
		/// <summary>
        /// Filtra por una propiedad y valor determinados 
        /// </summary>
        /// <param name="value">Valor de filtrado</param>
        /// <param name="field">Propiedad de búsqueda</param>
        /// <returns></returns>
		protected virtual bool DoFilterByProperty(object value, object secondValue, string column_name)
		{
			FilterItem fItem = BuildFilterItem(value, secondValue, column_name, typeof(string));
			_filter_property = IFilterProperty.ByParamenter;
			
			return DoFilter(fItem);
		}

		protected FilterItem BuildFilterItem(object value, object secondValue, string column_name, Type type)
		{
			FilterItem fItem = new FilterItem();
			fItem.Column = column_name;
			fItem.FilterProperty = _filter_property;
			fItem.Operation = _operation;

            if (type == typeof(DateTime))
            {
                if (fItem.Operation == Operation.Contains) fItem.Operation = Operation.Equal;

                switch (fItem.Operation)
                {
                    case Operation.Equal:
                    case Operation.Distinct:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 0:00:00";
                        break;

                    case Operation.Greater:
                    case Operation.LessOrEqual:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 23:59:59";
                        break;

                    case Operation.Less:
                    case Operation.GreaterOrEqual:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 0:00:00";
                        break;

                    case Operation.Between:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 0:00:00";
                        fItem.SecondValue = ((DateTime)secondValue).ToShortDateString() + " 23:59:59";
                        break;
                }
            }
            else
            {
                fItem.Value = value;
                fItem.SecondValue = secondValue;
            }

			return fItem;
		}

		// DEPRECATED
        /*protected virtual void AddFilterLabel(string column_name, object value) { AddFilterItem(column_name, value); }
		protected virtual void AddFilterLabel(FilterItem fItem) { AddFilterItem(fItem); }*/
		
		protected virtual void AddFilterItem(string column_name, object value)
		{
			FilterItem fItem = new FilterItem();
			fItem.Column = column_name;
			fItem.Value = value;
			AddFilterItem(fItem);
		}
		protected virtual void AddFilterItem(FilterItem fItem)
		{
			try
			{
				if (_current_action == molAction.FilterGlobal)
				{
					FilterList.Clear();
				}

				switch (_filter_property)
				{
					case IFilterProperty.All:
						{
							FilterItem new_filter = new FilterItem();

							new_filter.FilterProperty = _filter_property;
							new_filter.Operation = _operation;
							new_filter.Column = Resources.Labels.ANY_FIELD;
							new_filter.ColumnTitle = Resources.Labels.ANY_FIELD;
							new_filter.Property = Resources.Labels.ANY_FIELD;
							new_filter.Value = fItem.Value;
                            new_filter.SecondValue = fItem.SecondValue;
							new_filter.Active = true;

							FilterList.Add(new_filter);
						}
						break;

					case IFilterProperty.ByParamenter:
						{
							FilterItem new_filter = new FilterItem();

							new_filter.FilterProperty = _filter_property;
							new_filter.Operation = _operation;
							new_filter.Column = fItem.Column;
							new_filter.ColumnTitle = TablaBase.Columns[fItem.Column].HeaderText;
							new_filter.Property = TablaBase.Columns[fItem.Column].DataPropertyName;
							new_filter.Value = fItem.Value;
                            new_filter.SecondValue = fItem.SecondValue;
							new_filter.Active = true;

							FilterList.Add(new_filter);
						}
						break;

					case IFilterProperty.ByList:
						break;
				}
			}
			catch { }

		}

		protected void ReplaceFilterItem(string column_name, object value)
		{
			FilterList.Clear();
			AddFilterItem(column_name, value);
		}
		
		protected virtual void SetFilter(bool on)
		{
			try
			{
				SetMainList(on ? _filter_results : _sorted_list, SortProperty, SortDirection, true);
			}
			catch (Exception)
			{
				SetMainList(_sorted_list, SortProperty, SortDirection, true);
			}
		}

        #endregion

		#region WinForm handling

		/// <summary>
		/// Añade un nuevo formulario a la lista de formularios DriverEntityForm 
		/// activos y lo muestra.
		/// </summary>
		/// <param name="part">El formulario a añadir y mostrar.</param>
        public void AddForm(EntityDriverForm part)
		{
			try
			{
				Focus();
				part.CloseForm += new EventHandler(CloseForm);

				//Para futuros formularios modales
				if (part.IsModal)
				{
					part.ShowDialog(this);
					_action_result = part.ActionResult;
				}
				else
				{
					if (part is ItemMngBaseForm)
					{
						foreach (EntityDriverForm active_form in _list_active_form)
						{
							if (active_form is ItemMngBaseForm)
							{
								if ((((ItemMngBaseForm)active_form).MFType == ((ItemMngBaseForm)part).MFType)
									&& ((((ItemMngBaseForm)active_form).Oid == ((ItemMngBaseForm)part).Oid)
									|| (((ItemMngBaseForm)active_form).MFType == ManagerFormType.MFAdd)))
								{
									ShowForm(active_form);
									_action_result = active_form.ActionResult;
									return;
								}
							}
						}
					}
					else
					{
						foreach (EntityDriverForm active_form in _list_active_form)
						{
							if (active_form is LocalizeForm)
							{
								ShowForm(active_form);
								_action_result = active_form.ActionResult;
								return;
							}
						}
					}

					ShowForm(part);
					_action_result = part.ActionResult;

					//Si se desea mantener una lista de formularios activos...

					_list_active_form.Add(part);

					//Cierra los LocalizeForm que pudieran estar abiertos y no pertenecen al MngForm actual
					//para evitar que aparezcan varios a la vez y comiencen a intercambiarse al seleccionarlos
					foreach (Form form in MainBaseForm.Instance.MdiChildren)
					{
						if (form is LocalizeForm && !_list_active_form.Contains((EntityDriverForm)form))
							form.Close();
					}
				}
			}
			catch (Exception ex)
			{
				part.DisposeForm();
				throw ex;
			}
		}

		/// <summary>
		/// Muestra el formulario especificado por parámetro
		/// y lo trae al frente.
		/// </summary>
		/// <param name="part">The WinPart control to display.</param>
        protected void ShowForm(EntityDriverForm part)
		{
			part.Show();
            part.BringToFront();
        }

		/// <summary>
		/// Evento que surge al cerrar uno de los formularios EntityDriverForm
		/// activo. Cierra este formulario, lo elimina de la lista de formularios activos
		/// y elimina el OID al que referencia de la lista de OID´s activos.
		/// Finalmente refresca la tabla para que los cambios surtan efecto.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void CloseForm(object sender, EventArgs e)
		{
            EntityDriverForm part = (EntityDriverForm)sender;

            //Para saber si debemos o no refrescar las listas
            _action_result = part.DialogResult;
			
			part.CloseForm -= new EventHandler(CloseForm);

			_list_active_form.Remove(part);

			//Para formularios modales
			/*if (part.IsModal)
				part.CloseForm -= new EventHandler(CloseForm);
			//Formulario de búsqueda, adición y edicion
			else
			{
				//Formulario de búsqueda: se cierra directamente
				if (part is LocalizeForm)
				{
					part.CloseForm -= new EventHandler(CloseForm);
					return;
				}
			}*/
		}

		/// <summary>
		/// Función que elimina un DriverEntity de memoria para que vuelva a ser cargado
		/// con datos nuevos. Se hace referencia a él por medio de un determinado OID.
		/// </summary>
		/// <param name="oid"></param>
		protected void CloseFormById(long form_id)
		{
            foreach (EntityDriverForm part in _list_active_form)
			{
				if (part.FormId == form_id)
				{
					_list_active_form.Remove(part);
					break;
				}
			}

            RefreshFormsData();
		}

		#endregion

        #region Events

		private void EntityMngBaseForm_Layout(object sender, LayoutEventArgs e)
		{
			//SetGridFormat();
		}

        /// <summary>
        /// Evento que al cerrar la interfaz, cierra todos los formularios
        /// hijos que estén abiertos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityMngBaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (EntityDriverForm form in _list_active_form)
            {
                form.CloseForm -= new EventHandler(CloseForm);
                form.Close();
            }
        }

        private void EntityMngBaseForm_Shown(object sender, EventArgs e)
        {
			if (_active_column != null) ControlsMng.SetCurrentCell(TablaBase, _active_column.Index);
            if (TablaBase != null) TablaBase.Focus();
        }

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
			if (TablaBase == null) return;
            TablaBase.Focus();
        }

        private void DatosSearch_CurrentChanged(object sender, EventArgs e)
        {
            Select(ActiveFoundOID);
        }

		/*private void TablaBase_DoubleClick(object sender, EventArgs e)
		{
			ExecuteAction(molAction.Default);
		}

		private void TablaBase_KeyPress(object sender, KeyPressEventArgs e)
		{
			FilterByKey(e.KeyChar.ToString());
		}

		private void TablaBase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			SetColumnActive();
		}

		private void TablaBase_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SetColumnActive();
		}

		private void TablaBase_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(TablaBase.Rows[e.RowIndex]);
		}*/

        #endregion
    }
}