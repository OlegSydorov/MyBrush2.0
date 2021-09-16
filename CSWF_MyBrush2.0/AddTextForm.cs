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
    public delegate void AddText(object obj, string text);
    public partial class AddTextForm : Form
    {
        public event AddText OnTextAdded;
        string text;
        public AddTextForm()
        {
            text = null;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnTextAdded?.Invoke(this, textBox1.Text);
            this.Close();
        }
    }
}
