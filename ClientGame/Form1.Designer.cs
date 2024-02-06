namespace ClientGame
{
    partial class gameWindow
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.msgBox = new System.Windows.Forms.TextBox();
            this.wordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ConnectButton.Location = new System.Drawing.Point(520, 416);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(268, 26);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Отключиться";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.chatBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.chatBox.MeasureItem += lst_MeasureItem;
            this.chatBox.DrawItem += lst_DrawItem;
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 16;
            this.chatBox.Location = new System.Drawing.Point(520, 66);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(268, 340);
            this.chatBox.TabIndex = 1;
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.nameBox.Location = new System.Drawing.Point(520, 29);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(268, 22);
            this.nameBox.TabIndex = 2;
            this.nameBox.Text = "Введите имя игрока";
            this.nameBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClearBox);
            // 
            // msgBox
            // 
            this.msgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.msgBox.Location = new System.Drawing.Point(-3, 416);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(517, 22);
            this.msgBox.TabIndex = 3;
            this.msgBox.Text = "Введите текст";
            this.msgBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClearMsgBox);
            this.msgBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msgBox_KeyDown);
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(343, 13);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(70, 16);
            this.wordLabel.TabIndex = 4;
            this.wordLabel.Text = "wordLabel";
            // 
            // gameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.wordLabel);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.ConnectButton);
            this.Name = "gameWindow";
            this.Text = "Крокодил";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseWindow);
            this.Load += new System.EventHandler(this.gameWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gameWindow_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gameWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gameWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gameWindow_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.Label wordLabel;
    }
}

