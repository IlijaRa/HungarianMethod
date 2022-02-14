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
    public partial class Solution : Form
    {
        public Solution()
        {
            InitializeComponent();
        }
        List<TextBox> globalTextboxes = new List<TextBox>();
        int globalRows;
        int globalColumns;
        int[,] matrixThroughPhases; // ova matrica ce se menjati kroz korake

        // sluzi da se zada neko kalkulisanje po redu ili koloni
        public enum Axis
        {
            Rows,
            Columns
        }

        public void GenerateFormWithMatrix(int rows, int columns)
        {
            globalRows = rows;
            globalColumns = columns;
            matrixThroughPhases = new int[rows - 1, columns - 1];// dimenzije umanjene za 1 jer ne racuna polja koja imaju plavu boju
            //SettingUpForm(columns);
            //GenerateTextboxMatrix(rows, columns);
        }

        //public void SettingUpForm(int columns)
        //{
        //    switch (columns)
        //    {
        //        case 3:
        //            //label1.Location = new Point(125, 9);
        //            this.BackgroundImage = Properties.Resources.generatedMatrixBackground3x3;
        //            break;
        //        case 4:
        //            Size = new Size(670, 550);
        //            btn_back.Location = new Point(12, 448);
        //            this.BackgroundImage = Properties.Resources.generatedMatrixBackground4x4;
        //            break;
        //        case 5:
        //            Size = new Size(670, 650);
        //            btn_back.Location = new Point(12, 548);
        //            this.BackgroundImage = Properties.Resources.generatedMatrixBackground5x5;
        //            break;
        //        case 6:
        //            Size = new Size(770, 700);
        //            label1.Location = new Point(320, 9);
        //            btn_back.Location = new Point(12, 598);
        //            this.BackgroundImage = Properties.Resources.generatedMatrixBackground6x6;
        //            break;

        //    }
        //}

        //public List<TextBox> GenerateTextboxes(int rows, int columns)
        //{

        //    List<TextBox> textboxes = new List<TextBox>();

        //    for (int i = 0; i < rows * columns; i++)
        //    {
        //        textboxes.Add(new TextBox());
        //    }
        //    return textboxes;
        //}

        //public void GenerateTextboxMatrix(int rows, int columns)
        //{
        //    List<TextBox> textboxes = GenerateTextboxes(rows, columns);

        //    int counter = 0;
        //    int stop = 0;
        //    int X = 0;

        //    if (columns == 3)
        //        X = 150;
        //    if (columns == 4)
        //        X = 100;
        //    if (columns == 5)
        //        X = 35;
        //    if (columns == 6)
        //        X = 25;

        //    int Y = 70;

        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            // za trenutni textbox zadajemo sve ovo ispod
        //            textboxes[counter].Width = 100;
        //            textboxes[counter].Height = 154;
        //            textboxes[counter].Location = new Point(X, Y);
        //            textboxes[counter].TextAlign = HorizontalAlignment.Center;

        //            //textboxes[counter].Text = counter.ToString();
        //            // ova dva if-a ispod farbaju prvi red i prvu kolonu
        //            if (counter < columns)
        //                textboxes[counter].BackColor = Color.LightBlue;
        //            if (j == 0)
        //                textboxes[counter].BackColor = Color.LightBlue;
        //            // ovo dodaje textbox u formu
        //            this.Controls.Add(textboxes[counter]);
        //            // povecavamo vrednost koordinate X i povecavamo counter za 1
        //            X = X + 120;
        //            counter++;

        //            // ako je counter stigao do broja rows*columns onda stajemo
        //            if (counter > rows * columns)
        //            {
        //                stop = 1;
        //                break;
        //            }

        //        }

        //        if (stop == 1)
        //            break;

        //        // vratimo X koordinatu na pocetnu vrednost zbog sledeceg reda u matrici
        //        // a Y povecamo
        //        if (columns == 3)
        //            X = 150;
        //        if (columns == 4)
        //            X = 100;
        //        if (columns == 5)
        //            X = 35;
        //        if (columns == 6)
        //            X = 25;
        //        Y = Y + 60;
        //    }

        //    // prebacujem u globalTextboxes jer global koristimo za
        //    // pristup textboxevima od strane ostalih metoda
        //    globalTextboxes = textboxes;
        //}

        public void TransferSolutionWithFullMatrix(string[,] fullMatrix, string[,] solution )
        {
            int Zcriteria = 0;
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if(solution[i, j] == "/O/")
                    {
                    richTextBox1.Text = richTextBox1.Text + "\n" + fullMatrix[0, j + 1] + "-" + fullMatrix[i + 1, 0];
                        Zcriteria = Zcriteria + Convert.ToInt32(fullMatrix[i + 1, j + 1]);
                    }
                }
            }
            Z.Text = "Z=" + Zcriteria.ToString();
            //return matrix;
        }
    }

}
