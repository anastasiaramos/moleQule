using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos de ese Tipo
	/// </summary>
	public partial class EntityMngForm : moleQule.Face.ChildForm
	{

		#region Attributes & Properties

        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        protected object _selected;

		/// <summary>
		/// Mantiene una lista de formularios de tipo DriverEntityForm
		/// activos 
		/// </summary>
        protected IList<EntityDriverForm> _list_active_form;

        /// <summary>
        /// Nº de pasos para la barra de progreso
        /// </summary>
        protected override int BarSteps { get { return base.BarSteps + 4; } }

		/// <summary>
		/// Devuelve el OID del objeto activo seleccionado de la tabla
		/// </summary>
		/// <returns></returns>
		public virtual long ActiveOID { get { return -1; } }
        
        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        public object Selected { get { return _selected; } }
        
		#endregion

		#region Factory Methods

		public EntityMngForm()
            : this(false) {}

        public EntityMngForm(bool isModal)
            : base(isModal, null)
        {
            InitializeComponent();
            _list_active_form = new List<EntityDriverForm>();
        }

		#endregion

		#region Layout & Source
        
        /// <summary>
        /// Da formato a los controles para la vista de SelectForm
        /// </summary>
        protected virtual void SetSelectView() {}
        
		/// <summary>Refresca la lista despues de realizar alguna operación
		/// <returns>void</returns>
		/// </summary>
		protected void RefreshList()
		{
			using (StatusBusy busy = new StatusBusy(Messages.LOADING_DATA))
			{
				try
				{
                    // Sergio Rosas
                    // Se llama a ApplyFilter porque si estamos en la pestaña
                    // con los elementos filtrados y editamos uno (o creamos uno)
                    // al aceptar o cancelar, la lista que se muestra en la pestaña
                    // de busqueda es la lista entera. Hay que modificar la funcion ApplyFilter
                    // para que llame a RefreshMainData si detecta que esta en la pestaña "Todos"
					//RefreshMainData();
                    ApplyFilter();
                    PgMng.FillUp();
				}
				catch (System.Security.SecurityException ex)
				{
					MessageBox.Show(ex.Message);
				}
				catch (Exception ex)
				{
					string msg = Messages.OPERATION_ERROR + System.Environment.NewLine +
									iQExceptionHandler.GetAllMessages(ex);
					MessageBox.Show(msg);
				}
			}
		}

        public void RefreshChildForm()
        {
            foreach (EntityDriverForm formulario in _list_active_form)
            {
                if (formulario is ManagerForm && ((ManagerForm)formulario).RefreshChildren)
                    ((ChildForm)formulario).RefreshSecondaryData();
            }
        }

		protected ListSortDirection GetGridSortDirection(DataGridView grid)
		{
			ListSortDirection sortOrder = ListSortDirection.Ascending;

			if (grid.SortedColumn != null)
			{
				// El cast cambia ascending por descending
				if (grid.SortOrder == SortOrder.Descending)
					sortOrder = ListSortDirection.Descending;
			}

			return sortOrder;
		}

		protected string GetGridSortProperty(DataGridView grid)
		{
			string sortField = "Codigo";

			if (grid.SortedColumn != null)
			{
				sortField = grid.SortedColumn.Name;
			}

			return sortField;
		}
		
		/// <summary>Selecciona un elemento de la tabla
		/// </summary>
		/// <param name="oid">Identificar del elemento</param>
		protected virtual void Select(long oid) { throw new iQImplementationException("Select"); }

		/// <summary>
		/// Asigna una lista filtrada como source de la tabla
		/// </summary>
		/// <param name="filtro">Lista filtrada</param>
		protected virtual void Filter(object filtro) { throw new iQImplementationException("Filter"); }

		/// <summary>Aplica el filtro seleccionado
		/// <returns>void</returns>
		/// </summary>
		protected virtual void ApplyFilter() { /*throw new iQImplementationException("ApplyFilter");*/ }

		#endregion

		#region Actions

        public override void ExecuteAction(molAction action, bool nested)
        {
            try
            {
#if TRACE				
                Globals.Instance.Timer.Start();
#endif
                switch (action)
                {
                    case molAction.Add:
                        OpenAddForm();
                        break;

                    case molAction.Edit:
                        if (this.Datos.Count > 0)
                            OpenEditForm();
                        break;

                    case molAction.View:
                        if (this.Datos.Count > 0)
                            OpenViewForm();
                        break;

                    case molAction.Copy:
                        if (this.Datos.Count > 0)
                            DuplicateObject(ActiveOID);
                        break;

                    case molAction.Delete:
                        if (this.Datos.Count > 0)
                            DeleteObject(ActiveOID);
                        break;

                    case molAction.Print:
                        if (this.Datos.Count > 0)
                            PrintList();
                        break;

                    case molAction.Select:
                        if (this.Datos.Count > 0)
                            SelectObject();
                        break;

                    case molAction.Close:
                        DialogResult = DialogResult.Cancel;
                        Cerrar();
                        break;
                }
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
         
            }
        }

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { OpenEditForm(); }

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

		/// <summary>Abre el formulario para borrar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void DeleteObject(long oid) { throw new iQImplementationException("DeleteObject"); }

		/// <summary>Duplica un objeto y abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void DuplicateObject(long oid) { throw new iQImplementationException("DuplicateObject"); }

		/// <summary>Abre el formulario para encontrar item
		/// <returns>void</returns>
		/// </summary>
		public virtual void OpenLocalizeForm() { throw new iQImplementationException("OpenLocalizeForm"); }

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public virtual void PrintList() { throw new iQImplementationException("PrintList"); }

        /// <summary>Selecciona el registro activo
        /// <returns>void</returns>
        /// </summary>
        public virtual void SelectObject() 
        { 
            _selected = Datos.Current;
            this.DialogResult = DialogResult.OK;
            Close(); 
        }
        
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
						if (DialogResult.Yes == MessageBox.Show(Messages.CURRENT_EDITION_CLOSE,
																Labels.ADVISE_TITLE, 
                                                                MessageBoxButtons.YesNoCancel, 
																MessageBoxIcon.Question))
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
					if (MessageBox.Show(Messages.CURRENT_EDITION_CLOSE,
						                Labels.ADVISE_TITLE, 
                                        MessageBoxButtons.YesNoCancel, 
                                        MessageBoxIcon.Question) == DialogResult.Yes)
						this.Close();
				}
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Da formato a la tabla y aplica los roles 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EntityMngForm_Load(object sender, EventArgs e)
		{
			ApplyAuthorizationRules();
			RefreshList();
		}

		private void EntityMngForm_Shown(object sender, EventArgs e)
		{
			FormatControls();
		}

		private void EntityMngForm_Resize(object sender, EventArgs e)
		{
			FormatControls();
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
			part.CloseForm += new EventHandler(CloseForm);

			//Para futuros formularios modales
            if (part.IsModal)
            {
                PgMng.FillUp("EntityMngForm::AddForm (Mostrar Formulario)");
                part.ShowDialog();
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
                            return;
                        }
                    }
                }

                ShowForm(part);
                //Si se desea mantener una lista de formularios activos...

                _list_active_form.Add(part);

                //Cierra los LocalizeForm que pudieran estar abiertos y no pertenecen al MngForm actual
                //para evitar que aparezcan varios a la vez y comiencen a intercambiarse al seleccionarlos
                foreach (Form form in MainBaseForm.Instance.MdiChildren)
                {
                    if (form is LocalizeForm && !_list_active_form.Contains((EntityDriverForm)form))
                        form.Close();
                }

                //Formulario de busquedas: se añade un evento de búsqueda al formulario
                if (part is LocalizeForm)
                {
                    ((LocalizeForm)part).FindElement += new EventHandler(FindElement);
                    ((LocalizeForm)part).FilterElement += new EventHandler(FilterElement);
                    ((LocalizeForm)part).CloseForm += new EventHandler(CloseFromLocalize);
                }
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

			PgMng.FillUp("EntityMngForm::ShowForm (Mostrar Formulario)");
		}

		/// <summary>
		/// Evento que surge al cerrar uno de los formularios DriverEntityForm
		/// activo. Cierra este formulario, lo elimina de la lista de formularios activos
		/// y elimina el OID al que referencia de la lista de OID´s activos.
		/// Finalmente refresca la tabla para que los cambios surtan efecto.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void CloseForm(object sender, EventArgs e)
		{
            EntityDriverForm part = (EntityDriverForm)sender;

			_list_active_form.Remove(part);

			//Para formularios modales
			if (part.IsModal)
				part.CloseForm -= new EventHandler(CloseForm);

			//Formulario de búsqueda, adición y edicion
			else
			{
				//Formulario de búsqueda: se cierra directamente
				if (part is LocalizeForm)
				{
					part.CloseForm -= new EventHandler(CloseForm);
					part.Dispose();
					return;
				}
			}

			part.Dispose();
			RefreshList();
		}

		/// <summary>
		/// Evento que al cerrar la interfaz, cierra todos los formularios
		/// hijos que estén abiertos.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EntityMngForm_FormClosed(object sender, FormClosedEventArgs e)
		{
            foreach (EntityDriverForm form in _list_active_form)
			{
				form.CloseForm -= new EventHandler(CloseForm);
				//form.Dispose();
				form.Close();
			}
		}

		/// <summary>
		/// Selecciona un elemento de la lista a partir del LocalizeForm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void FindElement(object sender, EventArgs e)
		{
			LocalizeForm part = (LocalizeForm)sender;

			long id = part.ActiveOID();

			try
			{
				Select(id);
			}
			catch (SystemException ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}

		/// <summary>
		/// Filtra un elemento de la lista a partir del LocalizeForm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void FilterElement(object sender, EventArgs e)
		{
			LocalizeForm part = (LocalizeForm)sender;

			long id = part.ActiveOID();

			try
			{
				Filter(((LocalizeForm)sender).FilteredList);

			}
			catch (SystemException ex)
			{
				MessageBox.Show(ex.ToString());
			}

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

			RefreshList();
		}

		/// <summary>
		/// Funcion de tratamiento del evento CloseForm de los LocalizeForm para que 
		/// reajuste el tamaño de los formularios EntityMngForm al cerrarse
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void CloseFromLocalize(object sender, EventArgs e)
		{
            EntityDriverForm part = (EntityDriverForm)sender;
			MaximizeForm();
			part.Dispose();
			return;
		}

		#endregion

		#region Context Menu

		private void Nuevo_MI_Click(object sender, EventArgs e)
		{
			OpenAddForm();
		}

		private void Detalle_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				OpenViewForm();
		}

		private void Modificar_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				OpenEditForm();
		}

		private void Borrar_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				DeleteObject(ActiveOID);
			FormMngBase.Instance.RefreshFormsData();
		}

		private void Duplicar_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				DuplicateObject(ActiveOID);
		}

		private void Localizar_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				OpenLocalizeForm();
		}

		private void Imprimir_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				PrintList();
		}

		#endregion

	}
}