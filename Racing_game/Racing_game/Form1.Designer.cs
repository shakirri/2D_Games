namespace Racing_game
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRacingGame = new Button();
            label1 = new Label();
            lblRacingScore = new Label();
            btnSpaceWars = new Button();
            lblSpaceScore = new Label();
            label3 = new Label();
            label2 = new Label();
            label4 = new Label();
            btnAdventure = new Button();
            btnBikeR = new Button();
            SuspendLayout();
            // 
            // btnRacingGame
            // 
            btnRacingGame.BackgroundImage = Properties.Resources.Race_btn;
            btnRacingGame.BackgroundImageLayout = ImageLayout.Stretch;
            btnRacingGame.Font = new Font("Sylfaen", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRacingGame.ForeColor = Color.Salmon;
            btnRacingGame.Location = new Point(12, 12);
            btnRacingGame.Name = "btnRacingGame";
            btnRacingGame.Size = new Size(200, 89);
            btnRacingGame.TabIndex = 0;
            btnRacingGame.Text = "     Racing Game";
            btnRacingGame.UseVisualStyleBackColor = true;
            btnRacingGame.Click += btnRacingGame_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sylfaen", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(218, 31);
            label1.Name = "label1";
            label1.Size = new Size(99, 22);
            label1.TabIndex = 1;
            label1.Text = "High Score";
            // 
            // lblRacingScore
            // 
            lblRacingScore.AutoSize = true;
            lblRacingScore.BackColor = Color.Transparent;
            lblRacingScore.Font = new Font("Sylfaen", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRacingScore.Location = new Point(218, 64);
            lblRacingScore.Name = "lblRacingScore";
            lblRacingScore.Size = new Size(20, 22);
            lblRacingScore.TabIndex = 2;
            lblRacingScore.Text = "0";
            // 
            // btnSpaceWars
            // 
            btnSpaceWars.BackgroundImage = Properties.Resources.space;
            btnSpaceWars.BackgroundImageLayout = ImageLayout.Center;
            btnSpaceWars.Font = new Font("Sylfaen", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSpaceWars.ForeColor = Color.Salmon;
            btnSpaceWars.Location = new Point(12, 107);
            btnSpaceWars.Name = "btnSpaceWars";
            btnSpaceWars.Size = new Size(200, 89);
            btnSpaceWars.TabIndex = 3;
            btnSpaceWars.Text = "     Space Wars";
            btnSpaceWars.UseVisualStyleBackColor = true;
            btnSpaceWars.Click += btnSpaceWars_Click;
            // 
            // lblSpaceScore
            // 
            lblSpaceScore.AutoSize = true;
            lblSpaceScore.BackColor = Color.Transparent;
            lblSpaceScore.Font = new Font("Sylfaen", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSpaceScore.Location = new Point(218, 152);
            lblSpaceScore.Name = "lblSpaceScore";
            lblSpaceScore.Size = new Size(20, 22);
            lblSpaceScore.TabIndex = 5;
            lblSpaceScore.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Sylfaen", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(218, 119);
            label3.Name = "label3";
            label3.Size = new Size(99, 22);
            label3.TabIndex = 4;
            label3.Text = "High Score";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Snap ITC", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(431, 64);
            label2.Name = "label2";
            label2.Size = new Size(227, 48);
            label2.TabIndex = 6;
            label2.Text = "RI Games";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Showcard Gothic", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.IndianRed;
            label4.Location = new Point(380, 7);
            label4.Name = "label4";
            label4.Size = new Size(193, 46);
            label4.TabIndex = 7;
            label4.Text = "RI Games";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnAdventure
            // 
            btnAdventure.BackgroundImage = Properties.Resources.adventure;
            btnAdventure.BackgroundImageLayout = ImageLayout.Center;
            btnAdventure.Font = new Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, 1, true);
            btnAdventure.ForeColor = Color.Navy;
            btnAdventure.ImageAlign = ContentAlignment.TopLeft;
            btnAdventure.Location = new Point(12, 202);
            btnAdventure.Name = "btnAdventure";
            btnAdventure.Size = new Size(200, 89);
            btnAdventure.TabIndex = 8;
            btnAdventure.Text = "     Explore";
            btnAdventure.UseVisualStyleBackColor = true;
            btnAdventure.Click += btnAdventure_Click;
            // 
            // btnBikeR
            // 
            btnBikeR.BackgroundImage = Properties.Resources.btnbike;
            btnBikeR.BackgroundImageLayout = ImageLayout.Zoom;
            btnBikeR.Font = new Font("Sylfaen", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBikeR.ForeColor = Color.Salmon;
            btnBikeR.Location = new Point(12, 297);
            btnBikeR.Name = "btnBikeR";
            btnBikeR.Size = new Size(200, 89);
            btnBikeR.TabIndex = 9;
            btnBikeR.Text = "     Bike Rush";
            btnBikeR.UseVisualStyleBackColor = true;
            btnBikeR.Click += btnBikeR_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.menu;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(666, 434);
            Controls.Add(btnBikeR);
            Controls.Add(btnAdventure);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(lblSpaceScore);
            Controls.Add(label3);
            Controls.Add(btnSpaceWars);
            Controls.Add(lblRacingScore);
            Controls.Add(label1);
            Controls.Add(btnRacingGame);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRacingGame;
        private Label label1;
        private Label lblRacingScore;
        private Button btnSpaceWars;
        private Label lblSpaceScore;
        private Label label3;
        private Label label2;
        private Label label4;
        private Button btnAdventure;
        private Button btnBikeR;
    }
}
