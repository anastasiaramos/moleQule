using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Face.Common
{
	public partial class CompanyUIForm : CompanyForm
	{
		#region Business Methods

		protected Company _entity;

		public override Company Entity { get { return _entity; } set { _entity = value; } }
		public override CompanyInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public CompanyUIForm() 
			: this(-1, null, true, null) {}

		public CompanyUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;
            this.Datos_Contactos.RaiseListChangedEvents = false;

			Company temp = _entity.Clone();
            temp.ApplyEdit();

			// do the save
			try
			{
				_entity = temp.Save();
				_entity.ApplyEdit();

				// Se modifica el nombre de la foto
				if (_entity.Logo == "00.bmp")
				{
					Images.Rename(_entity.Logo, _entity.Code + ".bmp", Library.Common.ModuleController.LOGOS_EMPRESAS_PATH);
					_entity.Logo = _entity.Code + ".bmp";
					_entity.Save();
				}

				return true;
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
				this.Datos_Contactos.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region Style & Source

		/// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
		public override void FormatControls()
		{
			base.FormatControls();
		}

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			Datos_Contactos.DataSource = ContactoEmpresas.SortList(_entity.Contactos, "Nombre", ListSortDirection.Ascending);
        }

        #endregion

		#region Validation & Format

		private void ID_TB_Validated(object sender, EventArgs e)
		{
			FormatData();
		}

		#endregion

        #region Actions

        protected override void SaveAction()
        {
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            if (_action_result == DialogResult.OK)
            {
				if (AppContext.Principal.ActiveSchema != null)
				{
					if (_entity.Oid == AppContext.Principal.ActiveSchema.Oid)
						AppContext.Principal.ActiveSchema = (ISchemaInfo) _entity.GetInfo();
				}
            }
        }

		protected override void SelectTipoIDAction()
		{
			if (TipoID_CB.SelectedItem == null) return;

			ETipoID tipo = (ETipoID)(long)TipoID_CB.SelectedValue;
			MascaraID_Label.Text = AgenteBase.GetTipoIDMask(tipo);
		}

		#endregion

		#region Buttons

		private void CIF_RB_Click(object sender, EventArgs e)
		{
			Entity.TipoID = (long)TipoId.CIF;
			MascaraID_Label.Text = "X12345678";
		}

		private void NIF_RB_Click(object sender, EventArgs e)
		{
			Entity.TipoID = (long)TipoId.NIF;
			MascaraID_Label.Text = "12345678-X";
		}

		private void NIE_RB_Click(object sender, EventArgs e)
		{
			Entity.TipoID = (long)TipoId.NIE;
			MascaraID_Label.Text = "X1234567-X";
		}

		private void Otros_RB_Click(object sender, EventArgs e)
		{
			Entity.TipoID = (long)TipoId.OTROS;
			MascaraID_Label.Text = string.Empty;
		}

		private void Country_BT_Click(object sender, EventArgs e)
		{
			SelectInputForm form = new SelectInputForm(Country.Load());

			if (DialogResult.OK == form.ShowDialog())
			{
				Country item = form.Selected as Country;
				_entity.CountryIso2 = item.Iso2;
			}
		}

		private void Currency_BT_Click(object sender, EventArgs e)
		{
			SelectInputForm form = new SelectInputForm(Currency.Load());

			if (DialogResult.OK == form.ShowDialog())
			{
				Currency item = form.Selected as Currency;
				_entity.SetCurrency(item.ISOCode);
			}
		}

        private void Examinar_Button_Click(object sender, EventArgs e)
        {
            if (Browser.ShowDialog() == DialogResult.OK)
			{
				Entity.Logo = Entity.Code + ".bmp";
                Images.Save(Browser.FileName, Library.Common.ModuleController.LOGOS_EMPRESAS_PATH, Entity.Logo);
			}

			Images.Show(Entity.Logo, Library.Common.ModuleController.LOGOS_EMPRESAS_PATH, Logo_PictureBox);
        }

        private void Ninguno_Button_Click(object sender, EventArgs e)
        {
			Images.Delete(Entity.Logo, Library.Common.ModuleController.LOGOS_EMPRESAS_PATH);
			Entity.Logo = string.Empty;
			Images.Show(Entity.Logo, Library.Common.ModuleController.LOGOS_EMPRESAS_PATH, Logo_PictureBox);
        }

        private void Localidad_BT_Click(object sender, EventArgs e)
        {
            MunicipioSelectForm form = new MunicipioSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                MunicipioInfo item = (MunicipioInfo)form.Selected;

                if (item == null) return;

                _entity.CodPostal = item.CodPostal;
                _entity.Municipio = item.Nombre;
                _entity.Provincia = item.Provincia;
            }
        }

        private void Cuenta_BT_Click(object sender, EventArgs e)
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				_entity.CuentaBancaria = cuenta.Valor;
            }
        }

        #endregion
	}
}

