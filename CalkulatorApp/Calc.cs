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
                   