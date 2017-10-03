using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace moleQule.Library
{
    public class Country
    {
        public string Name { get; set; }
        public string Iso2 { get; set; }
		public string Currency { get; set; }
        public string Prefix { get; set; }

        public Country()
        {
            this.Name = "- select -";
            this.Iso2 = "";
            this.Prefix = "";
        }

        public static List<Country> Load()
        {
            List<Country> result = new List<Country>();            
			try
			{
				XmlReader xmlReader = XmlReader.Create(new StringReader(Library.Properties.Resources.Countries));
				XPathDocument doc = new XPathDocument(xmlReader);
				XPathNavigator navRoot = doc.CreateNavigator();
				XPathNodeIterator nodes = navRoot.Select("/ISO_3166-1_List_en/ISO_3166-1_Entry");
				foreach (XPathNavigator nav in nodes)
				{
					string name = nav.SelectSingleNode("ISO_3166-1_Country_name").Value.Trim();
					string iso2 = nav.SelectSingleNode("ISO_3166-1_Alpha-2_Code_element").Value.Trim();
					string prefix = nav.SelectSingleNode("Country_international_prefix").Value.Trim();
					string currency = (nav.SelectSingleNode("Country_currency") != null) ? nav.SelectSingleNode("Country_currency").Value.Trim() : "DOL";
					result.Add(new Country()
					{
						Name = name,
						Iso2 = iso2,
						Prefix = prefix,
						Currency = currency
					});
				}
				return result;
			}
			catch
			{
				throw new iQException("Country prefix file not found");
			}
        }
		
		public static Country Find(string iso2) { return Find(Load(), iso2); }
		public static Country Find(List<Country> countries, string iso2)
		{
			Country item = countries.Find(m => m.Iso2 == iso2);

			return (item == null)
					? countries.Find(m => m.Iso2 == "ES")
					: item;
		}

		public static Country FindByName(string name) { return FindByName(Load(), name); }
		public static Country FindByName(List<Country> countries, string name)
		{
			Country item = countries.Find(m => m.Name == name);

			return (item == null)
					? countries.Find(m => m.Name == "Spain")
					: item;
		}
	}

	public class Currency
	{
		RegionInfo _region = null;

		public string ISOCode { get; set; }
		public string Num { get; set; }
		public int Decimals { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string Symbol { get { if (_region == null) GetCurrencyRegion(); return _region.CurrencySymbol; } }
		public RegionInfo RegionInfo { get { if (_region == null) GetCurrencyRegion(); return _region; } }

		public Currency()
		{
			this.ISOCode = string.Empty;
			this.Num = string.Empty;
			this.Name = "- select -";
			this.Decimals = 0;
			this.Location = string.Empty;			
		}

		public static List<Currency> Load()
		{
			List<Currency> result = new List<Currency>();
			try
			{
				XmlReader xmlReader = XmlReader.Create(new StringReader(Library.Properties.Resources.ISO_4217));
				XPathDocument doc = new XPathDocument(xmlReader);
				XPathNavigator navRoot = doc.CreateNavigator();
				XPathNodeIterator nodes = navRoot.Select("/ISO_4217/CcyTbl/CcyNtry");
				foreach (XPathNavigator nav in nodes)
				{
					string code = (nav.SelectSingleNode("Ccy") != null) ? nav.SelectSingleNode("Ccy").Value.Trim() : string.Empty;
					string num = (nav.SelectSingleNode("CcyNbr") != null) ? nav.SelectSingleNode("CcyNbr").Value.Trim() : string.Empty;
					string name = (nav.SelectSingleNode("CcyNm") != null) ? nav.SelectSingleNode("CcyNm").Value.Trim() : string.Empty;
					int decimals = 0;
					int.TryParse((nav.SelectSingleNode("CcyMnrUnts") != null) ? nav.SelectSingleNode("CcyMnrUnts").Value.Trim() : "0", out decimals);
					string location = (nav.SelectSingleNode("CtryNm") != null) ? nav.SelectSingleNode("CtryNm").Value.Trim() : "0";
					
					result.Add(new Currency()
					{
						ISOCode = code,
						Num = num,
						Name = name,
						Decimals = decimals,
						Location = location
					});
				}
				return result;
			}
			catch
			{
				throw new iQException("Currency not found");
			}
		}

		public static Currency Find(string code) { return Find(Load(), code); }
		public static Currency Find(List<Currency> currencies, string code)
		{
			Currency item = currencies.Find(m => m.ISOCode == code);

			return (item == null)
					? currencies.Find(m => m.ISOCode == "EUR")
					: item;
		}

		public static Currency FindByName(string name) { return FindByName(Load(), name); }
		public static Currency FindByName(List<Currency> countries, string name)
		{
			Currency item = countries.Find(m => m.Name == name);

			return (item == null)
					? countries.Find(m => m.Name == "Euro")
					: item;
		}

		protected void GetCurrencyRegion() { _region = GetCurrencyRegion(ISOCode); }
		protected RegionInfo GetCurrencyRegion(string ISOCurrencySymbol)
		{
			RegionInfo region =
				CultureInfo
					.GetCultures(CultureTypes.AllCultures)
					.Where(c => !c.IsNeutralCulture)
					.Select(culture =>
					{
						try { return new RegionInfo(culture.LCID); }
						catch { return null; }
					})
					.Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
					.FirstOrDefault();

			return (region != null) ? region : new RegionInfo(AppControllerBase.Culture.LCID);
		}
	}
}
