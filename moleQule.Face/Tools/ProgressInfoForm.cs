using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
	public partial class ProgressInfoForm : Form
	{
		
		#region Business Methods

		/// <summary>
		/// Única instancia de la clase (Singleton Pattern)
		/// </summary>
		internal static ProgressInfoForm _singleton;
        protected Form _caller;
        protected bool _is_modal;

		/// <summary>
		/// Devuelve la instancia única
		/// </summary>
        public static ProgressInfoForm Instance { get { return _singleton != null ? _singleton : new ProgressInfoForm(); } set { _singleton = null; } }

        public Form Caller { get { return _caller; } set { _caller = value; } }

		/// <summary>
		/// Mensaje de información del formulario
		/// </summary>
		public string Message 
		{
			get { return ProgressInfo_TB.Text; }
			set { ProgressInfo_TB.Text = value; } 
		}

        public void RefreshAll()
        {
            ProgressInfo_PB.Refresh();
            ProgressInfo_TB.Refresh();
            Animation.Refresh();
            Refresh();
        }
		
        #endregion

		#region Factory Methods

		public ProgressInfoForm()
            : this(null) {}

        private ProgressInfoForm(Form caller)
        {
            this.Tag = Resources.Consts.NO_FORMAT;

            InitializeComponent();

            PictureBox.CheckForIllegalCrossThreadCalls = false;

            _singleton = this;

            _caller = caller; // (caller != null) ? caller : MainBaseForm.Instance;
        }

		#endregion
		
		#region Background
		
		/// <summary>
		/// Función que lanza la ejecución en segundo plano
		/// </summary>
		/// <param name="bar"></param>
		public virtual void RunBackGround(IBackGroundLauncher shuttle)
        {
            shuttle.Finished = false;
			BkWorker.RunWorkerAsync(shuttle);
		}

		/// <summary>
		/// This event handler is where the actual, potentially time-consuming work is done.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BkWorker_DoWork(object sender, DoWorkEventArgs e)
		{
            IBackGroundLauncher shuttle = (IBackGroundLauncher)e.Argument;

            try
            {
                // Get the BackgroundWorker that raised this event.
                BackgroundWorker worker = sender as BackgroundWorker;
                worker.WorkerSupportsCancellation = true;
                // Assign the result of the computation
                // to the Result property of the DoWorkEventArgs
                // object. This is will be available to the 
                // RunWorkerCompleted eventhandler.

                shuttle.BackGroundJob(worker);
                shuttle.Result = BGResult.OK;
            }
            catch (Exception ex)
            {
                shuttle.Result = BGResult.Error;
                MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                e.Result = shuttle;
            }
		}

		/// <summary>
		/// This event handler deals with the results of the background operation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BkWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            IBackGroundLauncher shuttle = ((IBackGroundLauncher)(e.Result));

			// First, handle the case where an exception was thrown.
			if (e.Error != null)
			{
                shuttle.Result = BGResult.Error;
                MessageBox.Show(e.Error.Message);
            }
			else if (e.Cancelled)
			{
				// Next, handle the case where the user canceled 
				// the operation.
				// Note that due to a race condition in 
				// the DoWork event handler, the Cancelled
				// flag may not have been set, even though
				// CancelAsync was called.
                shuttle.Result = BGResult.Cancelled;
			}
			else
			{
				// Finally, handle the case where the operation succeeded.
				shuttle.Result = BGResult.OK;
			}

            shuttle.Finished = true;
		}

		/// <summary>
		/// This event handler updates the progress PgMng.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BkWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
            if (e.ProgressPercentage <= ProgressInfo_PB.Maximum)
            {
                ProgressInfo_PB.Value = e.ProgressPercentage;
                RefreshAll();
            }
		}
		
		#endregion

        #region Events

        private void ProgressInfoForm_Deactivate(object sender, EventArgs e)
        {
            /*if (_caller != null)
                _caller.BringToFront();*/
        }

        #endregion
    }
}