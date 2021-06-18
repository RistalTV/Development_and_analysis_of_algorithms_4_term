using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_5_FD
{
    public partial class MainForm : Form
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
        public int ox;
        HG qwerty = new HG();
        public MainForm()
        {
            InitializeComponent();
            x_mid[0] = TabFD_MainPanel_outPut_Graph1.Width / 2;
            x_mid[1] = TabFD_MainPanel_outPut_Graph1.Height / 2;
            clear();
        }

        public void clear()
        {
            len = 0; len2 = 0;
            TabFD_MainPanel_outPut_1.Text = "МЕТОД НСП:\n";
            TabFD_MainPanel_outPut_2.Text = "МЕТОД ЦПС:\n";
            TabFD_MainPanel_outPut_Graph1.Invalidate();
            TabFD_MainPanel_outPut_Graph2.Invalidate();
            TabFD_MainPanel_down_StartPoint_x1.Enabled = true;
            TabFD_MainPanel_down_StartPoint_x2.Enabled = true;
            TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Enabled = true;
            TabFD_MainPanel_down_StartPoint_x1.Text = "2";
            TabFD_MainPanel_down_StartPoint_x2.Text = "2";
            TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text = "0,00001";
            TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text = "0,00001";
            TabFD_MainPanel_down_CritStopAndScale_ScaleTextBox.Text = "50";
            TabFD_MainPanel_down_CritStopAndScale_ScaleTextBox.Enabled = true;
        }

        void text()
        {
            float x1 = (point[len - 1].X - (float)x_mid[0]);
            x1 = x1 / ma;
            float x2 = (point[len - 1].Y - (float)x_mid[1]);
            x2 /= ma;
            f = 5 * x1 * x1 + 2 * x1 * x2 + 8 * x2 * x2 + x1 - x2;
            TabFD_MainPanel_outPut_1.Text += "Точка минимума:\t" + "x1= " + x1.ToString() + "\tx2=" + x2.ToString() + "\n";
            TabFD_MainPanel_outPut_1.Text += "Значение функциив точке min: f(x1,x2) = " + f.ToString() + "\n";
            TabFD_MainPanel_outPut_1.Text += "Количество базовых шагов, проделанных методом: " + (len / 3).ToString();
        }

        void text1()
        {
            float x1 = (point2[len2 - 1].X - (float)x_mid[0]);
            x1 /= ma;
            float x2 = (point2[len2 - 1].Y - (float)x_mid[1]);
            x2 /= ma;
            f2 = 5 * x1 * x1 + 2 * x1 * x2 + 8 * x2 * x2 + x1 - x2;
            TabFD_MainPanel_outPut_2.Text += "Точка минимума:\t" + "x1= " + x1.ToString() + "\tx2=" + x2.ToString() + "\n";
            TabFD_MainPanel_outPut_2.Text += "Значение функциив точке min: f(x1,x2) = " + f2.ToString() + "\n";
            TabFD_MainPanel_outPut_2.Text += "Количество базовых шагов, проделанных методом: " + (len2 / 2).ToString();
            TabFD_MainPanel_down_Buttons_PanelClear.Visible = true;
            TabFD_MainPanel_down_Buttons_PanelClear.Visible = true;
        }

        private void DSP_SteepestDescent_Start(object sender, EventArgs e)
        {
            try
            {
                pogr = double.Parse(TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text);
                x[0] = double.Parse(TabFD_MainPanel_down_StartPoint_x1.Text);
                x[1] = double.Parse(TabFD_MainPanel_down_StartPoint_x2.Text);
                ma = int.Parse(TabFD_MainPanel_down_CritStopAndScale_ScaleTextBox.Text);
                if (pogr > 0.1)
                {
                    MessageBox.Show("Критерий остановки не должен превышать 0.1");
                    TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text = "0,00001";
                    clear();
                    return;
                }
                if (ma > 100 || ma <= 0)
                {
                    MessageBox.Show("0 < Масштаб <=100 ");
                    TabFD_MainPanel_down_CritStopAndScale_ScaleTextBox.Text = "50";
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

            nsm(pogr, x, 0, 0, 0);
            text();
            x[0] = double.Parse(TabFD_MainPanel_down_StartPoint_x1.Text);
            x[1] = double.Parse(TabFD_MainPanel_down_StartPoint_x2.Text);
            cps(pogr, x, 1, 0, 0);
            text1();
            TabFD_MainPanel_down_StartPoint_x1.Enabled = false;
            TabFD_MainPanel_down_StartPoint_x2.Enabled = false;
            TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Enabled = false;
            TabFD_MainPanel_down_CritStopAndScale_ScaleTextBox.Enabled = false;
        }

        public void tochka(double x1, double x2, int key)
        {
            if (key == 0)
            {
                TabFD_MainPanel_outPut_Graph1.Invalidate();
                point[len].X = (float)((x1 * ma + x_mid[0]));
                point[len].Y = (float)((x2 * ma + x_mid[1]));
                len++;
            }

            if (key == 1)
            {
                TabFD_MainPanel_outPut_Graph2.Invalidate();
                point2[len2].X = (float)((x1 * ma + x_mid[0]));
                point2[len2].Y = (float)((x2 * ma + x_mid[1]));
                len2++;
            }

            if (key == 2)
            {
                TabFD_MainPanel_down_Buttons_PanelClear.Invalidate();
                point3[len3].X = (float)((x1 * ma + x_mid[0] * 1.5));
                point3[len3].Y = (float)((x2 * ma + x_mid[1] * 1.5));
                len3++;
            }
        }

        void grad(double[] d, double[] x)
        {//x1^2 + x2^2 - 5*x1 - 10*x2
            d[0] = (-1) * ( 2*x[0] + Math.Pow(x[1],2) - 5 - 10*x[1]); //2*x1 + x2^2 - 5 - 10*x2 
            d[1] = (-1) * ( Math.Pow(x[1], 2) + 2* x[1] - 5*x[0] -10);//x1^2 + 2*x2 - 5*x1 - 10
        }

        public double[] nsm(double pogr, double[] x, int cps, int shtraf, double mu)
        {
            int i;
            double epselum = 5.0, e_gold = pogr / razmer, j = 0;
            pogr = double.Parse(TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text);
            double[] du = new double[razmer];
            double[] y = new double[razmer];
            double[] y1 = new double[razmer];
            double[] d = new double[razmer];

            while (pogr < epselum)
            {
                for (i = 0; i < razmer; i++)
                    y[i] = x[i];

                tochka(x[0], x[1], cps);

                // Предворительный шаг ЦПС
                grad(d, y);
                j = qwerty.nach(x, d, e_gold, shtraf, mu, tb1, tb2);
                for (i = 0; i < razmer; i++)
                    x[i] = x[i] + j * d[i];
                tochka(x[0], x[1], cps);

                epselum = Math.Sqrt(qwerty.proverka(x, y));
            }
            return x;
        }

        public double[] cps(double pogr, double[] x, int cps, int shtraf, double mu)
        {
            int i;
            double epselum = 5.0, e_gold = pogr / razmer, j = 0;
            pogr = double.Parse(TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text);
            double[] du = new double[razmer];
            double[] y = new double[razmer];
            double[] y1 = new double[razmer];
            double[][] d = new double[2][];
            for (int q = 0; q < 2; q++) d[q] = new double[2];
            d[0][0] = 1;  d[1][0] = 0;
            d[0][1] = 0;  d[1][1] = 1;
            while (pogr < epselum)
            {
                for (i = 0; i < razmer; i++)
                    y[i] = x[i];
                tochka(x[0], x[1], cps);
                // Предворительный шаг ЦПС
                for (i = 0; i < razmer; i++)
                {
                    j = qwerty.nach(x, d[i], e_gold, shtraf, mu, tb1, tb2);
                    x[i] = x[i] + j;
                    tochka(x[0], x[1], cps);
                }
                epselum = Math.Sqrt(qwerty.proverka(x, y));
            }
            return x;
        }
        #region График
        private void Paint_pantl(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
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
                g.FillEllipse(Brushes.Green, (float)point3[len3 - 1].X, (float)point3[len3 - 1].Y, 3, 3);
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            TabFD_MainPanel_down_Buttons_PanelClear.Visible = false;
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] re = new double[2];
            double[] ru = new double[2];
            try
            {
                re[0] = double.Parse(TabPenFunc_MainPanel_Right_StartPoints_Points_x1.Text);
                re[1] = double.Parse(TabPenFunc_MainPanel_Right_StartPoints_Points_x2.Text);
                ma = int.Parse(TabPenFunc_MainPanel_Right_Scale_textBox.Text);
                if (ma > 100 || ma <= 0)
                {
                    MessageBox.Show("0 < Масштаб<= 100");
                    TabPenFunc_MainPanel_Right_Scale_textBox.Text = "50";
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверные входные данные.");
                TabPenFunc_MainPanel_Right_Scale_textBox.Text = "50";
                TabPenFunc_MainPanel_Right_StartPoints_Points_x1.Text = "5";
                TabPenFunc_MainPanel_Right_StartPoints_Points_x2.Text = "5";
                return;
            }
            TabPenFunc_MainPanel_Right_Graph.Invalidate();
            qwerty.alfa(re);
            if (Math.Abs(re[0]) <= 1 && Math.Abs(re[1]) <= 1) re[0] = 2;
            while (Math.Abs(qwerty.alfa(re)) > pogr)
            {
                pogr = double.Parse(TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text);
                ru = cps(pogr, re, 2, 1, mu);
                re = ru;
                if (Math.Abs(qwerty.alfa(re)) < pogr) break;
                mu = mu * 10;
                pogr = double.Parse(TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text);
            }
            double fvr;
            fvr = qwerty.function(ru, ru, 0, 1, 0);
            TabPenFunc_MainPanel_Right_OutPut.Text = "Точка минимумума\nс учетом допустимой области:\n";
            TabPenFunc_MainPanel_Right_OutPut.Text += "x1=" + ru[0].ToString() + "\n" 
                + "x2=" + ru[1].ToString() + "\n\nf(x1,x2)= " + fvr.ToString();
            TabPenFunc_MainPanel_Right_StartPoints_Points_x1.Enabled = false;
            TabPenFunc_MainPanel_Right_StartPoints_Points_x2.Enabled = false;
            TabPenFunc_MainPanel_Right_Buttons_Start.Enabled = false;
            TabPenFunc_MainPanel_Right_Scale_textBox.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            len3 = 0;
            TabPenFunc_MainPanel_Right_Graph.Invalidate();
            mu = 0.1;
            TabPenFunc_MainPanel_Right_StartPoints_Points_x1.Enabled = true;
            TabPenFunc_MainPanel_Right_StartPoints_Points_x2.Enabled = true;
            TabPenFunc_MainPanel_Right_Buttons_Start.Enabled = true;
            TabPenFunc_MainPanel_Right_Scale_textBox.Enabled = true;
            TabPenFunc_MainPanel_Right_OutPut.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabFD_MainPanel_down_CritStopAndScale_CritStopTextBox.Text = "0,00001";
            TabFD_MainPanel_down_StartPoint_x1.Text = "2";
            TabFD_MainPanel_down_StartPoint_x2.Text = "2";
        }
    }
}


