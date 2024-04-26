using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(10, 170, 10, 60);
        Rectangle player2 = new Rectangle(580, 170, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = -6;
        int ballYSpeed = -6;

        bool wPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

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
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X = ball.X + ballXSpeed;
            ball.Y = ball.Y + ballYSpeed;

            //check if it hits the top or bottom
            if (ball.Y <= 0 || ball.Y >= this.Height - ball.Height)
            {
                ballYSpeed = -ballYSpeed;
            }

            //move player 1
            if(wPressed == true && player1.Y > 0)
            {
                player1.Y = player1.Y - playerSpeed;
            }

            if(sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y = player1.Y + playerSpeed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y = player2.Y - playerSpeed;
            }

            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y = player2.Y + playerSpeed;
            }

            //check if the ball hit player 1
            if(player1.IntersectsWith(ball))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = player1.X + player1.Width;
            }

            //check if the ball hit player 2
            if (player2.IntersectsWith(ball))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = player2.X - player2.Width;
            }

            //check if ball goes off left side of screen
            if(ball.X <= 0)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                Random randGen = new Random();
                int randValue = randGen.Next(1, 3);

                if (randValue == 1)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 6;
                }
            }

            //check if ball goes off right side of screen
            if (ball.X >= this.Width)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                ball.X = 295;
                ball.Y = 195;

                Random randGen = new Random();
                int randValue = randGen.Next(1, 3);

                if (randValue == 1)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 6;
                }

            }

            //check for a winner
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 Wins";
                gameTimer.Stop();
            }

            if (player2Score == 3)
            {
                winLabel.Text = "Player 2 Wins";
                gameTimer.Stop();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
        }
    }
}
 