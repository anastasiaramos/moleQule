using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base para cualquier formulario de la aplicacion
	public partial class BaseForm : Form
    {
        #region Attributes & Properties

        protected ProgressInfoMng _pg_mng = null;

		public ProgressInfoMng PgMng { get { return _pg_mng; } set {_pg_mng = value;} }
		public ProgressInfoMng Bar { get { return PgMng; } set { PgMng = value; } } //DEPRECATED

		protected molView _view_mode;
		public molView ViewMode { get { return _view_mode; } set { _view_mode = value; } }

		protected EStatus Status { get; set; }

        #endregion

        #region Factory Methods

        public BaseForm()
        {
            InitializeComponent();

			ViewMode = molView.Normal;
			Status = EStatus.OK;
            
            if (Globals.Instance != null)
				_pg_mng = Globals.Instance.ProgressInfoMng;
        }

		#endregion

		#region Authorization

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected virtual void ApplyAuthorizationRules() { }

		#endregion

		#region Layout

		protected void CenterProgressControls()
		{
			Progress_Panel.Left = (Width - Progress_Panel.Width) / 2;
			Progress_Panel.Top = (ProgressBK_Panel.Height - Progress_Panel.Height) / 2;

			Progress_PB.Left = (Width - Progress_PB.Width) / 2;
			Progress_PB.Top = (Height - Progress_PB.Height - ProgressInfo_PB.Height - 5) / 2;

			ProgressInfo_PB.Left = Progress_PB.Left;
			ProgressInfo_PB.Top = Progress_PB.Bottom + 5;
		}

		public void EnableProgressPanel(bool enable)
		{
#if TRACE
			AppControllerBase.AppControler.Timer.Record("BaseForm::EnableProgressPanel - INI");
#endif
			if (!enable)
			{
				SuspendLayout();
				SetView();
				ApplyAuthorizationRules();

				Progress_PB.SendToBack();
				ProgressInfo_PB.SendToBack();
				Progress_PB.Visible = false;
				ProgressInfo_PB.Visible = false;
				
				//ProgressBK_Panel.Visible = false;
			}
			else
			{
				Progress_PB.BringToFront();
				ProgressInfo_PB.BringToFront();
				Progress_PB.Visible = true;
				ProgressInfo_PB.Visible = true;

				//ProgressBK_Panel.Visible = true;
				//ProgressBK_Panel.BringToFront();

				ResumeLayout();
				Refresh();
			}
#if TRACE
			AppControllerBase.AppControler.Timer.Record("BaseForm::EnableProgressPanel - END");
#endif
		}

		public virtual void FormatControls() 
		{
			CenterProgressControls();
		}

		public virtual void FillUpProgressBar()
		{
			ProgressInfo_PB.Value = ProgressInfo_PB.Maximum;
		}
		public virtual void IncreaseProgressBar()
		{
			ProgressInfo_PB.Value += ProgressInfo_PB.Step;
		}
		public virtual void ResetProgressBar(int max, int step)
		{
			ProgressInfo_PB.Maximum = max;
			ProgressInfo_PB.Step = step;
			ProgressInfo_PB.Value = ProgressInfo_PB.Minimum + step;
		}

		protected virtual void SetView() { SetView(_view_mode); }
		protected virtual void SetView(molView view) {}

		#endregion

		#region Events

		private void BaseForm_Resize(object sender, EventArgs e)
        {
			CenterProgressControls();
        }

		private void CultureManager_UICultureChanged(System.Globalization.CultureInfo newCulture)
		{
			FormatControls();
		}

		#endregion
    }
}