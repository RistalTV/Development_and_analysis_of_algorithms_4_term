using System;
using System.Windows.Forms;

namespace lab_3_new
{
    public partial class Form1 : Form
    {
        double[,] BaseMatrix = new double[3, 3]; // матрица коэффициентов
        double[] ResMatrix = new double[3]; // матрица правой части СЛАУ
        double[] Roots = new double[3]; // матрица корней СЛАУ
        double checkSum = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            output.Text = "";
            BaseMatrix[0, 0] = Convert.ToDouble(A11.Value);
            BaseMatrix[0, 1] = Convert.ToDouble(A12.Value);
            BaseMatrix[0, 2] = Convert.ToDouble(A13.Value);
            BaseMatrix[1, 0] = Convert.ToDouble(A21.Value);
            BaseMatrix[1, 1] = Convert.ToDouble(A22.Value);
            BaseMatrix[1, 2] = Convert.ToDouble(A23.Value);
            BaseMatrix[2, 0] = Convert.ToDouble(A31.Value);
            BaseMatrix[2, 1] = Convert.ToDouble(A32.Value);
            BaseMatrix[2, 2] = Convert.ToDouble(A33.Value);
            ResMatrix[0] = Convert.ToInt32(rA1.Value);
            ResMatrix[1] = Convert.ToInt32(rA2.Value);
            ResMatrix[2] = Convert.ToInt32(rA3.Value);
            output.Text += "Решение СЛАУ классическим методом Гаусса.\n ";
            Roots = GaussMethod(BaseMatrix, ResMatrix);
            for (int i = 0; i < 3; i++)
            {
                output.Text += $"x{i + 1} = {Roots[i]}; \n";
            }
            output.Text += "Проверка полученных корней: \n";
            for (int i = 0; i < 3; i++)
            {
                checkSum = 0;
                for (int j = 0; j < 3; j++)
                {
                    checkSum += BaseMatrix[i, j] * Roots[j];
                }
                if (Math.Round(checkSum) == ResMatrix[i])
                {
                    output.Text += $"Проверка {i + 1} строки прошла успешно!\n";
                }
                else
                {
                    output.Text += "В вычислениях допущена ошибка!\n";
                    break;
                }
            }
            output.Text += "\n";
            output.Text += "Решение СЛАУ первой модификацией метода Гаусса. \n";
            Roots = new double[3];
            Roots = GaussMethod_mod1(BaseMatrix, ResMatrix);
            output.Text += "Корни уравнения, полученные алгоритмом: \n";
            for (int i = 0; i < 3; i++)
            {
                output.Text += $"x{i + 1} = {Roots[i]}; \n";
            }
            output.Text += "\n";
            output.Text += $"Решение СЛАУ второй модификацией метода Гаусса. \n";
            Roots = new double[3];
            Roots = GaussMethod_mod2(BaseMatrix, ResMatrix);
            output.Text += $"Корни уравнения, полученные алгоритмом: \n";
            for (int i = 0; i < 3; i++)
            {
                output.Text += $"x{i + 1} = {Roots[i]}; \n";
            }
            output.Text += "\n";
            output.Text += $"Решение СЛАУ третьей модификацией метода Гаусса. \n";
            Roots = new double[3];
            Roots = GaussMethod_mod3(BaseMatrix, ResMatrix);
            output.Text += $"Корни уравнения, полученные алгоритмом: \n";
            for (int i = 0; i < 3; i++)
            {
                output.Text += $"x{i + 1} = {Math.Round(Roots[i])}; \n"; 
            }
            output.Text += "\n";
            output.Text += $"Решение СЛАУ методом Жордана. \n";
            Roots = new double[3];
            Roots = JordanMethod(BaseMatrix, ResMatrix, output.Text);
            output.Text += $"Корни уравнения, полученные алгоритмом: \n";
            for (int i = 0; i < 3; i++)
            {
                output.Text += $"x{i + 1} = {Math.Round(Roots[i])}; \n";
            }
            output.Text += "\n";
        }
        // классический метод Гаусса
        static double[] GaussMethod(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }
            // алгоритм решения СЛАУ методом Гаусса
            // прямой ход
            for (int i = 0; i < 3; i++) // внеш цикл по строкам
            {
                LeadingElem = ExtMatrix[i, i];
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for (int j = i + 1; j < 3; j++) // цикл по строкам
                {
                    checkElem = ExtMatrix[j, i];
                    for (int k = i; k < 4; k++) // внутр цикл по столбцам
                    {
                        ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                    }
                }
            }
            // обратный ход
            double sum;
            for (int i = 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                        sum += ExtMatrix[i, j] * result[j];
                }
                result[i] = ExtMatrix[i, 3] - sum;
            }
            return result;
        }
        // модиф метода Гаусса с выбором вед элемента по строке
        static double[] GaussMethod_mod1(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            bool IsSwapped = false;
            int pos = 0; // номер главного элемента в строке
            int[] positions = new int[3] { 0, 1, 2 }; // номера элементов в векотре решения слау
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                for (int j = i; j < 3; j++)
                {
                    if (Math.Max(LeadingElem, Math.Abs(ExtMatrix[i, j])) != LeadingElem)
                    {
                        IsSwapped = true;
                        LeadingElem = Math.Abs(ExtMatrix[i, j]); // выбор большего элемента
                        pos = j; // отслеживание его номера
                    }
                }
                if (IsSwapped)
                {
                    Swap<int>(ref positions[i], ref positions[pos]); // перестановка номеров элементов вектора решения СЛАУ
                    for (int k = 0; k < 3; k++)
                    {
                        Swap<double>(ref ExtMatrix[k, i], ref ExtMatrix[k, pos]); // перестановка столбцов расширенной матрицы
                    }
                }
                // прямой ход
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for (int j = i + 1; j < 3; j++) // цикл по строкам
                {
                    checkElem = ExtMatrix[j, i];
                    for (int k = i; k < 4; k++) // внутр цикл по столбцам
                    {
                        ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                    }
                }
            }
            // обратный ход
            double sum;
            for (int i = 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                        sum += ExtMatrix[i, j] * result[j];
                }
                result[i] = ExtMatrix[i, 3] - sum;
            }
            for (int i = 0; i < 3; i++)
            {
                int a = i;
                for (int j = 0; j < 3; j++)
                {
                    if (positions[j] == i)
                    {
                        a = j;
                        break;
                    }
                }
                Swap<double>(ref result[i], ref result[a]);
            }
            return result;
        }
        // модификация метода Гаусса с выбором элемента по столбцу
        static double[] GaussMethod_mod2(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            bool IsSwapped = false;
            int pos = 0; // номер главного элемента в строке
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                for (int j = i; j < 3; j++)
                {
                    if (Math.Max(LeadingElem, Math.Abs(ExtMatrix[j, i])) != LeadingElem)
                    {
                        IsSwapped = true;
                        LeadingElem = Math.Abs(ExtMatrix[j, i]); // выбор большего элемента
                        pos = j; // отслеживание номера
                    }
                }
                if (IsSwapped)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Swap<double>(ref ExtMatrix[i, k], ref ExtMatrix[pos, k]); // перестановка строк расширенной матрицы
                    }
                }
                // прямой ход
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for (int j = i + 1; j < 3; j++) // цикл по строкам
                {
                    checkElem = ExtMatrix[j, i];
                    for (int k = i; k < 4; k++) // внутр цикл по столбцам
                    {
                        ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                    }
                }
            }
            // обратный ход
            double sum;
            for (int i = 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                        sum += ExtMatrix[i, j] * result[j];
                }
                result[i] = ExtMatrix[i, 3] - sum;
            }
            return result;
        }
        // модиф метода Гаусса с выбором элемента по непреобразованной части М-ы
        static double[] GaussMethod_mod3(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double signedLeadingElem = 0;
            double checkElem = 0;
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            bool IsSwapped = false;
            int posX = 0; // номер столбца главного элемента в строке
            int posY = 0; // номер строки главного элемента в столбце
            int[] positions = new int[3] { 0, 1, 2 }; // номера элементов в векотре решения слау
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                signedLeadingElem = ExtMatrix[i, i];
                for (int j = i; j < 3; j++)
                {
                    for (int k = i; k < 3; k++)
                    {
                        if (Math.Max(LeadingElem, Math.Abs(ExtMatrix[j, k])) != LeadingElem)
                        {
                            IsSwapped = true;
                            LeadingElem = Math.Abs(ExtMatrix[j, k]); // выбор большего элемента
                            signedLeadingElem = ExtMatrix[j, k];
                            posX = j; // отслеживание номера строки
                            posY = k; // отслеживание номера столбца
                        }
                    }
                }
                if (IsSwapped)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Swap<double>(ref ExtMatrix[i, k], ref ExtMatrix[posX, k]); // перестановка строк расширенной матрицы
                    }
                    Swap<int>(ref positions[i], ref positions[posY]); // перестановка номеров элементов вектора решения СЛАУ
                    for (int k = 0; k < 3; k++)
                    {
                        Swap<double>(ref ExtMatrix[k, i], ref ExtMatrix[k, posY]); // перестановка столбцов расширенной матрицы
                    }
                }
                // прямой ход
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= signedLeadingElem;
                }
                for (int j = i + 1; j < 3; j++) // цикл по строкам
                {
                    checkElem = ExtMatrix[j, i];
                    for (int k = i; k < 4; k++) // внутр цикл по столбцам
                    {
                        ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                    }
                }
            }
            // обратный ход
            double sum;
            for (int i = 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                        sum += ExtMatrix[i, j] * result[j];
                }
                result[i] = ExtMatrix[i, 3] - sum;
            }
            var swappedRes = new double[3] { 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (positions[j] == i)
                    {
                        swappedRes[i] = result[j];
                        break;
                    }
                }
            }
            return swappedRes;
        }
        // метод Жордана
        double[] JordanMethod(double[,] Base, double[] Res, string el)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[,] ReverseMatrix = new double[3, 4];
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }
            for (int i = 0; i < 3; i++) // внеш цикл по строкам
            {
                LeadingElem = ExtMatrix[i, i];
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for (int j = 0; j < 3; j++) // цикл по столбцам для обрат матрицы
                {
                    ReverseMatrix[i, j] /= LeadingElem;
                }
               for (int j = 0; j < 3; j++) // цикл по строкам
                {
                    if (j != i)
                    {
                        checkElem = ExtMatrix[j, i];
                        for (int k = i; k < 4; k++) // внут цикл по столбцам
                        {
                            ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                        }
                        for (int k = 0; k < 3; k++) // внут цикл по столбцам для обрат матр
                        {
                            ReverseMatrix[j, k] -= checkElem * ReverseMatrix[i, k];
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                result[i] = ExtMatrix[i, 3];
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    el += $"{Math.Round(ReverseMatrix[i, j], 3)} ";
                }
            }
            return result;
        }
        // метод перемены мест элементов
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
