using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using Csla;
using moleQule.Face;
using moleQule.Library;

namespace moleQule.Face.Common
{
	public partial class SelectInputForm : Skin01.InputSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "SelectInputForm";
        public static Type Type { get { return typeof(SelectInputForm); } }

        private object _selected;

		public object Selected { get { return _selected; } set { _selected = value; } }

		private Type _tipo;

		private SortedBindingList<Country> _countries;
		private SortedBindingList<Currency> _currencies;

        #endregion

        #region Factory Methods

		public SelectInputForm()
			: this(true) {}

        public SelectInputForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();

            SetFormData();
        }

		public SelectInputForm(List<Country> list)
			: base(true)
		{
			InitializeComponent();

			_countries = new SortedBindingList<Country>(list);
			_countries.ApplySort("Name", ListSortDirection.Ascending);

			_tipo = typeof(Country);

			this.Text = Resources.Labels.SELECT_COUNTRY_TITLE;

			SetFormData();
		}

		public SelectInputForm(List<Currency> list)
			: base(true)
		{
			InitializeComponent();

			_currencies = new SortedBindingList<Currency>(list);
			_currencies.ApplySort("Name", ListSortDirection.Ascending);

			_tipo = typeof(Currency);

			this.Text = Resources.Labels.SELECT_CURRENCY_TITLE;

			SetFormData();
		}

        #endregion

        #region Style & Source

        public override void FormatControls()
        {
            base.FormatControls();

            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Nombre.Tag = 1;

            cols.Add(Nombre);

            ControlsMng.MaximizeColumns(Tabla, cols);
            Tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		protected override void RefreshMainData()
		{
			if (_tipo == typeof(Country))
			{
				Datos.DataSource = _countries;

				Nombre.DataPropertyName = "Name";

				Numero.DataPropertyName = "Iso2";
				Numero.Visible = true;
			}
			else if (_tipo == typeof(Currency))
			{
				Datos.DataSource = _currencies;

				Nombre.DataPropertyName = "Name";

				Numero.DataPropertyName = "ISOCode";
				Numero.HeaderText = "Código";
				Numero.Width = 50;
				Numero.Visible = true;

				Comments.DataPropertyName = "Location";
				Comments.HeaderText = "País";
				Comments.Width = 150;
				Comments.Visible = true;

			}
			base.RefreshMainData();
		}

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (Datos.Current != null)
            {
				_selected = Datos.Current;
            }

            _action_result = DialogResult.OK;
        }

        #endregion

        #region Events

        private void Tabla_DoubleClick(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

        #endregion

    }
}

