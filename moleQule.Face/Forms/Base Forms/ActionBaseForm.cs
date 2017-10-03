using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face.Resources;
using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Modifica un objeto que se pasa
    /// </summary>
    public partial class ActionBaseForm : moleQule.Face.ChildForm
    {
        #region Attributes & Properties

        protected object _input_data = null;

		public object InputData { get { return _input_data; } }

        #endregion

        #region Factory Methods

        public ActionBaseForm() 
            : this(true) {}

        public ActionBaseForm(bool isModal)
            : this(isModal, null) {}

        public ActionBaseForm(bool isModal, Form parent)
            : base(isModal, parent)
        {
            InitializeComponent();

            _action_result = DialogResult.Ignore;
        }

        #endregion

		#region Source

		/// <summary>
		/// Asigna el objeto al origen de datos y los orígenes de datos auxiliares
		/// <returns>void</returns>
		/// </summary>
		protected void SetFormData()
		{
            try
            {
                EnableEvents(false);
                RefreshSecondaryData();
                RefreshMainData();
            }
            catch (Exception ex)
            {
                if (null != iQExceptionHandler<iQLockException>.GetiQException(ex))
                {
                    PgMng.ShowInfoException(Messages.LOCK_ERROR);
                }
                else
                {
                    PgMng.ShowInfoException(ex);
                }

                Dispose();
            }
            finally 
            {
                EnableEvents(true);
            }
		}

		#endregion

        #region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (action)
			{
				case molAction.CustomAction1:
					//EnableForm(false);
					CustomAction1();
					break;

				case molAction.CustomAction2:
					//EnableForm(false);
					CustomAction2();
					break;

				case molAction.CustomAction3:
					//EnableForm(false);
					CustomAction3();
					break;

				case molAction.CustomAction4:
					//EnableForm(false);
					CustomAction4();
					break;

				default:
					base.DoExecuteAction(action);
					break;
			}
		}

		protected override void SubmitAction()
		{
			_back_job = BackJob.Submit;
			if (PgMng != null) PgMng.StartBackJob(this);
			if (PgMng != null) PgMng.FillUp();
		}

		public virtual void CustomAction1() { throw new iQImplementationException("CustomAction1"); }
		public virtual void CustomAction2() { throw new iQImplementationException("CustomAction2"); }
		public virtual void CustomAction3() { throw new iQImplementationException("CustomAction3"); }
		public virtual void CustomAction4() { throw new iQImplementationException("CustomAction4"); }

        #endregion

        #region Events

        #endregion

        #region CloseForm

        /// <summary>
        /// Evento que se genera al cerrar el formulario
        /// </summary>
        public event EventHandler CloseForm;

        /// <summary>
        /// Función para que lance un evento al cerrar el formulario 
        /// o lo cierre directamente
        /// </summary>
        public void Cerrar()
        {
            if (CloseForm != null)
                CloseForm(this, EventArgs.Empty);
        }

        #endregion
    }
}

