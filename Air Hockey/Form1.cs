using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        //Global variables
        Rectangle player1 = new Rectangle(40, 170, 35, 35);
        Rectangle player2 = new Rectangle(390, 170, 35, 35);
        Rectangle point = new Rectangle(295, 195, 10, 10);
        Rectangle speBoost = new Rectangle(200, 195, 10, 10);
        Rectangle reduceSpe = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 5;
        int player2Speed = 5;
        int reduceSpeXSpeed = -5;
        int reduceSpeYSpeed = 5;

        SoundPlayer soundPlayer = new SoundPlayer();

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
        Pen fuchsiaPen = new Pen(Color.Fuchsia);


        public Form1()
        {
            InitializeComponent();

            //makes both the point and speBoost block start in a random spot
            point.X = randomGenerator.Next(30, 415);
            point.Y = randomGenerator.Next(30, 415);

            speBoost.X = randomGenerator.Next(30, 415);
            speBoost.Y = randomGenerator.Next(30, 415);
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
            //move redosSpe
            reduceSpe.X += reduceSpeXSpeed;
            reduceSpe.Y += reduceSpeYSpeed;

            playerMovement();

            pointCheck();

            speedCheck();

            reduceSpeCheck();

            wallCheck();

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
            e.Graphics.DrawRectangle(fuchsiaPen, reduceSpe);
        }

        private void speBoostTimer_Tick(object sender, EventArgs e)
        {
            // makes the speed boost temporary
            speBoostTimer.Enabled = false;

            player1Speed = 5; 
            player2Speed = 5;

        }

        public void playerMovement ()
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

        }

        public void speedCheck()
        {
            //check if either player hits a speed boost. If so give the corresponding player a speed boost,
            //and redraw the speed boost somewhere else.
            if (player1.IntersectsWith(speBoost))
            {
                speBoostTimer.Enabled = true;

                speBoost.X = randomGenerator.Next(30, 415);
                speBoost.Y = randomGenerator.Next(30, 415);

                player1Speed *= 7;

                soundPlayer = new SoundPlayer(Properties.Resources.speed_boost_sound);
                soundPlayer.Play();
            }

            if (player2.IntersectsWith(speBoost))
            {
                speBoostTimer.Enabled = true;

                speBoost.X = randomGenerator.Next(30, 415); ;
                speBoost.Y = randomGenerator.Next(30, 415); ;

                player2Speed *= 7;

                soundPlayer = new SoundPlayer(Properties.Resources.speed_boost_sound);
                soundPlayer.Play();
            }
        }

        public void pointCheck ()
        {
            //check if either player hits a point square. If it dose give the corresponding player a point,
            //and redraw the point square somewhere else. 
            if (player1.IntersectsWith(point))
            {
                point.X = randomGenerator.Next(30, 415);
                point.Y = randomGenerator.Next(30, 415);

                player1Score++;

                soundPlayer = new SoundPlayer(Properties.Resources.Point_noise);
                soundPlayer.Play();
            }
            else if (player2.IntersectsWith(point))
            {
                point.X = randomGenerator.Next(30, 415);
                point.Y = randomGenerator.Next(30, 415);

                player2Score++;

                soundPlayer = new SoundPlayer(Properties.Resources.Point_noise);
                soundPlayer.Play();
            }

            if (player1Score == 5)
            {
                gameTimer.Stop();

                winnerLabel.Text = "Player 1 wins";

                soundPlayer = new SoundPlayer(Properties.Resources.Winning_sound);
                soundPlayer.Play();
            }

            if (player2Score == 5)
            {
                gameTimer.Stop();

                winnerLabel.Text = "Player 2 wins";

                soundPlayer = new SoundPlayer(Properties.Resources.Winning_sound);
                soundPlayer.Play();
            }
        }

        public void wallCheck ()
        {
            //check if player hits a wall
            //right wall
            if (player1.X > 405)
            {
                player1.X = 404;

            }
            if (player2.X > 405)
            {
                player2.X = 404; ;
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

            //check if redosSpe hit wall and change direction if it does 
            if (reduceSpe.Y < 30 || reduceSpe.Y > this.Height - reduceSpe.Height)
            {
                reduceSpeYSpeed *= -1;
            }

            if (reduceSpe.Y > 410 || reduceSpe.Y > this.Height - reduceSpe.Height)
            {
                reduceSpeYSpeed *= -1;
            }

            if (reduceSpe.X < 30 || reduceSpe.X > this.Width - reduceSpe.Width)
            {
                reduceSpeXSpeed *= -1;
            }

            if (reduceSpe.X > 425 || reduceSpe.X > this.Height - reduceSpe.Height)
            {
                reduceSpeXSpeed *= -1;
            }

        }

        public void reduceSpeCheck()
        {
            //check if either player hits a reduce speed. If so give the corresponding player a redosed speed,

            if (player1.IntersectsWith(reduceSpe))
            {
                reduceSpeXSpeed *= -1;
                reduceSpe.X = player1.X + player1.Width;

                player1Speed = 3;

                soundPlayer = new SoundPlayer(Properties.Resources.Reduce_speed);
                soundPlayer.Play();
            }

            if (player2.IntersectsWith(reduceSpe))
            {
                reduceSpeXSpeed *= -1;
                reduceSpe.X = player1.X + player1.Width;

                player2Speed = 3;

                soundPlayer = new SoundPlayer(Properties.Resources.Reduce_speed);
                soundPlayer.Play();
            }
        }
    }
}
