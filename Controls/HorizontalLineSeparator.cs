using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling.Controls
{
    public class HorizontalLineSeparator : GroupBox
    {
        // Methods
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 6, specified);
        }

        // Properties
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }
    }
}
