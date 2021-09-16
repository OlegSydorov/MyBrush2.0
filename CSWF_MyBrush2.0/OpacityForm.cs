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
    public delegate void SelectOpacity(object sender, float op);
    public partial class OpacityForm : Form
    {
        public event SelectOpacity OnOpacitySelected;
        public OpacityForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show((trackBar1.Value/10f).ToString());
            OnOpacitySelected?.Invoke(this, (trackBar1.Value/10f));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
