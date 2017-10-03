using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Infralution.Localization;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
    public interface IFormMng
    {
        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        void OpenForm(string formID);
        void OpenForm(string formID, object param);
		void OpenForm(string formID, object[] parameters, Form parent);

        object GetFormulario(Type childType);
    }

    /// <summary>
    /// Clase base para manejo (apertura y cierre) de formularios
    /// </summary>
    /// <remarks>
    /// Para utilizar el FormMngBase es necesario indicar cual será el MainForm padre de los formularios
    /// Este MainForm deberá ser un formulario heredado de MainFormBase
    /// </remarks>
    public class FormMngBase
    {
        #region Attributes

        private Form _main_form = null;

        /// <summary>
        /// Formulario principal padre del resto
        /// </summary>
        public Form MainForm 
        { 
            get 
            {
                if (_main_form != null)
                    return _main_form;
                else
                    throw new Exception(Messages.MAINFORM_NOT_DEFINED);
            } 
            
            set { _main_form = value; } 
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Única instancia de la clase MainBaseForm (Singleton)
        /// </summary>
        protected static FormMngBase _main;

        protected ProgressInfoMng _pmg;

        /// <summary>
        /// Unique FormMngBase Class Instance
        /// </summary>
        /// <remarks>
        /// Para utilizar el FormMngBase es necesario inicializar el MainForm padre de los formularios
        /// </remarks>
        public static FormMngBase Instance { get { return _main != null ? _main : new FormMngBase(); } }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public FormMngBase()
		{
			// Singleton
			_main = this;

            _pmg = Globals.Instance.ProgressInfoMng;
	    }

        #endregion

		#region Viewing

		/// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        public void OpenForm(string formID) { OpenForm(formID, null, null); }
		public void OpenForm(string formID, Form parent) { OpenForm(formID, null, parent); }
		public void OpenForm(string formID, object[] parameters) { OpenForm(formID, parameters, null); }

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        /// <param name="param">Parámetro para el formulario</param>
		public virtual void OpenForm(string formID, object[] parameters, Form parent)
        {
            try
            {
                switch (formID)
                {
                    case NewsBaseForm.ID:
                        {
                            if (!BuscarFormulario(NewsBaseForm.Type))
                            {
								NewsBaseForm em = new NewsBaseForm(parameters[1] as List<string>);
                                ShowFormulario(em);
                            }
                        } break;

                    case SchemaMngForm.ID:
                        {
                            if (!BuscarFormulario(SchemaMngForm.Type))
                            {
                                SchemaMngForm em = new SchemaMngForm();
                                ShowFormulario(em);
                            }
                        } break;

                    case SettingsBaseForm.ID:
                        {
                            if (!BuscarFormulario(SettingsBaseForm.Type))
                            {
								CloseAllForms();
								SettingsBaseForm em = new SettingsBaseForm();
								ShowFormulario(em);
                            }
                        } break;

					case UserMngForm.ID:
						{
							if (!AppContext.User.IsAdmin)
								iQExceptionHandler.TreatException(new iQAuthorizationException());

							if (!BuscarFormulario(UserMngForm.Type))
							{
								UserMngForm em = new UserMngForm(parent);
								ShowFormulario(em);
							}
						} break;

                    case UsersUIForm.ID:
                        {
                            if (!AppContext.User.IsAdmin)
                                iQExceptionHandler.TreatException(new iQAuthorizationException());

                            if (!BuscarFormulario(UsersUIForm.Type))
                            {
                                UsersUIForm em = new UsersUIForm(AppContext.Principal.ActiveSchema, parent);
                                ShowFormulario(em);
                            }
                        } break;

					case UserPasswordEditForm.ID:
						{
							if (!BuscarFormulario(UserPasswordEditForm.Type))
							{
								UserPasswordEditForm em = new UserPasswordEditForm(AppContext.User, true);
								ShowFormulario(em);
							}
						} break;

                    default:
                        {
							ProgressInfoMng.ShowException(String.Format(Messages.FORM_NOT_FOUND, formID));
                        } break;
                }
            }
            catch (Exception ex)
            {
				if (Globals.Instance.ProgressInfoMng != null)
				{
					Globals.Instance.ProgressInfoMng.ShowErrorException(ex);
					Globals.Instance.ProgressInfoMng.FillUp();
				}
				else
					ProgressInfoMng.ShowException(ex);
            }
        }

        /// <summary>
        /// Muestra el formulario pasado como parámetro y lo trae al frente
        /// </summary>
        /// <param name="form">Formulario</param>
        public void ShowFormulario(ChildForm form)
        {
			List<ChildForm> keepOpenForms = new List<ChildForm>();

			keepOpenForms.Add(form);

            if (form is EntityMngBaseForm)
				CloseAllForms(keepOpenForms);

            if (form.ActivateForm())
            {
                if (form.IsModal)
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                    form.BringToFront();
                }
            }
            else
                form.Dispose();
        }

		public void ShowFormulario(ChildForm form, ChildForm keepOpenForm)
		{
			List<ChildForm> keepOpenForms = new List<ChildForm>();

			keepOpenForms.Add(form);
			keepOpenForms.Add(keepOpenForm);

			if (form is EntityMngBaseForm)
				CloseAllForms(keepOpenForms);

			if (form.ActivateForm())
			{
				if (form.IsModal)
				{
					form.ShowDialog();
				}
				else
				{
					form.Show();
					form.BringToFront();
				}
			}
			else
				form.Dispose();
		}

        /// <summary>
        /// Devuelve verdadero si encuentra un formulario hijo del tipo pasado
        /// como parámetro
        /// </summary>
        /// <param name="childType">Tipo de formulario</param>
        public bool BuscarFormulario(Type childType)
        {
            foreach (ChildForm hijo in MainForm.MdiChildren)
            {
                if (hijo.GetType().Equals(childType))
                {
                    ShowFormulario(hijo);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Devuelve un formulario hijo del tipo pasado como parámetro
        /// </summary>
        /// <param name="childType">Tipo de formulario</param>
        public object GetFormulario(Type childType)
        {
            foreach (ChildForm hijo in MainForm.MdiChildren)
            {
                if (hijo.GetType().Equals(childType))
                    return hijo;
            }
            return null;
        }

		/// <summary>
		/// Cierra todos los formularios abiertos
		/// </summary>
		public void CloseAllForms()
		{
			foreach (ChildForm hijo in MainForm.MdiChildren)
			{
				hijo.Close();
			}
		}

		public void CloseAllForms(ChildForm keepOpenForm)
		{
			List<ChildForm> keepOpenForms = new List<ChildForm>();

			keepOpenForms.Add(keepOpenForm);

			CloseAllForms(keepOpenForms);
		}

        /// <summary>
        /// Cierra todos los formularios abiertos
        /// </summary>
        public void CloseAllForms(List<ChildForm> keepOpenForms)
        {
            foreach (ChildForm hijo in MainForm.MdiChildren)
            {
				bool keep_open = false;
 
				foreach (ChildForm form in keepOpenForms)
					if (form.FormId == hijo.FormId) keep_open = true;
				
				if (keep_open) continue;

                if (hijo is EntityMngBaseForm) hijo.Close();
            }
        }

        /// <summary>
        /// Cierra todos los formularios abiertos
        /// </summary>
        public void CloseAllChilds()
        {
            foreach (ChildForm hijo in MainForm.MdiChildren)
            {
                //if (!hijo.GetType().Equals(AppContext.Principal.ActiveSchemaType))
                //    hijo.Close();
            }
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Refresca todos los formularios abiertos
        /// </summary>
        public void RefreshForms()
        {
            foreach (ChildForm child in MainBaseForm.Instance.MdiChildren)
                child.Refresh();
        }

        /// <summary>
        /// Refresca los datos dependientes de todos los ManagerForm
        /// </summary>
        public void RefreshFormsData()
        {
#if TRACE
            Globals.Instance.Timer.Start();
            Globals.Instance.Timer.Record("FormMngBase::RefreshFormsData - Init");
#endif
            foreach (Form formulario in MainBaseForm.Instance.MdiChildren)
            {
                if (formulario is ManagerForm && ((ManagerForm)formulario).RefreshChildren)
                {
                    ((ChildForm)formulario).RefreshSecondaryData();
                }
                else if (formulario is EntityMngForm)
                {
                    ((EntityMngForm)formulario).RefreshChildForm();
                }
                else if (formulario is EntityMngBaseForm)
                {
                    ((EntityMngBaseForm)formulario).RefreshFormsData();
                }
            }
#if TRACE
            Globals.Instance.Timer.Record("FormMngBase::RefreshFormsData - End");
            MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
        }

        #endregion
    }
}
