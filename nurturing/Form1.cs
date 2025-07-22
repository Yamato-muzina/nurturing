using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace nurturing
{
    public partial class Form1 : Form
    {
        int num = 0;
        public Form1()
        {
            InitializeComponent();
            num = int.Parse(File.ReadAllText("selectnum.txt"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            select selectForm = new select();
            selectForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rearing selectForm = new rearing(num);
            selectForm.Show();
            this.Hide();
        }
    }
}
