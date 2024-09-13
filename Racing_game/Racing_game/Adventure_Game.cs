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
    public partial class Adventure_Game : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer enemySpawnTimer;
       // private Timer gameTimer;
        private Player player;
        private List<Enemy> enemies;
        private Inventory inventory;
        private Viewport viewport;
        private Dictionary<(int, int), MapChunk> mapChunks;
        private int chunkSize = 10;
        private int tileSize = 32;
        private Random random = new Random();

        private Image grassImage = Properties.Resources.tree1;
        private Image dirtImage = Properties.Resources.soil;
        private Image stoneImage = Properties.Resources.stone;
        private Image playerImage = Properties.Resources.player;
        private Image enemyImage = Properties.Resources.enemy;

        public Adventure_Game()
        {
            InitializeComponent();


            InitializeComponent();

            // Initialize game components
            player = new Player { X = 64, Y = 64 };
            player.Image = playerImage;
            enemies = new List<Enemy>();
            
            inventory = new Inventory();
            viewport = new Viewport(this.ClientSize.Width, this.ClientSize.Height);
            mapChunks = new Dictionary<(int, int), MapChunk>();

            // Setup the game loop timer
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 32; // ~60 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Setup the enemy spawn timer
            enemySpawnTimer = new System.Windows.Forms.Timer();
            enemySpawnTimer.Interval = random.Next(3000, 7000); // Random spawn interval between 3-7 seconds
            enemySpawnTimer.Tick += EnemySpawnTimer_Tick;
            enemySpawnTimer.Start();

            // Handle keyboard input
            this.KeyDown += Form1_KeyDown;

            // Generate initial chunks around the player's starting position
            GenerateChunksAroundPlayer();
            this.DoubleBuffered = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            Invalidate(); // Triggers a repaint
        }

        private void EnemySpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnEnemy();
            enemySpawnTimer.Interval = random.Next(3000, 7000); // Reset timer with new random interval
        }

        private void UpdateGame()
        {
            foreach (var enemy in enemies)
            {
                enemy.Chase(player, GetTileAt, tileSize);
                if (enemy.IsCollidingWith(player))
                {
                    // Game Over
                    gameTimer.Stop();
                    enemySpawnTimer.Stop();
                    MessageBox.Show("Game Over! The enemy caught you!");
                    return;
                }
            }

            viewport.CenterOn(player);

            // Generate new chunks if the player moves to a new chunk
            GenerateChunksAroundPlayer();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RenderGame(e.Graphics);
        }

        private void RenderGame(Graphics g)
        {
            int startX = Math.Max(0, viewport.X / tileSize);
            int startY = Math.Max(0, viewport.Y / tileSize);
            int endX = (viewport.X + viewport.Width) / tileSize + 1;
            int endY = (viewport.Y + viewport.Height) / tileSize + 1;

            for (int y = startY; y < endY; y++)
            {
                startX = Math.Max(0, viewport.X / tileSize);
                startY = Math.Max(0, viewport.Y / tileSize);
                endX = (viewport.X + viewport.Width) / tileSize + 1;
                endY = (viewport.Y + viewport.Height) / tileSize + 1;

                for (y = startY; y < endY; y++)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        var chunkCoords = (x / chunkSize, y / chunkSize);
                        if (!mapChunks.TryGetValue(chunkCoords, out MapChunk chunk))
                        {
                            continue;
                        }

                        int localX = x % chunkSize;
                        int localY = y % chunkSize;
                        int tile = chunk.Tiles[localY, localX];

                        Image image = null;
                        switch (tile)
                        {
                            case 0: image = grassImage; break; // Grass
                            case 1: image = dirtImage; break; // Dirt
                            case 2: image = stoneImage; break; // Stone
                        }

                        if (image != null)
                        {
                            g.DrawImage(image, (x * tileSize) - viewport.X, (y * tileSize) - viewport.Y, tileSize, tileSize);
                        }
                    }
                }

                player.Draw(g, viewport);

                foreach (var enemy in enemies)
                {
                    enemy.Draw(g, viewport);
                }

                inventory.Draw(g);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int dx = 0, dy = 0;
            switch (e.KeyCode)
            {
                case Keys.W: dy = -1; break;
                case Keys.S: dy = 1; break;
                case Keys.A: dx = -1; break;
                case Keys.D: dx = 1; break;
            }

            if (dx != 0 || dy != 0)
            {
                player.TryMove(dx, dy, GetTileAt, tileSize, chunkSize, mapChunks);
            }
        }

        private void GenerateChunksAroundPlayer()
        {
            int playerChunkX = player.X / (chunkSize * tileSize);
            int playerChunkY = player.Y / (chunkSize * tileSize);

            for (int y = playerChunkY - 1; y <= playerChunkY + 1; y++)
            {
                for (int x = playerChunkX - 1; x <= playerChunkX + 1; x++)
                {
                    if (!mapChunks.ContainsKey((x, y)))
                    {
                        mapChunks[(x, y)] = new MapChunk(chunkSize);
                    }
                }
            }
        }

        private int GetTileAt(int x, int y)
        {
            int chunkX = x / chunkSize;
            int chunkY = y / chunkSize;
            int localX = x % chunkSize;
            int localY = y % chunkSize;

            if (mapChunks.TryGetValue((chunkX, chunkY), out MapChunk chunk))
            {
                return chunk.Tiles[localY, localX];
            }
            return -1; // Invalid tile
        }

        private void SpawnEnemy()
        {
            while (true)
            {
                int x = random.Next(0, chunkSize * tileSize);
                int y = random.Next(0, chunkSize * tileSize);
                int tileX = x / tileSize;
                int tileY = y / tileSize;

                if (GetTileAt(tileX, tileY) != 0) // Ensure not spawning on grass
                {
                    enemies.Add(new Enemy { X = x, Y = y, Image = enemyImage });

                    break;
                }
            }
        }
    }

    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; } = 32;
        public Image Image { get; set; }

        public void TryMove(int dx, int dy, Func<int, int, int> getTileAt, int tileSize, int chunkSize, Dictionary<(int, int), MapChunk> mapChunks)
        {
            int newX = X + dx * tileSize;
            int newY = Y + dy * tileSize;

            int tileX = newX / tileSize;
            int tileY = newY / tileSize;
            int currentTile = getTileAt(tileX, tileY);

            if (currentTile == 0) // Grass
            {
                // Prevent movement
                return;
            }

            if (currentTile == 1) // Dirt
            {
                // Move slower
                X += dx * (tileSize / 2);
                Y += dy * (tileSize / 2);
            }
            else // Other tiles (e.g., Stone)
            {
                X = newX;
                Y = newY;
            }
        }

        public void Draw(Graphics g, Viewport viewport)
        {
            g.DrawImage(Image, X - viewport.X, Y - viewport.Y, Size, Size);
        }
    }


    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; } = 32;
        public Image Image { get; set; }

        public void Draw(Graphics g, Viewport viewport)
        {   
            g.DrawImage(Image, X - viewport.X, Y - viewport.Y, Size, Size);
        }

        public bool IsCollidingWith(Player player)
        {
            return (player.X < X + Size && player.X + player.Size > X &&
                    player.Y < Y + Size && player.Y + player.Size > Y);
        }

        public void Chase(Player player, Func<int, int, int> getTileAt, int tileSize)
        {
            int dx = Math.Sign(player.X - X);
            int dy = Math.Sign(player.Y - Y);

            int newX = X + dx * tileSize / 4; // Move towards the player
            int newY = Y + dy * tileSize / 4; // Move towards the player

            int tileX = newX / tileSize;
            int tileY = newY / tileSize;
            int currentTile = getTileAt(tileX, tileY);

            if (currentTile != 0) // Avoid grass
            {
                X = newX;
                Y = newY;
            }
        }
    }
    public class Inventory
    {
        private List<string> items;

        public Inventory()
        {
            items = new List<string>();
        }

        public void AddItem(string item)
        {
            items.Add(item);
        }

        public void Draw(Graphics g)
        {
            int x = 10;
            int y = 10;
            foreach (var item in items)
            {
                g.DrawString(item, SystemFonts.DefaultFont, Brushes.White, x, y);
                y += 20;
            }
        }
    }

    public class Viewport
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Viewport(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void CenterOn(Player player)
        {
            X = player.X - Width / 2 + player.Size / 2;
            Y = player.Y - Height / 2 + player.Size / 2;
        }
    }

    public class MapChunk
    {
        public int[,] Tiles { get; private set; }
        public int ChunkSize { get; private set; }

        public MapChunk(int chunkSize)
        {
            ChunkSize = chunkSize;
            Tiles = new int[chunkSize, chunkSize];
            GenerateChunk();
        }

        private void GenerateChunk()
        {
            Random random = new Random();
            for (int y = 0; y < ChunkSize; y++)
            {
                for (int x = 0; x < ChunkSize; x++)
                {
                    // Randomly generate tile types (0: Grass, 1: Dirt, 2: Stone)
                    Tiles[y, x] = random.Next(3);
                }
            }
        }
    }
}