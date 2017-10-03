using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Face.Skin01
{
    public partial class SelectSkinForm : moleQule.Face.SelectBaseForm
    {
        #region Business Methods

        #endregion

        #region Factory Methods

        public SelectSkinForm()
            : this(true)
        {
            InitializeComponent();
        }

        public SelectSkinForm(bool isModal)
            : base(isModal)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

		public override void FormatControls()
		{
			base.FormatControls();

			int botones = 0, espacio = 3, tab, pos = 0;
			int formWidth = this.Width;
			int formHeight = Paneles2.Panel1.Height;
			int buttonWidth = Submit_BT.Size.Width;
			int buttonHeight = Submit_BT.Size.Height;

			foreach (Control ctl in Paneles2.Panel1.Controls)
			{
				if ((ctl.GetType().Name == "Button") && ctl.Visible)
					botones++;
			}

			tab = (formWidth - espacio * (botones - 1) - buttonWidth * botones) / 2;

			foreach (Control ctl in Paneles2.Panel1.Controls)
			{
				if ((ctl.GetType().Name == "Button") && ctl.Visible)
				{
					int x = tab + (espacio + buttonWidth) * pos;
					int y = (formHeight - buttonHeight) / 2;

					ctl.SetBounds(x, y, buttonWidth, buttonHeight);
					pos++;
				}
			}
		}

        protected override void FormatControl(Control ctl)
        {
			if ((ctl.Tag != null) && (ctl.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) return;

			base.FormatControl(ctl);

			Type ctlType = ctl.GetType();
            switch (ctl.GetType().Name)
            {
                case "DataGridView":
                    {
                        ((DataGridView)ctl).BackgroundColor = System.Drawing.SystemColors.AppWorkspace;

                        foreach (DataGridViewColumn col in ((DataGridView)ctl).Columns)
                        {
                            col.DefaultCellStyle.BackColor = Color.White;
                        }

                    } break;
            }
        }

        protected new void MaximizeForm()
        {
            Form form = Globals.Instance.MainForm;

            this.Height = form.ClientSize.Height - 5;
            this.Top = form.ClientRectangle.Top + 90;
            foreach (Control ctl in form.Controls)
            {

                if (((ctl is MenuStrip) ||
                    (ctl is ToolStrip) ||
                    (ctl is StatusStrip))
                    && (ctl.Visible))
                    this.Height -= ctl.Height;
            }
            this.Left = form.ClientRectangle.Left + 2;
            this.Width = form.ClientSize.Width - 5;


            int botones = 0, espacio = 3, tab, pos = 0;
            int formWidth = PanelesV.Panel2.Width;
            int formHeight = PanelesV.Panel2.Height;
            int buttonWidth = Submit_BT.Size.Width;
            int buttonHeight = Submit_BT.Size.Height;

            foreach (Control ctl in Paneles2.Panel1.Controls)
            {
                if ((ctl.GetType().Name == "Button") && ctl.Visible)
                    botones++;
            }

            tab = (formWidth - espacio * (botones - 1) - buttonWidth * botones) / 2;

            foreach (Control ctl in Paneles2.Panel1.Controls)
            {
                if ((ctl.GetType().Name == "Button") && ctl.Visible)
                {
                    int x = tab + (espacio + buttonWidth) * pos;
                    int y = (formHeight - buttonHeight) / 2;

                    ctl.SetBounds(x, y, buttonWidth, buttonHeight);
                    pos++;
                }
            }
        }

        #endregion

        #region Buttons

        private void Aceptar_Button_Click(object sender, EventArgs e)
        {
            SubmitAction();

            //No deberían estar aquí por si falla la introducción de los datos
            //Se pone en los hijos
            //DialogResult = DialogResult.OK;
            //Close();
        }

        private void Cancelar_Button_Click(object sender, EventArgs e)
        {
            CancelAction();

            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}

