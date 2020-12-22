using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }

        static void Part1()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;

            Stack<long> operands = new Stack<long>();
            Stack<char> operators = new Stack<char>();

            long sum = 0;
            long op1 = 0;
            long op2 = 0;
            char op = ' ';

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(" ", "");
                foreach (char c in line)
                {
                    if (c >= '0' && c <= '9')
                    {
                        if (operators.Count > 0 && operators.Peek() != '(')
                        {
                            op1 = operands.Pop();
                            op2 = c - '0';
                            op = operators.Pop();
                            operands.Push(Compute(op1, op2, op));
                        }
                        else
                            operands.Push(c - '0');
                    }
                    else if (c == '+' || c == '*' || c == '(')
                    {
                        operators.Push(c);
                    }
                    else if (c == ')')
                    {
                        operators.Pop();

                        if (operators.Count > 0 && operators.Peek() != '(')
                        {
                            op2 = operands.Pop();
                            op1 = operands.Pop();
                            op = operators.Pop();
                            operands.Push(Compute(op1, op2, op));
                        }
                    }
                }
                sum += operands.Pop();
            }

            Console.WriteLine(sum);
        }

        static void Part2()
        {
            string filename = "../../../../input.txt";
            StreamReader sr = new StreamReader(filename);

            string line;

            Stack<char> operators = new Stack<char>();
            string postfix = "";

            long sum = 0;

            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    if (c >= '0' && c <= '9')
                        postfix += c;
                    else if (c == '(')
                        operators.Push(c);
                    else if (c == ')')
                    {
                        while (operators.Peek() != '(')
                            postfix += operators.Pop();
                        operators.Pop();
                    }
                    else if (c == '+' || c == '*')
                    {
                        if (operators.Count == 0 || (operators.Peek() == '(' || (c == '+' && operators.Peek() == '*')))
                            operators.Push(c);
                        else if (c == '*' || (c == '+' && operators.Peek() == '+'))
                        {
                            postfix += operators.Pop();
                            i--;
                        }
                    }
                }

                while (operators.Count > 0)
                {
                    postfix += operators.Pop();
                }

                sum += Evaluate(postfix);
                operators.Clear();
            }

            Console.WriteLine(sum);
        }

        static long Evaluate(string postfix)
        {
            Stack<long> operands = new Stack<long>();

            foreach (char c in postfix)
            {
                if (c >= '0' && c <= '9')
                {
                    operands.Push(c - '0');
                }
                else
                {
                    long op2 = operands.Pop();
                    long op1 = operands.Pop();
                    operands.Push(Compute(op1, op2, c));
                }
            }

            return operands.Pop();
        }

        static long Compute(long op1, long op2, char op)
        {
            long result = 0;
            if (op == '+')
            {
                result = (op1 + op2);
            }
            else if (op == '*')
            {
                result = (op1 * op2);
            }
            return result;
        }
    }
}
