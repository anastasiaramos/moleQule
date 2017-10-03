using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Common.Properties;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class ModuleController
	{
		#region Attributes & Properties

		Dictionary<ETipoEntidad, TEntidadRegistroBase> _active_entidades = new Dictionary<ETipoEntidad, TEntidadRegistroBase>();

		public Dictionary<ETipoEntidad, TEntidadRegistroBase> ActiveEntidades { get { return _active_entidades; } }

		public static string GetCuentasMask()
		{
			string mask = string.Empty;

			try
			{
				for (int i = 1; i <=  ModulePrincipal.GetNDigitosCuentasContablesSetting(); i++)
					mask += "0";
			}
			catch { mask = string.Empty; }

			return mask;
		}

		#endregion

		#region Factory Methods

		/// <summary>
		/// Única instancia de la clase ControlerBase (Singleton)
		/// </summary>
		protected static ModuleController _main;

		/// <summary>
		/// Unique Controler Class Instance
		/// </summary>
		public static ModuleController Instance { get { return (_main != null) ? _main : new ModuleController(); } }

		/// <summary>
		/// Contructor 
		/// </summary>
		protected ModuleController()
		{
			// Singleton
			_main = this;
			
			Init();
		}

		public void AutoPilot() {}

		private void Init()
		{
		}

		public static void CheckDBVersion()
		{
			ApplicationSettingInfo dbVersion = ApplicationSettingInfo.Get(Settings.Default.DB_VERSION_VARIABLE);

			//Version de base de datos equivalente o no existe la variable
			if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) == 0))
			{
				return;
			}
			//Version de base de datos superior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) > 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_HIGHER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
			//Version de base de datos inferior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) < 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_LOWER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
		}

		public void ClearEntities()
		{
			if (_active_entidades != null)
				_active_entidades.Clear();
		}

		public static void UpgradeSettings() { ModulePrincipal.UpgradeSettings(); }

		#endregion

		#region Settings

		public static string HELP_PATH { get { return Properties.Settings.Default.HELP_PATH; } }
		public static string LOGOS_EMPRESAS_PATH { get { return AppControllerBase.Reg32GetServerPath() + Properties.Settings.Default.LOGO_EMPRESA_PATH; } }

		#endregion

		#region Business Methods

		public void ActivateEntidad(ETipoEntidad tipo, string table, Type type, Type list_type)
		{
			TEntidadRegistroBase entidad = new TEntidadRegistroBase();
			entidad.Table = table;
			entidad.ETipoEntidad = tipo;
			entidad.Type = type;
			entidad.ListType = list_type;
			_active_entidades.Add(tipo, entidad);
		}

		public bool IsActive(ETipoEntidad tipo)
		{
			return _active_entidades[tipo].Active;
		}

		public TEntidadRegistroBase GetEntidad(ETipoEntidad tipo)
		{
			foreach (KeyValuePair<ETipoEntidad, TEntidadRegistroBase> item in _active_entidades)
				if (item.Value.ETipoEntidad == tipo)
					return item.Value;

			return default(TEntidadRegistroBase);
		}

		#endregion

		#region Scripts

		public static void ScaleListeningStart() { ScalesMng.Instance.ScaleListeningStart(); }

		public static void ScaleListeningStop() { ScalesMng.Instance.ScaleListeningStop(); }

		#endregion
	}

	public class ModuleDef : IModuleDef
	{
		public string Name { get { return "Common"; } }
		public Type Type { get { return typeof(Common.ModuleController); } }
		public Type[] Mappings 
		{ 
			get
			{
				return new Type[] 
                {   
					typeof(BankAccountMap),
                    typeof(CompanyMap),
                    typeof(CompanyContactMap),
                    typeof(CreditCardMap),
                    typeof(CreditCardStatementMap),
					typeof(CurrencyExchangeMap),
                    typeof(GrantMap),
                    typeof(GrantPeriodMap),
					typeof(InvoiceSubtypeMap),
					typeof(JobMap),                    
                    //typeof(IdiomaMap),
                    typeof(IRPFMap),
                    typeof(LocalityMap),
					typeof(MonitorMap),
					typeof(MonitorLineMap),
                    typeof(RelationMap),
                    typeof(RegistryMap),
					typeof(RegistryLineMap),
                    typeof(TaxMap),
					typeof(TPVMap),
                    typeof(WeighingMap),
                };
			}
		}

		public void GetEntities(Dictionary<Type, Type> recordEntities)
		{
			if (recordEntities.ContainsKey(typeof(BankAccount))) return;

			recordEntities.Add(typeof(BankAccount), typeof(BankAccountRecord));
			recordEntities.Add(typeof(Company), typeof(CompanyRecord));
			recordEntities.Add(typeof(ContactoEmpresa), typeof(CompanyContactRecord));
            recordEntities.Add(typeof(CreditCard), typeof(CreditCardRecord));
            recordEntities.Add(typeof(CreditCardStatement), typeof(CreditCardStatementRecord));
            recordEntities.Add(typeof(CurrencyExchange), typeof(CurrencyExchangeRecord));
			recordEntities.Add(typeof(Ayuda), typeof(GrantRecord));
			recordEntities.Add(typeof(AyudaPeriodo), typeof(GrantPeriodRecord));
			recordEntities.Add(typeof(SubtipoFactura), typeof(InvoiceSubtypeRecord));
			recordEntities.Add(typeof(Cargo), typeof(JobRecord));
			recordEntities.Add(typeof(IRPF), typeof(IRPFRecord));
			recordEntities.Add(typeof(Municipio), typeof(LocalityRecord));
			recordEntities.Add(typeof(Monitor), typeof(MonitorRecord));
			recordEntities.Add(typeof(MonitorLine), typeof(MonitorLineRecord));
            recordEntities.Add(typeof(Relation), typeof(RelationRecord));
			recordEntities.Add(typeof(Registro), typeof(RegistryRecord));
			recordEntities.Add(typeof(LineaRegistro), typeof(RegistryLineRecord));
			recordEntities.Add(typeof(Impuesto), typeof(TaxRecord));
			recordEntities.Add(typeof(TPV), typeof(TPVRecord));
			recordEntities.Add(typeof(Pesaje), typeof(WeighingRecord));
		}
	}
}
