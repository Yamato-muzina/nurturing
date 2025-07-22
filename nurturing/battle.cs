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
    public partial class battle : Form
    {
        private int result;
        string filePath = null;
        string filePath_enemy = null;
        List<string> lines = new List<string>();
        List<string> lines_enemy = new List<string>();
        string[] values = new string[0];
        string[] values_enemy = new string[0];
        int needXP = 100;
        int level = 0;
        int HP = 1;
        int HP_enemy = 1;
        int Strength = 0;
        int Defense = 0;
        int Defense_enemy = 0;
        int Strength_enemy = 0;
        int haveXP = int.Parse(File.ReadAllText("havexp.txt"));
        bool spawn = false;
        Random random = new Random();
        public battle(int judgechimera)
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

            values = lines[2].Split(',');
            HP = int.Parse(values[1]); //HPを取得
            values = lines[3].Split(',');
            Strength = int.Parse(values[1]); // 攻撃力を取得
            values = lines[4].Split(',');
            Defense = int.Parse(values[1]); //防御力を取得

            values = lines[6].Split(',');
            level = int.Parse(values[1]); // レベルをインクリメント
            values[1] = level.ToString();
            needXP = level * 100;
            label2.Text = haveXP + "/" + needXP;
            lines[6] = string.Join(",", values); // 6行目を更新


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                button2.Enabled = true;
                if (comboBox1.Text == "ゴブリン")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_bf4q22bf4q22bf4q.jpeg");
                    filePath_enemy = @"C:\Users\yamat\source\repos\nurturing\csv\enemi_Lv1.csv";
                }
                else if (comboBox1.Text == "サトゴリン")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_jfdhnmjfdhnmjfdh.jpeg");
                    filePath_enemy = @"C:\Users\yamat\source\repos\nurturing\csv\enemi_Lv5.csv";
                }
                else if (comboBox1.Text == "ルーンウォーデン")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_37u2ur37u2ur37u2.jpeg");
                    filePath_enemy = @"C:\Users\yamat\source\repos\nurturing\csv\enemi_Lv12.csv";
                }
                else if (comboBox1.Text == "ヴァルガロス")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_31kidi31kidi31ki.jpeg");
                    filePath_enemy = @"C:\Users\yamat\source\repos\nurturing\csv\enemi_Lv27.csv";
                }
                else if (comboBox1.Text == "アビスロード・ゼラトス")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\Gemini_Generated_Image_hm35yqhm35yqhm35.jpeg");
                    filePath_enemy = @"C:\Users\yamat\source\repos\nurturing\csv\enemi_Lv50.csv";
                }
            }
            if (comboBox1.Text == "")
            {
                label3.Text = ("召喚したい敵を選択してください");
            }
            else
            {
                // CSV全体をlinesに読み込む（ここが重要！）
                lines_enemy = new List<string>(File.ReadAllLines(filePath_enemy, Encoding.UTF8));

                label3.Text = ""; // 初期化

                // 1行目はヘッダーなので読み飛ばす
                for (int i = 1; i < lines_enemy.Count; i++)
                {
                    values_enemy = lines_enemy[i].Split(',');
                    if (values_enemy.Length >= 2)
                    {
                        label3.Text += $"{values_enemy[0]}: {values_enemy[1]}\n\n";
                    }
                }
                
                values_enemy = lines_enemy[2].Split(',');
                HP_enemy = int.Parse(values_enemy[1]); //HPを取得
                values_enemy = lines_enemy[3].Split(',');
                Strength_enemy = int.Parse(values_enemy[1]); // 攻撃力を取得
                values_enemy = lines_enemy[4].Split(',');
                Defense_enemy = int.Parse(values_enemy[1]); //防御力を取得
                spawn = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (spawn)
            {
                button3.Enabled = true;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rearing rearingForm = new rearing(result);
            values = lines[2].Split(',');
            values[1] = values[2];
            lines[2] = string.Join(",", values); // 行を更新
            rearingForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BattleEnd()) return;
            {
                int damage = Math.Max(0, Strength + (Strength * random.Next(0, 8) / 10) - Defense_enemy );
                HP_enemy -= damage;
                values = lines[1].Split(',');
                values_enemy = lines_enemy[1].Split(',');
                label4.Text = $"{values[1]}の攻撃！ {values_enemy[1]}に{damage}ダメージ！";
                RefreshStatus();
                EnemyTurn();
            }
        }
        void EnemyTurn()
        {
            if (BattleEnd()) return;
            int damage = Math.Max(0, Strength_enemy + (Strength_enemy * random.Next(0, 8) / 10) - Defense);
            HP -= damage;
            values = lines[1].Split(',');
            values_enemy = lines_enemy[1].Split(',');
            label5.Text = $"{values_enemy[1]}の攻撃！ {values[1]}に{damage}ダメージ！";
            RefreshStatus();
            BattleEnd();
            
        }
        void RefreshStatus()
        {
            //味方のステータス更新
            lines = new List<string>(File.ReadAllLines(filePath, Encoding.UTF8));

            label1.Text = ""; // 初期化
            values = lines[2].Split(',');
            values[1] = HP.ToString(); //HPを取得
            lines[2] = string.Join(",", values);
            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            for (int i = 1; i < lines.Count; i++)
            {
                values = lines[i].Split(',');
                if (values.Length >= 2)
                {
                    label1.Text += $"{values[0]}: {values[1]}\n\n";
                }
            }
            //敵のステータス更新
            lines_enemy = new List<string>(File.ReadAllLines(filePath_enemy, Encoding.UTF8));

            label3.Text = ""; // 初期化
            values_enemy = lines_enemy[2].Split(',');
            values_enemy[2] = HP_enemy.ToString(); 
            lines_enemy[2] = string.Join(",", values_enemy); 
            File.WriteAllLines(filePath_enemy, lines_enemy, Encoding.UTF8);


            for (int i = 1; i < lines_enemy.Count; i++)
            {
                values_enemy = lines_enemy[i].Split(',');
                if (i != 2)
                {
                    label3.Text += $"{values_enemy[0]}: {values_enemy[1]}\n\n";
                }
                else {label3.Text += $"{values_enemy[0]}: {values_enemy[2]}\n\n"; }
            }

        }
        bool BattleEnd()
        {
            if (HP <= 0)
            {
                label4.Text = "あなたの敗北……";
                label5.Text = "";
                button3.Enabled = false;
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
                haveXP = 0;
                button2.Enabled = false;
                values = lines[2].Split(',');
                values[1] = values[2];
                lines[2] = string.Join(",", values);
                HP = int.Parse(values[2]);
                RefreshStatus();
                return true;
            }
            if (HP_enemy <= 0)
            {
                label4.Text = "勝利！！";
                label5.Text = "";
                button3.Enabled = false;
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
                button2.Enabled = false;
                pictureBox2.Image = Image.FromFile(@"C:\Users\yamat\source\repos\nurturing\nurturing\Resources\istockphoto-1150995124-612x612.jpg");
                values = lines[2].Split(',');
                values[1] = values[2];
                lines[2] = string.Join(",", values); // 行を更新
                HP = int.Parse(values[2]);
                values_enemy = lines_enemy[5].Split(',');
                haveXP += int.Parse(values_enemy[1]);
                while (haveXP >= needXP)
                {
                    // レベルアップ処理
                    values = lines[6].Split(',');
                    int level = int.Parse(values[1]) + 1; // レベルをインクリメント
                    values[1] = level.ToString();
                    haveXP = haveXP - needXP;
                    needXP = level * 100;
                    lines[6] = string.Join(",", values); // 6行目を更新
                    File.WriteAllLines(filePath, lines, Encoding.UTF8);

                    // ステータスアップと表示
                    Lvup();
                }

                // 経験値表示を更新
                label2.Text = haveXP + "/" + needXP;
                return true;
            }
            return false;
        }
        public void Lvup()
        {
            label1.Text = ""; // 表示リセット
            string[] values = lines[1].Split(',');
            label1.Text += $"{values[0]}: {values[1]}\n\n";
            for (int i = 2; i < lines.Count - 1; i++)
            {
                values = lines[i].Split(',');
                values[1] = (int.Parse(values[1]) + random.Next(12, 25)).ToString(); // ステータス + 12～25
                lines[i] = string.Join(",", values); // 行を更新
                label1.Text += $"{values[0]}: {values[1]}\n\n";
            }
            values = lines[2].Split(',');
            values[2] = values[1];
            lines[2] = string.Join(",", values); // 行を更新
            values = lines[6].Split(',');
            label1.Text += $"{values[0]}: {values[1]}\n\n";
            File.WriteAllLines(filePath, lines, Encoding.UTF8); // ファイルも更新
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void battle_Load(object sender, EventArgs e)
        {

        }
    }
}
