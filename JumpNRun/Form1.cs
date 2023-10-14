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

            createPictureBoxShots();
        }

        List<PictureBox> shots = new List<PictureBox>();
        List<Color> colors = new List<Color>()
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Indigo,
                Color.Violet,
                Color.Cyan,
                Color.Magenta,
                Color.White,
                Color.Black,
                Color.Gray,
                Color.Tan,
                Color.Brown
            };

        private int counter = 0;
        private int moveIncrement = 15;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                case 'A':
                    moveLeft();
                    break;
                case 'd':
                case 'D':
                    moveRight();
                    break;

                    //case (char)(Keys.Left + 32):

                    //    moveLeft();

                    //    break;

                    //case (char)(Keys.Right + 32):

                    //    moveRight();

                    //    break;

                    //case 'w':
                    //    pbRunner.Location = new Point(pbRunner.Location.X, pbRunner.Location.Y - 15);
                    //    break;
                    //case 's':
                    //    pbRunner.Location = new Point(pbRunner.Location.X, pbRunner.Location.Y + 15);
                    //    break;
            }
        }

        /// <summary>
        /// If the timer ticks the shots should be moved and if a shot hits the runner, the game is over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            shots[0].Location = new Point(shots[0].Location.X, shots[0].Location.Y + 5);

            foreach (var picturebox in shots)
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

            if (shots[0].Location.Y > Height)
            {
                this.Controls.Remove(shots[0]);
                shots.RemoveAt(0);
                createPictureBoxShots();
            }
        }

        /// <summary>
        /// Creates the red picterboxes to dogde
        /// </summary>
        public void createPictureBoxShots()
        {
            Random random = new Random();
            int randX = random.Next(0, Width);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(5, 10);
            pictureBox.BackColor = Color.Red;

            counter++;
            if (counter == 5) // every fifth shot appears exactly over the runner
            {
                int xPos = pbRunner.Location.X + (pbRunner.Width / 2);
                pictureBox.Location = new Point(xPos, 0);
                counter = 0;
            }
            else
            {
                pictureBox.Location = new Point(randX, 0);
            }

            shots.Add(pictureBox);
            this.Controls.Add(pictureBox);
        }

        public void moveLeft()
        {
            if (pbRunner.Location.X + pbRunner.Width < 0)
            {
                pbRunner.Location = new Point(Width - pbRunner.Width, pbRunner.Location.Y);
            }
            else
            {
                pbRunner.Location = new Point(pbRunner.Location.X - moveIncrement, pbRunner.Location.Y);
            }
        }

        public void moveRight()
        {
            if (pbRunner.Location.X > Width)
            {
                pbRunner.Location = new Point(0, pbRunner.Location.Y);
            }
            else
            {
                pbRunner.Location = new Point(pbRunner.Location.X + moveIncrement, pbRunner.Location.Y);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int newRand = rand.Next(colors.Count);

            pbRunner.BackColor = colors[newRand];
        }
    }
}
