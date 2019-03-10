using System;

namespace LabWork1Back
{
    public enum MessageTypeEnum {News, Question, Answer, Invite, Comment };

    [Serializable]
    public struct Message
    {
        public long ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public long SenderID { get; set; }
        public long ReceiverID { get; set; }
        public string Content { get; set; }
        public ushort SpamScore { get; set; }
    }
}