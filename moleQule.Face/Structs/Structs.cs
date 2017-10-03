using System;
using System.Collections.Generic;

namespace moleQule.Face
{
	public enum ManagerFormType
	{
		MFAdd = 0,
        MFView = 1,
        MFEdit = 2,
        MFEditList = 3,
        MFViewList = 4
	}

    public enum EntityMngFormTypeData
    {
        Default = 0,
        ByParameter = 1
    }

    public enum molAction
    {
        Copy = 0,
        Print = 1,
        Select = 2,
        Add = 3,
        View = 4,
        Edit = 5,
        Delete = 6,
        Close = 7,
        Default = 8,
        SelectAll = 9,
        FilterOn = 10,
        FilterOff = 11,
        PrintDetail = 12,
        Save = 13,
        Cancel = 14,
        ShowDocuments = 15,
        Submit = 16,
        Unlock = 17,
        Lock = 18,
        CancelBkJob = 19,
        FilterAll = 20,
		SendEmail = 21,
		EmailPDF = 22,
		EmailLink = 23,
		ExportPDF = 24,
		AdvancedSearch = 25,
		FilterGlobal = 26,
		PrintListQR = 27,
		ChangeStateContabilizado = 28,
		ChangeStateEmitido = 29,
		ChangeStateAnulado = 30,
		Refresh = 32,
		CustomAction1 = 33,
		CustomAction2 = 34,
		CustomAction3 = 35,
		CustomAction4 = 36,
		DateSelection = 37,
    }

    public enum molView
    {
        Normal = 0,
        Select = 1,
		Tree = 2,
		Enbebbed = 3,
		ReadOnly = 4
    }

	public enum ETipoChart { Detallado = 0, Agrupado = 1 }

    public enum IFilterType
    {
        None = 0,
        Filter = 1,
        Search = 2,
		FilterBack = 3
    }

	public enum IFilterProperty
	{
		All = 0,
		ByParamenter = 1,
		ByList = 2
	}

	[Serializable()]
    public class FilterItem
    {
		public IFilterProperty FilterProperty = IFilterProperty.ByParamenter;
        public Library.CslaEx.Operation Operation;
        public string Property;
		public string Column;
		public string ColumnTitle;
        public object Value;
        public object SecondValue;
		public bool Active = true;

		public string Text 
        { 
            get 
            { 
                return ColumnTitle.ToUpper() + " " + Library.CslaEx.EnumText.GetString(Operation) + " '" + ValueToString + "'"
                    + ( SecondValue != null && SecondValueToString != string.Empty ? " - '" + SecondValueToString + "'; " : "; ");
            }
        }
		public string ValueToString 
		{
			get
			{
				if (Value != null)
				{
					return ((Value is DateTime)) ? ((DateTime)Value).ToShortDateString() : Value.ToString();
				}
				else
					return string.Empty;
			}
		}
        public string SecondValueToString
        {
            get
            {
                if (SecondValue != null)
                {
                    return ((SecondValue is DateTime)) ? ((DateTime)SecondValue).ToShortDateString() : SecondValue.ToString();
                }
                else
                    return string.Empty;
            }
        }
    }

	[Serializable()]
	public class FilterList : List<FilterItem> {}

	[Serializable()]
	public struct ChartParams
	{
		public string Title;
		public string LegendTitle;
	}
}
