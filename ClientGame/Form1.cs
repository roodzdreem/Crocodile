using ClientGame.ChatClient;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace ClientGame
{
    public partial class gameWindow : Form, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        readonly Label upLabel = new Label();
        readonly Label downLabel = new Label();
        Color penColor;
        string word;
        bool inMenu = true;
        int ID;
        int artistID;
        bool isMouseDown = false;
        bool haveArtist;
        Graphics gr;
        Point mouseCords;
        Point tempMouseCords;
        public gameWindow()
        {
            InitializeComponent();
        }

        private void ConnectPlayer()
        {
            
            if (!isConnected)
            {
                CreateClient();
                haveArtist = FindArtist();
                ID = client.Connect(nameBox.Text, haveArtist);

                if (!haveArtist)
                {
                    haveArtist = true;
                }
                artistID = client.GetArtistID();
                word = client.GetWord();
                nameBox.Enabled = false;
                isConnected = true;

            }
        }
        private void DisconnectPlayer()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                nameBox.Enabled = true;
                isConnected = false;
                gr.Clear(DefaultBackColor);
                wordLabel.Text = "";
                wordLabel.Visible = false;
            }
        }
       

        private void ClearBox(object sender, MouseEventArgs e)
        {
            nameBox.Text = "";
        }
        public void CreateClient()
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }
        private void CreateButton(Button button, int x, int y, string text)
        {
            button.BackColor = Color.White;
            button.AutoSize = true;
            button.Text = text;
            button.Font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point);
            button.Location = new Point(x - button.PreferredSize.Width / 2, y);

        }
        private void CreateLabel(Label tempLabel, int x, int y, string text)
        {
            tempLabel.AutoSize = true;
            tempLabel.Text = text;
            tempLabel.Font = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Point);
            tempLabel.Location = new Point(x - tempLabel.PreferredWidth / 2, y);
        }


        public void MsgCallback(string msg)
        {
            chatBox.Items.Add(msg);
            msgBox.Clear();
        }
        public void PaintingCallback(Point cords, Point tempCords, Color color)
        {
            Pen pen = new Pen(color);
            if (!inMenu)
                gr.DrawLine(pen, tempCords, cords);
        }
        public void ClearWindowCallback()
        {
            gr.Clear(DefaultBackColor);
        }

        private void CloseWindow(object sender, FormClosedEventArgs e)
        {
            DisconnectPlayer();
        }
        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(msgBox.Text, ID);
                }
                msgBox.Text = "";
            }
        }
        private void ClearMsgBox(object sender, MouseEventArgs e)
        {
            msgBox.Text = "";
        }

        


        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(chatBox.Items[e.Index].ToString(), chatBox.Font, chatBox.Width).Height;
        }
        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (chatBox.Items.Count > 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(chatBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }



        private void gameWindow_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown=false;
        }
        private void gameWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && isConnected)
            {
                mouseCords = e.Location;
                if (IsArtist() && mouseCords.X > 10 && mouseCords.Y > 10 && mouseCords.X < Constants.RIGHTMENUPANELSX - 10 && mouseCords.Y < msgBox.Location.Y - 10)
                {
                    client.DrawLine(mouseCords, tempMouseCords, penColor);
                    tempMouseCords = mouseCords;
                }
            }
        }
        private void gameWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (isConnected)
            {
                artistID = client.GetArtistID();
                GetGameWord();
                tempMouseCords = e.Location;
                mouseCords = tempMouseCords;
                isMouseDown = true;
            }
        }



        private void gameWindow_Load(object sender, EventArgs e)
        {
            ShowStartMenu();
        }
        private void gameWindow_Paint(object sender, PaintEventArgs e)
        {
            gr = CreateGraphics();
        }


        

        private void ColorButtons0Click(object sender, EventArgs e)
        {
            penColor = Color.Black;
        }
        private void ColorButtons1Click(object sender, EventArgs e)
        {
            penColor = Color.Red;
        }
        private void ColorButtons2Click(object sender, EventArgs e)
        {
            penColor = Color.Blue;
        }
        private void ColorButtons3Click(object sender, EventArgs e)
        {
            penColor = Color.Yellow;
        }
        private void ColorButtons4Click(object sender, EventArgs e)
        {
            penColor = Color.Green;
        }
        public void ColorButtons5Click(object sender, EventArgs e)
        {
            client.ClearWindow();
        }

        private void ShowWordLabel()
        {
            wordLabel.Visible = true;
            wordLabel.Text = word;
            Controls.Add(wordLabel);

        }
        private void ShowGameInterface()
        {
            Controls.Clear();
            inMenu = false;
            chatBox.Items.Clear();
            chatBox.Height = Constants.WINDOWHEIGHT - 100;
            nameBox.Location = new Point(Constants.RIGHTMENUPANELSX, Constants.FIRSTRIGHTMENUPANELSY);
            chatBox.Location = new Point(Constants.RIGHTMENUPANELSX, Constants.FIRSTRIGHTMENUPANELSY + Constants.RIGHTMENUPANELSYRANGE + nameBox.Height);
            ConnectButton.Location = new Point(Constants.RIGHTMENUPANELSX, chatBox.Location.Y + chatBox.Height + Constants.RIGHTMENUPANELSYRANGE);
            msgBox.Width = Constants.RIGHTMENUPANELSX - 20;
            msgBox.Location = new Point(10, ConnectButton.Location.Y + ConnectButton.Height - 70);
            msgBox.Multiline = true;
            msgBox.Height = 70;
            if (IsArtist())
            {
                ClientSize = new Size(Constants.WINDOWWIDTH + Constants.ARTISTPANELSWIDTH, Constants.WINDOWHEIGHT);
                ShowArtistPanels();
                ShowWordLabel();
                msgBox.Enabled = false;

            }
            else
            {
                ClientSize = new Size(Constants.WINDOWWIDTH, Constants.WINDOWHEIGHT);
                msgBox.Enabled = true;
                wordLabel.Visible = false;
            }
            Controls.Add(chatBox);
            Controls.Add(nameBox);
            Controls.Add(msgBox);
            Controls.Add(ConnectButton);
        }
        private void ShowArtistPanels()
        {
            Button[] colorButtons = new Button[Constants.BUTTONSAMOUNT];
            for (int i = 0; i < Constants.BUTTONSAMOUNT; i++)
            {
                colorButtons[i] = new Button();
                colorButtons[i].ClientSize = new Size(Constants.BUTTONSIZE, Constants.BUTTONSIZE);
                colorButtons[i].Location = new Point(Constants.WINDOWWIDTH, Constants.FIRSTRIGHTMENUPANELSY + i * (Constants.RIGHTMENUPANELSYRANGE + Constants.BUTTONSIZE));
                switch (i)
                {
                    case 0:
                        colorButtons[i].BackColor = Color.Black;
                        break;
                    case 1:
                        colorButtons[i].BackColor = Color.Red;
                        break;
                    case 2:
                        colorButtons[i].BackColor = Color.Blue;
                        break;
                    case 3:
                        colorButtons[i].BackColor = Color.Yellow;
                        break;
                    case 4:
                        colorButtons[i].BackColor = Color.Green;
                        break;
                    case 5:
                        colorButtons[i].BackColor = Color.White;
                        colorButtons[i].Text = "Очистить";
                        colorButtons[i].Location = new Point(Constants.WINDOWWIDTH - 15, Constants.FIRSTRIGHTMENUPANELSY + i * (Constants.RIGHTMENUPANELSYRANGE + Constants.BUTTONSIZE));
                        colorButtons[i].ClientSize = new Size(65, Constants.BUTTONSIZE);
                        break;
                }
                Controls.Add(colorButtons[i]);
            }
            colorButtons[0].Click += ColorButtons0Click;
            colorButtons[1].Click += ColorButtons1Click;
            colorButtons[2].Click += ColorButtons2Click;
            colorButtons[3].Click += ColorButtons3Click;
            colorButtons[4].Click += ColorButtons4Click;
            colorButtons[5].Click += ColorButtons5Click;
        }
        public void ShowWinMenu(string name, string word) 
        {
            ClearForm();
            inMenu = true;
            gr.Clear(DefaultBackColor);
            CreateLabel(downLabel, Constants.WINDOWWIDTH / 2, 150, $"Загаданное слово: {word}") ;
            CreateLabel(upLabel, Constants.WINDOWWIDTH / 2, 100, $"Победил игрок: {name}");


            Button restartButton = new Button();
            CreateButton(restartButton, Constants.WINDOWWIDTH / 2,200, "Перезапуск");
            restartButton.Click += RestartButtonClick;

            Button exitButton = new Button();
            CreateButton(exitButton, Constants.WINDOWWIDTH / 2 , 250, "Выход");
            exitButton.Click += ExitButtonClick;

            Controls.Add(upLabel);
            Controls.Add(downLabel);
            Controls.Add(restartButton);
            Controls.Add(exitButton);
        }
        private void ShowStartMenu()
        {

            ClearForm();
            inMenu = true;
            CreateLabel(upLabel, Constants.WINDOWWIDTH / 2, 100, "Крокодил");



            Button startButton = new Button();
            startButton.BackColor = Color.White;
            startButton.AutoSize = true;
            startButton.Text = "Подключиться";
            startButton.Font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point);
            startButton.Location = new Point(Constants.WINDOWWIDTH / 2 - startButton.PreferredSize.Width / 2, 200);
            startButton.Click += StartButtonClick;

            nameBox.Location = new Point(Constants.WINDOWWIDTH / 2 - nameBox.Width / 2, 150);
            nameBox.BackColor = Color.White;
            nameBox.Multiline = true;
            nameBox.Font = new Font("Arial", 13, FontStyle.Bold, GraphicsUnit.Point);
            nameBox.Height = upLabel.PreferredSize.Height;


            Controls.Add(upLabel);
            Controls.Add(nameBox);

            Controls.Add(startButton);
        }


        

        private void ClearForm()
        {
            BackColor = DefaultBackColor;
            ClientSize = new Size(Constants.WINDOWWIDTH, Constants.WINDOWHEIGHT);
            Controls.Clear();
        }

    
        private void ExitButtonClick(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectPlayer();
                ShowStartMenu();
            }
        }
        
        private void StartButtonClick(object sender, EventArgs e)
        {
            ConnectPlayer();
            penColor = Color.Black;
            ShowGameInterface();
            
        }

        private void RestartButtonClick(object sender, EventArgs e)
        {
            msgBox.Enabled = true;
            
            ConnectPlayer();
            client.StartGame(ID);
            word = client.GetWord();
            artistID = client.GetArtistID();
            ShowGameInterface();
        }

        

        public bool FindArtist()
        {
            return client.GetArtist();
        }

        public void GetGameWord()
        {
            if (isConnected && IsArtist())
                word = client.GetWord();
        }
        private bool IsArtist()
        {
            return (artistID == ID);
        }

        public void Win(string name, string word)
        {
            ShowWinMenu(name,word);
        }

        
    }
}
