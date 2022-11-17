using System;
using System.Windows.Forms;

namespace HungarianMethod
{
    public partial class Solution : Form
    {
        public Solution(string steps)
        {
            InitializeComponent();
            richTextBox2.Text = steps;
        }

        // this function transfer full matrix, result matrix (with independent zeros in all rows and columns) and calculate the result
        public void TransferSolutionWithFullMatrix(string[,] fullMatrix, string[,] solution)
        {
            int Zcriteria = 0;
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if (solution[i, j] == "/O/")
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + fullMatrix[0, j + 1] + "-" + fullMatrix[i + 1, 0];
                        Zcriteria = Zcriteria + Convert.ToInt32(fullMatrix[i + 1, j + 1]);
                    }
                }
            }
            Z.Text = "Z=" + Zcriteria.ToString();
        }
    }
}
