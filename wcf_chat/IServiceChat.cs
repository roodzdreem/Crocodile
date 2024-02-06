using System.ServiceModel;
using System.Drawing;

namespace wcf_chat
{
    [ServiceContract(CallbackContract = typeof(IServerCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name, bool haveArtist);
        [OperationContract]
        void StartGame(int id);
        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);
        [OperationContract(IsOneWay = true)]
        void ClearWindow();

        [OperationContract(IsOneWay = true)]
        void DrawLine(Point cords, Point tempCords, Color color);
        [OperationContract]
        bool GetArtist();
        [OperationContract]
        int GetArtistID();
        [OperationContract]
        string GetWord();

    }

    public interface IServerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
        [OperationContract(IsOneWay = true)]
        void PaintingCallback(Point cords, Point tempCords, Color color);
        [OperationContract]
        void GetGameWord();
        [OperationContract]
        bool FindArtist();
        [OperationContract(IsOneWay = true)]
        void Win(string name, string word);
        [OperationContract(IsOneWay = true)]
        void ClearWindowCallback();
        [OperationContract]
        void CreateClient();

    }

}
