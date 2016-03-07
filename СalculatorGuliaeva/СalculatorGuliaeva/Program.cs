using System;
using System.Collections.Generic;

namespace СalculatorGuliaeva
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Polish notation");
            
            //считываем строку в польской нотации
            string line;
            try
            {
                line = Console.ReadLine();
            }
            catch(System.IO.IOException e)
            {
                Console.WriteLine("Error readline {0}", e.Message);
                Console.ReadKey();
                return;
            }
            if (line == null)
            {
                Console.WriteLine("Error readline");
                Console.ReadKey();
                return;
            }
            
            //разделяем строку на аргументы
            string[] args = line.Split(new Char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Split Error");
                Console.ReadKey();
                return;
            }
            
            //проходим по массиву и вычисляем выражение, записывая данные в стек
            Stack<double> st = new Stack<double>();
            for (int i = args.Length - 1; i >= 0; i--)
            {
                //если встречается арифметический оператор
                if (args[i] == "*" || args[i] == "/" || args[i] == "+" || args[i] == "-")
                {
                    if (st.Count >= 2)
                    {
                        double a = st.Pop();
                        double b = st.Pop();

                        switch (args[i])
                        {
                            case "*":
                                st.Push(a * b);
                                break;
                            case "/":
                                st.Push(a / b);
                                break;
                            case "+":
                                st.Push(a + b);
                                break;
                            case "-":
                                st.Push(a - b);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error count an arithmetic operator sign");
                        Console.ReadKey();
                        return;
                    }
                }
                //должно встретится число
                else
                {
                    try
                    {
                        double temp = double.Parse(args[i]);
                        st.Push(temp);
                    }
                    catch
                    {
                        Console.WriteLine("Error parse {0}", args[i]);
                        Console.ReadKey();
                        return;
                    }
                }
            }
            //если ни в одно исключение не вылетели, то ответ будет в значении стека, при условии что там единственное значение. иначе - в выражении было лишнее число
            if (st.Count == 1)
            {
                Console.WriteLine(st.Peek().ToString());
            }
            else
            {
                Console.WriteLine("Error. more numbers");
            }
            Console.ReadKey();
        }
    }
}
