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

        public void GenerateFormWithMatrix(int rows, int columns)
        {
            SettingUpForm(columns);
            GenerateMatrix(rows, columns);
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

            //// za uzimanje vrednosti iz textboxa
            //foreach (TextBox t in list)
            //{
            //    StringBuilder bu = new StringBuilder();
            //    if (t.Text != String.Empty)
            //    {
            //        bu.Append(t.Text, "|");
            //    }
            //    MessageBox.Show(bu.ToString());
            //}

            return textboxes;
        }

        public void GenerateMatrix(int rows, int columns)
        {
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
            List<TextBox> textboxes = GenerateTextboxes(rows, columns);

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

            // prebacujem u globalTextboxes jer global koristimo za reset vrednosti iz textboxeva
            globalTextboxes = textboxes;
        }

        // Metoda koja formira Textbox-ove. Formira 36 njih jer 
        // je najveci moguci unos 6x6 = 36
        

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

        }
    }
}
