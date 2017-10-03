using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Skin01;

namespace moleQule.Face
{
	public partial class RestoreInputBaseForm : InputSkinForm
	{

        #region Business Methods

		public const string ID = "RestoreInputBaseForm";
		public static Type Type { get { return typeof(RestoreInputBaseForm); } }

        #endregion

        #region Factory Methods

        public RestoreInputBaseForm()
			: this(true) {}

		public RestoreInputBaseForm(bool isModal)
            : base(isModal)
        {
			InitializeComponent();
			RefreshSecondaryData();
        }

        #endregion

		#region Layout & Source

		/// <summary>
		/// Obtiene los datos de origen de los combobox
		/// </summary>
		public override void RefreshSecondaryData()
		{
		}

		#endregion

        #region Actions

        protected virtual void RestoreBackup(string filename) { throw new iQImplementationException("RestoreInputForm::RestoreBackup()"); }

        #endregion

        #region Buttons

        protected override void SubmitAction()
		{
            if (File.Exists(Fichero_TB.Text))
            {
                RestoreBackup(Fichero_TB.Text);
            }
            else
                MessageBox.Show("No existe el fichero seleccionado");
		}

		private void Examinar_Button_Click(object sender, EventArgs e)
		{
            Browser.InitialDirectory = Application.StartupPath + AppControllerBase.BACKUP_PATH;

			if (Browser.ShowDialog() == DialogResult.OK)
			{
				Fichero_TB.Text = Browser.FileName;
			}
		}

		#endregion

		#region Events

		#endregion
	}
}

