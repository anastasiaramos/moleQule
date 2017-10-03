using System; 
using System.Collections.Generic;
using System.Globalization;

namespace moleQule.Library
{
	public enum DateInterval
	{
		Day,
		DayOfYear,
		Hour,
		Minute,
		Month,
		Quarter,
		Second,
		Weekday,
		WeekOfYear,
		Year
	}

	public class DateAndTime
	{
		public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
		{
			return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
		}

        public static long GetJavascriptTimestamp(System.DateTime input)
        {
            System.TimeSpan span = new System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks);
            System.DateTime time = input.Subtract(span);
            return (long)(time.Ticks / 10000);
        }

		private static int GetQuarter(int nMonth)
		{
			if (nMonth <= 3)
				return 1;
			if (nMonth <= 6)
				return 2;
			if (nMonth <= 9)
				return 3;
			return 4;
		}

		public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
		{
			if (interval == DateInterval.Year)
				return dt2.Year - dt1.Year;

			if (interval == DateInterval.Month)
				return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

			TimeSpan ts = dt2 - dt1;

			if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
				return Round(ts.TotalDays);

			if (interval == DateInterval.Hour)
				return Round(ts.TotalHours);

			if (interval == DateInterval.Minute)
				return Round(ts.TotalMinutes);

			if (interval == DateInterval.Second)
				return Round(ts.TotalSeconds);

			if (interval == DateInterval.Weekday)
			{
				return Round(ts.TotalDays / 7.0);
			}

			if (interval == DateInterval.WeekOfYear)
			{
				while (dt2.DayOfWeek != eFirstDayOfWeek)
					dt2 = dt2.AddDays(-1);
				while (dt1.DayOfWeek != eFirstDayOfWeek)
					dt1 = dt1.AddDays(-1);
				ts = dt2 - dt1;
				return Round(ts.TotalDays / 7.0);
			}

			if (interval == DateInterval.Quarter)
			{
				double d1Quarter = GetQuarter(dt1.Month);
				double d2Quarter = GetQuarter(dt2.Month);
				double d1 = d2Quarter - d1Quarter;
				double d2 = (4 * (dt2.Year - dt1.Year));
				return Round(d1 + d2);
			}

			return 0;
		}

		private static long Round(double dVal)
		{
			if (dVal >= 0)
				return (long)Math.Floor(dVal);
			return (long)Math.Ceiling(dVal);
		}

        public static DateTime Parse(int day, int month, int year) { return DateTime.Parse(string.Format("{0}/{1}/{2}", day.ToString("00"), month.ToString("00"), (year != 0 ? year.ToString("0000") : DateTime.Now.Year.ToString("0000"))), CultureInfo.CreateSpecificCulture("es-ES")); }
		public static DateTime FirstDay(int year) { return DateTime.Parse("01/01/" + (year != 0 ? year.ToString("0000") : DateTime.Now.Year.ToString("0000")), CultureInfo.CreateSpecificCulture("es-ES")); }
        public static DateTime FirstDay(int month, int year) { return DateTime.Parse(string.Format("01/{0}/{1}", month.ToString("00"), (year != 0 ? year.ToString("0000") : DateTime.Now.Year.ToString("0000"))), CultureInfo.CreateSpecificCulture("es-ES")); }
        public static DateTime LastDay(int year) { return DateTime.Parse("31/12/" + (year != 0 ? year.ToString("0000") : DateTime.Now.Year.ToString("0000")), CultureInfo.CreateSpecificCulture("es-ES")); } 
		public static DateTime LastDay(int month, int year) 
		{
			int last_day = DateTime.DaysInMonth(year, month);
			return DateTime.Parse(string.Format("{0}/{1}/{2}", last_day.ToString("00"), month.ToString("00"), year.ToString("0000")), CultureInfo.CreateSpecificCulture("es-ES")); 
		}

		public static int DaysToNextMonth(DateTime dt)
		{
			int daysto = 0;
			DateTime aux = new DateTime(dt.Year, dt.Month, dt.Day);
			int month = aux.Month;
			while (month == aux.Month)
			{
				daysto++;
				aux = aux.AddDays((double)1);
			}
			return daysto;
		}
	}
}