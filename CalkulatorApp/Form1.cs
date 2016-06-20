using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalkulatorApp
{
    public partial class Calculator : Form
    {
        string memory = ""; // dla zapamiętania 1 liczby
        Stack<char> Filtr = new Stack<char>();// dla filtrowania powturzeń
        public Calculator()
        {
            InitializeComponent();
        }

        // Po naciśnięciu zapisywało się znaczenie pola Text i dodawało się do niego
        private void buttonClickEvent(object sender, EventArgs e)
        {
            // ten filtr działa żeby nie było miżliwe zapisać 2----5
            Filtr.Push(Convert.ToChar((string)((Button)sender).Text));
            if (textBox1.Text == "" ||
                (char.IsDigit(textBox1.Text.Last()) || textBox1.Text.Last() == ')' || (textBox1.Text.Last() != Filtr.Peek() && char.IsDigit(Filtr.Peek()))) ||
                Filtr.Peek() == '√' || Filtr.Peek() == '(' )
            {
                // ten if dla takiego zapisu => √(
                if ((Button)sender == buttonRoot)
                    textBox1.Text += (string)((Button)sender).Text + "(";
                else
                {
                    textBox1.Text += (string)((Button)sender).Text;
                }
            }
        }
        // Wyświetla formułę która była wprowadzona z dodatkowym symbolem "=".
        private void buttonResult_Click(object sender, EventArgs e)
        {
            string result = this.textBox1.Text;
            labelExpression.Text = result + "=";
            this.textBox1.Text = Calc.Calculate(result).ToString();
        }
        // Czyści pole textBox1
        private void buttonCLR_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";   
        }
        //Niszczy ostatni wprowadzony symbol
        private void buttonClrLast_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength != 0)
                this.textBox1.Text = textBox1.Text.Remove(textBox1.TextLength - 1);            
        }
        //Dodaje liczbę do pamięci
        private void buttonMplus_Click(object sender, EventArgs e)
        {
            this.memory = this.textBox1.Text;
        }
        //Czyści pamięć
        private void buttonMminus_Click(object sender, EventArgs e)
        {
            this.memory = "";
        }
        //Zapisuje liczbę z pamięci
        private void buttonMresult_Click(object sender, EventArgs e)
        {
            this.textBox1.Text += this.memory;
        }


        
    }
}
    