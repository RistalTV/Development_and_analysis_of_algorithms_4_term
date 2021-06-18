using System;
namespace Gauss_algorithm1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double[,] BaseMatrix = new double[3,3]; // матрица коэффициентов
            double[] ResMatrix = new double[3]; // матрица правой части СЛАУ
            double[] Roots = new double[3]; // матрица корней СЛАУ
            double checkSum = 0;
            Console.WriteLine("Лабораторная работа №3 по предмету РиАА. Выполнил Берест Д.Ф., группа ФИТУ 2-5");
            Console.WriteLine("------------------------------------------------------------------------------ \n");
            Console.WriteLine("Заполите матрицу коэффициентов СЛАУ: ");
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    BaseMatrix[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Console.WriteLine("Заполите матрицу правой части СЛАУ: ");
            for(int i = 0; i < 3; i++)
            {
                ResMatrix[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Данная СЛАУ имеет вид: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 3; j++)
                {
                    if(j != 2)
                    Console.Write("{0} * x{1} + ", BaseMatrix[i,j], j + 1);
                    else Console.Write("{0} * x{1}", BaseMatrix[i, j], j);
                }
                Console.Write("= {0}", ResMatrix[i]);
                Console.WriteLine();
            }
            Console.WriteLine("Решение СЛАУ классическим методом Гаусса. ");

            Roots = GaussMethod(BaseMatrix, ResMatrix);
            Console.Write("Корни уравнения, полученные алгоритмом: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x{0} = {1}; ", i + 1, Roots[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Проверка полученных корней: ");
            for(int i = 0; i < 3; i++)
            {
                checkSum = 0;
                for(int j = 0; j < 3; j++)
                {
                    checkSum += BaseMatrix[i, j] * Roots[j];
                }
                if(Math.Round(checkSum) == ResMatrix[i])
                {
                    Console.WriteLine("Проверка {0} строки прошла успешно!", i+1 );
                }
                else
                {
                    Console.WriteLine("В вычислениях допущена ошибка!");
                    break;
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------ \n");
            Console.WriteLine("Решение СЛАУ первой модификацией метода Гаусса. ");
            Roots = new double[3];
            Roots = GaussMethod_mod1(BaseMatrix, ResMatrix);
            Console.Write("Корни уравнения, полученные алгоритмом: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x{0} = {1}; ", i + 1, Roots[i]);
            }
            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------------------------ \n");
            Console.WriteLine("Решение СЛАУ второй модификацией метода Гаусса. ");
            Roots = new double[3];
            Roots = GaussMethod_mod2(BaseMatrix, ResMatrix);
            Console.Write("Корни уравнения, полученные алгоритмом: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x{0} = {1}; ", i + 1, Roots[i]);
            }
            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------------------------ \n");
            Console.WriteLine("Решение СЛАУ третьей модификацией метода Гаусса. ");
            Roots = new double[3];
            Roots = GaussMethod_mod3(BaseMatrix, ResMatrix);
            Console.Write("Корни уравнения, полученные алгоритмом: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x{0} = {1}; ", i + 1, Math.Round(Roots[i]));
            }
            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------------------------ \n");
            Console.WriteLine("Решение СЛАУ методом Жордана. ");
            Roots = new double[3];
            Roots = JordanMethod(BaseMatrix, ResMatrix);
            Console.Write("Корни уравнения, полученные алгоритмом: ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("x{0} = {1}; ", i + 1, Math.Round(Roots[i]));
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        // классический метод Гаусса
        static double[] GaussMethod(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[] result = new double[3] {0,0,0}; 
            double[,] ExtMatrix = new double[3,4];
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (j < 3)
                        ExtMatrix[i, j] = Base[i, j];
                    else
                        ExtMatrix[i, j] = Res[i];
                }
            }

            // алгоритм решения СЛАУ методом Гаусса
            // прямой ход
            for(int i = 0; i < 3; i++) // внеш цикл по строкам
            {
                LeadingElem = ExtMatrix[i,i];
                for(int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for(int j = i + 1; j < 3; j++) // цикл по строкам
                {
                    checkElem = ExtMatrix[j,i];
                    for(int k = i; k < 4; k++) // внутр цикл по столбцам
                    {
                        ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                    }
                }
            }
            // обратный ход
            double sum; 
            for(int i = 2; i >= 0; i--)
            {
                sum = 0;
                for(int j = 0; j < 3; j++)
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

            for(int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                for(int j = i; j < 3; j++)
                {
                    if(Math.Max(LeadingElem, Math.Abs(ExtMatrix[i,j])) != LeadingElem)
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
            for(int i = 0; i < 3; i++)
            {
                int a = i;
                for(int j = 0; j < 3; j++)
                {
                    if(positions[j] == i)
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

            for(int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                for (int j = i; j < 3; j++)
                {
                    if(Math.Max(LeadingElem, Math.Abs(ExtMatrix[j,i])) != LeadingElem)
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
            for(int i = 0; i < 3; i++)
            {
                IsSwapped = false;
                LeadingElem = Math.Abs(ExtMatrix[i, i]); // предположение, что элемент глав диагонали - наибольший (по модулю) 
                signedLeadingElem = ExtMatrix[i, i];
                for (int j = i; j < 3; j++)
                {
                    for(int k = i; k < 3; k++)
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
        static double[] JordanMethod(double[,] Base, double[] Res)
        {
            double LeadingElem = 0; // ведущий элемент матрицы
            double checkElem = 0;
            double[] result = new double[3] { 0, 0, 0 };
            double[,] ExtMatrix = new double[3, 4];
            double[,] ReverseMatrix = new double[3, 4];

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
            for(int i = 0; i < 3; i++) // внеш цикл по строкам
            {
                LeadingElem = ExtMatrix[i, i];
                for (int j = i; j < 4; j++) // цикл по столбцам
                {
                    ExtMatrix[i, j] /= LeadingElem;
                }
                for(int j = 0; j < 3; j++) // цикл по столбцам для обрат матрицы
                {
                    ReverseMatrix[i, j] /= LeadingElem;
                }
                for(int j = 0; j < 3; j++) // цикл по строкам
                {
                    if(j != i)
                    {
                        checkElem = ExtMatrix[j, i];
                        for(int k = i; k < 4; k++) // внут цикл по столбцам
                        {
                            ExtMatrix[j, k] -= checkElem * ExtMatrix[i, k];
                        }
                        for(int k = 0; k < 3; k++) // внут цикл по столбцам для обрат матр
                        {
                            ReverseMatrix[j, k] -= checkElem * ReverseMatrix[i, k];
                        }
                    }
                }
            }
            for(int i = 0; i < 3; i++)
            {
                result[i] = ExtMatrix[i, 3];
            }
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Console.Write(Math.Round(ReverseMatrix[i, j], 3) + " ");
                }  
                Console.WriteLine();
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
