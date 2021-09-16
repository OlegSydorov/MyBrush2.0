using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWF_MyBrush2._0
{
    public delegate void HatchStyleSelect(object obj, HatchStyle style);
    public partial class HatchForm : Form
    {
        public event HatchStyleSelect OnHatchStyleSelected;
        HatchStyle styleSelected;
        public HatchForm()
        {
            InitializeComponent();
            foreach (string hs in Enum.GetNames(typeof(HatchStyle)))
            {
                comboBox2.Items.Add(hs);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnHatchStyleSelected?.Invoke(this, styleSelected);
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            styleSelected = (HatchStyle)Enum.Parse(typeof(HatchStyle), comboBox2.SelectedItem.ToString(), true);
         
        }
    }
}
