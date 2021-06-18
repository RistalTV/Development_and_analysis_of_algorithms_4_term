using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab_5
{
    public partial class Form1 : Form
    {
        public const int razmer = 2;
        public double mu = 0.1;
        public int ma;
        public double[] x_mid = new double[2];
        public PointF[] point = new PointF[1000];
        public PointF[] point2 = new PointF[1000];
        public PointF[] point3 = new PointF[1000];

        public int flag = 0, flag2 = 0;
        public int len = 0, len2 = 0, len3 = 0;
        public double[] x = new double[razmer];
        public double[] tb1 = new double[6];
        public double[] tb2 = new double[6];
        public double pogr, f = 0.0, f2 = 0.0;
        //public int cps;
        public int ox;
        HG qwerty = new HG();
        public Form1()
        {

            InitializeComponent();
            x_mid[0] = TabHG_tableLayoutPanel_panel_UP_Graph_1.Width / 2;
            x_mid[1] = TabHG_tableLayoutPanel_panel_UP_Graph_1.Height / 2;
            clear();
        }

        public void clear()
        {
            len = 0; len2 = 0;
            TabHG_RichTextBox_HookJivs.Text = "МЕТОД ХУКА - ДЖИВСА:\n";
            TabHG_RichTextBox_MethodDSP.Text = "МЕТОД ЦПС:\n";
            TabHG_tableLayoutPanel_panel_UP_Graph_1.Invalidate();
            TabHG_tableLayoutPanel_panel_UP_Graph_2.Invalidate();
            TabHG_TextBox_StartPoint_x1.Enabled = true;
            TabHG_TextBox_StartPoint_x2.Enabled = true;
            TabHG_TextBox_Optional_eps.Enabled = true;
            TabHG_TextBox_StartPoint_x1.Text = "2";
            TabHG_TextBox_StartPoint_x2.Text = "2";
            TabHG_TextBox_Optional_eps.Text = "0,00001";
            TabHG_TextBox_1_СoeFunc.Text = "1";
            TabHG_TextBox_2_СoeFunc.Text = "7";
            TabHG_TextBox_3_СoeFunc.Text = "4";
            TabHG_TextBox_5_СoeFunc.Text = "0";
            TabHG_TextBox_6_СoeFunc.Text = "5";
            TabHG_TextBox_7_СoeFunc.Text = "0";
            TabHG_TextBox_Optional_eps.Text = "0,00001";
            TabHG_TextBox_Optional_Scale.Text = "50";
            TabHG_TextBox_Optional_Scale.Enabled = true;
        }

        void text()
        {
            float x1 = (point[len - 1].X - (float)x_mid[0]);
            x1 = x1 / ma;
            float x2 = (point[len - 1].Y - (float)x_mid[1]);
            x2 /= ma;
            f = x1 * x1 + 4 * x1 * x2 + 7 * x2 * x2 + 5 * x2;
            TabHG_RichTextBox_HookJivs.Text += "Точка минимума:\t" + "x1= " + x1.ToString() + "\tx2=" + x2.ToString() + "\n";
            TabHG_RichTextBox_HookJivs.Text += "Значение функциив точке min: f(x1,x2) = " + f.ToString() + "\n";
            //richTextBox1.Text += "Количество базовых шагов, проделанных методом: " + (len/3).ToString();                       
        }

        void text1()
        {
            float x1 = (point2[len2 - 1].X - (float)x_mid[0]);
            x1 /= ma;
            float x2 = (point2[len2 - 1].Y - (float)x_mid[1]);
            x2 /= ma;
            f2 = x1 * x1 + 4 * x1 * x2 + 7 * x2 * x2 + 5 * x2;
            TabHG_RichTextBox_MethodDSP.Text += "Точка минимума:\t" + "x1= " + x1.ToString() + "\tx2=" + x2.ToString() + "\n";
            TabHG_RichTextBox_MethodDSP.Text += "Значение функциив точке min: f(x1,x2) = " + f2.ToString() + "\n";
            //richTextBox2.Text += "Количество базовых шагов, проделанных методом: " + (len2/2).ToString();
            TabHG_tableLayoutPanel_Down_panel_Clear.Visible = true;
            TabHG_tableLayoutPanel_Down_panel_Clear.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pogr = double.Parse(TabHG_TextBox_Optional_eps.Text);
                x[0] = double.Parse(TabHG_TextBox_StartPoint_x1.Text);
                x[1] = double.Parse(TabHG_TextBox_StartPoint_x2.Text);
                tb1[0] = double.Parse(TabHG_TextBox_1_СoeFunc.Text);
                tb1[1] = double.Parse(TabHG_TextBox_2_СoeFunc.Text);
                tb1[2] = double.Parse(TabHG_TextBox_3_СoeFunc.Text);
                tb1[3] = double.Parse(TabHG_TextBox_5_СoeFunc.Text);
                tb1[4] = double.Parse(TabHG_TextBox_6_СoeFunc.Text);
                tb1[5] = double.Parse(TabHG_TextBox_7_СoeFunc.Text);
                ma = int.Parse(TabHG_TextBox_Optional_Scale.Text);

                if (pogr > 0.1)
                {
                    MessageBox.Show("Критерий остановки не должен превышать 0.1");
                    TabHG_TextBox_Optional_eps.Text = "0,00001";
                    clear();
                    return;
                }
                if (ma > 100 || ma <= 0)
                {
                    MessageBox.Show("0 < Масштаб <=100 ");
                    TabHG_TextBox_Optional_Scale.Text = "50";
                    clear();
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверные входные данные. Повторите снова.");
                clear();
                return;
            }

            huk(pogr, x, 0, 0, 0, tb1, tb2);
            text();
            x[0] = double.Parse(TabHG_TextBox_StartPoint_x1.Text);
            x[1] = double.Parse(TabHG_TextBox_StartPoint_x2.Text);
            huk(pogr, x, 1, 0, 0, tb1, tb2);
            text1();
            TabHG_TextBox_StartPoint_x1.Enabled = false;
            TabHG_TextBox_StartPoint_x2.Enabled = false;
            TabHG_TextBox_Optional_eps.Enabled = false;
            TabHG_TextBox_Optional_Scale.Enabled = false;
        }

        public void tochka(double xxx1, double xxx2, int key)
        {
            if (key == 0)
            {
                TabHG_tableLayoutPanel_panel_UP_Graph_1.Invalidate();
                point[len].X = (float)((xxx1 * ma + x_mid[0]));
                point[len].Y = (float)((xxx2 * ma + x_mid[1]));
                len++;
            }

            if (key == 1)
            {
                TabHG_tableLayoutPanel_panel_UP_Graph_2.Invalidate();
                point2[len2].X = (float)((xxx1 * ma + x_mid[0]));
                point2[len2].Y = (float)((xxx2 * ma + x_mid[1]));
                len2++;
            }

            if (key == 2)
            {
                TabHG_tableLayoutPanel_Down_panel_Clear.Invalidate();
                point3[len3].X = (float)((xxx1 * ma + x_mid[0] * 1.5));
                point3[len3].Y = (float)((xxx2 * ma + x_mid[1] * 1.5));
                len3++;
            }
        }

        public double[] huk(double pogr, double[] x, int cps, int shtraf, double mu, double[] tb1, double[] tb2)
        {
            int i;
            double epselum = 5.0, e_gold = pogr / razmer, j = 0;
            pogr = double.Parse(TabHG_TextBox_Optional_eps.Text);
            double[] du = new double[razmer];
            double[] y = new double[razmer];
            double[] xs = new double[razmer];
            double[] xs2 = new double[razmer];
            for (i = 0; i < razmer; i++) { xs[i] = x[i]; xs2[i] = x[i]; }
            double[][] d = new double[2][];

            for (int q = 0; q < 2; q++) d[q] = new double[2];
            d[0][0] = 1; d[0][1] = 0; d[1][0] = 0; d[1][1] = 1;
            while (pogr < epselum)
            {
                for (i = 0; i < razmer; i++)
                {
                    y[i] = x[i];
                }
                ////////////////////
                tochka(y[0], y[1], cps);
                ////////////////////
                //predvaritelnii shag CPS
                for (i = 0; i < razmer; i++)
                {
                    j = qwerty.nach(y, d[i], e_gold, shtraf, mu, tb1, tb2);
                    y[i] = y[i] + j;
                    ////////////////////////
                    tochka(y[0], y[1], cps);
                    ////////////////////////
                }

                for (i = 0; i < razmer; i++) { xs[i] = xs2[i]; xs2[i] = y[i]; }

                if (cps == 0)
                {
                    // Vichislenie vectora Uskoryaushego shaga
                    for (i = 0; i < razmer; i++)
                        du[i] = xs[i] - xs2[i];
                    //Uskoryaushii shag
                    j = qwerty.nach(y, du, e_gold, shtraf, mu, tb1, tb2);

                    for (i = 0; i < razmer; i++)
                        y[i] = y[i] + j * du[i];

                    tochka(y[0], y[1], cps);
                }
                epselum = Math.Sqrt(qwerty.proverka(x, y));

                for (i = 0; i < razmer; i++)
                {
                    x[i] = y[i];
                }
            }
            return x;
        }


        /*
          ________       _______         _______           ____________
         |              |       |       |       |         |     |      |    |      |        |   /
         |              |       |       |       |         |     |      |    |     /|        |  /
         |              |_______|       |_______|         |_____|______|    |    / |        |/
         |              |               |       |               |           |   /  |        | \
         |              |               |       |               |           |  /   |        |  \
         |              |               |       |               |           |/     |        |   \ 
        */
        private void Paint_pantl(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font txt1 = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString("x1", txt1, Brushes.Gray, (float)(TabHG_tableLayoutPanel_panel_UP_Graph_2.Size.Width - 20), (float)(x_mid[1]));
            g.DrawString("x2", txt1, Brushes.Gray, (float)(x_mid[0]), (float)(TabHG_tableLayoutPanel_panel_UP_Graph_2.Size.Height - 20));
            g.DrawLine(Pens.Gray, (float)x_mid[0], 0, (float)x_mid[0], 500);
            g.DrawLine(Pens.Gray, 0, (float)x_mid[1], 500, (float)x_mid[1]);
            for (int i = 1; i < len; i++)
            {
                g.DrawLine(Pens.Red, point[i - 1].X, point[i - 1].Y, point[i].X, point[i].Y);
                g.FillEllipse(Brushes.Green, (float)point[len - 1].X, (float)point[len - 1].Y, 3, 3);

            }

        }


        private void Paint_pantl2(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font txt1 = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString("x1", txt1, Brushes.Gray, (float)(TabHG_tableLayoutPanel_panel_UP_Graph_1.Size.Width - 20), (float)(x_mid[1]));
            g.DrawString("x2", txt1, Brushes.Gray, (float)(x_mid[0]), (float)(TabHG_tableLayoutPanel_panel_UP_Graph_1.Size.Height - 20));
            g.DrawLine(Pens.Gray, (float)x_mid[0], 0, (float)x_mid[0], 500);
            g.DrawLine(Pens.Gray, 0, (float)x_mid[1], 500, (float)x_mid[1]);
            for (int i = 1; i < len2; i++)
            {
                g.DrawLine(Pens.Red, point2[i - 1].X, point2[i - 1].Y, point2[i].X, point2[i].Y);
                g.FillEllipse(Brushes.Green, (float)point2[len2 - 1].X, (float)point2[len2 - 1].Y, 3, 3);
            }

        }

        private void Paint_pantl3(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.Gray, (float)(x_mid[0] * 1.5), 0, (float)(x_mid[0] * 1.5), 1000);
            g.DrawLine(Pens.Gray, 0, (float)(x_mid[1] * 1.5), 1000, (float)(x_mid[1] * 1.5));
            Font txt1 = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString("x1", txt1, Brushes.Gray, (float)(panel4.Size.Width - 20), (float)(x_mid[1] * 1.5));
            g.DrawString("x2", txt1, Brushes.Gray, (float)(x_mid[0] * 1.5), (float)(panel4.Size.Height - 20));
            double[] vr = new double[2];
            if (len3 > 0)
            {
                double vr1 = -100, vr2 = -100;
                while (vr1 < 100)
                {
                    vr[0] = vr1;
                    while (vr2 < 100)
                    {
                        vr[1] = vr2;
                        if (qwerty.alfa(vr) <= 0) g.FillEllipse(Brushes.Black, (float)((vr[0] * ma + x_mid[0] * 1.5)), (float)((vr[1] * ma + x_mid[1] * 1.5)), (int)(2), (int)(2));
                        vr2 += 0.3;
                    }
                    vr2 = -1000;
                    vr1 += 0.3;
                }
            }
            for (int i = 1; i < len3; i++)
            {
                g.DrawLine(Pens.Red, point3[i - 1].X, point3[i - 1].Y, point3[i].X, point3[i].Y);
                g.FillEllipse(Brushes.Green, (float)point3[len3 - 1].X, (float)point3[len3 - 1].Y, 4, 4);

            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e)
        {
            TabHG_tableLayoutPanel_Down_panel_Clear.Visible = false;
            clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] re = new double[2];
            double[] ru = new double[2];
            try
            {
                re[0] = double.Parse(TabPenFunc_TextBox_1_StartPointX1.Text);
                re[1] = double.Parse(TabPenFunc_TextBox_2_StartPointX2.Text);
                ma = int.Parse(TabPenFunc_TextBox_3_scale.Text);
                if (ma > 100 || ma <= 0)
                {
                    MessageBox.Show("0 < Масштаб<= 100");
                    TabPenFunc_TextBox_3_scale.Text = "50";
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверные входные данные.");
                TabPenFunc_TextBox_3_scale.Text = "50";
                TabPenFunc_TextBox_1_StartPointX1.Text = "5";
                TabPenFunc_TextBox_2_StartPointX2.Text = "5";
                return;
            }
            panel4.Invalidate();
            pogr = -10;
            while (Math.Abs(qwerty.alfa(re)) > pogr)
            {
                pogr = double.Parse(TabHG_TextBox_Optional_eps.Text);
                ru = huk(pogr, re, 2, 1, mu, tb1, tb2);
                re = ru;
                if (Math.Abs(qwerty.alfa(re)) < pogr) break;
                mu = mu * 10;
                pogr = double.Parse(TabHG_TextBox_Optional_eps.Text);
            }
            double fvr;
            fvr = qwerty.function(ru, ru, 0, 1, 0, ru, ru);
            TabPenFunc_RichTextBox_PenaltyFunction.Text = "Точка минимумума\nс учетом допустимой области:\n";
            TabPenFunc_RichTextBox_PenaltyFunction.Text += "x1=" + ru[0].ToString() + "\n" + "x2=" + ru[1].ToString() + "\n\nf(x1,x2)= " + fvr.ToString();
            TabPenFunc_TextBox_1_StartPointX1.Enabled = false;
            TabPenFunc_TextBox_2_StartPointX2.Enabled = false;
            TabPenFunc_Btn_Start.Enabled = false;
            TabPenFunc_TextBox_3_scale.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            len3 = 0;
            panel4.Invalidate();
            mu = 0.1;
            TabPenFunc_TextBox_1_StartPointX1.Enabled = true;
            TabPenFunc_TextBox_2_StartPointX2.Enabled = true;
            TabPenFunc_Btn_Start.Enabled = true;
            TabPenFunc_TextBox_3_scale.Enabled = true;
            TabPenFunc_RichTextBox_PenaltyFunction.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabHG_TextBox_1_СoeFunc.Text = "1";
            TabHG_TextBox_2_СoeFunc.Text = "7";
            TabHG_TextBox_3_СoeFunc.Text = "4";
            TabHG_TextBox_5_СoeFunc.Text = "0";
            TabHG_TextBox_6_СoeFunc.Text = "5";
            TabHG_TextBox_7_СoeFunc.Text = "0";
            TabHG_TextBox_Optional_eps.Text = "0,00001";
            TabHG_TextBox_StartPoint_x1.Text = "2";
            TabHG_TextBox_StartPoint_x2.Text = "2";

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


    }
}


