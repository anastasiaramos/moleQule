using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace moleQule.Face.Skin01
{
    /// <summary>
    /// Clase Base para Búsqueda y Filtrado de Elementos de un Tipo de Entidad
    /// </summary>
	public partial class LocalizeSkinForm : moleQule.Face.LocalizeForm
    {

        #region Factory Methods

		public LocalizeSkinForm()
        {
            InitializeComponent();
			this.Dock = DockStyle.Bottom;
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			base.FormatControls();

			int radioButton = 0, espacio = 50, tab, pos = 0;
            int formWidth = Campos_Groupbox.Width;
            int formHeight = Campos_Groupbox.Height;
            int rbWidth = 100;
            int rbHeight = 20;

            foreach (Control ctl in Campos_Groupbox.Controls)
            {
                Type ctlType = ctl.GetType();

                if (ctl.GetType().Name == "RadioButton")
                    radioButton++;
            }

            tab = (formWidth - espacio * (radioButton - 1) - rbWidth * radioButton) / 2;

            foreach (Control ctl in Campos_Groupbox.Controls)
            {
                Type ctlType = ctl.GetType();

                if (ctl.GetType().Name == "RadioButton")
                {
                    int x = tab + (espacio + rbWidth) * pos;
                    int y = ((formHeight - rbHeight) / 2) + 10;

                    ctl.SetBounds(x, y, rbWidth, rbHeight);
                    pos++;
                }
            }
        }

        #endregion

        #region Buttons

        protected virtual void Buscar_Button_Click(object sender, EventArgs e)
        {
            Find(Valor_TB.Text);
        }

        protected virtual void Filtrar_Button_Click(object sender, EventArgs e)
		{
			Filter(Valor_TB.Text);
		}

        #endregion

		#region Events

		private void LocalizeSkinForm_Paint(object sender, PaintEventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
		}

		#endregion

	}
}