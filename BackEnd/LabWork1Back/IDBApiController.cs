using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork1Back
{
    interface IDBApiController
    {
        void saveChanges();

        List<Message> getAllMessages();

        List<Message> getMessaggesEndsWith(string pattern);

        List<Message> getMessagges(string userId, DateTime timestamp);

        List<Message> getMessages(string userFromID, string userToID, MessageType type);

        
    }
}
