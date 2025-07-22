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
    public partial class rearing : Form
    {
        private int result;
        string filePath = null;
        List<string> lines = new List<string>();
        string[] values = new string[0];
        int needXP = 100;
        int level = 0;
        Random random = new Random();

        public rearing(int judgechimera)
        {
            InitializeComponent();
            result = judgechimera;
            Chimera chimera = new Chimera();
            Unicorn unicorn = new Unicorn();
            Dragon dragon = new Dragon();
            // 判定処理
            if (result == 1)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_ropnhhropnhhropn.jpeg");
                filePath = @"C:\Users\yamat\source\repos\nurturing\csv\chimera_savedate.csv";
            }
            else if (result == 2)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_ip4iw0ip4iw0ip4i.jpeg");
                filePath = @"C:\Users\yamat\source\repos\nurturing\csv\unicorn_savedate.csv";
            }
            else if (result == 3)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_1y3k1p1y3k1p1y3k.jpeg");
                filePath = @"C:\Users\yamat\source\repos\nurturing\csv\dragon_savedate.csv";
            }
            if (!File.Exists(filePath))
            {
                MessageBox.Show("CSVファイルが見つかりません。");
                return;
            }


            // CSV全体をlinesに読み込む（ここが重要！）
            lines = new List<string>(File.ReadAllLines(filePath, Encoding.UTF8));

            label1.Text = ""; // 初期化

            // 1行目はヘッダーなので読み飛ばす
            for (int i = 1; i < lines.Count; i++)
            {
                values = lines[i].Split(',');
                if (values.Length >= 2)
                {
                    label1.Text += $"{values[0]}: {values[1]}\n\n";
                }
            }
            values = lines[6].Split(',');
            level = int.Parse(values[1]); // レベルをインクリメント
            values[1] = level.ToString();
            needXP = level * 100;
            label4.Text = haveXP + "/" + needXP;
            lines[6] = string.Join(",", values); // 6行目を更新
        }
        


        private void rearing_Load(object sender, EventArgs e)
        {
           
        }
        
        int haveXP = int.Parse(File.ReadAllText("havexp.txt"));
        

        private void button1_Click(object sender, EventArgs e)
        {
            int index = label4.Text.IndexOf("/");
            haveXP += random.Next(10, 100);
            if (haveXP >= needXP)
            {
                // レベルアップ処理
                string[] values = lines[6].Split(',');
                int level = int.Parse(values[1]) + 1; // レベルをインクリメント
                values[1] = level.ToString();
                haveXP = haveXP - needXP;
                needXP = level * 100;
                lines[6] = string.Join(",", values); // 6行目を更新
                File.WriteAllLines(filePath, lines, Encoding.UTF8);

                // ステータスアップと表示
                Lvup();

                // 経験値表示を更新
                label4.Text = haveXP + "/" + needXP;
            }
            else
            {
                // 経験値のみ更新
                label4.Text = haveXP + "/" + needXP;
            }
        }
        
        public void Lvup()
        {
            label1.Text = ""; // 表示リセット
            string[] values = lines[1].Split(',');
            label1.Text += $"{values[0]}: {values[1]}\n\n";
            for (int i = 2; i < lines.Count - 1; i++)
            {
                values = lines[i].Split(',');
                values[1] = (int.Parse(values[1]) + random.Next(12, 25)).ToString(); // ステータス + 14
                lines[i] = string.Join(",", values); // 行を更新
                label1.Text += $"{values[0]}: {values[1]}\n\n";
            }
            values = lines[2].Split(',');
            values[2] = values[1]; // MaxHP + 14
            lines[2] = string.Join(",", values); // 行を更新
            values = lines[6].Split(',');
            label1.Text += $"{values[0]}: {values[1]}\n\n";
            File.WriteAllLines(filePath, lines, Encoding.UTF8); // ファイルも更新
        }

        private void rearing_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText("havexp.txt", haveXP.ToString());
            File.WriteAllText(values[1], needXP.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText("havexp.txt", haveXP.ToString());
            File.WriteAllText(values[1], needXP.ToString());
            battle rearingForm = new battle(result);
            File.WriteAllText("selectnum.txt", result.ToString());
            rearingForm.Show();
            this.Hide();
        }
    }
}
