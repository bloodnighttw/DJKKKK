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
      private int[,] shortest = new int[1010,1010]; // [original x , original y , to x , to y]
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

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    shortest[i,j] = int.MaxValue/128;
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
                        shortest[i*10+ j, i*10+ j] = Convert.ToInt32(temp);
                        //MessageBox.Show(shortest[i*10+ j, i*10+ j]+"");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int diff = 0; diff < 100; diff++)
            {
                for (int first = 0; first < 100; first++)
                {

                    
                    var end = first + diff;
                    if(end >= 100) continue;
                    
                    
                    int y=first+diff*10,x=first+diff;
                    if (y < 100)
                    {
                        int temp = int.MaxValue;
                        for (int j = 0; j < diff; j++)
                        {
                            temp = Math.Min(shortest[first,first+ j*10]+shortest[first+(j+1)*10,y],temp);

                        }

                        if (temp < shortest[first, y])
                        {
                            shortest[first, y] = temp;
                        }
                        //if(first==0 && y== 10)MessageBox.Show($"short {first},{x} {temp}");


                    }

                    //MessageBox.Show($"{first} & {x}");
                    if (x < 100 && first / 10 == x / 10)
                    {
                        int temp = int.MaxValue;
                        for (int j = first; j < x; j+=10)
                        {
                            temp = Math.Min(shortest[first, j]+shortest[j+1,x],temp);
                        }
                        
                        if (temp < shortest[first, x])
                        {
                            shortest[first, x] = temp;
                        }
                        
                    }


                    
                    for (int i = first; i <= end; i++)
                    {
                        var temp = shortest[first, i] + shortest[i, end]-shortest[i,i];
                        //if(first==0 && end == 11)MessageBox.Show($"{first},{end},{diff},{i},{shortest[first, i]},{shortest[i, end]}\t{temp},{shortest[0, 11]}");
                        if (temp < shortest[first, end])
                            shortest[first,end] = temp;
                    }
                    
                }
            }

            MessageBox.Show($"{shortest[0,11]}");//0 99 975

        }
  }
}