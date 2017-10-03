using System;

namespace moleQule.Library
{
    public static class Validator
	{
		#region Business Methods

        public static bool NIFValidation(string value)
        {
            if (CIFValidation(value)) return true;
                
            if (value == null) return false;

            if (value.Length == 0) return false;

            if (value.Length != 9) return false;

            if (!Char.IsLetter(value[8])) return false;

            for (int i = 0; i < 7; i++)
                if (!Char.IsNumber(value[i])) return false;

            return true;
        }

        public static bool CIFValidation(string value)
        {
            if (value == null) return false;

            if (value.Length == 0) return false;

            if (value.Length != 9) return false;

            if (!Char.IsLetter(value[0])) return false;

            if (!Char.IsLetterOrDigit(value[8])) return false;

            for (int i = 1; i < 8; i++)
                if (!Char.IsNumber(value[i])) return false;

            return true;
        }

        public static bool DNIValidation(string value)
        {
            if (value == null) return false;

            if (value.Length == 0) return false;

            if (value.Length != 8) return false;

            for (int i = 0; i < 7; i++)
                if (!Char.IsNumber(value[i])) return false;

            return true;
        }

        public static void ValidateCIF(string field, string value)
		{
            if ((value == string.Empty) || (value == null)) return;

            bool invalid = false;
			long lvalue = 0;

            value = value.Trim();

			try
			{
				invalid = !Char.IsLetter(value.Substring(0, 1).ToCharArray()[0]);

                if (value.Contains("-"))
                {
                    if (!invalid)
                    {
                        invalid = (value.Length != 10);
                        lvalue = Convert.ToInt64(value.Substring(2));
                    }
                }
                else
                {
                    if (!invalid)
                    {
                        invalid = (value.Length != 9);
                        lvalue = Convert.ToInt64(value.Substring(1));
                    }
                }

				if (invalid)
					throw new iQValidationException(string.Empty, string.Empty, field);
			}
			catch
			{
				throw new iQValidationException(string.Empty, string.Empty, field);
			}
		}

		public static void ValidateNIE(string field, string value)
		{
            if ((value == string.Empty) || (value == null)) return;

            bool invalid = false;
			long lvalue = 0;

			try
			{
				invalid = Char.IsNumber(value.Substring(0, 1).ToCharArray()[0]);
				if (!invalid)
				{
					invalid = (value.Substring(1, 7).Length < 7);
					lvalue = Convert.ToInt64(value.Substring(1, 7));
					if (!invalid)
						invalid = Char.IsNumber(value.Substring(8, 1).ToCharArray()[0]);
				}

				if (invalid)
					throw new iQValidationException(string.Empty, string.Empty, field);
			}
			catch
			{
				throw new iQValidationException(string.Empty, string.Empty, field);
			}
		}

		public static void ValidateNIF(string field, string value)
		{
            if ((value == string.Empty) || (value == null)) return;
            if (value.Length == 0) return;

            bool invalid = false;
		    long lvalue = 0;

            value = value.Trim();

			try
			{
                lvalue = Int64.Parse(value.Substring(0, 8));

                if (value.Contains("-"))
                {
                    invalid = (value.Length != 10);

                    if (!invalid)
                        invalid = (value.Substring(8, 1) != "-");

                    if (!invalid)
                        invalid = !Char.IsLetter(value.Substring(9, 1).ToCharArray()[0]);
                }
                else
                {
                    invalid = (value.Length != 9);

                    if (!invalid)
                        invalid = !Char.IsLetter(value.Substring(8, 1).ToCharArray()[0]);
                }
				
                if (invalid)
					throw new iQValidationException(string.Empty, string.Empty, field);
			}
			catch
			{
				throw new iQValidationException(string.Empty, string.Empty, field);
			}
		}

		public static void ValidateInt(string value, byte size)
		{
			if ((value == string.Empty) || (value == null)) return;

			try
			{
				Convert.ToInt64(value);
				if (value.Length != size)
					throw new iQValidationException("");
			}
			catch (Exception ex)
			{
				throw new iQValidationException(ex.Message);
			}
        }

        public static void ValidateInt(string value)
        {
            if ((value == string.Empty) || (value == null)) return;

            try
            {
                Convert.ToInt64(value);
            }
            catch (Exception ex)
            {
                throw new iQValidationException(ex.Message);
            }
        }

		#endregion
	}
}
