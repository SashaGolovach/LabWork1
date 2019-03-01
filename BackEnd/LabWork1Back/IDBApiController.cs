using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork1Back
{
    interface IDBApiController
    {
        void saveChanges();

        IEnumerable<Message> getAllMessages();

        IEnumerable<Message> getMessagges(string pattern);

        IEnumerable<Message> getMessagges(string userId, DateTime timestamp);

        IEnumerable<Message> getMessages(string userFromID, string userToID, MessageType type);

        
    }
}
