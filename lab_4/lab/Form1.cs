using System;
using System.Windows.Forms;
namespace lab
{    public partial class Form1 : Form    {
        public Form1()
        {
            InitializeComponent();
        }
        //<summary>
        //  Метод Симпсона
        //</summary>
        private void tabPage1_button1_Click(object sender, EventArgs e)
        {
            double I,  I1,  a = ((double)tabPage1_numericUpDown1.Value), 
                b = ((double)tabPage1_numericUpDown2.Value), 
                eps = ((double)tabPage1_numericUpDown3.Value);
            I = eps + 1; I1 = 0;
            for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
            {
                double h, sum2 = 0, sum4 = 0, sum = 0;
                h = (b - a) / (2 * N);
                for (int i = 1; i <= 2 * N - 1; i += 2)
                {
                    sum4 += f(a + h * i);
                    sum2 += f(a + h * (i + 1));
                }
                sum = f(a) + 4 * sum4 + 2 * sum2 - f(b);  I = I1;  I1 = (h / 3) * sum; }
            tabPage1_richTextBox1.Text = "";
            tabPage1_richTextBox1.Text += $"    dx\n---------------  = {I1}\n(1+Cos(x))";
        }
        double f(double x)
        {
            return 1/(1 + Math.Cos(x));
        }
        double Integ(double a, double b, int n, double y)
        {
            return ((b - a) / (2 * n) * y);
        }
        double integral_l(double a, double b, int n)
        {
            double x, h=(b - a) / n; double sum = 0.0;
            double fx;
            for (int i = 0; i < n; i++)
            { x = a + i * h;   fx = f(x);  sum += fx;    }
            return (sum * h);
        }
        double integral_r(double a, double b, int n)
        {
            double x, h;  double sum = 0.0;  double fx;
            h = (b - a) / n;
            for (int i = 1; i <= n; i++)
            {   x = a + i * h;  fx = f(x);    sum += fx; }
            return (sum * h);
        }
        //<summary>
        // Метод трапеций
        //</summary>
        private void tabPage2_button1_Click(object sender, EventArgs e)
        {
            int n = ((int)tabPage2_numericUpDown3.Value);
            double In,
                a = ((double)tabPage2_numericUpDown1.Value),
                b = ((double)tabPage2_numericUpDown2.Value),
                dy = (b - a) / n,  y = 0;         y += f(a) + f(b);
            for (int i = 1; i < n; i++)
            {                y += 2 * (f(a + dy * i));
            }            In = Integ(a, b, n, y);
            tabPage2_richTextBox1.Text = "";
            tabPage2_richTextBox1.Text += $"    dx\n---------------  = {In}\n(1+Cos(x))";
        }
        //<summary>
        //  Метод левых прямоугольников.
        //</summary>
        private void tabPage3_button2_Click(object sender, EventArgs e)
        {            double a = ((double)tabPage3_numericUpDown4.Value),
                b = ((double)tabPage3_numericUpDown5.Value),
                eps = ((double)tabPage3_numericUpDown6.Value);
            double s1, s;
            int n = 1;
            s1 = integral_l(a, b, n);
            do
            {
                s = s1;
                n = 2 * n;
                s1 = integral_l(a, b, n); 
            } while (Math.Abs(s1 - s) > eps);
            tabPage3_richTextBox2.Text = "";
            tabPage3_richTextBox2.Text += $"    dx\n---------------  = {s1}\n(1+Cos(x))";
        }
        //<summary>
        //  Метод правых прямоугольников.
        //</summary>
        private void tabPage4_button3_Click(object sender, EventArgs e)
        {
            double a = ((double)tabPage4_numericUpDown7.Value),
                b = ((double)tabPage4_numericUpDown8.Value),
                eps = ((double)tabPage4_numericUpDown9.Value);
            double s1, s;            int n = 1;
            s1 = integral_r(a, b, n);
            do            {                s = s1;
                n = 2 * n;
                s1 = integral_r(a, b, n);
            } while (Math.Abs(s1 - s) > eps);
            tabPage4_richTextBox3.Text = "";
            tabPage4_richTextBox3.Text += $"    dx\n---------------  = {s1}\n(1+Cos(x))";
        }
        //<summary>
        //  Метод средних прямоугольников.
        //</summary>
        private void tabPage5_button4_Click(object sender, EventArgs e)
        {
            double a = ((double)tabPage5_numericUpDown10.Value),
                b = ((double)tabPage5_numericUpDown11.Value),
                eps = ((double)tabPage5_numericUpDown12.Value),
                n = ((double)tabPage5_numericUpDown1.Value),
                s,                 s1,
            h = b - a;
            s = h * f(((a + b) / 2));
            do
            {
                h = (b - a) / n;
                s1 = s;
                s = 0;
                for (int k = 0; k <= n - 1; k++)
                {
                    s = s + f((a + h / 2 + k * h));
                }
                s = s * h;
            } while (((Math.Abs(s - s1)) / 3) > eps);
            tabPage5_richTextBox4.Text = "";
            tabPage5_richTextBox4.Text += $"    dx\n---------------  = {s}\n(1+Cos(x))";
            var date1 = new DateTime(2008, 5, 1, 8, 30, 52);
        }
    }
}