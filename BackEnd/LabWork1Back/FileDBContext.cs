using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace LabWork1Back
{
    [Serializable]
    public class LocalDB
    {
        List<Message> data = new List<Message>();
    }

    public class FileDBContext : IDBApiController
    {
        readonly string path;

        LocalDB localDB;

        public FileDBContext(string localDBPath)
        {
            path = localDBPath;

        }

        public List<Message> getAllMessages()
        {
            return localDB.
        }

        public List<Message> getMessages(string userFromID, string userToID, MessageType type)
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessagges(string userId, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessaggesEndsWith(string pattern)
        {
            return null;
        }

        public void saveChanges()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, localDB);

                //implement params values for logger class
                Logger.Write(String.Format("DB on {0} has been serialized", path));
            }
        }
    }
}
