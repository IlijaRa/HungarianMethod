using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HungarianMethod
{
    public partial class GeneratedMatrix : Form
    {

        List<TextBox> globalTextboxes = new List<TextBox>();
        int globalRows;
        int globalColumns;
        string globalTypeOfProblem = "";

        public GeneratedMatrix(int rows, int columns, string typeOfProblem)
        {
            InitializeComponent();
            globalRows = rows;
            globalColumns = columns;
            globalTypeOfProblem = typeOfProblem;
            TextBox[,] generatedTextboxMatrix = GenerateTextboxMatrix(rows, columns);
            PaintFirstRowFirstColumn(generatedTextboxMatrix);
            MoveMatrixToList(generatedTextboxMatrix);
        }

        public void MoveMatrixToList(TextBox[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    globalTextboxes.Add(matrix[i, j]);
                }
            }
        }

        public enum Axis
        {
            Rows,
            Columns
        }

        public TextBox[,] GenerateTextboxMatrix(int rows, int columns)
        {
            TextBox[,] MatrixNodes = new TextBox[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var tb = new TextBox();
                    MatrixNodes[i, j] = tb;
                    tb.Name = "Node_" + i.ToString() + j.ToString();
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Location = new Point(60 + (j * 120), 60 + (i * 50));
                    tb.Visible = true;
                    if (i == 0 && j == 0)
                    {
                        tb.Text = "\\";
                        tb.Enabled = false;
                        tb.BackColor = Color.FromArgb(240, 128, 128);
                    }

                    this.Controls.Add(tb);
                }
            }
            return MatrixNodes;
        }

        public void PaintFirstRowFirstColumn(TextBox[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == 0)
                        matrix[i, j].BackColor = Color.FromArgb(153, 180, 209);
                    if (j == 0)
                        matrix[i, j].BackColor = Color.FromArgb(240, 128, 128);
                    if (i == 0 && j == 0)
                        matrix[i, j].BackColor = Color.FromArgb(227, 227, 227);
                }
            }
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
            Console.Clear();

            if (CheckIfValuesAreIntParsable())
            {

                string[,] fullMatrix = GenerateFullMatrix(globalRows, globalColumns);
                printMatrixString(fullMatrix, "Full matrix:\n");

                string[,] MatrixThroughPhases = new string[globalRows - 1, globalColumns - 1];

                if (globalTypeOfProblem.Equals("Minimization"))
                {
                    MatrixThroughPhases = GenerateStartMatrix(globalRows, globalColumns);
                }
                else if (globalTypeOfProblem.Equals("Maximization"))
                {
                    MatrixThroughPhases = GenerateStartMatrix(globalRows, globalColumns);
                    int[,] intMatrixThroughPhases = ConvertStringMatrixToIntMatrix(MatrixThroughPhases);
                    intMatrixThroughPhases = MultiplyWithMinusOne(intMatrixThroughPhases);
                    MatrixThroughPhases = ConvertIntMatrixToStringMatrix(intMatrixThroughPhases);
                }

                printMatrixString(MatrixThroughPhases, "Matrix without first row and first column:\n");


                string[,] solution = FirstStepHungarianMethod(MatrixThroughPhases);
                solution = OtherStepsHungarianMethod(solution);

                int i = 1;
                while (i <= 10)
                {
                    string[,] matrix = FindIndependentZeros(solution);
                    if (CheckIfAllRowsHaveIndependentZero(matrix) == true)
                        break;
                    else
                    {
                        solution = OtherStepsHungarianMethod(solution);
                        i++;
                    }

                }
                solution = FindIndependentZeros(solution);
                printMatrixString(solution, "Final result:\n");
                PaintIndependentZerosInMatrix(solution);

                Solution solutionForm = new Solution();
                solutionForm.TransferSolutionWithFullMatrix(fullMatrix, solution);
                solutionForm.Show();
            }
            else
                MessageBox.Show("Denied");
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
            //-----> 1. Step <-----//
            stringMatrixThroughPhases = CalculateZerosByAxis(stringMatrixThroughPhases, Axis.Rows);
            printMatrixString(stringMatrixThroughPhases, "Subtracted minimum from other values in a row:\n");

            stringMatrixThroughPhases = CalculateZerosByAxis(stringMatrixThroughPhases, Axis.Columns);
            printMatrixString(stringMatrixThroughPhases, "Subtracted minimum from other values in a column:\n");
            return stringMatrixThroughPhases;
        }
        public string[,] OtherStepsHungarianMethod(string[,] stringMatrixThroughPhases)
        {
            //-----> 2. Step <-----//
            stringMatrixThroughPhases = FindIndependentZeros(stringMatrixThroughPhases);
            printMatrixString(stringMatrixThroughPhases, "Found independent zeros:\n");

            //-----> 3. Step <-----//
            List<int> markedRows = MarkRowsWithoutIndependentZero(stringMatrixThroughPhases);
            Console.WriteLine("------>Marked rows without independent zero in it<------\n");

            //-----> 4. Step <-----//
            List<int> scratchedColumns = MarkColsWhereRowIsZero(stringMatrixThroughPhases, markedRows);
            stringMatrixThroughPhases = ScratchColsWhereRowIsZero(stringMatrixThroughPhases, scratchedColumns);
            printMatrixString(stringMatrixThroughPhases, "Scratched columns where row has value zero:");

            //-----> 5. Step <-----//
            List<int> AllMarkedRows = MarkRowsWithIndependentZerosInScratchedCols(stringMatrixThroughPhases, scratchedColumns, markedRows);
            Console.WriteLine("------>Marked rows where scratched columns have independent zero<------\n");

            //-----> 6. Step <-----//
            stringMatrixThroughPhases = ScratchUnmarkedRows(stringMatrixThroughPhases, AllMarkedRows);
            printMatrixString(stringMatrixThroughPhases, "Scratched all unmarked rows:");

            //-----> 7. Step <-----//
            stringMatrixThroughPhases = TransformationWithMinimumValue(stringMatrixThroughPhases);
            printMatrixString(stringMatrixThroughPhases, "Min value of unscratched values is subtracted from all unscratched values, and added to all cross-scratched values:");

            return stringMatrixThroughPhases;
        }

        // This function is called to check if the algorithm is over
        public bool CheckIfAllRowsHaveIndependentZero(string[,] matrix)
        {
            bool rowHasIndependentZero = true;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
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

        public void PaintIndependentZerosInMatrix(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == "/O/")
                    {
                        // Checks in the list which textbox has the same narrative as this one with an independent zero to color it
                        foreach (var textbox in globalTextboxes)
                        {
                            if (textbox.Name.Equals("Node_" + (i + 1).ToString() + (j + 1).ToString()))
                            {
                                textbox.BackColor = Color.FromArgb(0, 120, 215);
                            }
                        }
                    }
                }
            }
        }

        // Checks whether the entered values ​​can be converted to int values
        public bool CheckIfValuesAreIntParsable()
        {
            int result;
            bool valuesAreValid = true;
            foreach (var textbox in globalTextboxes)
            {
                if (textbox.BackColor != Color.FromArgb(153, 180, 209) &&
                    textbox.BackColor != Color.FromArgb(240, 128, 128) &&
                    textbox.BackColor != Color.FromArgb(227, 227, 227))
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

        // Forms a matrix of values ​​we entered but without the values ​​of the pink/gray-blue textboxes; it means that dimensions are reduced by 1
        public string[,] GenerateStartMatrix(int rows, int columns)
        {
            int arrayCounter = 0;
            string[] validValues = MakingArrayOfActualValues(rows, columns);

            // Dimensions reduced by 1 because it does not count fields that are pink/gray-blue
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

        // makes an array of values ​​in non-blue-gray/pink textboxes
        public string[] MakingArrayOfActualValues(int rows, int columns)
        {
            string[] actualValues = new string[rows * columns];
            int valuesCounter = 0;
            foreach (var textbox in globalTextboxes)
            {
                if (textbox.BackColor != Color.FromArgb(153, 180, 209) &&
                    textbox.BackColor != Color.FromArgb(240, 128, 128) &&
                    textbox.BackColor != Color.FromArgb(227, 227, 227))
                {
                    actualValues[valuesCounter] = textbox.Text;
                    valuesCounter++;
                }
            }

            return actualValues;
        }

        // Forms a matrix of values ​​we entered but with all values ​​including pink/gray-blue textboxes
        public string[,] GenerateFullMatrix(int rows, int columns)
        {
            int arrayCounter = 0;
            string[,] fullMatrix = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    fullMatrix[i, j] = globalTextboxes[arrayCounter].Text;
                    arrayCounter++;
                }

            }

            return fullMatrix;
        }

        // The function takes a matrix and an axis that tells whether to subtract minimum by rows or columns
        public string[,] CalculateZerosByAxis(string[,] matrix, Axis axis)
        {
            // if axis is a columns then it will transpose the matrix and do the same as if the axis were rows
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int min = FindMinByRow(matrix, i);
                SubtractMinWithOtherRowValues(matrix, i, min);
            }

            // returns the matrix to its original orientation
            if (axis == Axis.Columns)
                matrix = Transpose(matrix);

            return matrix;
        }

        // Transpose a matrix
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

        // seek a minimum in each row
        public int FindMinByRow(string[,] matrix, int row)
        {
            int min = Convert.ToInt32(matrix[row, 0]);
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                int value = -10;
                int.TryParse(matrix[row, j], out value);
                if (value != -10)
                {
                    if (value < min)
                        min = value;
                }
                else
                    MessageBox.Show("Bad parsing!");
            }
            return min;
        }

        // Subtracts the minimum from that row from other values ​​in the same row
        public void SubtractMinWithOtherRowValues(string[,] matrix, int row, int min)
        {
            int matrix_value = -10;
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                int.TryParse(matrix[row, j], out matrix_value);
                if (matrix_value != -10)
                {
                    matrix_value = matrix_value - min;
                    matrix[row, j] = matrix_value.ToString();
                }
            }

        }

        // print matrix in the console with forwarded message
        public void printMatrixString(string[,] matrix, string message)
        {
            Console.WriteLine(message);
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

            // priority is a list containing values ​​from 1 to the matrix dimension
            // priority tells in which order it will look at the number of zeros in the rows,
            // first looks at whether there is one 0 in the row, then two 0, etc.
            List<int> priority = new List<int>();
            for (int i = 1; i < globalRows; i++)
                priority.Add(i);

            foreach (var numberOfZerosPerRow in priority)
            {
                stringMatrix = CountAndScratchZeros(stringMatrix, numberOfZerosPerRow);
            }

            return stringMatrix;
        }

        public string[,] CountAndScratchZeros(string[,] stringMatrix, int numberOfZerosPerRow)
        {
            for (int i = 0; i < stringMatrix.GetLength(0); i++)
            {
                int zeroCounter = 0;
                // goes through the rows and counts how many zeros are in it
                for (int j = 0; j < stringMatrix.GetLength(0); j++)
                {
                    if (stringMatrix[i, j] == "0" || stringMatrix[i, j] == "∅")
                    {
                        zeroCounter++;
                    }
                }

                // if he found the number numberOfZerosInRow in the row,
                // 0 on the coordinates [i, j] will switch to /O/. Other zeros in the same row and column is going to transform to "∅"
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
                    // if the element is crossed out, do not cross it out again
                    matrix[i, col] = "--" + matrix[i, col] + "--";
                }
            }

            return matrix;
        }

        // Receives the markedRows parameter because before this the function MarkRowsWithoutIndependentZero
        // flaged all rows that do not have an independent zero and returned a list of those rows
        // This function builds on that list to add more rows that have an independent zero in the crossed-out columns
        // by receiving the scratchedCols parameter to know which columns to check
        public List<int> MarkRowsWithIndependentZerosInScratchedCols(string[,] matrix, List<int> scratchedCols, List<int> markedRows)
        {
            foreach (var col in scratchedCols)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    // /O/ changes to --/O/--
                    if (matrix[i, col] == "--/O/--")
                    {
                        markedRows.Add(i);
                    }
                }
            }

            return markedRows;
        }

        public string[,] ScratchUnmarkedRows(string[,] matrix, List<int> markedRows)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (!markedRows.Contains(i))
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        // if the element is crossed out, do not cross it out again
                        matrix[i, j] = "--" + matrix[i, j] + "--";
                    }
                }
            }

            return matrix;
        }

        public string[,] TransformationWithMinimumValue(string[,] matrix)
        {
            int min = FindMinimalUnscratchedValue(matrix);
            string[,] transformedMatrix = SubtractUnscratchedValues(matrix, min);
            transformedMatrix = AddMinimumToCrossScratchPositions(transformedMatrix, min);
            transformedMatrix = RewriteOneTimeScratchPositions(transformedMatrix);
            return transformedMatrix;
        }


        public int FindMinimalUnscratchedValue(string[,] matrix)
        {
            // we need to set some value to the initial minimum
            // but it must be checked whether this value can be passed to int, since the matrix is string matrix currently.
            // If it can't, it checks the next value and so on until it finds the first value it can convert to int.
            int min = FindValidMinimum(matrix);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    // this checks if elements in the matrix that are not crossed out
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


        public int FindValidMinimum(string[,] matrix)
        {
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
            return min;
        }

        public string[,] SubtractUnscratchedValues(string[,] matrix, int minValue)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
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
                    // this check looks only for those elements in the matrix that are cross-scratched and adds a minimum to them
                    // '----' means that the value has been crossed out twice, and '--' that it has been crossed out once
                    if (matrix[i, j].StartsWith("----"))
                    {
                        if (matrix[i, j] == "----/O/----" || matrix[i, j] == "----∅----")
                        {
                            matrix_value = matrix_value + minValue;
                            matrix[i, j] = matrix_value.ToString();
                            matrix_value = 0;
                        }

                        else
                        {
                            // we remove the dashes, transfer to int and then we can add minimal value to cross-scratched values
                            string substring = matrix[i, j].Substring(4);// removes first '----' from the number
                            substring = substring.Remove(substring.Length - 4); // removes last '----' from the number
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
                            // we remove the dashes, transfer to int and then we can add minimal value to cross-scratched values
                            string substring = matrix[i, j].Remove(0, 2); // removes first '--' from the number
                            substring = substring.Remove(substring.Length - 2); // removes last '--' from the number
                            matrix[i, j] = substring;
                        }
                    }
                }
            }
            return matrix;
        }
    }
}
