using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace nurturing
{
    public partial class select : Form
    {
        string unicorn = @"C:\Users\yamat\source\repos\nurturing\csv\unicorn.csv";
        List<string> linesu = new List<string>();
        string chimera = @"C:\Users\yamat\source\repos\nurturing\csv\chimera.csv";
        List<string> linesc = new List<string>();
        string dragon = @"C:\Users\yamat\source\repos\nurturing\csv\dragon.csv";
        List<string> linesd = new List<string>();
        
        public select()
        {
            InitializeComponent();
            if (!File.Exists(unicorn) || !File.Exists(chimera) || !File.Exists(dragon))
            {
                MessageBox.Show("CSVファイルが見つかりません。");
                return;
            }
            linesu = new List<string>(File.ReadAllLines(unicorn, Encoding.UTF8));
            linesc = new List<string>(File.ReadAllLines(chimera, Encoding.UTF8));
            linesd = new List<string>(File.ReadAllLines(dragon, Encoding.UTF8));
            int haveXP = 0;
            File.WriteAllText("havexp.txt", haveXP.ToString());

        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == pictureBox1)
            {
                Chimera chimeraForm = new Chimera();
                chimeraForm.Show();
            }
            else if (pictureBox == pictureBox2)
            {
                Unicorn unicorn = new Unicorn();
                unicorn.Show();
            }
            else if (pictureBox == pictureBox3)
            {
                Dragon dragonForm = new Dragon();
                dragonForm.Show();
            }
            this.Hide();
        }
    }
}
