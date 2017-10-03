using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Selección de Datos para un Informe
	/// </summary>
    public partial class ReportSelectBaseForm : moleQule.Face.ChildForm
    {
        #region Attributes & Properties

        protected PrintSource _source = PrintSource.All;

        public virtual PrintSource Source { get { return _source; } set { _source = value; } }

        #endregion

        #region Factory Methods

        public ReportSelectBaseForm() : this (true) {}

        public ReportSelectBaseForm(bool isModal)
            : base(isModal, null)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        public void SetDataSource(object list)
        {
            Datos.DataSource = list;
        }

        #endregion

        #region Actions

		protected virtual void AcceptAction() 
		{
			using (StatusBusy st = new StatusBusy(Resources.Messages.LOADING_DATA))
			{
				using (ProgressInfoMng bar = new ProgressInfoMng())
				{
					try
					{
						PgMng.Grow();
						PrintAction();
						DialogResult = DialogResult.OK;
						Close();
					}
					catch (Exception ex)
					{
						MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),
										Labels.APP_TITLE,
										MessageBoxButtons.OK,
										MessageBoxIcon.Exclamation);
					}
					finally
					{
						PgMng.FillUp();
					}
				}
			}		
		}

        #endregion
    }
}

