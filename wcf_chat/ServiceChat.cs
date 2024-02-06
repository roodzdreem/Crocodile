using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;


namespace wcf_chat
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    public class ServiceChat : IServiceChat
    {
        readonly List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        readonly string[] words = { "кот", "собака", "птица", "блогер", "фея" };
        string word;
        bool _haveArtist = false;
        int artistID;

        public int Connect(string name, bool haveArtist)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;
            if (!haveArtist)
            {
                AddArtist( user);
                _haveArtist = true;
            }
            
            users.Add(user);
            return user.ID;
        }

        

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user!=null)
            {
                if (id == artistID)
                {
                    EndGame(id);
                }
                
                    users.Remove(user);
            }
        }

        private void EndGame(int id)
        {
            _haveArtist = false;
            foreach (var item in users)
            {
                if (item.ID != id)
                {
                    item.operationContext.GetCallbackChannel<IServerCallback>().Win("Ведущий вышел из игры", word);
                }
            }
            //users.Remove(user);
        
        }
        public void SendMsg(string msg, int id)
        {
            string answer = "";
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                
                foreach (var item in users)
                {
                    if (msg.ToLower() != word)
                    {
                        answer = DateTime.Now.ToShortTimeString();
                        answer += " | " + user.Name + ": ";
                        answer += msg;
                        item.operationContext.GetCallbackChannel<IServerCallback>().MsgCallback(answer);
                    }
                    else
                    {
                        _haveArtist = false;
                        try
                        {
                            item.operationContext.GetCallbackChannel<IServerCallback>().Win(user.Name, word);   
                        }
                        catch (Exception ex)
                        {
                            item.operationContext.GetCallbackChannel<IServerCallback>().CreateClient();
                            item.operationContext.GetCallbackChannel<IServerCallback>().Win(user.Name, word);
                        }
                    }
                }
            }
        }

        
        public void DrawLine(Point cords, Point tempCords, Color color)
        {
            foreach (var item in users)
            {
                item.operationContext.GetCallbackChannel<IServerCallback>().PaintingCallback(cords,tempCords, color);
            }
        }
        private void AddArtist(ServerUser user)
        {
            artistID = user.ID;
            word = RandomWord(words);
        }
        public bool GetArtist()
        {
            return _haveArtist;
        }
        public int GetArtistID()
        {
            return artistID;
        }

        
        private string RandomWord(string[] words)
        {
            Random rnd = new Random();
            int index= rnd.Next(words.Length);
            return words[index];
        }

        public string GetWord()
        {
            return word;
        }

        public void StartGame(int id)
        {
            if (!_haveArtist)
            {
                artistID = id;
                _haveArtist = true;
                word = RandomWord(words);
            }
            
        }

        public void ClearWindow()
        {
            foreach (var item in users)
            {
                item.operationContext.GetCallbackChannel<IServerCallback>().ClearWindowCallback();
            }
        }
    }
}
