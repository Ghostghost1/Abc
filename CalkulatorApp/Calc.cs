using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalkulatorApp
{
    class Calc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double Calculate(string s)
        {
            s = '(' + s + ')';//podany cąg znaków
            Stack<double> Operands = new Stack<double>();//liczby
            Stack<char> Functions = new Stack<char>();//operacje
            int pos = 0;//indeks elementu w zmiennej s
            object token;// objekt, który odpowiada za symbol
            object prevToken = 'w';// poprzedni symbol


            // Pętla która segreguje symboly
            do
            {
                token = GetToken(s, ref pos);
                // żeby działalo coś takie: -4 => 0-4
                if (token is char && prevToken is char && (char)prevToken = '(' && ((char)token == '+' || (char)token == '-'))
                    Operands.Push(0);
                //żeby działalo coś takie: 4√(5+4) => 4*√(5+4)
                if (token is char && (char)token == '√' && (prevToken is double || (char)prevToken == ')'))
                    Functions.Push('*');

                // jeżeli jiczba
                if (token is double)
                {
                    Operands.Push((double)token);
                }
                // jeżeli operacja lub nawiasy
                else if (token is char)
                {
                    // jeżeli podano zamknięty nawias to obliczamy wszystko np: (3+5)*8 => 8*8
                    if ((char)token == ')')
                    {
                        while (Functions.Count > 0 && Functions.Peek() != '(')
                            PopFunction(Operands, Functions);
                        Functions.Pop();// zniszcza "("

                        //żeby działalo coś takie: √(3*3)*6 => 3*6
                        if (Functions.Count() != 0 && Functions.Peek() == '√')
                            PopFunction(Operands, Functions);
                    }
                    else
                    {
                        // Pętla która oblicz jeżeli stek nie jest pusty
                        while (CanPop((char)token, Functions))
                            if (Operands.Count() == 1 && Functions.Count() >= 2)// bez tego if'a nie działa 4√(9)
                                break;
                            else
                                PopFunction(Operands, Functions);

                        Functions.Push((char)token);//podaje operacje w stek
                    }
                }
                prevToken = token;// nadaje dla wczeszniejszego if'a
            }
            while (token != null);// działa do tej pory, dopoky podany symbol nie jest null

            if (Operands.Count > 1 || Functions.Count > 0)//Błąd kiedy stek operacji jest pust a w steku są 2 lub więcej liczb
                throw new Exception("Błąd");

            return Operands.Pop();
        }

        //metoda wyszukuje operacje
        private static void PopFunction(Stack<double> Operands, Stack<char> Functions)
        {
            // ten if jest potrzebny, ponieważ operzcja √ nie potrzebuje 2 operandów
            if (Functions.Peek() == '√')
            {
                Operands.Push(Math.Sqrt(Operands.Pop()));
                Functions.Pop();
            }
            else
            {
                double B = Operands.Pop();
                double A = Operands.Pop();
                switch (Functions.Pop())
                {
                    case '+':
                        Operands.Push(A + B);
                        break;
                    case '-':
                        Operands.Push(A - B);
                        break;
                    case '*':
                        Operands.Push(A * B);
                        break;
                    case '/':
                        Operands.Push(A / B);
                        break;
                    case '^':
                        Operands.Push(Math.Pow(A, B));
                        break;
                    case '%':
                        Operands.Push(A % B);
                        break;
                }
            }
        }

        //zwraca true kiedy jest możliwe obliczenie
        private static bool CanPop(char Znak, Stack<char> Functions)
        {
            if (Functions.Count == 0)
                return false;
            int priority1 = GetPriority(Znak);
            int priority2 = GetPriority(Functions.Peek());

            return priority1 >= 0 && priority2 >= 0 && priority1 >= priority2;
        }