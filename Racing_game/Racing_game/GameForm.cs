using Racing_game.Properties;
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

    public partial class GameForm : Form
    {
        private PlayerBike playerBike;
        private List<EnemyBike> enemyBikes;
        private Random random;
        private bool leftPressed, rightPressed, attackPressed;
        private Image backgroundImage;
        public GameForm()
        {
            // this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            playerBike = new PlayerBike(this.ClientSize.Width/2 -50, this.ClientSize.Height - 200); // Positioned in the middle at the bottom
            enemyBikes = new List<EnemyBike>();
            random = new Random();
            backgroundImage = Resources.road; // Assuming 'road' is the name of the background image in Resources
            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            Invalidate(); // Trigger Paint event
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: leftPressed = true; break;
                case Keys.D: rightPressed = true; break;
                case Keys.Space: attackPressed = true; break;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: leftPressed = false; break;
                case Keys.D: rightPressed = false; break;
                case Keys.Space: attackPressed = false; break;
            }
        }

        private void UpdateGame()
        {
            if (leftPressed) playerBike.TurnLeft();
            if (rightPressed) playerBike.TurnRight();
            if (attackPressed) playerBike.Attack();
            else playerBike.StopAttack();

            playerBike.Update();
            UpdateEnemyBikes();

            // Check for collisions and handle attacks
            HandleCollisionsAndAttacks();
        }

        private void UpdateEnemyBikes()
        {
            foreach (var enemy in enemyBikes)
            {
                enemy.Update(playerBike);
            }

            // Spawn new enemy bikes at random intervals
            if (random.Next(100) < 1)
            {
                enemyBikes.Add(new EnemyBike(random.Next(this.ClientSize.Width / 2 - 10, this.ClientSize.Width / 2 + 10), this.ClientSize.Height / 2));

            }

            // Remove enemy bikes that have moved past the bottom of the screen
            enemyBikes.RemoveAll(bike => bike.Position.Y > this.ClientSize.Height);
        }

        private void HandleCollisionsAndAttacks()
        {
            foreach (var enemy in enemyBikes)
            {
                if (playerBike.Bounds.IntersectsWith(enemy.Bounds))
                {
                    // Handle collision
                    playerBike.Speed = 0;
                    enemy.Speed = 0;

                    // Handle attack
                    if (playerBike.IsAttacking)
                    {
                        // Enemy takes damage or is destroyed
                        enemy.Health -= 10;
                        if (enemy.Health <= 0)
                        {
                            enemyBikes.Remove(enemy);
                            break;
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Draw background image
            g.DrawImage(backgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);



            // Draw enemy bikes
            foreach (var enemy in enemyBikes)
            {
                float scale = (enemy.Position.Y - (float)this.ClientSize.Height / 2) / (float)this.ClientSize.Height;
                int width = (int)(92 * scale * 2.4);
                int height = (int)(200 * scale * 2.4);
                g.DrawImage(enemy.Image, enemy.Position.X, enemy.Position.Y, width, height);
            }
            // Draw player bike
            g.DrawImage(playerBike.Image, playerBike.Position.X, playerBike.Position.Y, 92, 200);
        }
    }

    public abstract class Bike
    {
        public PointF Position { get; set; }
        public float Speed { get; set; }
        public float Acceleration { get; set; }
        public float MaxSpeed { get; set; }
        public float TurnSpeed { get; set; }
        public bool IsAttacking { get; set; }
        public Bitmap Image { get; set; }
        public RectangleF Bounds => new RectangleF(Position.X, Position.Y, 92, 200);

        public Bike(float x, float y)
        {
            Position = new PointF(x, y);
            Speed = 0;
            Acceleration = 0.1f;
            MaxSpeed = 5.0f;
            TurnSpeed = 0.05f;
            IsAttacking = false;
        }

        public void Update()
        {
            Position = new PointF(Position.X + Speed * (float)Math.Cos(TurnSpeed), Position.Y + Speed * (float)Math.Sin(TurnSpeed));
        }

        public void TurnLeft()
        {
            Position = new PointF(Position.X - 5, Position.Y);
        }

        public void TurnRight()
        {
            Position = new PointF(Position.X + 5, Position.Y);
        }

        public void Attack()
        {
            IsAttacking = true;
        }

        public void StopAttack()
        {
            IsAttacking = false;
        }
    }

    public class PlayerBike : Bike
    {
        public PlayerBike(float x, float y) : base(x, y)
        {
            Image = Resources.bike1;
        }
    }

    public class EnemyBike : Bike
    {
        public int Health { get; set; }

        public EnemyBike(float x, float y) : base(x, y)
        {
            Image = Resources.bike2;
            Health = 100;
        }

        public void Update(PlayerBike playerBike)
        {
            float centerX = playerBike.Position.X + 46; // Center X position of player bike
            float angle = (Position.X - centerX) / 100.0f; // Adjust angle based on X position
            Position = new PointF(Position.X - angle, Position.Y + 2); // Move downward with angle
        }
    }
}
