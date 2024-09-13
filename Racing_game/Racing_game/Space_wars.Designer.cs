namespace Racing_game
{
    partial class Space_wars
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            spawnTimer = new System.Windows.Forms.Timer(components);
            gameTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // spawnTimer
            // 
            spawnTimer.Enabled = true;
            spawnTimer.Tick += spawnTimer_Tick;
            // 
            // gameTimer
            // 
            gameTimer.Tick += gameTimer_Tick;
            // 
            // Space_wars
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(592, 419);
            Name = "Space_wars";
            Text = "Space_wars";
            Paint += Space_wars_Paint;
            KeyDown += Space_wars_KeyDown;
            KeyUp += Space_wars_KeyUp;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer spawnTimer;
        private System.Windows.Forms.Timer gameTimer;
    }
}