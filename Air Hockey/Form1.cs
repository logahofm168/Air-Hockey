using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        //Global variables
        Rectangle player1 = new Rectangle(10, 170, 35, 35);
        Rectangle player2 = new Rectangle(330, 170, 35, 35);
        Rectangle point = new Rectangle(295, 195, 10, 10);
        Rectangle speBoost = new Rectangle(200, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 5;
        int player2Speed = 5;

        Random randomGenerator = new Random();

        int randomNumber;

        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Pen bluePen = new Pen(Color.Blue);
        Pen redPen = new Pen(Color.Red);
        Pen whitePen = new Pen(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }

            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            if (aPressed == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }

            if (dPressed == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1Speed;
            }

            //move player2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }

            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (leftPressed == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }

            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            //check if either player hits a point square. If it dose give the corresponding player a point,
            //and redraw the point square somewhere else. 
            if (player1.IntersectsWith(point))
            {
                point.X = randomGenerator.Next(30, 330);
                point.Y = randomGenerator.Next(30, 330);

                player1Score++;
            }
            else if (player2.IntersectsWith(point))
            {
                point.X = randomGenerator.Next(30, 415);
                point.Y = randomGenerator.Next(30, 415);

                player2Score++;
            }

            if (player1Score == 5)
            {
                gameTimer.Stop();

                winnerLabel.Text = "Player 1 wins";

            }

            if (player2Score == 5)
            {
                gameTimer.Stop();

                winnerLabel.Text = "Player 2 wins";
            }

            //check if either player hits a speed boost. If so give the corresponding player a speed boost,
            //and redraw the speed boost somewhere else.
            if (player1.IntersectsWith(speBoost))
            {
                speBoost.X = randomGenerator.Next(30, 415); 
                speBoost.Y = randomGenerator.Next(30, 415);

                player1Speed *= randomGenerator.Next(1, 3); 
            }
            
            if (player2.IntersectsWith(speBoost))
            {
                speBoost.X = randomGenerator.Next(30, 415); ;
                speBoost.Y = randomGenerator.Next(30, 415); ;

                player2Speed *= randomGenerator.Next(1, 3);
            }

            //check if player hits a wall
            //right wall
            if (player1.X > 405)
            {
                player1.X = 404;
            }
            if (player2.X > 405)
            {
                player2.X = 404;
            }
            //left wall
            if (player1.X < 25)
            {
                player1.X = 26;
            }
            if (player2.X < 25)
            {
                player2.X = 26;
            }
            //top wall
            if (player1.Y < 25)
            {
                player1.Y = 26;
            }
            if (player2.Y < 25)
            {
                player2.Y = 26;
            }
            //bottom wall
            if (player1.Y > 390)
            {
                player1.Y = 389;
            }
            if (player2.Y > 390)
            {
                player2.Y = 389;
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //displayes all drawn and written components   

            p1ScoreLabel.Text = $"Player 1 :{player1Score}";
            p2ScoreLabel.Text = $"Player 2 :{player2Score}";

            e.Graphics.DrawRectangle(whitePen, 25, 25, 415, 400);
            e.Graphics.DrawRectangle(bluePen, player1);
            e.Graphics.DrawRectangle(redPen, player2);
            e.Graphics.FillRectangle(whiteBrush, point);
            e.Graphics.FillEllipse(yellowBrush, speBoost);
        }
    }
}
