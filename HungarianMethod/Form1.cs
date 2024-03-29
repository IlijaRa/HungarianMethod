﻿using System;
using System.Reflection;
using System.Windows.Forms;

namespace HungarianMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonContinue(object sender, EventArgs e)
        {
            if (CheckIfAllFieldsHaveValue())
            {
                if (CheckIfValuesAreIntParsable())
                {
                    int rows = Convert.ToInt32(Textbox1.Text);
                    int columns = Convert.ToInt32(Textbox2.Text);
                    if (AreDimensionsValid(rows, columns))
                    {
                        if (AreRowsColsEqual(rows, columns))
                        {
                            if (IsTypeOfProblemChosen() == true)
                            {
                                GeneratedMatrix gen = new GeneratedMatrix(rows, columns, TypeOfProblem());
                                gen.Show();
                                this.Hide();
                            }
                            else
                                MessageBox.Show("You have to choose what is the type of the problem!");
                        }
                        else
                            MessageBox.Show("Matrix need to be squared!");
                    }
                    else
                        MessageBox.Show("Values for rows and columns need to be larger that 1 !");
                }
                else
                    MessageBox.Show("Values need to be integer!");
            }
            else
                MessageBox.Show("You need to populate all fields!");

        }

        // this function is used to propagate a string value "Minimization"
        // if minimization problem is checked, otherwise the string value "Maximization"
        public string TypeOfProblem()
        {
            string type = "";
            if (minimization.Checked == true)
                type = "Minimization";
            else
                type = "Maximization";
            return type;
        }

        public bool CheckIfAllFieldsHaveValue()
        {
            bool result = false;

            if (Textbox1.Text != "" && Textbox2.Text != "")
            {
                result = true;
            }

            return result;
        }


        // check if entered values are integer
        public bool CheckIfValuesAreIntParsable()
        {
            bool inputValid = false;
            int nrows = 0;
            int ncol = 0;
            int.TryParse(Textbox1.Text, out nrows);
            int.TryParse(Textbox2.Text, out ncol);
            if (nrows != 0 && ncol != 0)
            {
                inputValid = true;
            }
            return inputValid;
        }

        // check if dimensions are valid, need to be larger that 1x1 matrix
        public bool AreDimensionsValid(int rows, int columns)
        {
            bool valueValid = false;
            if ((rows >= 2) && (columns >= 2))
            {
                valueValid = true;
            }
            return valueValid;
        }

        // check if rows and columns are the same value
        public bool AreRowsColsEqual(int rows, int columns)
        {
            bool valueValid = false;
            if (rows == columns)
            {
                valueValid = true;
            }
            return valueValid;
        }

        // check if minimization or maximization is chosen
        public bool IsTypeOfProblemChosen()
        {
            bool isChecked = true;
            if (minimization.Checked == false && maximization.Checked == false)
            {
                isChecked = false;
            }
            return isChecked;
        }

        private void ExitApplication(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
