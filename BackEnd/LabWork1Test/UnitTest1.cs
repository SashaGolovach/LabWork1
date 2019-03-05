using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabWork1Back;

namespace LabWork1Test
{
    [TestClass]
    public class UnitTest1
    {
        private Random random = new Random();
        
        public string RandomString()
        {
            int length = random.Next(64);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        private IDBApiController _repository;
        
        public UnitTest1()
        {
            _repository = new FileDBController("local.db");          
        }
        
        [TestMethod]
        public void fileDbSaveAndLoadData()
        {
            List<Message> data = new List<Message>();
            int N = 10_000;
            for (int i = 0; i < N; i++)
            {
                Message m = new Message()
                {
                    Content = RandomString(),
                    MessageType = (MessageTypeEnum) random.Next(5),
                    SenderID = random.Next(int.MaxValue),
                    ReceiverID = random.Next(int.MaxValue),
                    TimeStamp = DateTime.Now,
                    SpamScore = (ushort) random.Next(100)
                };
                _repository.addMessage(m);
                data.Add(m);
            }
            _repository.saveChanges();
            _repository.LoadData();
            Assert.Equals(_repository.getAllMessages(), data);
        }
    }
}