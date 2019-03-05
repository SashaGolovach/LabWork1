using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork1Back
{
    public interface IDBApiController
    {
        void saveChanges();

        IEnumerable<Message> getAllMessages();

        IEnumerable<Message> getMessagges(string pattern);

        IEnumerable<Message> getMessagges(long userId, DateTime timestamp);

        IEnumerable<Message> getMessages(long userFromID, long userToID, MessageTypeEnum type);

        void addMessage(Message message);
        void LoadData();
    }
}
