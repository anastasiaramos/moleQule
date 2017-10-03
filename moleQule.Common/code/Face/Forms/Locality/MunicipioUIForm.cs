using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class MunicipioUIForm : MunicipioForm
    {
        #region Attributes & Properties
		
        public new const string ID = "MunicipioUIForm";
		public new static Type Type { get { return typeof(MunicipioUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Municipio _entity;

        public override Municipio Entity { get { return _entity; } set { _entity = value; } }
        public override MunicipioInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

		public MunicipioUIForm(long oid, Form parent)
			: this(oid, null, true, parent) { }

		public MunicipioUIForm(Municipio entity, Form parent)
            : this(-1, new object[1] { entity }, true, parent) {}

		public MunicipioUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                Municipio temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();
					

                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine +
                                    ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }

        #endregion

        #region Style & Source

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
						
            base.RefreshMainData();
        }
		
		/// <summary>
        /// Asigna los datos de origen para controles que dependen de otros
        /// </summary>
        /// <param name="controlName"></param>
        protected override void SetDependentControlSource(string controlName)
        {
            /*switch (controlName)
            {
                case "ID_GB":
                    {
                        NIF_RB.Checked = (EntityInfo.TipoId == (long)TipoId.NIF);
                        NIE_RB.Checked = (EntityInfo.TipoId == (long)TipoId.NIE);
                        DNI_RB.Checked = (EntityInfo.TipoId == (long)TipoId.DNI);

                    } break;
            }*/
        }
		
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

    }
}
