using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using moleQule.Face.Controls;
using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base para cualquier formulario hijo del formulario principal de la Aplicacion
    /// </summary>
	public partial class ChildForm : BaseForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        public virtual Type EntityType { get { return null; } }

        /// <summary>
        /// ID único del formulario.
        /// Se utiliza para identificarlo en la lista que maneja EntityMngForm
        /// </summary>
        protected long _form_id = -1;
        protected BaseForm _parent;
        protected bool _is_modal;

        protected DialogResult _action_result = DialogResult.Cancel;
		protected molAction _current_action = molAction.Close;

		CRViewer _report_viewer;

		protected bool _show_colors = false;

        public long FormId { get { return _form_id; } set { _form_id = value; } }
        public bool IsModal { get { return _is_modal; } }
		public DialogResult ActionResult { get { return _action_result; } }
		protected bool EventsEnabled { get; set; }

		/// <summary>
		/// Nº de pasos para la barra de progreso
		/// </summary>
		protected virtual int BarSteps { get { return 2; } }

		/// <summary>
		/// Visor para informes
		/// </summary>
		public CRViewer ReportViewer
		{
			get
			{
				if (_report_viewer == null)
					_report_viewer = new CRViewer();

				return _report_viewer;
			}
		}

#if TRACE
        /// <summary>
        /// Timer para depuración
        /// </summary>
        protected moleQule.Library.Timer _timer;
#endif
        #endregion

        #region Factory Methods

		/// <summary>
		/// Constructor por defecto. Exigencia de Visual Studio
		/// para mostrar los preview de los formularios
		/// </summary>
		protected ChildForm() 
			: this(false, null) { }

        public ChildForm(bool isModal, Form parent)
        {
            InitializeComponent();
            
            _form_id = DateTime.Now.ToBinary();

			_is_modal = isModal;

            this.MdiParent = _is_modal ? null : ((parent != null) ? ((parent.IsMdiContainer) ? parent : MainBaseForm.Instance) : MainBaseForm.Instance);

            _parent = parent as BaseForm;

            HelpProvider.HelpNamespace = Application.StartupPath + Resources.Paths.HELP_PATH + "Help.chm";

            if ((PgMng != null) && (BarSteps > 2))
            {
                if ((_parent != null) && (_parent is BaseForm))
                    PgMng.Reset(BarSteps * 2, 2, Resources.Messages.LOADING_DATA, "ChildForm()", _parent as BaseForm);
                else
                    PgMng.Reset(BarSteps * 2, 2, Resources.Messages.LOADING_DATA, "ChildForm()", this);
            }
#if TRACE
            // Para depuración de tiempos
            _timer = Library.Timer.Instance;
#endif
        }

		public virtual void InitForm()
		{
#if TRACE
			PgMng.Record("ChildForm::InitForm INI");
#endif
			ApplyAuthorizationRules();
			FormatForm();
#if TRACE
			PgMng.Record("ChildForm::InitForm END");
#endif
		}

		public virtual void DisposeForm() { CleanCache(); }

		#endregion

		#region Cache

		protected virtual void BuildCache() {}
		protected virtual void CleanCache() {}

		#endregion

        #region IBackGroundLauncher

        bool _finished = false;
        string _param = string.Empty;
        protected BGResult _result = BGResult.Working;
        protected enum BackJob { Submit, RefreshList, LoadSchema } 
        protected BackJob _back_job = BackJob.Submit;

        /// <summary>
        /// La llama el backgroundworker para avisar que ha terminado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool Finished { get { return _finished; } set { _finished = value; } }
        public BGResult Result { get { return _result; } set { _result = value; } }

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackGroundJob(BackgroundWorker bk)
        {
            try
            {
                switch (_back_job)
                {
                    case BackJob.Submit:
                        BkSubmitAction();
                        break;
                }
            }
            catch (Exception ex)
            {
                CancelBackGroundJob();
				PgMng.ShowInfoException(ex);
            }
        }

        public void BackGroundJob() {}

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en primer plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ForeGroundJob() {}

        protected void CancelBackGroundJob()
        {
            if (PgMng != null) PgMng.CancelBackJob(this);
        }

        protected virtual void BkSubmitAction() { throw new iQImplementationException("BkSubmitAction"); }

        #endregion

        #region Layout

		public virtual bool ActivateForm() { return true; }

		public void CleanError(Control control)
		{
			ErrorMng_EP.Clear();
			
			Color back_color = ControlTools.Instance.BasicStyle.BackColor;

			if (control is TextBox)
			{
				if (((TextBox)control).ReadOnly) back_color = ControlTools.Instance.ReadOnlyStyle.BackColor;
			}

			control.BackColor = back_color;
		}

        public new void CenterToScreen() { base.CenterToScreen(); }

		protected virtual void FormatForm()
		{
			SuspendLayout();

			try { _show_colors = SettingsMng.Instance.GetFormatGridsSetting(); } catch { }
			FormatControls();

			ResumeLayout(true);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			if ((this.Tag != null) && (this.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) return;

			FormatControls(Controls);

			FitColumns();
		}

		private void FormatControls(Control.ControlCollection controls)
		{
			foreach (Control ctl in controls)
			{
				if (ctl is SplitContainer)
				{
					if ((ctl.Tag == null) || (ctl.Tag.ToString().ToUpper() != Resources.Consts.NO_FORMAT))
						FormatControl(ctl);

					FormatControls(((SplitContainer)ctl).Panel1.Controls);
					FormatControls(((SplitContainer)ctl).Panel2.Controls);
				}
				else if (ctl is Panel)
				{
					if ((ctl.Tag == null) || (ctl.Tag.ToString().ToUpper() != Resources.Consts.NO_FORMAT))
						FormatControl(ctl);

					FormatControls(((Panel)ctl).Controls);
				}
				else if (ctl is TabControl)
				{
					if ((ctl.Tag == null) || (ctl.Tag.ToString().ToUpper() != Resources.Consts.NO_FORMAT))
						FormatControl(ctl);

					foreach (TabPage page in ((TabControl)(ctl)).TabPages)
						FormatControls(page.Controls);
				}
				else if (ctl is GroupBox)
				{
					if ((ctl.Tag == null) || (ctl.Tag.ToString().ToUpper() != Resources.Consts.NO_FORMAT))
						FormatControl(ctl);

					FormatControls(((GroupBox)(ctl)).Controls);
				}
				else
				{
					if ((ctl.Tag != null) && (ctl.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) continue;
					FormatControl(ctl);
				}
			}
		}

		protected virtual void FormatControl(Control ctl)
		{
			Type ctlType = ctl.GetType();

			if (ctl is DataGridView)
			{
				((DataGridView)ctl).ColumnHeadersDefaultCellStyle = ControlTools.Instance.HeaderStyle;
				((DataGridView)ctl).ColumnHeadersHeight = 34;
				((DataGridView)ctl).RowHeadersWidth = 25;
				((DataGridView)ctl).DefaultCellStyle = ControlTools.Instance.BasicStyle;
				((DataGridView)ctl).BackgroundColor = System.Drawing.SystemColors.ControlLight;
				((DataGridView)ctl).EditMode = DataGridViewEditMode.EditOnKeystroke;
				((DataGridView)ctl).SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
				((DataGridView)ctl).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

				foreach (DataGridViewColumn col in ((DataGridView)ctl).Columns)
				{
					if ((col.Tag != null) && (col.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) continue;

					if ((col.DefaultCellStyle.Format == string.Empty) && (col.ValueType != null))
					{
						if (col.ValueType.Equals(typeof(long)))
							col.DefaultCellStyle.Format = ControlTools.Instance.LongStyle.Format;
						else if (col.ValueType.Equals(typeof(double)))
							col.DefaultCellStyle.Format = ControlTools.Instance.DecimalStyle.Format;
						else if (col.ValueType.Equals(typeof(decimal)))
							col.DefaultCellStyle.Format = ControlTools.Instance.DecimalStyle.Format;
						else if (col.ValueType.Equals(typeof(DateTime)))
						{
							if (col.DefaultCellStyle.Format == "d")
							{
								col.DefaultCellStyle = ControlTools.Instance.DateStyle;
							}
							else
							{
								col.Width = 95;
								col.DefaultCellStyle = ControlTools.Instance.DateTimeStyle;
							}
						}
					}

					if (col.ReadOnly)
					{
						col.DefaultCellStyle.ForeColor = Color.Black;
						col.DefaultCellStyle.BackColor = Color.Gainsboro;
					}
				}

				if (((DataGridView)ctl).Enabled == false)
				{
					((DataGridView)ctl).DefaultCellStyle.ForeColor = Color.Black;
					((DataGridView)ctl).DefaultCellStyle.BackColor = Color.Gainsboro;
					((DataGridView)ctl).TabStop = false;
					((DataGridView)ctl).Enabled = true;
				}
			}
			else
			{
				switch (ctl.GetType().Name)
				{
					case "Button":
						{
							ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Bold);
							ctl.ForeColor = Color.FromArgb(0, 0, 0);
						}
						break;

					case "Label":
						{
							ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
						}
						break;

					case "MaskedTextBox":
						{
							ctl.ForeColor = Color.FromArgb(0, 0, 192);

							if ((ctl.Name == "Codigo_TB") || (ctl.Name == "Numero_TB"))
							{
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Bold);
								ctl.Width = 80;
								((MaskedTextBox)(ctl)).TextAlign = HorizontalAlignment.Center;
								((MaskedTextBox)(ctl)).ReadOnly = true;
							}
							else
								ctl.Font = new Font("Tahoma", (float)8.25);

							if (((MaskedTextBox)ctl).ReadOnly)
							{
								ctl.BackColor = Color.WhiteSmoke;
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
								ctl.ForeColor = Color.Black;
								ctl.TabStop = false;
							}
						}
						break;

					case "TextBox":
						{
							ctl.ForeColor = Color.FromArgb(0, 0, 192);

							if ((ctl.Name == "Codigo_TB") || (ctl.Name == "Numero_TB"))
							{
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Bold);
								ctl.Width = 80;
								((TextBox)(ctl)).TextAlign = HorizontalAlignment.Center;
								((TextBox)(ctl)).ReadOnly = true;
							}
							else
								ctl.Font = new Font("Tahoma", (float)8.25);

							if (((TextBox)ctl).ReadOnly)
							{
								ctl.BackColor = Color.WhiteSmoke;
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
								ctl.ForeColor = Color.Black;
								ctl.TabStop = false;
							}
						}
						break;

					case "RichTextBox":
						{
							ctl.ForeColor = Color.FromArgb(0, 0, 192);
							ctl.Font = new Font("Tahoma", (float)8.25);

							if (((RichTextBox)ctl).ReadOnly)
							{
								ctl.BackColor = Color.WhiteSmoke;
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
								ctl.ForeColor = Color.Black;
								ctl.TabStop = false;
							}
						}
						break;

					case "NumericTextBox":
						{
							ctl.ForeColor = Color.FromArgb(0, 0, 192);
							ctl.Font = new Font("Tahoma", (float)8.25);
							((TextBox)(ctl)).TextAlign = HorizontalAlignment.Right;

							if (((TextBox)ctl).ReadOnly)
							{
								ctl.BackColor = Color.WhiteSmoke;
								ctl.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
								ctl.ForeColor = Color.Black;
								ctl.TabStop = false;
							}
						}
						break;

					case "ListBox":
					case "ComboBox":
						{
							ctl.Font = new Font("Tahoma", (float)8.25);
							ctl.ForeColor = Color.FromArgb(0, 0, 192);
						}
						break;

					case "DateTimePicker":
					case "mQDateTimePicker":
						{
							((DateTimePicker)ctl).Font = new Font("Tahoma", (float)8.25);
							((DateTimePicker)ctl).CalendarForeColor = Color.FromArgb(0, 0, 192);
							((DateTimePicker)ctl).ShowCheckBox = true;
							if (((DateTimePicker)ctl).Format != DateTimePickerFormat.Custom)
							{
								((DateTimePicker)ctl).Width = 115;
								((DateTimePicker)ctl).Format = DateTimePickerFormat.Short;
							}
						}
						break;

					case "PictureBox":
						{
							((PictureBox)ctl).SizeMode = PictureBoxSizeMode.Zoom;
							((PictureBox)ctl).BorderStyle = BorderStyle.FixedSingle;
						}
						break;
				}
			}
		}

		public virtual void FitColumns() {}

		public void MarkError(Control control, string message)
		{ 
			ErrorMng_EP.SetError(control, message);
			
			int padding = ErrorMng_EP.GetIconPadding(control);

			if (control is TextBox) 
			{
				if (((TextBox)control).ReadOnly) padding += 30;
			}

			ErrorMng_EP.SetIconPadding(control, padding);
			control.BackColor = Color.FromArgb(255, 192, 192);
		}

        /// <summary>
        /// Maximiza el tamaño del formulario al total del area cliente.
		/// Si utilizamos el Maximize lo aplica a todos los formularios abiertos
        /// </summary>
        protected virtual void MaximizeForm() { MaximizeForm(new Size(0, 0)); }
		public void MaximizeForm(int width, int height) { MaximizeForm(new Size(width, height)); }
		public void MaximizeForm(Size max_size)
        {
			SuspendLayout();

			int hOffset = 5;
			int wOffset = 5;

            if ((this.ParentForm == null) || (ParentForm == MainBaseForm.Instance))
            {
				foreach (Control ctl in MainBaseForm.Instance.Controls)
                {
					if (!ctl.Visible) continue;
					if (((ctl is ToolStrip) && (!_is_modal)) ||
						(ctl is MenuStrip) ||
                        (ctl is StatusStrip))
                        hOffset += ctl.Height;
                }
            }
            else
            {
                foreach (Control ctl in ParentForm.Controls)
                {
					if (!ctl.Visible) continue;
					if (((ctl is ToolStrip) && (!_is_modal)) ||
						(ctl is MenuStrip) ||
                        (ctl is StatusStrip))
                        hOffset += ctl.Height;
                }
            }

			//Corrección al tamaño máximo solicitado
			if ((max_size.Width != 0) && (max_size.Width < MainBaseForm.Instance.ClientSize.Width - wOffset))
			{
				this.Width = max_size.Width;
				wOffset = 0;
			}
			else
			{
				this.Left = MainBaseForm.Instance.ClientRectangle.X;
				this.Width = MainBaseForm.Instance.ClientSize.Width - wOffset;
			}

			if ((max_size.Height != 0) && (max_size.Height < MainBaseForm.Instance.ClientSize.Height - hOffset))
			{
				this.Height = max_size.Height;
				hOffset = 0;
			}
			else
			{
				this.Height = MainBaseForm.Instance.ClientSize.Height - hOffset;
				this.Top = MainBaseForm.Instance.ClientRectangle.Y;
			}

			if (_is_modal)
			{
				CenterToScreen();
			}
			else if ((wOffset == 0) || (hOffset == 0))
			{
				CenterToParent();
			}
			
			ResumeLayout(true);
        }

		/// <summary>
		/// Adapta el alto del formulario al espacio que deja otro formulario
		/// </summary>
		/// <param name="form">Formulario con el que se comparte espacio</param>
		protected void ResizeHeight(Form form)
		{
			List<Form> list = new List<Form>();
			list.Add(form);
			ResizeHeight(list);
		}
		protected void ResizeHeight(List<Form> fvisibles)
		{
			SuspendLayout();

			if (this.ParentForm == null) return;

			foreach (Form f in fvisibles)
				this.Height = this.ParentForm.ClientSize.Height - f.Height - 5;

			this.Top = this.ParentForm.ClientRectangle.Top;
			foreach (Control ctl in ParentForm.Controls)
			{
				if (((ctl is MenuStrip) ||
					(ctl is ToolStrip) ||
					(ctl is StatusStrip))
					&& (ctl.Visible))
					this.Height -= ctl.Height;
			}

			ResumeLayout();
		}

		protected virtual void ActivateAction(molAction action, bool state) { }
		protected virtual void EnableAction(molAction action, bool state) { }

		protected void HideAction(molAction action) { ActivateAction(action, false); }        
		protected void ShowAction(molAction action) { ActivateAction(action, true); }        

        #endregion

        #region Source

        /// <summary>
		/// Refresca todos los datos secundarios asociados a la entidad gestionada
		/// por el formulario.
		/// </summary>
        public virtual void RefreshSecondaryData() {}

		/// <summary>
		/// Asigna la entidad y sus hijos al origen de datos
		/// <returns>void</returns>
		/// </summary>
		protected virtual void RefreshMainData() {}

        protected virtual void RefreshSources() {}

        #endregion

		#region Business Methods

		protected void ShowReport(ReportClass rpt)
		{
			if (rpt != null)
			{
				ReportViewer.SetReport(rpt);
				ReportViewer.ShowDialog();
			}
			else
			{
				PgMng.ShowInfoException(Resources.Messages.NO_DATA_REPORTS);
			}
		}

		protected void PrintReport(ReportClass rpt)
        {
            PrintReport(rpt, SettingsMng.Instance.GetDefaultPrinter());
		}
        protected void PrintReport(ReportClass rpt, int nCopies)
        {
            PrintReport(rpt, SettingsMng.Instance.GetDefaultPrinter(), nCopies);
        }
        protected void PrintReport(ReportClass rpt, string printerName)
        {
            PrintReport(rpt, printerName, 1);
        }
        protected void PrintReport(ReportClass rpt, string printerName, int nCopies)
        {
            if (rpt != null)
            {
                try
                {
                    System.Drawing.Printing.PrinterSettings printer_settings = new System.Drawing.Printing.PrinterSettings();
                    printer_settings.PrinterName = printerName;
                    printer_settings.Copies = (short)nCopies;
                    System.Drawing.Printing.PageSettings page_settings = new System.Drawing.Printing.PageSettings(printer_settings);

                    CrystalDecisions.CrystalReports.Engine.ReportClass report = rpt;
                    report.PrintToPrinter(printer_settings, page_settings, false);
                }
                catch
                {
                    rpt.PrintToPrinter(nCopies, true, 0, 0);
                }
            }
            else
            {
                PgMng.ShowInfoException(Resources.Messages.NO_DATA_REPORTS);
            }
        }

		protected void ExportPDF(ReportClass rpt, string output_file_name)
		{
			if (rpt != null)
			{
				ExportOptions options = new ExportOptions();
				DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();

                SaveFile_SFD.InitialDirectory = SettingsMng.Instance.GetPDFPrintsFolder();
				SaveFile_SFD.FileName = output_file_name;
                SaveFile_SFD.AddExtension = true;
                SaveFile_SFD.DefaultExt = "pdf";

				if (SaveFile_SFD.ShowDialog() == DialogResult.OK)
				{
					PgMng.Grow(String.Format(Face.Resources.Messages.EXPORTING_PDF, SaveFile_SFD.FileName), string.Empty);

					diskFileDestinationOptions.DiskFileName = SaveFile_SFD.FileName;
					options.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
					options.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
					options.ExportDestinationOptions = diskFileDestinationOptions;

					PgMng.Grow();

					rpt.Export(options);
				}
			}
			else
			{
				PgMng.ShowInfoException(Resources.Messages.NO_DATA_REPORTS);

			}
		}

		#endregion

		#region Actions

		public virtual void DoExecuteAction(molAction action)
		{
			switch (action)
			{
				case molAction.Cancel:

					DialogResult = DialogResult.Cancel;
					CancelAction();
					break;

				case molAction.CancelBkJob:

					CancelBkJobAction();
					break;

				case molAction.Default:

					//Para permitir que se ejecute la accion por defecto
					Status = EStatus.OK;

					DefaultAction();
					break;

				case molAction.Print:

					PrintAction();
					break;

				case molAction.Submit:

					SubmitAction();
					if (_action_result == DialogResult.OK) DialogResult = DialogResult.OK;
					break;
			}
		}

		public void ExecuteAction(molAction action) { ExecuteAction(action, false); }

		public virtual void ExecuteAction(molAction action, bool nested)
        {
            try
            {
#if TRACE
				PgMng.Record(String.Format("ChildForm::ExecuteAction {0} INI", action.ToString()));
#endif
				if (!nested)
				{
					if (Status == EStatus.Working) return;

					Status = EStatus.Working;

					_current_action = action;

					//Se usa un atributo porque si uso el DialogResult el ShowDialog entiende que quiero cerrar el formulario
					_action_result = DialogResult.Ignore;
				}

				DoExecuteAction(action);
#if TRACE
				PgMng.Record(String.Format("ChildForm::ExecuteAction {0} END", action.ToString()));
#endif
            }
			catch (iQImplementationException ex)
			{
				PgMng.ShowInfoException(ex);
			}
			catch (Exception ex)
			{
				PgMng.ShowErrorException(ex);
			}
            finally
            {
				Status = EStatus.OK;
				//EnableForm(true);
				PgMng.FillUp();				
            }
        }

        protected virtual void SubmitAction() { throw new iQImplementationException("SubmitAction"); }
        protected virtual void CancelAction() { Close(); }
        protected virtual void PrintAction() { throw new iQImplementationException("PrintAction"); }
        protected virtual void DefaultAction() { ExecuteAction(molAction.Cancel); }
        protected virtual void CancelBkJobAction() { CancelBackGroundJob(); }

        #endregion

        #region Buttons

        private void CancelBkJob_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.CancelBkJob); }

        #endregion

        #region Events

		protected virtual void EnableEvents(bool enable)
		{
			EventsEnabled = enable;
		}

        private void ChildForm_Load(object sender, EventArgs e)
		{
#if TRACE       
            if (PgMng != null) PgMng.Record("ChildForm::OnLoad INI"); 
#endif
            InitForm();
#if TRACE
			if (PgMng != null) PgMng.Record("ChildForm::OnLoad END"); 
#endif
        }

        private void ChildForm_Shown(object sender, EventArgs e)
        {
           if ((PgMng != null) && (BarSteps > 2)) PgMng.FillUp();
        }

        private void ChildForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.MaximizeForm();
            }
        }

		private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				DisposeForm();
			}
			catch { }
		}

		#endregion
    }
}