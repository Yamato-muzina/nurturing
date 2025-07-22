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
    public partial class name_select : Form
    {
        string filePath = "";
        List<string> lines = new List<string>();
        int num = 0;
        public name_select(int num)
        {
            InitializeComponent();
            switch (num)
            {
                case 1:
                    filePath = @"C:\Users\yamat\source\repos\nurturing\csv\chimera.csv";
                    break;
                case 2:
                    filePath = @"C:\Users\yamat\source\repos\nurturing\csv\unicorn.csv";
                    break;
                case 3:
                    filePath = @"C:\Users\yamat\source\repos\nurturing\csv\dragon.csv";
                    break;
            }
        }

        private void name_select_Load(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("CSVファイルが見つかりません。");
                return;
            }

            // CSV全体をlinesに読み込む（ここが重要！）
            lines = new List<string>(File.ReadAllLines(filePath, Encoding.UTF8));

            label6.Text = ""; // 初期化

            // 1行目はヘッダーなので読み飛ばす
            for (int i = 1; i < lines.Count; i++)
            {
                string[] values = lines[i].Split(',');
                if (values.Length >= 2)
                {
                    label6.Text += $"{values[0]}: {values[1]}\n\n";
                    switch (num)
                    {
                        case 1:
                            File.WriteAllLines(@"C:\Users\yamat\source\repos\nurturing\csv\chimera_savedate.csv", lines, Encoding.UTF8);
                            break;
                        case 2:
                            File.WriteAllLines(@"C:\Users\yamat\source\repos\nurturing\csv\unicorn_savedate.csv", lines, Encoding.UTF8);
                            break;
                        case 3:
                            File.WriteAllLines(@"C:\Users\yamat\source\repos\nurturing\csv\dragon_savedate.csv", lines, Encoding.UTF8);
                            break;
                    }
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (lines.Count > 1)
            {
                // 2行目をカンマで分割
                string[] values = lines[1].Split(',');

                // 2列目（インデックス1）が存在するか確認
                if (values.Length > 1)
                {
                    values[1] = textBox1.Text; // ここに変更したい値を入れる
                    lines[1] = string.Join(",", values);
                }

                // ファイルに上書き保存
                File.WriteAllLines(filePath, lines, Encoding.UTF8);

                // ラベルの再表示
                label6.Text = "";
                for (int i = 1; i < lines.Count; i++)
                {
                    string[] vals = lines[i].Split(',');
                    if (vals.Length >= 2)
                    {
                        label6.Text += $"{vals[0]}: {vals[1]}\n\n";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //form1(選択画面)に戻る
            select selectForm = new select();
            selectForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //育成画面に移動　どのモンスターを選んだか判定
            rearing rearingForm = new rearing(2);
            rearingForm.Show();
            this.Close();
        }
       
    }
}
    

