using System;
namespace lab_5_FD
{class HG
    {const int razmer = 2;
        public double proverka(double[] a1, double[] a2)
        {double q = 0;
            for (int i = 0; i < razmer; i++)
            {q += Math.Pow((a1[i] - a2[i]), 2);}
            return q;}
        // Функция
        public double function(double[] _x, double[] d, double l, int shtraf, double mu)
        {double[] x = new double[razmer];
            for (int i = 0; i < razmer; i++)
            {x[i] = _x[i] + d[i] * l;}
            // Функции
            if (shtraf == 0)
                // ЦПС, Наискорейший спуск
                return 5 * Math.Pow(x[0], 2) + 2 * x[0] * x[1] + 8 * Math.Pow(x[1], 2) + x[0] - 2 * x[1]; 
            else
                // Штрафных функций
                return (Math.Pow(x[0], 2) + Math.Pow(x[1], 2) - 5 * x[0] - 10 * x[1] + mu * alfa(x));}
		// Функция штрафа 
        public double alfa(double[] r)
        {
            double a = 0.0;
            if (r[0] * r[0] - 2 * r[1] > 0)
                // Обратное допустимой 1 половина области и н же штраф
                { a += r[0] * r[0] - 2 * r[1]; }
            if ((-2) * r[0] + r[1] > 0)
                // Обратное допустимой 1 половина области и н же штраф
                { a += (-2) * r[0] + r[1]; }
            return a;}
        public double nach(double[] x1, double[] d, double e_gold, int shtraf, double mu, double[] tb1, double[] tb2)
        {double delta = 1, f2 = 0, f1 = f2 - delta, f3 = f2 + delta, q1, q2, q3;
            while (true){
                q1 = function(x1, d, f1, shtraf, mu);
                q2 = function(x1, d, f2, shtraf, mu);
                q3 = function(x1, d, f3, shtraf, mu);
                if (q1 >= q2 && q2 <= q3)
                {
                    return gold(x1, f1, f3, d, e_gold, shtraf, mu, tb1, tb2);
                }
                else if (q1 < q3)
                {
                    f3 = f2;
                    f2 = f1;
                    f1 = f1 - delta;
                    q1 = function(x1, d, f1, shtraf, mu);
                }
                else
                {
                    f1 = f2;
                    f2 = f3;
                    f3 = f3 + delta;
                    q3 = function(x1, d, f3, shtraf, mu);
                }}}
        public double gold(double[] x1, double x00, double x33, double[] d, double e_gold, int shtraf, double mu, double[] tb1, double[] tb2)
        {double t = ((1 + Math.Sqrt(5)) / 2), b = 1 / t, a = 1 - b, I, x11, f1, f2, x22;
            I = x33 - x00;
            x11 = x00 + a * I;
            x22 = x00 + b * I;
            f1 = function(x1, d, x11, shtraf, mu);
            f2 = function(x1, d, x22, shtraf, mu);
            while (Math.Abs(I) > e_gold)
            {if (f2 < f1){
                    I = x33 - x11;
                    x00 = x11;
                    x11 = x22;
                    x22 = x00 + b * I;
                    f1 = f2;
                    f2 = function(x1, d, x22, shtraf, mu);
                }else{
                    I = x22 - x00;
                    x33 = x22;
                    x22 = x11;
                    x11 = x00 + a * I;
                    f2 = f1;
                    f1 = function(x1, d, x11, shtraf, mu);
                }}
            if (f2 < f1)
                return x22;
            else
                return x11;
			}}}