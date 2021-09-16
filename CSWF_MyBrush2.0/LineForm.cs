using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWF_MyBrush2._0
{
    public delegate void SelectWidth(object sender, int width);
    public partial class LineForm : Form
    {
        public event SelectWidth OnWidthSelected;
        public LineForm()
        {
            InitializeComponent();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnWidthSelected?.Invoke(this, trackBar1.Value+1);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }
}
