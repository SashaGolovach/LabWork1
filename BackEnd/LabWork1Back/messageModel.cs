using System;

namespace LabWork1Back
{
    public enum MessageType {News, Question, Answer, Invite, Comment };

    [Serializable]
    public struct Message
    {
        public System.DateTime timeStamp { get; }
        public MessageType messageType { get; set; }
        public string senderID { get; }
        public string receiverID { get; }
        public string content { get; set; }
        public ushort spamScore { get; set; }
    }
}