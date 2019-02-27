namespace LabWork1Back
{
    public enum MessageType {News, Question, Answer, Invite, Comment };

    public struct Message
    {
        System.DateTime timeStamp { get; }
        MessageType messageType { get; set; }
        string senderID { get; }
        string receiverID { get; }
        string content { get; set; }
        ushort spamScore { get; set; }
    }
}