namespace Racing_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            lblRacingScore.Text = Properties.Settings.Default.racingScore;
            lblSpaceScore.Text = Properties.Settings.Default.spaceWarsScore;
        }

        private void btnRacingGame_Click(object sender, EventArgs e)
        {
            RacingGame racingGame = new RacingGame(this);
            racingGame.ShowDialog();
        }

        public void UpdateScores()
        {
            lblRacingScore.Text = Properties.Settings.Default.racingScore;
            lblSpaceScore.Text = Properties.Settings.Default.spaceWarsScore;
        }

        private void btnSpaceWars_Click(object sender, EventArgs e)
        {
            Space_wars space_Wars = new Space_wars(this);
            space_Wars.ShowDialog();
        }

        private void btnAdventure_Click(object sender, EventArgs e)
        {
            Adventure_Game adventure_Game = new Adventure_Game();
            adventure_Game.ShowDialog();
        }

        private void btnBikeR_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.ShowDialog();
        }
    }
}
