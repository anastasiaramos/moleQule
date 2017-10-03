using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace moleQule.Face.Controls
{
    public class NumericTextBox : TextBox
    {
        private bool _text_is_integer = false;
        private bool _text_is_currency = false;

        private NumberFormatInfo _numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;

        public bool TextIsInteger
        {
            get { return _text_is_integer; }
            set 
            {
                if (value)
                {
                    int pos = this.Text.IndexOf(_numberFormatInfo.NumberDecimalSeparator);
                    //Si hay coma, nos quedamos con la parte entera del numero.
                    if (pos != -1)
                        this.Text = this.Text.Substring(0, pos);
                    _text_is_currency = false;
                }
                _text_is_integer = value;
            }
        }

        public bool TextIsCurrency
        {
            get { return _text_is_currency; }
            set
            {
                if (value)
                {
                    _text_is_integer = false;
                }
                _text_is_currency = value;
            }
        }

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            string decimalSeparator = _numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = _numberFormatInfo.NumberGroupSeparator;
            string negativeSign = _numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator))
            {
                // Si el numero es entero, no se pueden poner comas decimales
                if (TextIsInteger)
                    e.Handled = true;
            }
            else if (keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            //else if (this.allowSpace && e.KeyChar == ' ')
            //{

            //}
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
            }
        }

        public NumericTextBox()
        {
            this.TextAlign = HorizontalAlignment.Right;
            //this.Validating += new CancelEventHandler(NumericTextBox_Validating);
        }
        
        void NumericTextBox_Validating(object sender, CancelEventArgs e)
        {

            if (this.Text.Length == 0)
                this.Text = (0).ToString("N2");
            else
            {
                if (TextIsInteger)
                {
                    this.Text = Convert.ToInt64(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, "")).ToString("#########0,######");
                }
                else if (TextIsCurrency)
                {
                    this.Text = Convert.ToDecimal(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, "")).ToString("#########0.,###### €");
                }
                else
                {
                    this.Text = Convert.ToDecimal(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, "")).ToString("#########0.,######");
                }
            }
        }

        public int IntValue
        {
            get
            {
                if (this.Text.Length == 0)
                    return 0;
                try
                {
                    return int.Parse(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, ""));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public long LongValue
        {
            get
            {
                if (this.Text.Length == 0)
                    return 0;

                try
                {
                    return long.Parse(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, ""));
                }
                catch
                {
                    return (long)0;
                }
            }
        }

        public decimal DecimalValue
        {
            get
            {
                if (this.Text.Length == 0)
                {
                    return 0.0m;
                }
                try
                {
                    return decimal.Parse(this.Text.Replace(" ", "").Replace(_numberFormatInfo.CurrencySymbol, ""));
                }
                catch
                {
                    return 0.0m;
                }
            }
        }

    }

}
