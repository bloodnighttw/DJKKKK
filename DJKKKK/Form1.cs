using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJKKKK
{
  public partial class Form1 : Form
  {
      private TextBox[,] boards ;
      private int[,] shortest = new int[100,100]; // [original x , original y , to x , to y]
      private int[] basic = new int[100];
      private int[,] reverseTable = new int[100, 100];
        public Form1()
        {
            InitializeComponent();
            boards = new [,]
            {
                {textBox1, textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8,textBox9,textBox10},
                {textBox20,textBox19,textBox18,textBox17,textBox16,textBox15,textBox14,textBox13,textBox12,textBox11},
                {textBox30,textBox29,textBox28,textBox27,textBox26,textBox25,textBox24,textBox23,textBox22,textBox21},
                {textBox40,textBox39,textBox38,textBox37,textBox36,textBox35,textBox34,textBox33,textBox32,textBox31},
                {textBox50,textBox49,textBox48,textBox47,textBox46,textBox45,textBox44,textBox43,textBox42,textBox41},
                {textBox60,textBox59,textBox58,textBox57,textBox56,textBox55,textBox54,textBox53,textBox52,textBox51},
                {textBox70,textBox69,textBox68,textBox67,textBox66,textBox65,textBox64,textBox63,textBox62,textBox61},
                {textBox80,textBox79,textBox78,textBox77,textBox76,textBox75,textBox74,textBox73,textBox72,textBox71},
                {textBox90,textBox89,textBox88,textBox87,textBox86,textBox85,textBox84,textBox83,textBox82,textBox81},
                {textBox100,textBox99,textBox98,textBox97,textBox96,textBox95,textBox94,textBox93,textBox92,textBox91}
            };
        }

        private const int max = int.MaxValue / 128;

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    shortest[i,j] = max;
                }
            }
            
            openFileDialog1.Filter = "二元檔案(*.dat)|*.dat";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        string temp = br.ReadString();
                        boards[i, j].Text = temp;
                        basic[i * 10 + j] = Convert.ToInt32(temp);
                        //MessageBox.Show(shortest[i*10+ j, i*10+ j]+"");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int no = 0; no < 100; no++)
            {
                if (no % 10 != 9)
                {
                    shortest[no, no + 1] = basic[no] + basic[no + 1];
                    shortest[no + 1, no] = basic[no] + basic[no + 1];
                }

                if (no / 10 != 9)
                {
                    shortest[no, no + 10] = basic[no] + basic[no + 10];
                    shortest[no + 10, no] = basic[no] + basic[no + 10];
                }
            } //setup neighbor

            for (int no = 0; no < 100; no++)
            {
                for (int insert = 0; insert < 100; insert++)
                {
                    if (shortest[no, insert] == max)
                        continue; //
                    for (int end = 0; end < 100; end++)
                    {
                        if (shortest[insert, end] == max)
                            continue;
                        shortest[no, end] = Min(no, insert, end);
                        shortest[end, no] = shortest[no, end];
                    }
                }
            }



            
            ChangeColor(0,99);
            //MessageBox.Show($"{shortest[0, 99]}"); //0 99 97
            label1.Text = $"{shortest[0, 99]}";
        }

        int Min(int no, int insert, int end)
        {
            if (shortest[no, end] > shortest[no, insert] + shortest[insert, end] - basic[insert])
            {
                reverseTable[no,end] = insert;
                reverseTable[end,no] = insert;
                return shortest[no, insert] + shortest[insert, end] - basic[insert];
            }

            return shortest[no, end];
        }


        private void ChangeColor(int start, int end)
        {

            


            if (start == end || reverseTable[start, end] == 0)
            {
                boards[start/10,start%10].ForeColor = Color.Blue;
                boards[end/10,end%10].ForeColor = Color.Blue;
                return;
            }
            
            ChangeColor(start,reverseTable[start,end]);
            ChangeColor(reverseTable[start,end],end);

            
        }

  }
}