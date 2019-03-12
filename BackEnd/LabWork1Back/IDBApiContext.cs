using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork1Back
{
  public interface IDBApiContext
  {
    void SaveChanges();

    void LoadData();

    IEnumerable<Message> GetAllMessages();

    IEnumerable<Message> GetMessages(string pattern);

    IEnumerable<Message> GetMessages(DateTime timestamp);

    IEnumerable<Message> GetMessages(long userFromID, long userToID, MessageTypeEnum type);

    Message AddMessage();

    void DeleteMessage(long ID);

    void EditMessage(Message m);

    bool MessageExist(long id);

  }
}
