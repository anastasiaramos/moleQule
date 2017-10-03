using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin01
{
	/// <summary>
	/// Clase Base para Selección de Datos para un Informe
	/// </summary>
	public partial class ReportSelectSkinForm : ReportSelectBaseForm
    {

		#region Business Methods

		public override PrintSource Source
		{
			get { return _source; }
			set
			{
				_source = value;
				Seleccion_RB.Checked = (_source == PrintSource.Selection);
				Todos_RB.Checked = (_source == PrintSource.All);
			}
		}

		#endregion

        #region Factory Methods

        public ReportSelectSkinForm() : this (true) {}

		public ReportSelectSkinForm(bool isModal)
            : base(isModal)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        #endregion

        #region Buttons

        private void Aceptar_Button_Click(object sender, EventArgs e)
        {
			AcceptAction();
        }

        private void Cancelar_Button_Click(object sender, EventArgs e)
        {
			CancelAction();
        }

        #endregion

        #region Events

		private void Source_GB_Validated(object sender, EventArgs e)
		{
			_source = Seleccion_RB.Checked ? PrintSource.Selection : PrintSource.All;
			
			Entidad_CB.Enabled = !Todos_RB.Checked;

			if ((Seleccion_RB.Checked) && (Datos.Count <= 0))
			{
				Seleccion_RB.Checked = false;
				Todos_RB.Checked = true;

				MessageBox.Show(Resources.Messages.NO_DATA_ENTITY,
								Labels.EMPTY_ENTITY_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}
		}


		/*        public virtual void Seleccion_RB_CheckedChanged(object sender, EventArgs e)
				{


					//_source = Seleccion_RB.Checked ? PrintSource.Selection : PrintSource.All;
					//Entidad_CB.Enabled = !Todos_RB.Checked;
				}

				/*public virtual void Todos_RB_CheckedChanged(object sender, EventArgs e)
				{
					_source = Seleccion_RB.Checked ? PrintSource.Selection : PrintSource.All;
					Entidad_CB.Enabled = !Todos_RB.Checked;
				}*/

        #endregion

    }
}

