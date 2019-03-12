using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LabWork1Back
{
  public enum DBFileType
  {
    JsonTextFile,
    BinaryFile
  }

  public class FileDBContext : IDBApiContext
  {

    public readonly string path;
    public readonly DBFileType dbFileType;

    public List<Message> data = new List<Message>();

    public FileDBContext(string localDBPath, DBFileType t)
    {
      path = localDBPath;
      dbFileType = t;
      if (File.Exists(path))
        LoadData();
    }

    public IEnumerable<Message> GetAllMessages()
    {
      return data;
    }

    public IEnumerable<Message> GetMessages(long userFromID, long userToID, MessageTypeEnum type)
    {
      var messages = from m in data
                     where m.SenderID == userFromID && m.ReceiverID == userToID && m.MessageType == type
                     select m;
      return messages;
    }

    public IEnumerable<Message> GetMessages(DateTime timestamp)
    {
      var messages = from m in data
                     where m.TimeStamp > timestamp
                     select m;
      return messages;
    }

    public IEnumerable<Message> GetMessages(string pattern)
    {
      var messagesEndWithPattern = from m in data
                                   where m.Content != null && m.Content.EndsWith(pattern)
                                   select m;
      return messagesEndWithPattern;
    }

    private long GenerateID() {
      var random = new Random();
      long id = random.Next();
      while (MessageExist(id))
        id = random.Next();
      return id;
    }

    public Message AddMessage()
    {
      var message = new Message()
      {
        ID = GenerateID(),
        TimeStamp = DateTime.Now
      };
      data.Insert(0, message);
      return message;
    }

    public void EditMessage(Message m)
    {
      for (int i = 0; i < data.Count; i++)
      {
        if (data[i].ID == m.ID)
        {
          data[i] = m;
          return;
        }
      }
    }

    public bool MessageExist(long id)
    {
      for (int i = 0; i < data.Count; i++)
        if (data[i].ID == id)
          return true;
      return false;
    }

    public void DeleteMessage(long ID)
    {
      data.Remove(data.Single(m => m.ID == ID));
    }

    public void SaveChanges()
    {
      switch (dbFileType)
      {
        case DBFileType.BinaryFile:
          SaveDataToBinaryFile();
          break;
        case DBFileType.JsonTextFile:
          SaveDataToJsonFile();
          break;
      }
    }

    private void SaveDataToBinaryFile()
    {
      BinaryFormatter formatter = new BinaryFormatter();

      using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
      {
        formatter.Serialize(fs, data);
        //TODO: implement params values for logger class
        Logger.Write(String.Format("DB on {0} has been serialized", path));
      }
    }

    private void SaveDataToJsonFile()
    {
      var jsonData = JsonConvert.SerializeObject(data);

      using (StreamWriter sw = new StreamWriter(path))
      {
        sw.Write(jsonData);
      }
    }

    private void LoadDataFromJsonFile()
    {
      string jsonData;
      using (var fs = new StreamReader(path))
      {
        jsonData = fs.ReadToEnd();
      }
      data = JsonConvert.DeserializeObject<List<Message>>(jsonData);
    }

    private void LoadDataFromBinaryFile()
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using (FileStream fs = File.Open(path, FileMode.Open))
      {
        data = (List<Message>)formatter.Deserialize(fs);
      }
    }

    public void LoadData() {
      switch (dbFileType)
      {
        case DBFileType.BinaryFile:
          LoadDataFromBinaryFile();
          break;
        case DBFileType.JsonTextFile:
          LoadDataFromJsonFile();
          break;
      }
    }
  }
}
