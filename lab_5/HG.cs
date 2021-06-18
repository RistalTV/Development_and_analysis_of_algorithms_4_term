using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_5
{
    class HG
    {
        const int razmer = 2;

        public double proverka(double[] a1, double[] a2)
        {
            double q = 0;

            for (int i = 0; i < razmer; i++)
            {
                q += Math.Pow((a1[i] - a2[i]), 2);
            }
            return q;
        }


        public double function(double[] x, double[] d, double l, int shtraf, double mu, double[] tb1, double[] tb2)
        {
            double[] xxx = new double[razmer];

            for (int i = 0; i < razmer; i++)
            {
                xxx[i] = x[i] + d[i] * l;
            }
            if (shtraf == 0)
                return tb1[0] * xxx[0] * xxx[0] + tb1[1] * xxx[1] * xxx[1] + tb1[2] * xxx[0] * xxx[1] + tb1[3] * xxx[0] + tb1[4] * xxx[1] + tb1[5];

            else
                return xxx[0] * xxx[0] + xxx[1] * xxx[1] - xxx[0] - 2 * xxx[1] + mu * alfa(xxx);

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////ДЛЯ ШТРАФНЫХ ФУНКЦИЙ///////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double alfa(double[] r)
        {
            double a = 0.0;
            if (3 * r[0] * r[0] + 2 * r[1] * r[1] - 6 > 0) a += 3 * r[0] * r[0] + 2 * r[1] * r[1] - 6;
            return a;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public double nach(double[] x1, double[] d, double e_gold, int shtraf, double mu, double[] tb1, double[] tb2)
        {
            double delta = 1, f2 = 0, f1 = f2 - delta, f3 = f2 + delta, q1, q2, q3;
            while (true)
            {
                q1 = function(x1, d, f1, shtraf, mu, tb1, tb2);
                q2 = function(x1, d, f2, shtraf, mu, tb1, tb2);
                q3 = function(x1, d, f3, shtraf, mu, tb1, tb2);

                if (q1 >= q2 && q2 <= q3)
                {
                    return gold(x1, f1, f3, d, e_gold, shtraf, mu, tb1, tb2);
                }

                else
              if (q1 < q3)
                {
                    f3 = f2;
                    f2 = f1;
                    f1 = f1 - delta;
                    q1 = function(x1, d, f1, shtraf, mu, tb1, tb2);
                }
                else
                {
                    f1 = f2;
                    f2 = f3;
                    f3 = f3 + delta;
                    q3 = function(x1, d, f3, shtraf, mu, tb1, tb2);
                }
            }
        }
        //
        //
        public double gold(double[] x1, double x00, double x33, double[] d, double e_gold, int shtraf, double mu, double[] tb1, double[] tb2)
        {
            double t = ((1 + Math.Sqrt(5)) / 2), b = 1 / t, a = 1 - b, I, x11, f1, f2, x22;
            I = x33 - x00;
            x11 = x00 + a * I;
            x22 = x00 + b * I;
            f1 = function(x1, d, x11, shtraf, mu, tb1, tb2);
            f2 = function(x1, d, x22, shtraf, mu, tb1, tb2);

            while (Math.Abs(I) > e_gold)
            {
                if (f2 < f1)
                {
                    I = x33 - x11;
                    x00 = x11;
                    x11 = x22;
                    x22 = x00 + b * I;
                    f1 = f2;
                    f2 = function(x1, d, x22, shtraf, mu, tb1, tb2);
                }
                else
                {
                    I = x22 - x00;
                    x33 = x22;
                    x22 = x11;
                    x11 = x00 + a * I;
                    f2 = f1;
                    f1 = function(x1, d, x11, shtraf, mu, tb1, tb2);
                }
            }

            if (f2 < f1)
                return x22;
            else
                return x11;
        }


    }
}
