using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungarianMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Dugme continue koje pritiska korisnik kada je uneo zeljeni broj kolona i redova
        private void ButtonContinue(object sender, EventArgs e)
        {
            if (IsInputValid(Textbox1.Text, Textbox2.Text))
            {
                int rows = Convert.ToInt32(Textbox1.Text);
                int columns = Convert.ToInt32(Textbox2.Text); 
                if (AreValuesValid(rows, columns))
                {
                    GeneratedMatrix gen = new GeneratedMatrix();
                    gen.GenerateMatrix(rows, columns);
                    gen.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Values for rows and columns can be only [3, 6] !");
            }
            else
                MessageBox.Show("Bad input values!");

        }

        // funkcija koja proverava da li je dobar unos (los je ako nije celobrojna vrednost)
        public bool IsInputValid(string rows, string columns)
        {
            bool inputValid = false;
            int nrows = 0;
            int ncol = 0;
            int.TryParse(rows, out nrows);
            int.TryParse(columns, out ncol);
            if (nrows != 0 && ncol != 0)
            {
                inputValid = true;
            }
            return inputValid;
        }

        // proverava da li je celobrojna vrednost u rasponu od 3 do 6
        // ukljucujuci i te vrednosti
        public bool AreValuesValid(int rows, int columns)
        {
            bool valueValid = false;
            if ((rows >= 3 && rows <= 6) && (columns >= 3 && columns <= 6))
            {
                valueValid = true;
            }
            return valueValid;
        }

        private void buttonX(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
