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

                string[,] fullMatrix = GenerateFullMatrix(globalRows, globalColumns);
                Console.WriteLine("Kompletna matrica\n");
                printMatrixString(fullMatrix);

                string[,] stringMatrixThroughPhases = new string[globalRows - 1, globalColumns - 1];
                if (minimization.Checked == true) { 
                    Console.WriteLine("Matrica bez prve kolone i vrste\n");
                    stringMatrixThroughPhases = GenerateStartMatrix(globalRows, globalColumns);
                    printMatrixString(stringMatrixThroughPhases);
                }
                else if(maximization.Checked == true)
                {
                    stringMatrixThroughPhases = GenerateStartMatrix(globalRows, globalColumns);
                    int[,] intMatrixThroughPhases = ConvertStringMatrixToIntMatrix(stringMatrixThroughPhases);
                    intMatrixThroughPhases = MultiplyWithMinusOne(intMatrixThroughPhases);
                    stringMatrixThroughPhases = ConvertIntMatrixToStringMatrix(intMatrixThroughPhases);
                    printMatrixString(stringMatrixThroughPhases);
                }

                //Console.WriteLine("Pocetna matrica");
                //printMatrixString(stringMatrixThroughPhases);

                string[,] solution = FirstStepHungarianMethod(stringMatrixThroughPhases);
                printMatrixString(stringMatrixThroughPhases);

                solution = OtherStepsHungarianMethod(solution);
                printMatrixString(solution);
                int i = 1;
                while (i <= 10)
                {
                    string[,] matrix = FindIndependentZeros(solution);
                    if (CheckIfAllRowsHaveIndependentZero(matrix) == true)
                        break;
                    else
                    {
                        solution = OtherStepsHungarianMethod(solution);
                        printMatrixString(solution);
                        i++;
                    }

                }

                //solution = FindIndependentZeros(solution);
                //printMatrixString(solution);
                //bool hasinde = CheckIfAllRowsHaveIndependentZero(solution);
                //Console.WriteLine(hasinde.ToString());

                Solution solutionForm = new Solution();
                solutionForm.GenerateFormWithMatrix(globalRows, globalColumns);
                // full matrica sadrzi i nazive prve kolone i prve vrste (A,B,C,D,E, 1, 2, 3, 4, 5...)
                solutionForm.TransferSolutionWithFullMatrix(fullMatrix, solution);
                solutionForm.Show();
                //PaintIndependentZeros(solution);
                //this.Hide();


            }
            else
                MessageBox.Show("Denied");
        }

        //public void PaintIndependentZeros(string[,] solution)
        //{
        //    for (int i = 0; i < solution.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < solution.GetLength(1); j++)
        //        {
        //            if (solution[i, j] == "/O/")
        //            {
        //                globalTextboxes[(i+2) * (j+2)].BackColor = Color.Red;
        //            }
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            QuickTip tip = new QuickTip();
            tip.Show();
        }

        public int[,] MultiplyWithMinusOne(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] * -1;
                }
            }
            return matrix;
        }

        public string[,] FirstStepHungarianMethod(string[,] stringMatrixThroughPhases)
        {
            //-----> 1. Transformacija koef. matrice <-----//
            stringMatrixThroughPhases = CalculateZerosByAxis(stringMatrixThroughPhases, Axis.Rows);
            stringMatrixThroughPhases = CalculateZerosByAxis(stringMatrixThroughPhases, Axis.Columns);
            return stringMatrixThroughPhases;
        }
        public string[,] OtherStepsHungarianMethod(string[,] stringMatrixThroughPhases)
        {

            //-----> 2. Odredjivanje nezavisne nule <-----//
            //string[,] stringMatrixThroughPhases = ConvertIntMatrixToStringMatrix(matrixThroughPhases);
            stringMatrixThroughPhases = FindIndependentZeros(stringMatrixThroughPhases);
            printMatrixString(stringMatrixThroughPhases);
            //-----> 3. Oznaciti sve vrste koje nemaju nezavisne 0 <-----//
            List<int> markedRows = MarkRowsWithoutIndependentZero(stringMatrixThroughPhases);
            //printMatrixString(stringMatrixThroughPhases);
            //-----> 4. Precrtati sve kolone koje imaju nulu u oznacenim redovima. <-----//
            List<int> scratchedColumns = MarkColsWhereRowIsZero(stringMatrixThroughPhases, markedRows);
            stringMatrixThroughPhases = ScratchColsWhereRowIsZero(stringMatrixThroughPhases, scratchedColumns);
            printMatrixString(stringMatrixThroughPhases);
            //-----> 5. Oznaciti sve vrste koje imaju nezavisnu nulu u precrtanim kolonama. <-----//
            List<int> AllMarkedRows = MarkRowsWithIndependentZerosInScratchedCols(stringMatrixThroughPhases, scratchedColumns, markedRows);
            //printMatrixString(stringMatrixThroughPhases);
            //-----> 6. Precrtati sve neoznacene kolone. <-----//
            stringMatrixThroughPhases = ScratchUnmarkedColumns(stringMatrixThroughPhases, AllMarkedRows);
            printMatrixString(stringMatrixThroughPhases);
            //-----> 7. Sve neprecrtane smanjujemo za vrednost najmanjeg broja od neprecrtanih,
            //dok vrednost na preseku precrtane kol i reda povecavamo za taj broj. <-----//
            stringMatrixThroughPhases = TransformationWithMinimumValue(stringMatrixThroughPhases);
            printMatrixString(stringMatrixThroughPhases);

            return stringMatrixThroughPhases;
        }

        public bool CheckIfAllRowsHaveIndependentZero(string[,] matrix)
        {
            bool rowHasIndependentZero = true;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == "/O/")
                        counter++;
                }
                if (counter == 0)
                {
                    rowHasIndependentZero = false;
                    break;
                }
                counter = 0;
            }
            return rowHasIndependentZero;
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
                    minimization.Location = new Point(18, 310);
                    maximization.Location = new Point(18, 360);
                    btn_back.Location = new Point(12, 448);
                    btn_solve.Location = new Point(257, 448);
                    btn_reset.Location = new Point(505, 448);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground4x4;
                    break;
                case 5:
                    Size = new Size(670, 650);
                    minimization.Location = new Point(18, 410);
                    maximization.Location = new Point(18, 460);
                    btn_back.Location = new Point(12, 548);
                    btn_solve.Location = new Point(257, 548);
                    btn_reset.Location = new Point(505, 548);
                    this.BackgroundImage = Properties.Resources.generatedMatrixBackground5x5;
                    break;
                case 6:
                    Size = new Size(770, 700);
                    label1.Location = new Point(320, 9);
                    minimization.Location = new Point(18, 460);
                    maximization.Location = new Point(18, 510);
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
            textboxes[0].Text = "\\";

            textboxes[1].Text = "A";
            textboxes[2].Text = "B";
            textboxes[3].Text = "C";
            textboxes[4].Text = "D";
            textboxes[5].Text = "E";

            textboxes[6].Text = "1";
            textboxes[12].Text = "2";
            textboxes[18].Text = "3";
            textboxes[24].Text = "4";
            textboxes[30].Text = "5";

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
        public string[,] GenerateStartMatrix(int rows, int columns)
        {
            int arrayCounter = 0;
            string[] validValues = MakingArrayOfActualValues(rows, columns);

            // dimenzije umanjene za 1 jer ne racuna polja koja imaju plavu boju
            string[,] startMatrix = new string[rows - 1, columns - 1];

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    startMatrix[i, j] = validValues[arrayCounter];
                    arrayCounter++;
                }

            }

            return startMatrix;
        }

        public string[,] GenerateFullMatrix(int rows, int columns)
        {
            int arrayCounter = 0;

            string[] validValues = MakingArrayOfAllValues(rows, columns);

            // dimenzije umanjene za 1 jer ne racuna polja koja imaju plavu boju
            string[,] startMatrix = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    startMatrix[i, j] = validValues[arrayCounter];
                    arrayCounter++;
                }

            }

            return startMatrix;
        }

        // pravi niz od vrednosti u testboxevima koji nisu plave boje
        public string[] MakingArrayOfActualValues(int rows, int columns)
        {
            string[] actualValues = new string[rows * columns];
            int valuesCounter = 0;
            foreach (var textbox in globalTextboxes)
            {
                if (textbox.BackColor != Color.LightBlue)
                {
                    actualValues[valuesCounter] = textbox.Text;
                    valuesCounter++;
                }
            }

            return actualValues;
        }

        public string[] MakingArrayOfAllValues(int rows, int columns)
        {
            string[] actualValues = new string[rows * columns];
            int valuesCounter = 0;
            foreach (var textbox in globalTextboxes)
            {
                //if (textbox.BackColor != Color.LightBlue)
                //{
                    actualValues[valuesCounter] = textbox.Text;
                    valuesCounter++;
                //}
            }

            return actualValues;
        }

        // Funkcija prima matricu i axis koji govori
        // da li ce resavati nule po rows ili columns
        public string[,] CalculateZerosByAxis(string[,] matrix, Axis axis)
        {
            // transponuje
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                //trazi minimum po svakom redu
                int min = Convert.ToInt32(matrix[i, 0]);
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    int value = -10;
                    int.TryParse(matrix[i, j], out value);
                    if (value != -10)
                    {
                        if (value < min) // Convert.ToInt32(matrix[i, j]) umesto value
                            min = value;
                    }
                    else
                        MessageBox.Show("Bad parsing!");
                }

                //radi razliku vrednosti sa min
                int matrix_value = -10;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    int.TryParse(matrix[i, j], out matrix_value);
                    //matrix_value = value;//Convert.ToInt32(matrix[i, j]);
                    if (matrix_value != -10) {
                        matrix_value = matrix_value - min;
                        matrix[i, j] = matrix_value.ToString();
                    }
                }
            }

            // vraca matricu u prvobitnu orijentaciju
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            //printMatrix(matrix);
            return matrix;
        }

        // funkcija koja transponuje matricu
        public string[,] Transpose(string[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            string[,] result = new string[h, w];

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

        public string[,] ConvertIntMatrixToStringMatrix(int[,] matrix)
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

        public int[,] ConvertStringMatrixToIntMatrix(string[,] matrix)
        {
            int[,] intMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    intMatrix[i, j] = Convert.ToInt32(matrix[i, j]);
                }
            }

            return intMatrix;
        }

        public string[,] FindIndependentZeros(string[,] stringMatrix)
        {

            // priority je lista u kojoj se nalaze vrednosti od 1 do dimenzije matrice
            // priority govori kojim redom ce gledati broj nula u vrstama
            // prvo gleda da li u vrsti ima jedna 0, pa dve 0 itd.
            List<int> priority = new List<int>();
            for (int i = 1; i < globalRows; i++)
                priority.Add(i);

            foreach (var numberOfZerosPerRow in priority)
            {
                for (int i = 0; i < stringMatrix.GetLength(0); i++)
                {
                    int zeroCounter = 0;
                    // prolazi kroz vrstu i broji koliko nula ima u njoj
                    for (int j = 0; j < stringMatrix.GetLength(0); j++)
                    {
                        if (stringMatrix[i, j] == "0" || stringMatrix[i, j] == "∅")
                        {
                            zeroCounter++;
                        }
                    }

                    // ako je pronasao broj numberOfZerosInRow u vrsti,
                    // 0 na kordinatama [i, j] ce prebaciti u /O/ a ostale nule po vrstama u kolonama u "/"
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

                // sluzi za dobijanje vrsta u kojima se ne nalazi broj nula numberOfZerosPerRow
                // taj broj belezi u listu "lista" kako bi znao koje vise vrste da ne prolazi
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
                        // ovaj if sluzi da ne bi ubacivao vise puta istu vrstu ili kolonu u listu, nego da budu samo distinct vrednosti
                        if (!lista.Contains(i))
                            lista.Add(i);
                    }
                    IndependentZeroCounter = 0;
                }

            }

            return stringMatrix;
        }

        public void ScratchZerosInTheSameRowAndColumn(string[,] stringMatrix, int x_coord, int y_coord)
        {

            for (int i = 0; i < stringMatrix.GetLength(0); i++)
            {
                if (stringMatrix[i, y_coord] == "0" && i != x_coord)
                {
                    stringMatrix[i, y_coord] = "∅";
                }
            }

            for (int j = 0; j < stringMatrix.GetLength(0); j++)
            {
                if (stringMatrix[x_coord, j] == "0" && j != y_coord)
                {
                    stringMatrix[x_coord, j] = "∅";
                }
            }

        }

        public List<int> MarkRowsWithoutIndependentZero(string[,] matrix)
        {

            bool rowHasIndependentZero = false;
            List<int> RowsWithoutIndependentZero = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == "/O/")
                    {
                        rowHasIndependentZero = true;
                        break;
                    }
                }

                if (rowHasIndependentZero == false)
                    RowsWithoutIndependentZero.Add(i);
                else
                    rowHasIndependentZero = false;


            }

            //foreach(var row in RowsWithoutIndependentZero)
            //{
            //    for (int j = 0; j < matrix.GetLength(0); j++)
            //    {
            //        matrix[row, j] = "--" + matrix[row, j] + "--";
            //    }
            //}

            return RowsWithoutIndependentZero;
        }

        public List<int> MarkColsWhereRowIsZero(string[,] matrix, List<int> rowsWithoutIndependentZero)
        {
            List<int> scratchedColumns = new List<int>();

            foreach (var row in rowsWithoutIndependentZero)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[row, j] == "∅" && !scratchedColumns.Contains(j))
                    {
                        scratchedColumns.Add(j);
                    }
                }
            }
            return scratchedColumns;
        }
        public string[,] ScratchColsWhereRowIsZero(string[,] matrix, List<int> scratchedColumns)
        {
            foreach (var col in scratchedColumns)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    // ukoliko je precrtan element, da ga ne precrtava ponovo
                    //if(!matrix[i, col].Contains("--"))
                    matrix[i, col] = "--" + matrix[i, col] + "--";
                }
            }

            return matrix;
        }

        // prima parametar markedRows jer je pre ovoga funkcija MarkRowsWithoutIndependentZero
        // oznacila sve vrste koje nemaju nezavisnu nulu i vratila listu sa tim vrstama
        // ova funkcija se nadovezuje na tu listu da doda jos one vrste koje imaju nezavisnu nulu u precrtanim kolonama
        // s tim sto prima parametar scratchedCols kako bi znala po kojim kolonama da proverava
        public List<int> MarkRowsWithIndependentZerosInScratchedCols(string[,] matrix, List<int> scratchedCols, List<int> markedRows)
        {
            foreach (var col in scratchedCols)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    // /O/ se menja u --/O/-- ili vise crtica tako da sredi ovo
                    if (matrix[i, col] == "--/O/--")
                    {
                        markedRows.Add(i);
                    }
                }
            }

            return markedRows;
        }

        public string[,] ScratchUnmarkedColumns(string[,] matrix, List<int> markedRows)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (!markedRows.Contains(i))
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        //if (!matrix[i, j].Contains("--"))
                        matrix[i, j] = "--" + matrix[i, j] + "--";
                    }
                }
            }

            return matrix;
        }

        public string[,] TransformationWithMinimumValue(string[,] matrix)
        {
            int min = FindMinimum(matrix);
            string[,] transformedMatrix = SubtractValues(matrix, min);
            transformedMatrix = AddMinimumToCrossScratchPositions(transformedMatrix, min);
            transformedMatrix = RewriteOneTimeScratchPositions(transformedMatrix);
            return transformedMatrix;
        }


        public int FindMinimum(string[,] matrix)
        {
            //moramo postaviti za pocetni minimum neku vrednost
            // ali mora se proveriti da li ta vrednost moze da se prebaci u int ,posto je matrixa stringova trenutno,
            // ako ne moze, proverava sledecu vrednost i tako dok ne naidje na
            // prvu vrednost koju moze konvertovati u int.
            int min = 0;
            bool startMinFound = false;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    int.TryParse(matrix[i, j], out min);
                    if (min != 0)
                    {
                        startMinFound = true;
                        break;
                    }
                }
                if (startMinFound == true)
                    break;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    // ova provera jer gleda samo one elemente u matrici koji nisu precrtani
                    if (!matrix[i, j].Contains("--"))
                    {
                        int value = 0;
                        int.TryParse(matrix[i, j], out value);
                        if (value != 0)
                        {
                            if (value < min)
                                min = value;
                        }
                    }
                }
            }

            return min;
        }

        public string[,] SubtractValues(string[,] matrix, int minValue)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    // ova provera jer gleda samo one elemente u matrici koji nisu precrtani
                    // i oduzima minimum od njih
                    if (!matrix[i, j].Contains("--"))
                    {
                        int value = 0;
                        int.TryParse(matrix[i, j], out value);
                        if (value != 0)
                        {
                            int matrix_value = Convert.ToInt32(matrix[i, j]);
                            matrix_value = matrix_value - minValue;
                            matrix[i, j] = matrix_value.ToString();
                        }
                    }
                }
            }
            return matrix;
        }

        public string[,] AddMinimumToCrossScratchPositions(string[,] matrix, int minValue)
        {
            int matrix_value = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    // ova provera gleda samo one elemente u matrici koji su unakrsno precrtani
                    // i dodaje im minimum
                    // ---- znaci da je 2 puta precrtana vrednost, a -- da je jednom
                    if(matrix[i, j].StartsWith("----"))
                    {
                        if (matrix[i, j] == "----/O/----" || matrix[i, j] == "----∅----")
                        {
                            matrix_value = matrix_value + minValue;
                            matrix[i, j] = matrix_value.ToString();
                            matrix_value = 0;
                        }

                        else //if(matrix[i, j].StartsWith("----") && matrix[i, j].EndsWith("----"))
                        {
                            // otklanjamo crtice iz zapisa kako bi prebacili u int da bi mogli da saberemo
                            string substring = matrix[i, j].Substring(4);// uklanja prve ---- iz broja
                            substring = substring.Remove(substring.Length - 4); // uklanja poslednje ---- broja
                            matrix_value = Convert.ToInt32(substring) + minValue;
                            matrix[i, j] = matrix_value.ToString();
                            matrix_value = 0;
                        }
                    }
                }
            }

            return matrix;
        }

        public string[,] RewriteOneTimeScratchPositions(string[,] matrix)
        {
            int matrix_value = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j].EndsWith("--"))
                    {
                        if (matrix[i, j] == "--/O/--" || matrix[i, j] == "--∅--")
                        {
                            matrix_value = 0;
                            matrix[i, j] = matrix_value.ToString();
                        }
                        else if (!matrix[i, j].Contains("--------"))
                        {
                            // otklanjamo crtice iz zapisa kako bi prebacili u int
                            string substring = matrix[i, j].Remove(0, 2); // uklanja prve -- iz broja
                            substring = substring.Remove(substring.Length - 2); // uklanja poslednje -- broja
                            matrix[i, j] = substring;
                        }
                    }
                }
            }
            return matrix;
        }
    }
}
