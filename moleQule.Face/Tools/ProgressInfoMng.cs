using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
	public class ProgressInfoMng : IDisposable
    {	
		#region Attributes

		private string _name;
		private BGResult _result = BGResult.Working;
		private Cursor _cursor = Globals.Instance.Cursor;
		private ProgressInfoForm _pg_form = Globals.Instance.ProgressInfoForm;
		private ToolStripProgressBar _pg_bar = Globals.Instance.ProgressBar;
        private BGAction _action = BGAction.Default;
        private BaseForm _caller = null;
        bool _do_grow = false;
        bool _bk_grow = false;
        public BackgroundWorker BkWorker = new BackgroundWorker();
        private string _track_message = string.Empty;
#if TRACE
        moleQule.Library.Timer _timer;
#endif

		IBackGroundLauncher _shuttle;

		public BGResult State { get; set; }
		public EStatus PGStatus { get; set; }

        #endregion

        #region Properties

        /// <summary>
		/// Nombre de la instancia
		/// </summary>
		public string Name { get { return _name; } set { _name = value; } }
		
        /// <summary>
		/// Mensaje de información del formulario
		/// </summary>
		public string Message
		{
			get { return _pg_form.Message; }
			set
			{
				_pg_form.Message = value;
				_pg_form.Refresh();
                if (_caller != null)
                {
                    _caller.ProgressMsg_LB.Text = value;
                    _caller.Progress_Panel.Refresh();
                }
			}
		}
		
        /// <summary>
		/// Resultado de la operación
		/// </summary>
		public BGResult Result { get { return _result; } set { _result = value; } }

        /// <summary>
        /// Resultado de la operación
        /// </summary>
        public BGAction Action { get { return _action; } set { _action = value; } }

        public BaseForm Caller { get { return _caller; } set { _caller = value; } }

#if TRACE
        public moleQule.Library.Timer Timer { get { return _timer; } set { _timer = value; } }
#endif

		#endregion

		#region Factory Methods

		/// <summary>
		/// Única instancia de la clase (Singleton Pattern)
		/// </summary>
		internal static ProgressInfoMng _singleton = null;

		/// <summary>
		/// Devuelve la instancia única
		/// </summary>
		public static ProgressInfoMng Instance { get { return _singleton != null ? _singleton : new ProgressInfoMng(); } }

		public ProgressInfoMng()
		{
            if (MainBaseForm.Instance != null)
            {
                _singleton = this;

                Globals.Instance.Cursor = Cursors.WaitCursor;

                _pg_form = ProgressInfoForm.Instance;
				_pg_bar = Globals.Instance.ProgressBar;
                _cursor = Globals.Instance.Cursor;
#if TRACE
				_timer = Library.Timer.Instance;
#endif
                // 
                // BkWorker
                // 
                this.BkWorker.WorkerReportsProgress = true;
                this.BkWorker.WorkerSupportsCancellation = true;
                this.BkWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkWorker_DoWork);
                this.BkWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BkWorker_RunWorkerCompleted);
                this.BkWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BkWorker_ProgressChanged);

                Init(100, 1);
            }
		}

        public void Init(int max, int step) 
        { 
            Init(max, step, string.Empty, string.Empty, null); 
        }
        public void Init(int max, int step, string status_messsage, string track_messsage) 
        { 
            Init(max, step, status_messsage, track_messsage, null); 
        }
        public void Init(int max, int step, string status_messsage, string track_messsage, ChildForm caller)
        {
            if (MainBaseForm.Instance != null)
            {
                // Formulario que lo invoca
                _caller = caller;
                _pg_form.Caller = caller;

                // Barra de progreso del formulario de progreso
                _pg_form.ProgressInfo_PB.Minimum = 0;
                _pg_form.ProgressInfo_PB.Maximum = max + step;
                _pg_form.ProgressInfo_PB.Value = _pg_form.ProgressInfo_PB.Minimum + step;
                _pg_form.ProgressInfo_PB.Step = step;

                // Barra de progreso de la barra de estado
				if (_pg_bar != null)
				{
					_pg_bar.Maximum = max + step;
					_pg_bar.Value = _pg_bar.Minimum + step;
					_pg_bar.Step = step;
				}

                // Mensaje de estado
                Message = status_messsage;
            }
        }

        public void CloseForm()
        {
            _pg_form.Close();
            ProgressInfoForm.Instance.Dispose();
            ProgressInfoForm.Instance = null;
            _pg_form = null;
        }

        public void ShowForm() { ShowForm(true); }
        public void HideForm() { ShowForm(false); }

		Thread ProgressThread { get; set; }
		protected void OpenProgressForm()
		{
			_pg_form.ShowDialog(_caller);
		}

		public void ShowForm(bool show)
		{
			if (_pg_form == null) return;

            if (show)
            {
                if (_caller == null) 
					_pg_form.Show();

				/*if (ProgressThread != null) return;

				ProgressThread = new Thread(OpenProgressForm);
				ProgressThread.Start();*/
			}
            else
            {              
				if (_caller == null) 
					_pg_form.Hide();

				/*if (ProgressThread == null) return;
				if (!ProgressThread.IsAlive) return;

				ProgressThread.Abort();
				ProgressThread = null;*/
            }

            Refresh();
		}

        private void Show(bool show)
        {
            if (_caller != null)
            {
                _caller.EnableProgressPanel(show);
				Refresh();
				//if (show) Application.DoEvents();
            }

            if (_pg_bar != null) _pg_bar.Visible = show;

            if (Globals.Instance.AnimLabel != null) Globals.Instance.AnimLabel.Visible = show;

            if (_pg_form != null) 
			{ 
				ShowForm(show); 
			}
        }

		private void RefreshCaller()
		{
			while (!_shuttle.Finished)
			{
				if (_do_grow) DoGrow();
				Application.DoEvents();
			}
		}

		// IDisposable
		private bool _disposedValue = false; // To detect redundant calls

		protected void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (Globals.Instance != null)
					{
						Globals.Instance.AnimLabel.Visible = false;
						Globals.Instance.Cursor = _cursor;
						_pg_form.ProgressInfo_PB.Visible = false;
						_pg_form.ProgressInfo_PB.Value = _pg_form.ProgressInfo_PB.Minimum;

						Globals.Instance.ProgressBar.Visible = false;
						Globals.Instance.ProgressBar.Value = Globals.Instance.ProgressBar.Minimum;

						Globals.Instance.Refresh();
					}
				}
			}

			_disposedValue = true;
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Driver Methods

		public void StartBackJob(IBackGroundLauncher shuttle)
		{
            try
            {
				State = BGResult.Working;

				_shuttle = shuttle;
                _bk_grow = true;

                RunBackGround(shuttle);

                shuttle.ForeGroundJob();

                while (!shuttle.Finished)
                {
                    if (_do_grow) DoGrow();
					Refresh();
					Application.DoEvents();
                }

                _result = shuttle.Result;
            }
            catch (Exception ex)
            {
                _result = BGResult.Error;
				ShowInfoException(ex);
            }
            finally
            {
                _bk_grow = false;
				State = BGResult.OK;
				_shuttle = null;
            }
        }

        public void CancelBackJob(IBackGroundLauncher shuttle)
        {
            _result = BGResult.Cancelled;
            shuttle.Result = BGResult.Cancelled;
            shuttle.Finished = true;
            FillUp();
        }

        public void Reset() { Reset((_pg_bar != null) ? _pg_bar.Maximum : 1, (_pg_bar != null) ? _pg_bar.Step : 1); }
		public void Reset(int max, int step) { Reset(max, step, Resources.Messages.LOADING_DATA); }
        public void Reset(int max, int step, string status_messsage) { Reset(max, step, status_messsage, string.Empty); }
        public void Reset(int max, int step, string status_messsage, string track_messsage)
        {
            Reset(max, step, status_messsage, track_messsage, null);
        }  
        public void Reset(int max, int step, string status_messsage, BaseForm caller)
        {
            Reset(max, step, status_messsage, string.Empty, caller);
        }
        public void Reset(int max, int step, string status_messsage, string track_message, BaseForm caller)
        {
			PGStatus = EStatus.Working;
			State = BGResult.Working;

            if (_pg_form == null) _pg_form = ProgressInfoForm.Instance;

            // Formulario que lo invoca
            _caller = caller;
            _pg_form.Caller = caller;

            if (_caller != null)
            {
                // Barra de progreso del formulario de progreso
				_caller.ResetProgressBar(max + step, step);
                /*_caller.ProgressInfo_PB.Maximum = max + step;
                _caller.ProgressInfo_PB.Value = _pg_form.ProgressInfo_PB.Minimum + step;
                _caller.ProgressInfo_PB.Step = step;*/
            }

            // Barra de progreso del formulario de progreso
            _pg_form.ProgressInfo_PB.Maximum = max + step;
            _pg_form.ProgressInfo_PB.Value = _pg_form.ProgressInfo_PB.Minimum + step;
            _pg_form.ProgressInfo_PB.Step = step;

			try
			{
				// Barra de progreso de la barra de estado
				_pg_bar.Maximum = max + step;
				_pg_bar.Value = _pg_bar.Minimum + step;
				_pg_bar.Step = step;
			}
			catch { }

            // Mensaje de estado
            Message = status_messsage;

            this.Start(track_message);
#if TRACE
            // Timer para depuración
            _timer.Reset();
#endif
        }

        protected void Start() { Start(string.Empty); }
        protected void Start(string trackMessage) 
        {
			PGStatus = EStatus.Working;
            _result = BGResult.Working;

            Show(true);

            Grow(string.Empty, "START RECORDING | " + trackMessage);
       }

        public void Grow() { Grow(string.Empty); }
        public void Grow(string message) { Grow(message, string.Empty); }
        public void Grow(string message, string trackMessage)
        {
            if (message != string.Empty)
            {
                _pg_form.Message = message;
                if (_caller != null) _caller.ProgressMsg_LB.Text = message;
            }

            _do_grow = true;
            _track_message = trackMessage;

            if (_bk_grow)
                return;
            else
                DoGrow();
#if TRACE
            _timer.Record(_track_message);
            _track_message = string.Empty;
#endif
        }

        public void DoGrow()
        {
            if (_pg_bar != null)
            {
                if (_pg_bar.Value + _pg_bar.Step > _pg_bar.Maximum) { return; }

                // Barra de progreso de la barra de estado
                if (_pg_bar.Value + _pg_bar.Step <= _pg_bar.Maximum)
                    _pg_bar.Value += _pg_bar.Step;
            }

            if (_pg_form != null)
            {
                // Barra de progreso del formulario de progreso
                _pg_form.ProgressInfo_PB.Value += _pg_form.ProgressInfo_PB.Step;
            }

			if (Caller != null) Caller.IncreaseProgressBar();

            Refresh();

            _do_grow = false;
        }

        public void FillUp() { FillUp(string.Empty); }
        public void FillUp(string message) { FillUp(message, string.Empty); }
		public void FillUp(string message, string track_message)
		{
			try
			{
				if (PGStatus != EStatus.Working) return;
#if TRACE
				_timer.Record("ProgressInfoMng::FillUp INI | " + track_message);
#endif
				if (message != string.Empty) Message = message;

				// Barra de progreso de la barra de estado
				if (_pg_bar != null) _pg_bar.Value = _pg_bar.Maximum;

				// Barra de progreso del formulario de progreso
				if (_pg_form != null) _pg_form.ProgressInfo_PB.Value = _pg_form.ProgressInfo_PB.Maximum;

				if (Caller != null) Caller.FillUpProgressBar();

				Refresh();

				_result = BGResult.OK;

				State = BGResult.OK;

				// Cerramos el formulario
				Show(false);
#if TRACE
				_timer.Record("ProgressInfoMng::FillUp END | " + track_message);
				ShowCronos();
#endif
				_caller = null;
				PGStatus = EStatus.Closed; 
			}
			catch 
			{
				PGStatus = EStatus.Error; 
			}

		}

        public void Refresh()
        {
            if (_caller != null)
            {
				_caller.Progress_Panel.ResumeLayout(true);
            }
			else
				_pg_form.RefreshAll();
            
            if (Globals.Instance.StatusBar != null) Globals.Instance.StatusBar.Refresh();
            
            if (!_bk_grow)
            {
                //Application.DoEvents();
                //System.Threading.Thread.Sleep(100);
            }
        }
		
        #endregion

#if TRACE
        #region Performance

        public void Record(string label)
        {
			_timer.Record(label);
        }

        public string GetRecords()
        {
            string message = "Nº Intervalos de Progreso: " + Convert.ToString(_pg_bar.Maximum / _pg_bar.Step);
			message += System.Environment.NewLine;
			message += "Nº Intervalos de Debug: " + Convert.ToString(_timer.GetIntervals());
			message += System.Environment.NewLine;
            message += System.Environment.NewLine + _timer.GetCronos();
            return message;
        }

        public void ShowCronos()
        {
            MessageBox.Show(GetRecords(), "Barra de Progreso Global");
        }

        #endregion
#endif
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

				worker.WorkerReportsProgress = true;

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
			//Refresh();
        }

        #endregion

		#region Errors

		public static DialogResult ShowQuestion(string question)
		{
			return MessageBox.Show(question,
								SettingsMng.Instance.GetApplicationTitle(),
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question);
		}

		public static DialogResult ShowException(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public static DialogResult ShowException(Exception ex, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),
									SettingsMng.Instance.GetApplicationTitle(),
									buttons,
									icon);
		}
		public static DialogResult ShowException(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public static DialogResult ShowException(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.Show(message,
							SettingsMng.Instance.GetApplicationTitle(),
							buttons,
							icon);
		}

		public static DialogResult ShowError(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public static DialogResult ShowError(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static DialogResult ShowInfo(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public static DialogResult ShowInfo(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static DialogResult ShowWarning(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		public static DialogResult ShowWarning(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		public DialogResult ShowDeleteConfirmation()
		{
			return ShowQuestion(Resources.Messages.DELETE_CONFIRM);
		}

		public DialogResult ShowInfoException(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public DialogResult ShowInfoException(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		public DialogResult ShowWarningException(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
		}
		public DialogResult ShowWarningException(string message)
		{
			return ShowException(message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
		}

		public DialogResult ShowErrorException(Exception ex)
		{
			return ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public DialogResult ShowErrorException(string message)
		{
			return ShowException(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		#endregion
	}
}
