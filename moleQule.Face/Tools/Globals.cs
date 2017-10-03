using System;
using System.Windows.Forms;

namespace moleQule.Face
{
	public class Globals
	{

		#region Atributos

		/// <summary>
		/// Única instancia de la clase Globals (Singleton)
		/// </summary>
		private static Globals _singleton;

		/// <summary>
		/// Devuelve la instancia única del MainBaseForm
		/// </summary>
		public static Globals Instance { get { return _singleton != null ? _singleton : new Globals(); } }

		private moleQule.Library.Timer _timer;

		private Form _main_form;
		private ProgressInfoForm _pg_form;
		private ProgressInfoMng _pg_mng = null;
		private Cursor _cursor;
		private StatusStrip _status_bar;
		private ToolStripStatusLabel _legend_label;
		private ToolStripStatusLabel _status_label;
		private ToolStripStatusLabel _anim_label;
		private ToolStripProgressBar _progress_bar;

		public moleQule.Library.Timer Timer { get { return _timer; } }

		public Form MainForm
		{
			get { return _main_form; }
			set { _main_form = value; }
		}
		public ProgressInfoForm ProgressInfoForm
		{
			get { return _pg_form; }
			set { _pg_form = value; }
		}
		public ProgressInfoMng ProgressInfoMng
		{
			get { return _pg_mng; }
			set { _pg_mng = value; }
		}
		public Cursor Cursor
		{
			get { return _cursor; }
			set { _cursor = value; }
		}
		public StatusStrip StatusBar
		{
			get { return _status_bar; }
			set { _status_bar = value; }
		}
		public ToolStripStatusLabel LegendLabel
		{
			get { return _legend_label; }
			set { _legend_label = value; }
		}
		public ToolStripStatusLabel StatusLabel
		{ 
			get { return _status_label;  }
			set { _status_label = value; }
		}
		public ToolStripStatusLabel AnimLabel
		{
			get { return _anim_label; }
			set { _anim_label = value; }
		}
		public ToolStripProgressBar ProgressBar
		{
			get { return _progress_bar; }
			set { _progress_bar = value; }
		}

		public void Refresh()
		{
			if (_main_form != null)
				_main_form.Refresh();

			if (_pg_form != null)
				_pg_form.Refresh();

			Application.DoEvents();
		}

		#endregion

		#region Factory Methods

		public Globals()
		{
			// Singleton
			_singleton = this;

			_timer = Library.Timer.Instance;
		}

		#endregion

	}
}