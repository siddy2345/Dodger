using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            createPictureBox();
            
        }

        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Color> colors = new List<Color>()
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Indigo,
                Color.Violet
            };
        private int counter = 0;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            switch (e.KeyChar)
            {
                case 'a':

                    if (pbRunner.Location.X + pbRunner.Width < 0)
                    {
                        pbRunner.Location = new Point(Width - pbRunner.Width, pbRunner.Location.Y);
                    }
                    else
                    {
                        pbRunner.Location = new Point(pbRunner.Location.X - 15, pbRunner.Location.Y);
                    }

                    break;

                case 'd':

                    if (pbRunner.Location.X > Width)
                    {
                        pbRunner.Location = new Point(0, pbRunner.Location.Y);
                    }
                    else
                    {
                        pbRunner.Location = new Point(pbRunner.Location.X + 15, pbRunner.Location.Y);
                    }
                                        
                    break;

                    //case 'w':
                    //    pbRunner.Location = new Point(pbRunner.Location.X, pbRunner.Location.Y - 15);
                    //    break;
                    //case 's':
                    //    pbRunner.Location = new Point(pbRunner.Location.X, pbRunner.Location.Y + 15);
                    //    break;

            }

            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //pictureBoxes.Add(createPictureBox());

            //foreach (var item in pictureBoxes)
            //{
            //    item.Location = new Point(40, item.Location.Y + 5);
            //}

            
            pictureBoxes[0].Location = new Point(pictureBoxes[0].Location.X, pictureBoxes[0].Location.Y + 5);

            foreach (var picturebox in pictureBoxes)
            {
                if (picturebox.Bounds.IntersectsWith(pbRunner.Bounds))
                {
                    timer1.Stop();
                    DialogResult result = MessageBox.Show("Leider verloren :(\nWillst du es nochmal versuchen?", "Verloren", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result.Equals(DialogResult.Yes))
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Application.Exit();
                    }

                }
            }

            if (pictureBoxes[0].Location.Y > Height)
            {
                this.Controls.Remove(pictureBoxes[0]);
                pictureBoxes.RemoveAt(0);
                createPictureBox();
            }
        }

        public PictureBox createPictureBox()
        {
            Random random = new Random();
            int randX = random.Next(0, Width);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(5, 10);
            pictureBox.BackColor = Color.Red;

            counter++;
            if (counter == 5)
            {
                int xPos = pbRunner.Location.X + (pbRunner.Width / 2);
                pictureBox.Location = new Point(xPos, 0);
                counter = 0;
            }
            else
            {
                pictureBox.Location = new Point(randX, 0);
            }
            
            pictureBoxes.Add(pictureBox);
            this.Controls.Add(pictureBox);

            return pictureBox;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int newRand = rand.Next(colors.Count);

            pbRunner.BackColor = colors[newRand];

        }
    }
}
