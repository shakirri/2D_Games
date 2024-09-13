using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racing_game
{
    public partial class Space_wars : Form
    {
        private Form1 form1;
        private int spaceSpeed = 10;
        private const int shipSpeed = 5;
        private Point shipPosition;
        private Random random = new Random();
        private int leftBound = 140;
        private int rightBound;

        private PictureBox playerShipPictureBox;

        private bool leftArrowKeyPressed = false;
        private bool rightArrowKeyPressed = false;
        private bool upArrowKeyPressed = false;
        private bool downArrowKeyPressed = false;
        private bool spaceBarPressed = false;

        private int backgroundY = 0;
        private int score = 0;



        private List<EnemyShip> enemyShips = new List<EnemyShip>();
        private List<Bullet> bullets = new List<Bullet>();

        private Image[] enemyShipImages = { Properties.Resources.shipbig, Properties.Resources.ship1, Properties.Resources.shipshuttle, Properties.Resources.meteor };
        private Image bulletImage = Properties.Resources.bullet1; // Assuming there's a bullet image in resources

        public Space_wars(Form1 parent)
        {
            form1 = parent;
            InitializeComponent();
            InitializeGame();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            spawnTimer.Interval = 2000;
            
            spawnTimer.Start();

            gameTimer.Interval = 1000 / 60;
            
            gameTimer.Start();
        }

        private void spawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnShip();
            score++;
            UpdateScoreLabel();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            MoveShipsDown();
            MoveBullets();
            CheckCollisions();
            UpdateBackgroundPosition();
            UpdateShipPosition();
            this.Invalidate();
        }

        private void InitializeGame()
        {
            this.BackgroundImage = Properties.Resources.space2;
            this.BackgroundImageLayout = ImageLayout.Tile;

            if (this.BackgroundImage != null)
            {
                this.ClientSize = new Size(this.BackgroundImage.Width / 2, this.BackgroundImage.Height / 2);
            }

            rightBound = this.ClientSize.Width - leftBound;

            playerShipPictureBox = new PictureBox
            {
                Image = Properties.Resources.shippl,
                Size = new Size(50, 100),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point((this.ClientSize.Width - 50) / 2, this.ClientSize.Height - 100)
            };

            this.Controls.Add(playerShipPictureBox);
            shipPosition = playerShipPictureBox.Location;

            this.DoubleBuffered = true;
        }

        private void UpdateBackgroundPosition()
        {
            backgroundY += spaceSpeed;

            if (backgroundY >= this.ClientSize.Height)
            {
                backgroundY = 0;
            }
        }

        private void Space_wars_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, backgroundY - this.ClientSize.Height, this.ClientSize.Width, this.ClientSize.Height));
            e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, backgroundY, this.ClientSize.Width, this.ClientSize.Height));

            foreach (var ship in enemyShips)
            {
                e.Graphics.DrawImage(ship.Image, ship.Position);
            }

            foreach (var bullet in bullets)
            {
                e.Graphics.DrawImage(bullet.Image, bullet.Position);
            }

            e.Graphics.DrawImage(playerShipPictureBox.Image, shipPosition);
        }

        private void SpawnShip()
        {
         
            int xPos = random.Next(leftBound, rightBound - 50);
            Image shipImage = enemyShipImages[random.Next(enemyShipImages.Length)];
            enemyShips.Add(new EnemyShip(new Rectangle(xPos, 0, 50, 100), shipImage));
        }

        private void UpdateScoreLabel()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label scoreLabel && scoreLabel.Text.StartsWith("Score: "))
                {
                    scoreLabel.Text = "Score: " + score.ToString();
                    break;
                }
            }
        }

        private void MoveShipsDown()
        {
            for (int i = enemyShips.Count - 1; i >= 0; i--)
            {
                var ship = enemyShips[i];
                ship.Position = new Rectangle(ship.Position.X, ship.Position.Y + shipSpeed * ((score / 5) + 2), ship.Position.Width, ship.Position.Height);

                if (ship.Position.Y > this.ClientSize.Height)
                {
                    enemyShips.RemoveAt(i);
                }
            }
        }

        private void MoveBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.Position = new Rectangle(bullet.Position.X, bullet.Position.Y - bullet.Speed, bullet.Position.Width, bullet.Position.Height);

                if (bullet.Position.Y < 0)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        private void UpdateShipPosition()
        {
            if (leftArrowKeyPressed && shipPosition.X > leftBound)
            {
                shipPosition.X -= shipSpeed;
                if (shipPosition.X < leftBound)
                    shipPosition.X = leftBound;
            }
            if (rightArrowKeyPressed && shipPosition.X < rightBound - playerShipPictureBox.Width)
            {
                shipPosition.X += shipSpeed;
                if (shipPosition.X > rightBound - playerShipPictureBox.Width)
                    shipPosition.X = rightBound - playerShipPictureBox.Width;
            }
            if (upArrowKeyPressed)
            {
                shipPosition.Y -= shipSpeed;
                if (shipPosition.Y < 0)
                    shipPosition.Y = 0;
            }
            if (downArrowKeyPressed)
            {
                shipPosition.Y += shipSpeed;
                if (shipPosition.Y > this.ClientSize.Height - playerShipPictureBox.Height)
                    shipPosition.Y = this.ClientSize.Height - playerShipPictureBox.Height;
            }
            if (spaceBarPressed)
            {
                FireBullet();
            }

            playerShipPictureBox.Location = shipPosition;
        }

        private void FireBullet()
        {
            Rectangle bulletPosition = new Rectangle(shipPosition.X + playerShipPictureBox.Width / 2 - 5, shipPosition.Y, 10, 20);
            bullets.Add(new Bullet(bulletPosition, 10, bulletImage));
            spaceBarPressed = false;
        }

        private void Space_wars_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                leftArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                rightArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                upArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                downArrowKeyPressed = true;
            else if (e.KeyCode == Keys.Space)
                spaceBarPressed = true;
        }

        private void Space_wars_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                leftArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                rightArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                upArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                downArrowKeyPressed = false;
            else if (e.KeyCode == Keys.Space)
                spaceBarPressed = false;
        }

        private void CheckCollisions()
        {
            Rectangle playerShipBounds = new Rectangle(shipPosition, playerShipPictureBox.Size);

            for (int i = enemyShips.Count - 1; i >= 0; i--)
            {
                var enemyShip = enemyShips[i];
                Rectangle enemyShipBounds = enemyShip.Position;

                // Check collision with player ship
                if (playerShipBounds.IntersectsWith(enemyShipBounds))
                {
                    ShowBoomAtCollisionLocation(enemyShip.Position.Location);
                    EndGame();
                    return;
                }

                // Check collision with bullets
                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    var bullet = bullets[j];
                    if (bullet.Position.IntersectsWith(enemyShipBounds))
                    {
                        enemyShips.RemoveAt(i);
                        bullets.RemoveAt(j);
                        break;
                    }
                }
            }
        }
        private void ShowBoomAtCollisionLocation(Point collisionLocation)
        {
            PictureBox boomPictureBox = new PictureBox
            {
                Image = Properties.Resources.explosion,
                Size = new Size(300, 100),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(collisionLocation.X - 130, collisionLocation.Y)
            };

            this.Controls.Add(boomPictureBox);
            boomPictureBox.BringToFront();

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
            gameTimer.Stop();
            spawnTimer.Stop();

            if (int.Parse(Properties.Settings.Default.spaceWarsScore) < score)
            {
                Properties.Settings.Default.spaceWarsScore = score.ToString();
                Properties.Settings.Default.Save();
                form1.UpdateScores();
            }

            MessageBox.Show("Game Over! You crashed!");
        }
    }

    public class EnemyShip
    {
        public Rectangle Position { get; set; }
        public Image Image { get; set; }

        public EnemyShip(Rectangle position, Image image)
        {
            Position = position;
            Image = image;
        }
    }

    public class Bullet
    {
        public Rectangle Position { get; set; }
        public int Speed { get; set; }
        public Image Image { get; set; }

        public Bullet(Rectangle position, int speed, Image image)
        {
            Position = position;
            Speed = speed;
            Image = image;
        }
    }
}
