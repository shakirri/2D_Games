using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Racing_game
{
    public partial class RacingGame : Form
    {
        Form1 form1;
        private int RoadSpeed = 10;
        private const int CarSpeed = 5;
        private Point carPosition;
        private Random random = new Random();
        private int leftBound = 140; // Define left boundary
        private int rightBound;

        PictureBox carPictureBox;

        private bool leftArrowKeyPressed = false;
        private bool rightArrowKeyPressed = false;
        private bool upArrowKeyPressed = false;
        private bool downArrowKeyPressed = false;
        private int backgroundY = 0;
        private int score = 0;

       
        Image[] images = {Properties.Resources.car_A, Properties.Resources.car_B, Properties.Resources.car_C, Properties.Resources.car_D, Properties.Resources.car_E }; 
        public RacingGame(Form1 parent)
        {
            form1 = parent;
            InitializeComponent();
            Label scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.Location = new Point(10, 10); // Adjust the position as needed
            this.Controls.Add(scoreLabel);
            this.DoubleBuffered = true;
            InitializeGame();
            InitializeTimer();
            

        }
        private void InitializeTimer()
        {
            timer.Interval = 1000; // Adjust as needed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SpawnCar();
            score++;
            UpdateScoreLabel();


        }
        private void SpawnCar()
        {
            // Randomly select a car image path
            Image randomCarImagePath = images[random.Next(images.Length)];

            // Create a new PictureBox for the car
            PictureBox carPictureBox = new PictureBox();
            carPictureBox.Image = randomCarImagePath;
            carPictureBox.Size = new Size(50, 100);
            carPictureBox.BackColor = Color.Transparent;
            carPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // Randomly select horizontal position
            int xPos = random.Next(this.ClientSize.Width - carPictureBox.Width);

            // Set the position of the car
            carPictureBox.Location = new Point(xPos, 0);

            // Add the car to the form
            this.Controls.Add(carPictureBox);
        }
        private void UpdateScoreLabel()
        {
            // Update the score label text
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Text.StartsWith("Score: "))
                {
                    control.Text = "Score: " + score.ToString();
                    break;
                }
            }
        }
        private void MoveCarsDown()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control != carPictureBox && ((PictureBox)control).Image != null)
                {
                    PictureBox carPictureBox = (PictureBox)control;
                    carPictureBox.Top += CarSpeed* ((int)(score / 5) + 2);

                    // Remove cars that have gone beyond the bottom of the form
                    if (carPictureBox.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(carPictureBox);
                        carPictureBox.Dispose();
                    }
                }
            }
        }



        private void InitializeGame()
        {
            // Set up the form
            this.BackgroundImage = Properties.Resources.background_1;
            this.BackgroundImageLayout = ImageLayout.Tile;// Path to your road image
            this.ClientSize = this.BackgroundImage.Size;

            rightBound = this.ClientSize.Width - leftBound;
            // Set up the car
            PictureBox carPictureBox = new PictureBox();
            carPictureBox.Image = Properties.Resources.Daco_6030572; // Path to your car image
            carPictureBox.Size = new Size(50, 100); // Set the size to 200x100
            carPictureBox.BackColor = Color.Transparent;
            carPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Use StretchImage mode to fit the image to the size
            carPictureBox.Location = new Point((this.ClientSize.Width - carPictureBox.Width) / 2, this.ClientSize.Height - carPictureBox.Height);

            this.Controls.Add(carPictureBox);
            carPosition = carPictureBox.Location; // Update carPosition here

            // Assign the carPictureBox to the class-level variable
            this.carPictureBox = carPictureBox; // Add this line

            // Set up the game loop
            timer1.Interval = 1000 / 30; // 60 frames per second
                                         // timer1.Tick += GameLoop_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Move the background (road)
            // Move the background (road)
            MoveCarsDown();
            CheckCollisions();
            backgroundY += RoadSpeed* ((int)(score/5)+1);

            if (backgroundY >= this.ClientSize.Height)
            {
                backgroundY = 0;
            }
            
            // Update the position of the background image
            UpdateBackgroundPosition();

            // Move the car left or right
            if (leftArrowKeyPressed && carPosition.X > leftBound)
            {
                carPosition.X -= CarSpeed;
                // Ensure the car stays within the bounds of the form
                if (carPosition.X < leftBound)
                    carPosition.X = leftBound;
            }
            if (rightArrowKeyPressed && carPosition.X < rightBound - carPictureBox.Width)
            {
                carPosition.X += CarSpeed;
                // Ensure the car stays within the bounds of the form
                if (carPosition.X > rightBound - carPictureBox.Width)
                    carPosition.X = rightBound - carPictureBox.Width;
            }
            if (upArrowKeyPressed)
            {
                carPosition.Y -= CarSpeed;
                // Ensure the car stays within the bounds of the form
                if (carPosition.Y < 0)
                    carPosition.Y = 0;
            }
            if (downArrowKeyPressed)
            {
                carPosition.Y += CarSpeed;
                // Ensure the car stays within the bounds of the form
                if (carPosition.Y > this.ClientSize.Height - carPictureBox.Height)
                    carPosition.Y = this.ClientSize.Height - carPictureBox.Height;
            }

            // Update the car's position
            carPictureBox.Location = carPosition;
        }
        private void UpdateBackgroundPosition()
        {
            this.Invalidate(); // Refresh the form to trigger the Paint event
        }




        private void RacingGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                leftArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                rightArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                upArrowKeyPressed = true; // Move car up
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                downArrowKeyPressed = true; // Move car down
        }

        private void RacingGame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                leftArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                rightArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                upArrowKeyPressed = false; // Move car up
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                downArrowKeyPressed = false; // Move car down
        }

        private void RacingGame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, backgroundY - this.ClientSize.Height, this.ClientSize.Width, this.ClientSize.Height));
            e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, backgroundY, this.ClientSize.Width, this.ClientSize.Height));
        }
        private void CheckCollisions()
        {
            Rectangle playerCarBounds = carPictureBox.Bounds;

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control != carPictureBox && ((PictureBox)control).Image != null)
                {
                    PictureBox otherCarPictureBox = (PictureBox)control;
                    Rectangle otherCarBounds = otherCarPictureBox.Bounds;

                    if (playerCarBounds.IntersectsWith(otherCarBounds))
                    {
                        // Handle collision here
                        ShowBoomAtCollisionLocation(otherCarPictureBox.Location);
                        EndGame(); // For example, you can end the game
                        return; // No need to continue checking for collisions if one is found
                    }
                }
            }
        }
        private void ShowBoomAtCollisionLocation(Point collisionLocation)
        {
            PictureBox boomPictureBox = new PictureBox();
            boomPictureBox.Image = Properties.Resources.explosion; // Load your boom image
            boomPictureBox.Size = new Size(300, 100); // Set the size of the boom picture
            boomPictureBox.BackColor = Color.Transparent;
            
            boomPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Adjust as needed
            boomPictureBox.Location = new Point(collisionLocation.X - 130, collisionLocation.Y);

            this.Controls.Add(boomPictureBox); // Add the boom picture to the form's controls collection
            boomPictureBox.BringToFront();
            // Optionally, you can remove the boom picture after a certain delay
            Task.Delay(2000).ContinueWith(_ =>
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Controls.Remove(boomPictureBox);
                        boomPictureBox.Dispose();
                    });
                }
                catch { }
            });
        }
        private void EndGame()
        {

            // Implement game-over logic here
            timer.Stop(); // Stop the game timer
            timer1.Stop();
            if (Convert.ToInt32(Properties.Settings.Default.racingScore) < score)
            {
                Properties.Settings.Default.racingScore = score.ToString();
                Properties.Settings.Default.Save();
                form1.UpdateScores();
            }
            MessageBox.Show("Game Over! You crashed!"); // Show a message to the player
                                                        // You can also reset the game or go to a game-over screen
        }
    }
}


