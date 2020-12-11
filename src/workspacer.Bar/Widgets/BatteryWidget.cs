using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace workspacer.Bar.Widgets
{
    public class BatteryWidget : BarWidgetBase
    {
        public Color LowChargeColor { get; set; } = Color.Red;
        public Color MedChargeColor { get; set; } = Color.Yellow;
        public Color HighChargeColor { get; set; } = Color.Green;
        public bool HasBatteryWarning { get; set; } = true;
        public double LowChargeThreshold { get; set; } = 0.10;
        public double MedChargeThreshold { get; set; } = 0.50;
        public int Interval { get; set; } = 5000;
        public string FontName { get; set; } = null;

        private System.Timers.Timer _timer;

        public override IBarWidgetPart[] GetParts()
        {
            PowerStatus pwr = SystemInformation.PowerStatus;
            float currentBatteryCharge = pwr.BatteryLifePercent;

            if (HasBatteryWarning)
            {
                if (currentBatteryCharge <= LowChargeThreshold)
                {
                    return Parts(Part(currentBatteryCharge.ToString("#0%"), LowChargeColor, null, null, FontName));
                }
                else if (currentBatteryCharge <= MedChargeThreshold)
                {
                    return Parts(Part(currentBatteryCharge.ToString("#0%"), MedChargeColor, null, null, FontName));
                }
                else
                {
                    return Parts(Part(currentBatteryCharge.ToString("#0%"), HighChargeColor, null, null, FontName));
                }
            }
            else
            {
                return Parts(Part(currentBatteryCharge.ToString("#0%"), null, null, null, FontName));
            }
        }

        public override void Initialize()
        {
            _timer = new System.Timers.Timer(Interval);
            _timer.Elapsed += (s, e) => Context.MarkDirty();
            _timer.Enabled = true;
        }
    }
}
