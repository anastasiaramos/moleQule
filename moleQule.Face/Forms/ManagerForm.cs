using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base para Formularios de Consulta, Edición y Borrado de Elementos de una Entidad
    /// </summary>
    public partial class ManagerForm : moleQule.Face.EntityDriverForm, IBackGroundLauncher
    {
        #region Attributes

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected ManagerFormType _mf_type;

        protected bool _refresh_children = true;
        private bool _cancel_confirmation = true;
        protected bool _disposing = false;

		object[] _params = null;

        #endregion

        #region Properties

        /// <summary>
        /// Propiedad que indica el tipo del formulario hijo
        /// Deben inicializarlo los AddForm, ViewForm y EditForm
        /// </summary>
        public bool CancelConfirmation { get { return _cancel_confirmation; } set { _cancel_confirmation = value; } }

        /// <summary>
        /// Indica si hay que actualizar los formularios clientes en caso de que se modifiquen datos 
        /// de este formulario
        /// </summary>
        public bool RefreshChildren { get { return _refresh_children; } set { _refresh_children = value; } }

        /// <summary>
        /// Propiedad que indica el tipo del formulario hijo
        /// Deben inicializarlo los AddForm, ViewForm y EditForm
        /// </summary>
        public ManagerFormType MFType { get { return _mf_type; } }

        /// <summary>
        /// Abre un formulario para seleccionar un registro (SelectForm) y 
        /// asigna los valores al control
        /// </summary>
        /// <param name="entity">Tipo de entidad que se quiere obtener</param>
        /// <param name="ctl">Control al que asignar la selección</param>
        protected virtual void GetItemFromList(long entity, Control ctl) { }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor para formularios de insercion (AddForms)
        /// No se le especifica Oid asociado al formulario
        /// </summary>
        public ManagerForm() 
			: this(false, null) {}

        public ManagerForm(bool is_modal, Form parent)
            : base(is_modal, parent)
        {
            InitializeComponent();
        }

		public ManagerForm(bool is_modal, Form parent, object[] parameters)
			: base(is_modal, parent)
		{
			InitializeComponent();

			_params = parameters;
		}

        /// <summary>
        /// Obtiene los datos del formulario desde el objeto asociado.
        /// Para formularios ADD
        /// Funcion virtual pura, todos los hijos deben declararla.
        /// </summary>
        protected virtual void GetFormSourceData() { throw new iQImplementationException("GetFormSourceData()"); }
		protected virtual void GetFormSourceData(object[] parameteres) { throw new iQImplementationException("GetFormSourceData(object[] parameteres)"); }

        /// <summary>
        /// Obtiene los datos del formulario desde el objeto asociado.
        /// Para formularios UI
        /// Funcion virtual pura, todos los hijos deben declararla.
        /// </summary>
        protected virtual void GetFormSourceData(long oid) { throw new iQImplementationException("GetFormSourceData(long oid)"); }
		protected virtual void GetFormSourceData(long oid, object[] parameteres) { throw new iQImplementationException("GetFormSourceData(long oid, object[] parameteres)"); }

		protected void GetFormData(long oid, object[] parameters)
        {
            //PARCHE: Codigo para que no pete en tiempo de diseño
            if (PgMng == null) return;

			try
			{
#if TRACE
                if (PgMng != null) PgMng.Record("ManagerForm::GetFormSourceData INI");
#endif
				if (oid == -1)
					if (parameters == null) GetFormSourceData();
					else GetFormSourceData(parameters);
				else
					if (parameters == null) GetFormSourceData(oid);
					else GetFormSourceData(oid, parameters);
#if TRACE
				if (PgMng != null) PgMng.Record("ManagerForm::GetFormSourceData END");
#endif
			}
			catch (Exception ex)
			{
				_disposing = true;
				throw ex;
			}
        }

        /// <summary>
        /// Guarda el objeto y sus entidades dependientes
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaveObject() { return false; }

        /// <summary>
        /// Guarda el objeto y sus entidades dependientes.
        /// Version para ejecutar en segundo plano.
        /// </summary>
        /// <returns></returns>
        protected virtual void BkSaveObject(ProgressInfoMng bar) { return; }

        #endregion

        #region IBackGroundLauncher

        protected new enum BackJob { GetFormData }
        protected new BackJob _back_job = BackJob.GetFormData;

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
                    case BackJob.GetFormData:
                        BkGetFormData(bk);
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

        protected void BkGetFormData(BackgroundWorker bk)
        {
            try
            {
                EnableEvents(false);
#if TRACE
				PgMng.Record("ManagerForm::RefreshSecondaryData INI");
#endif
                RefreshSecondaryData();
                PgMng.Grow(string.Empty, "ManagerForm::RefreshSecondaryData END");
#if TRACE				
				PgMng.Record("ManagerForm::RefreshMainData INI");
#endif
                RefreshMainData();
                PgMng.Grow(string.Empty, "ManagerForm::RefreshMainData END");
#if TRACE				
				PgMng.Record("ManagerForm::BuildCache INI");
#endif
                BuildCache();
                PgMng.Grow();
#if TRACE				
				PgMng.Record("ManagerForm::BuildCache END");
#endif
            }
            /*catch (System.Security.SecurityException ex)
            {
				PgMng.ShowInfoException(ex);
			}*/
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
            }
            finally
            {
                EnableEvents(true);
            }
        }

        #endregion

        #region Layout

        protected override void FormatControl(Control control)
		{
            if ((this.Tag != null) && (this.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) return;

			base.FormatControl(control);

			Control ctl = control;

			switch (ctl.GetType().Name)
            {
                case "Label":
                    ctl.Enabled = true;
                break;

                case "TextBox":
                case "NumericTextBox":
                    if (!((TextBox)ctl).Enabled)
                    {
                        ctl.Enabled = true;
                        ctl.ForeColor = Color.Black;
                    }
                break;

                case "RichTextBox":
                    if (!((RichTextBox)ctl).Enabled)
                    {
                        ctl.Enabled = true;
                        ctl.ForeColor = Color.Black;
                    }
                break;

                case "DateTimePicker":
                case "mQDateTimePicker":
                    if (!((DateTimePicker)ctl).Enabled)
                        ((DateTimePicker)ctl).CalendarForeColor = Color.Black;
                break;
                    
                case "DataGridView":
                    if (!((DataGridView)ctl).ReadOnly)
                        ((DataGridView)ctl).DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 192);
                    else
                        ((DataGridView)ctl).DefaultCellStyle.ForeColor = Color.Black;
                break;
            }
        }

		protected void SetGridFormat(DataGridView grid)
		{
			foreach (DataGridViewRow row in grid.Rows)
				SetRowFormat(row);
		}
        
		protected virtual void SetRowFormat(DataGridViewRow row) {}

		/// <summary>
        /// Elimina Posibilidad de Edición en el Formulario.
        /// Se utiliza en los ViewForm.
        /// </summary>
        /// <param name="controls"></param>
        protected virtual void SetReadOnlyControls(Control.ControlCollection controls)
        {
            foreach (Control ctl in controls)
            {
                Type ctlType = ctl.GetType();

                switch (ctl.GetType().Name)
                {
                    case "Button":
                        {
                            ctl.Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    case "TextBox":
                    case "NumericTextBox":
                        {
                            ((TextBox)(ctl)).ReadOnly = true;
                            ctl.TabStop = false;
                        } break;

                    case "RichTextBox":
                        {
                            ((RichTextBox)(ctl)).ReadOnly = true;
                            ctl.TabStop = false;
                        } break;

                    case "ListBox":
                        {
                            ((ListBox)(ctl)).Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    case "CheckBox":
                        {
                            ((CheckBox)(ctl)).Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    case "ComboBox":
                        {
                            ((ComboBox)(ctl)).Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    case "GroupBox":
                        {
                            this.SetReadOnlyControls(((GroupBox)(ctl)).Controls);
                        } break;

                    case "DataGridView":
                        {
                            //((DataGridView)ctl).Enabled = false; //Está comentado para que funcionen los scrolls (Ana)
                            ((DataGridView)ctl).ReadOnly = true;
                            try
                            {
                                ((DataGridView)ctl).AllowUserToAddRows = false;
                                ((DataGridView)ctl).AllowUserToDeleteRows = false;
                            }
                            catch { }
                        } break;

                    case "DateTimePicker":
                        {
                            ((DateTimePicker)ctl).Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    case "TabControl":
                        {
                            foreach (TabPage page in ((TabControl)(ctl)).TabPages)
                                this.SetReadOnlyControls(page.Controls);
                        } break;

                    case "SplitContainer":
                        {
                            this.SetReadOnlyControls(((SplitContainer)ctl).Panel1.Controls);
                            this.SetReadOnlyControls(((SplitContainer)ctl).Panel2.Controls);
                        } break;

                    case "RadioButton":
                        {
                            ctl.Enabled = false;
                            ctl.TabStop = false;
                        } break;

                    default:
                        {
                            ctl.Enabled = false;
                            ctl.TabStop = false;
                        } break;
                }

            }
        }

        #endregion

        #region Source

        /// <summary>
        /// Asigna el objeto al origen de datos y los orígenes de datos
        /// auxiliares a los ComboBox
        /// <returns>void</returns>
        /// </summary>
        protected virtual void SetFormData()
        {
            try
            {
                //Por si se llama despues de un intento fallido de acceder a un registro
                if (_disposing)
                    return;
                
				if (PgMng != null) PgMng.Message = Resources.Messages.LOADING_DATA;

				if (_parent != null)
					BkGetFormData(null);
				else
				{
					_back_job = BackJob.GetFormData;
					if (PgMng != null) PgMng.StartBackJob(this);
				}
            }
            catch (Exception ex)
            {
                if (null != iQExceptionHandler<iQLockException>.GetiQException(ex))
                {
                    PgMng.ShowInfoException(Resources.Messages.LOCK_ERROR);
                }
                else
                {
					PgMng.ShowInfoException(ex);
                }

                _disposing = true;
                Close();
            }
            finally
            {
#if TRACE
				if (PgMng != null) PgMng.Record("ManagerForm::SetFormData()");
#endif
            }
        }
        
        /// <summary>
        /// Asigna los valores del grid que no están asociados a propiedades
        /// </summary>
        protected virtual void SetUnlinkedGridValues(string gridName) { }
		protected virtual void SetUnlinkedGridValues(Control control) { }

        /// <summary>
        /// Asigna los origenes de datos a las celdas que tienen combo
        /// </summary>
        protected virtual void SetCellsDataSource(string gridName) { }
		protected virtual void SetCellsDataSource(Control control) { }

        /// <summary>
        /// Asigna los datos de origen para controles que dependen de otros
        /// </summary>
        /// <param name="controlName"></param>
        protected virtual void SetDependentControlSource(string controlName) {}
		protected virtual void SetDependentControlSource(Control control) {}

        #endregion

        #region Validation & Format

        /// <summary>
        /// Da formato a los datos
        /// </summary>
        protected virtual void FormatData() { }

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected virtual void ValidateInput() { }

        #endregion

        #region Print

        /// <summary>
        /// Imprime el objeto
        /// </summary>
        public virtual void PrintObject() { throw new iQImplementationException("PrintObject"); }

        /// <summary>
        /// Imprime datos secundarios asociados al objeto
        /// </summary>
        /// <param name="entidad">Tipo de entidad. "BaseEntity" struct</param>
        /// <param name="source">Seleccion / Todos</param>
        /// <param name="type">Tipo de impresión</param>
        public virtual void PrintData(long entidad, PrintSource source, PrintType type) { throw new iQImplementationException("PrintData"); }

        #endregion

        #region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (action)
			{
				case molAction.Cancel:

					if (CancelConfirmation)
					{
						if (DialogResult.Yes == ProgressInfoMng.ShowQuestion(Resources.Messages.CANCEL_CONFIRM))
						{
							_action_result = DialogResult.Cancel;
							CancelAction();
						}
						else
							_action_result = DialogResult.Ignore;
					}
					else
					{
						_action_result = DialogResult.Cancel;
						CancelAction();
					}

					break;

				case molAction.Close:

					DialogResult = DialogResult.Cancel;
					Cerrar();
					break;

				case molAction.Print:

					_action_result = DialogResult.Ignore;
					PrintAction();
					break;

				case molAction.Save:

					ErrorMng_EP.Clear();
#if !TRACE
					PgMng.Reset(3, 1, Face.Resources.Messages.SAVING, this);
#endif
					//Se usa un atributo porque si uso el DialogResult el ShowDialog entiende que quiero cerrar el formulario
					_action_result = DialogResult.Ignore;

					SaveAction();

					switch (_action_result)
					{
						case DialogResult.OK:
							_action_result = DialogResult.OK;
							Close();
							break;

						case DialogResult.Cancel:
							_action_result = DialogResult.Cancel;
							Close();
							break;

						case DialogResult.Ignore:
							break;
					}

					break;

				case molAction.ShowDocuments:

					_action_result = DialogResult.Ignore;
					DocumentsAction();
					break;

				default:
					base.DoExecuteAction(action);
					break;
			}
		}	

        protected virtual void SaveAction() { throw new iQImplementationException("SaveAction"); }

        protected virtual void DocumentsAction() { throw new iQImplementationException("DocumentsAction"); }

        #endregion

        #region Events

        private void ManagerForm_Activated(object sender, EventArgs e)
        {
			FormatControls();
			FormatData();
        }

        #endregion
    }
}