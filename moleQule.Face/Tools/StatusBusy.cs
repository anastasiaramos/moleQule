using System;
using System.Windows.Forms;

namespace moleQule.Face
{
	public class StatusBusy : IDisposable
	{
		private string _oldStatus = string.Empty;
		private Cursor _oldCursor = null;

		public string Text { set { Globals.Instance.StatusLabel.Text = value; } }

		public StatusBusy(string statusText)
		{
			if (Globals.Instance != null)
			{
				if (Globals.Instance.StatusLabel != null)
				{
					_oldStatus = Globals.Instance.StatusLabel.Text;
					Globals.Instance.StatusLabel.Text = statusText;
				}

				if (Globals.Instance.Cursor != null)
				{
					_oldCursor = Globals.Instance.Cursor;
					Globals.Instance.Cursor = Cursors.WaitCursor;
				}

				Globals.Instance.Refresh();
			}
		}

		// IDisposable
		private bool _disposedValue = false; // To detect redundant calls

		protected void Dispose(bool disposing)
		{
			if (_disposedValue) return;
			
			if (disposing)
			{
				if (Globals.Instance != null)
				{
                    if (Globals.Instance.StatusLabel != null)
					Globals.Instance.StatusLabel.Text = _oldStatus;
					Globals.Instance.Cursor = _oldCursor;
					Globals.Instance.Refresh();
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
	}
}
