using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace LabWork1Back
{
    public class FileDBController : IDBApiController
    {
        readonly string path;

        public List<Message> data = new List<Message>();

        public FileDBController(string localDBPath)
        {
            path = localDBPath;
            if(File.Exists(path))
                LoadData();
        }

        public IEnumerable<Message> getAllMessages()
        {
            return data;
        }

        public IEnumerable<Message> getMessages(long userFromID, long userToID, MessageTypeEnum type)
        {
            var messagges = from m in data
                where m.SenderID == userFromID && m.ReceiverID == userToID && m.MessageType == type
                select m;
            return messagges;
        }

        public IEnumerable<Message> getMessagges(long userId, DateTime timestamp)
        {
            var messagges = from m in data
                where m.ReceiverID == userId && m.TimeStamp > timestamp
                select m;
            return messagges;
        }

        public IEnumerable<Message> getMessagges(string pattern)
        {
            var messagesEndWithPattern = from m in data
                where m.Content.EndsWith(pattern)
                select m;
            return messagesEndWithPattern;
        }

        public void addMessage(Message message)
        {
            data.Add(message);
        }

        public void saveChanges()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
                //TODO: implement params values for logger class
                Logger.Write(String.Format("DB on {0} has been serialized", path));
            }
            
        }

        public void LoadData() { 
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                data = (List<Message>)formatter.Deserialize(fs);
            }
        }
    }
}
