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

            
