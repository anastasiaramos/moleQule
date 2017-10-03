using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    /// <summary>
    /// Clase base para manejo (apertura y cierre) de formularios
    /// Es único en el sistema (singleton)
    /// </summary>
    /// <remarks>
    /// Para utilizar el FormMng es necesario indicar cual será el MainForm padre de los formularios
    /// Este MainForm deberá ser un formulario heredado de MainFormBase
    /// </remarks>
    public class FormMng : IFormMng
    {
        #region Factory Methods

        /// <summary>
        /// Única instancia de la clase MainBaseForm (Singleton)
        /// </summary>
        protected static FormMng _main;

        /// <summary>
        /// Unique FormMng Class Instance
        /// </summary>
        /// <remarks>
        /// Para utilizar el FormMng es necesario inicializar el MainForm padre de los formularios
        /// </remarks>
        public static FormMng Instance { get { return (_main != null) ? _main : new FormMng(); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormMng()
		{
			// Singleton
			_main = this;
		}

        #endregion

        #region Business Methods

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        public void OpenForm(string formID) { OpenForm(formID, null, null); }
		public void OpenForm(string formID, object param) { OpenForm(formID, new object[1] { param }); }

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        /// <param name="parameters">Parámetro para el formulario</param>
        public void OpenForm(string formID, object[] parameters, Form parent)
        {
            try
            {
                switch (formID)
                {
					case AyudaMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(AyudaMngForm.Type))
							{
								AyudaMngForm em = new AyudaMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case CargoUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CargoUIForm.Type))
                            {
                                CargoUIForm em = new CargoUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BankAccountMngForm.ID:
                        {
							if (!FormMngBase.Instance.BuscarFormulario(BankAccountMngForm.Type))
                            {
								BankAccountMngForm em = new BankAccountMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case CompanyMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(CompanyMngForm.Type))
							{
								CompanyMngForm em = new CompanyMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

					case CompanySelectForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(CompanySelectForm.Type))
							{
								CompanySelectForm em = new CompanySelectForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

					case CurrencyExchangeUIForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(CurrencyExchangeUIForm.Type))
							{
								CurrencyExchangeUIForm em = new CurrencyExchangeUIForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;


                    case ImpuestoUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ImpuestoUIForm.Type))
                            {
                                ImpuestoUIForm em = new ImpuestoUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case IRPFUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(IRPFUIForm.Type))
                            {
                                IRPFUIForm em = new IRPFUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case SubtipoFacturaUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(SubtipoFacturaUIForm.Type))
                            {
                                SubtipoFacturaUIForm em = new SubtipoFacturaUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case LineaRegistroMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							LineaRegistroMngForm em = new LineaRegistroMngForm(parent, (ETipoRegistro)parameters[0], (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

                    case MunicipioMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(MunicipioMngForm.Type))
                            {
                                MunicipioMngForm em = new MunicipioMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case RegistroMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							RegistroMngForm em = new RegistroMngForm(parent, (ETipoRegistro)parameters[0], (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

					case PesajeMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(PesajeMngForm.Type))
							{
								PesajeMngForm em = new PesajeMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case CreditCardUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CreditCardUIForm.Type))
                            {
                                CreditCardUIForm em = new CreditCardUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case TPVUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(TPVUIForm.Type))
                            {
                                TPVUIForm em = new TPVUIForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    default:
                        {
                            throw new iQImplementationException(string.Format(moleQule.Face.Resources.Messages.FORM_NOT_FOUND, formID), string.Empty);
                        }
                }
            }
            catch (iQImplementationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
				if (Globals.Instance.ProgressInfoMng != null)
				{
					Globals.Instance.ProgressInfoMng.ShowErrorException(ex);
					Globals.Instance.ProgressInfoMng.FillUp();
				}
				else
					ProgressInfoMng.ShowException(ex);
            }
        }

        /// <summary>
        /// Devuelve un formulario hijo del tipo pasado como parámetro
        /// </summary>
        /// <param name="childType">Tipo de formulario</param>
        public object GetFormulario(Type childType) { return FormMngBase.Instance.GetFormulario(childType); }

        #endregion
    }
}
