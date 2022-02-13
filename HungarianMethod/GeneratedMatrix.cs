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
    public partial class GeneratedMatrix : Form
    {
        public GeneratedMatrix()
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
            SettingUpForm(columns);
            GenerateTextboxMatrix(rows, columns);
        }

        private void buttonBack(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void buttonReset(object sender, EventArgs e)
        {
            foreach (var textbox in globalTextboxes)
                textbox.Clear();
        }

        private void buttonSolve(object sender, EventArgs e)
        {
            if (CheckIfValuesAreIntParsable())
            {
                matrixThroughPhases = GenerateStartMatrix(globalRows, globalColumns);
                //printMatrix(matrixThroughPhases);

                //-----> 1. Transformacija koef. matrice <-----//
                matrixThroughPhases = CalculateZerosByAxis(matrixThroughPhases, Axis.Rows);
                //printMatrix(matrixThroughPhases);

                matrixThroughPhases = CalculateZerosByAxis(matrixThroughPhases, Axis.Columns);
                //printMatrix(matrixThroughPhases);

                //-----> 2. Odredjivanje nezavisne nule <-----//
                string[,] stringMatrixThroughPhases = ConvertMatrixToStringMatrix(matrixThroughPhases);
                stringMatrixThroughPhases = FindIndependentZeros(stringMatrixThroughPhases);
                printMatrixString(stringMatrixThroughPhases);
                //TODO:

            }
            else
                MessageBox.Show("Denied");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuickTip tip = new QuickTip();
            tip.Show();
        }

        // podesavanje velicine i rasporeda elemenata na osnovu broja redova i kolona
        // podesava i velicinu forme 
        public void SettingUpForm(int columns)
        {
            switch (columns)
            {
                case 3:
                    //label1.Location = new Point(125, 9);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground3x3;
                    break;
                case 4:
                    Size = new Size(670, 550);
                    radioButton1.Location = new Point(18, 310);
                    radioButton2.Location = new Point(18, 360);
                    btn_back.Location = new Point(12, 448);
                    btn_solve.Location = new Point(257, 448);
                    btn_reset.Location = new Point(505, 448);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground4x4;
                    break;
                case 5:
                    Size = new Size(670, 650);
                    radioButton1.Location = new Point(18, 410);
                    radioButton2.Location = new Point(18, 460);
                    btn_back.Location = new Point(12, 548);
                    btn_solve.Location = new Point(257, 548);
                    btn_reset.Location = new Point(505, 548);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground5x5;
                    break;
                case 6:
                    Size = new Size(770, 700);
                    label1.Location = new Point(320, 9);
                    radioButton1.Location = new Point(18, 460);
                    radioButton2.Location = new Point(18, 510);
                    btn_back.Location = new Point(12, 598);
                    btn_solve.Location = new Point(315, 598);
                    btn_reset.Location = new Point(605, 598);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground6x6;
                    break;

            }
        }

        public List<TextBox> GenerateTextboxes(int rows, int columns)
        {

            List<TextBox> textboxes = new List<TextBox>();

            for (int i = 0; i < rows * columns; i++)
            {
                textboxes.Add(new TextBox());
            }
            textboxes[7].Text = "10";
            textboxes[8].Text = "4";
            textboxes[9].Text = "6";
            textboxes[10].Text = "10";
            textboxes[11].Text = "12";

            textboxes[13].Text = "11";
            textboxes[14].Text = "7";
            textboxes[15].Text = "7";
            textboxes[16].Text = "9";
            textboxes[17].Text = "14";

            textboxes[19].Text = "13";
            textboxes[20].Text = "8";
            textboxes[21].Text = "12";
            textboxes[22].Text = "14";
            textboxes[23].Text = "15";

            textboxes[25].Text = "14";
            textboxes[26].Text = "16";
            textboxes[27].Text = "13";
            textboxes[28].Text = "17";
            textboxes[29].Text = "1";

            textboxes[31].Text = "17";
            textboxes[32].Text = "11";
            textboxes[33].Text = "17";
            textboxes[34].Text = "20";
            textboxes[35].Text = "19";

            return textboxes;
        }

        public void GenerateTextboxMatrix(int rows, int columns)
        {
            List<TextBox> textboxes = GenerateTextboxes(rows, columns);

            int counter = 0;
            int stop = 0;
            int X = 0;

            if (columns == 3)
                X = 150;
            if (columns == 4)
                X = 100;
            if (columns == 5)
                X = 35;
            if (columns == 6)
                X = 25;

            int Y = 70;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // za trenutni textbox zadajemo sve ovo ispod
                    textboxes[counter].Width = 100;
                    textboxes[counter].Height = 154;
                    textboxes[counter].Location = new Point(X, Y);
                    textboxes[counter].TextAlign = HorizontalAlignment.Center;

                    //textboxes[counter].Text = counter.ToString();
                    // ova dva if-a ispod farbaju prvi red i prvu kolonu
                    if (counter < columns)
                        textboxes[counter].BackColor = Color.LightBlue;
                    if (j == 0)
                        textboxes[counter].BackColor = Color.LightBlue;
                    // ovo dodaje textbox u formu
                    this.Controls.Add(textboxes[counter]);
                    // povecavamo vrednost koordinate X i povecavamo counter za 1
                    X = X + 120;
                    counter++;

                    // ako je counter stigao do broja rows*columns onda stajemo
                    if (counter > rows * columns)
                    {
                        stop = 1;
                        break;
                    }

                }

                if (stop == 1)
                    break;

                // vratimo X koordinatu na pocetnu vrednost zbog sledeceg reda u matrici
                // a Y povecamo
                if (columns == 3)
                    X = 150;
                if (columns == 4)
                    X = 100;
                if (columns == 5)
                    X = 35;
                if (columns == 6)
                    X = 25;
                Y = Y + 60;
            }

            // prebacujem u globalTextboxes jer global koristimo za
            // pristup textboxevima od strane ostalih metoda
            globalTextboxes = textboxes;
        }

        // proverava da li se unete vrednosti mogu pretvoriti u int vrednosti
        public bool CheckIfValuesAreIntParsable()
        {
            int result;
            bool valuesAreValid = true;
            foreach (var textbox in globalTextboxes)
            {
                if (textbox.BackColor != Color.LightBlue)
                {
                    int.TryParse(textbox.Text, out result);
                    if (result == 0)
                    {
                        valuesAreValid = false;
                        break;
                    }
                }
            }
            return valuesAreValid;
        }

        // formira matricu vrednosti koje smo uneli ali bez vrednosti plavih textboxeva; znaci dimenzije umanjene za 1
        public int[,] GenerateStartMatrix(int rows, int columns)
        {
            int arrayCounter = 0;
            int[] validValues = MakingArrayOfActualValues(rows, columns);

            // dimenzije umanjene za 1 jer ne racuna polja koja imaju plavu boju
            int[,] startMatrix = new int[rows - 1, columns - 1];

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    startMatrix[i, j] = validValues[arrayCounter];
                    arrayCounter++;
                    //Console.Write(startMatrix[i, j] + "\t");
                }
                //Console.WriteLine("\n");
            }

            //printMatrix(startMatrix);

            return startMatrix;
        }

        // pravi niz od vrednosti u testboxevima koji nisu plave boje
        public int[] MakingArrayOfActualValues(int rows, int columns)
        {
            int[] actualValues = new int[rows * columns];
            int valuesCounter = 0;
            foreach (var textbox in globalTextboxes)
            {
                if (textbox.BackColor != Color.LightBlue)
                {
                    actualValues[valuesCounter] = Convert.ToInt32(textbox.Text);
                    valuesCounter++;
                }
            }

            return actualValues;
        }

        // Funkcija prima matricu i axis koji govori
        // da li ce resavati nule po rows ili columns
        public int[,] CalculateZerosByAxis(int[,] matrix, Axis axis)
        {
            // transponuje
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                //trazi minimum po svakom redu
                int min = matrix[i, 0];
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] < min)
                        min = matrix[i, j];
                }

                //radi razliku vrednosti sa min
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[i, j] -= min;
                }
            }

            // vraca matricu u prvobitnu orijentaciju
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            //printMatrix(matrix);
            return matrix;
        }

        // funkcija koja transponuje matricu
        public int[,] Transpose(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        public void printMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("\n");
        }

        public void printMatrixString(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("\n");
        }

        public string[,] ConvertMatrixToStringMatrix(int[,] matrix)
        {
            string[,] stringMatrix = new string[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    stringMatrix[i, j] = Convert.ToString(matrix[i, j]);
                }
            }

            return stringMatrix;
        }

        public string[,] FindIndependentZeros(string[,] stringMatrix)
        {
            //string[,] independentZerosCoordinates = new string[stringMatrix.GetLength(0),2];
            //int independentZerosCounter = 0;

            //for (int i = 0; i < stringMatrix.GetLength(0); i++)
            //{
            //    int zeroCounter = 0;
            //    // prolazi kroz vrstu i broji koliko nula ima u njoj
            //    for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //    {
            //        if (stringMatrix[i, j] == "0")
            //        {
            //            zeroCounter++;
            //        }
            //    }

            //    // ako je pronasao samo jednu nulu u vrsti,
            //    // uzece njene koordinate i smestiti u matricu "independentZerosCoordinates"
            //    if (zeroCounter == 1)
            //    {
            //        for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //        {
            //            if (stringMatrix[i, j] == "0")
            //            {
            //                stringMatrix[i, j] = "/O/";
            //                ScratchZerosInTheSameRowAndColumn(stringMatrix, i, j);
            //            }
            //        }
                    
            //        //for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //        //{
            //        //    if (stringMatrix[i, j] == "0") 
            //        //    {
            //        //        stringMatrix[i, j] = "/O/";
            //        //        //independentZerosCoordinates[independentZerosCounter, 0] = i.ToString();
            //        //        //independentZerosCoordinates[independentZerosCounter, 1] = j.ToString();
            //        //        //independentZerosCounter++;

            //        //        for (int row = 1; row < stringMatrix.GetLength(0); row++)
            //        //        {
            //        //            if (stringMatrix[row, j] == "0")
            //        //                stringMatrix[row, j] = "/";
            //        //        }

            //        //    }
            //        //}
            //    }
            //}

            List<int> priority = new List<int>();
            for (int i = 1; i < globalRows - 1; i++)
                priority.Add(i);

            foreach(var numberOfZerosPerRow in priority)
            {
                for (int i = 0; i < stringMatrix.GetLength(0); i++)
                {
                    int zeroCounter = 0;
                    // prolazi kroz vrstu i broji koliko nula ima u njoj
                    for (int j = 0; j < stringMatrix.GetLength(0); j++)
                    {
                        if (stringMatrix[i, j] == "0")
                        {
                            zeroCounter++;
                        }
                    }

                    // ako je pronasao samo jednu nulu u vrsti,
                    // uzece njene koordinate i smestiti u matricu "independentZerosCoordinates"
                    if (zeroCounter == numberOfZerosPerRow)
                    {
                        for (int j = 0; j < stringMatrix.GetLength(0); j++)
                        {
                            if (stringMatrix[i, j] == "0")
                            {
                                stringMatrix[i, j] = "/O/";
                                ScratchZerosInTheSameRowAndColumn(stringMatrix, i, j);
                            }
                        }
                    }
                }

                // sluzi za dobijanje vrsta u kojima se ne nalazi samo 1 nula
                List<int> lista = new List<int>();
                int IndependentZeroCounter = 0;

                for (int i = 0; i < stringMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < stringMatrix.GetLength(0); j++)
                    {
                        if (stringMatrix[i, j] == "/O/")
                            IndependentZeroCounter++;
                    }
                    if (!(IndependentZeroCounter == numberOfZerosPerRow))
                    {
                        if(!lista.Contains(i))
                            lista.Add(i);
                    }
                    IndependentZeroCounter = 0;
                }

            }








            //// sluzi za dobijanje vrsta u kojima se ne nalazi samo 1 nula
            //List<int> lista = new List<int>();
            //int IndZeroCnt = 0;



            //// trazi sledecu vrstu koja ima 2 nule u sebi
            //int zeroCnt2 = 0;
            //for(int i = 0; i < lista.Count(); i++)
            //{
            //    for(int j=0; j < stringMatrix.GetLength(0); j++)
            //    {
            //        if (stringMatrix[lista[i], j] == "0")
            //            zeroCnt2++;
            //    }

            //    if (zeroCnt2 == 2)
            //    {
            //        for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //        {
            //            if (stringMatrix[lista[i], j] == "0")
            //            {
            //                stringMatrix[lista[i], j] = "/O/";
            //                ScratchZerosInTheSameRowAndColumn(stringMatrix, lista[i], j);
            //            }
            //        }
            //    }
            //}

            //// trazi sledecu vrstu koja ima 3 nule u sebi
            //int zeroCnt3 = 0;
            //for (int i = 0; i < lista.Count(); i++)
            //{
            //    for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //    {
            //        if (stringMatrix[lista[i], j] == "0")
            //            zeroCnt3++;
            //    }

            //    if (zeroCnt3 == 3)
            //    {
            //        for (int j = 0; j < stringMatrix.GetLength(0); j++)
            //        {
            //            if (stringMatrix[lista[i], j] == "0")
            //            {
            //                stringMatrix[lista[i], j] = "/O/";
            //                ScratchZerosInTheSameRowAndColumn(stringMatrix, lista[i], j);
            //            }
            //        }
            //    }
            //}

            return stringMatrix;
        }

        public void ScratchZerosInTheSameRowAndColumn(string[,] stringMatrix, int x_coord, int y_coord)
        {

            for (int i = 0; i < stringMatrix.GetLength(0); i++)
            {
                if (stringMatrix[i, y_coord] == "0" && i != x_coord)
                {
                    stringMatrix[i, y_coord] = "/";
                }
            }

            for (int j = 0; j < stringMatrix.GetLength(0); j++)
            {
                if (stringMatrix[x_coord, j] == "0" && j != y_coord)
                {
                    stringMatrix[x_coord, j] = "/";
                }
            }

        }
    }
}
