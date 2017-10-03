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
    public class mQDateTimePicker : DateTimePicker
    {
        public mQDateTimePicker()
            : base()
        {
            this.Format = DateTimePickerFormat.Long;
            this.ShowCheckBox = true;
            this.Checked = false;
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            this.Value = Convert.ToDateTime(this.Value.ToLongDateString());
        }

        public void SetWidthByMeasuredString (Font font)
        {
            CheckBox chk = new CheckBox();
            this.Width = TextRenderer.MeasureText(this.Value.ToShortDateString() + chk.Width, font).Width;
        }
    }

}
