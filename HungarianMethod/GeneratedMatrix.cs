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

        public void GenerateMatrix(int rows, int columns)
        {

            // Formiranje radio buttona na odredjenoj lokaciji
            RadioButton radio1 = new RadioButton();
            RadioButton radio2 = new RadioButton();
            radio1.Location = new Point(18, 265);
            radio1.Text = "Values in matrix represent number of hours";
            this.Controls.Add(radio1);
            radio2.Location = new Point(18, 316);
            radio2.Text = "Values in matrix represent quantity per hour ";
            this.Controls.Add(radio2);

            if (rows > 4)
            {
                //GeneratedMatrix gen = new GeneratedMatrix();
                //gen.Size = Size(922, 622);
                Size = new Size(922, 522);
                radio1.Location = new Point(18, 265);
                radio2.Location = new Point(18, 316);
            }
            List<TextBox> textboxes = GenerateTextboxes(rows, columns);
            
            int counter = 0;
            int stop = 0;
            int X = 20;
            int Y = 20;

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
                    if (j==0)
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
                X = 20;
                Y = Y + 60;

            }
        }
        // Metoda koja formira Textbox-ove. Formira 36 njih jer 
        // je najveci moguci unos 6x6 = 36
        public List<TextBox> GenerateTextboxes(int rows, int columns)
        {
            TextBox textbox1 = new TextBox();
            TextBox textbox2 = new TextBox();
            TextBox textbox3 = new TextBox();
            TextBox textbox4 = new TextBox();
            TextBox textbox5 = new TextBox();
            TextBox textbox6 = new TextBox();
            TextBox textbox7 = new TextBox();
            TextBox textbox8 = new TextBox();
            TextBox textbox9 = new TextBox();
            TextBox textbox10 = new TextBox();
            TextBox textbox11 = new TextBox();
            TextBox textbox12 = new TextBox();
            TextBox textbox13 = new TextBox();
            TextBox textbox14 = new TextBox();
            TextBox textbox15 = new TextBox();
            TextBox textbox16 = new TextBox();
            TextBox textbox17 = new TextBox();
            TextBox textbox18 = new TextBox();
            TextBox textbox19 = new TextBox();
            TextBox textbox20 = new TextBox();
            TextBox textbox21 = new TextBox();
            TextBox textbox22 = new TextBox();
            TextBox textbox23 = new TextBox();
            TextBox textbox24 = new TextBox();
            TextBox textbox25 = new TextBox();
            TextBox textbox26 = new TextBox();
            TextBox textbox27 = new TextBox();
            TextBox textbox28 = new TextBox();
            TextBox textbox29 = new TextBox();
            TextBox textbox30 = new TextBox();
            TextBox textbox31 = new TextBox();
            TextBox textbox32 = new TextBox();
            TextBox textbox33 = new TextBox();
            TextBox textbox34 = new TextBox();
            TextBox textbox35 = new TextBox();
            TextBox textbox36 = new TextBox();

            List<TextBox> textboxes = new List<TextBox>();

            //for (int i = 1; i <= rows*columns; i++)
            //{
            //    TextBox textbox = (TextBox)Controls["textbox" + i];
            //    textboxes.Add(textbox);
            //    textBox.Text = "blank"; // Place what you want to do to every textbox here.
            //}

            textboxes.Add(textbox1);
            textboxes.Add(textbox2);
            textboxes.Add(textbox3);
            textboxes.Add(textbox4);
            textboxes.Add(textbox5);
            textboxes.Add(textbox6);
            textboxes.Add(textbox7);
            textboxes.Add(textbox8);
            textboxes.Add(textbox9);
            textboxes.Add(textbox10);
            textboxes.Add(textbox11);
            textboxes.Add(textbox12);
            textboxes.Add(textbox13);
            textboxes.Add(textbox14);
            textboxes.Add(textbox15);
            textboxes.Add(textbox16);
            textboxes.Add(textbox17);
            textboxes.Add(textbox18);
            textboxes.Add(textbox19);
            textboxes.Add(textbox20);
            textboxes.Add(textbox21);
            textboxes.Add(textbox22);
            textboxes.Add(textbox23);
            textboxes.Add(textbox24);
            textboxes.Add(textbox25);
            textboxes.Add(textbox26);
            textboxes.Add(textbox27);
            textboxes.Add(textbox28);
            textboxes.Add(textbox29);
            textboxes.Add(textbox30);
            textboxes.Add(textbox31);
            textboxes.Add(textbox32);
            textboxes.Add(textbox33);
            textboxes.Add(textbox34);
            textboxes.Add(textbox35);
            textboxes.Add(textbox36);

            return textboxes;
        }

    }
}
