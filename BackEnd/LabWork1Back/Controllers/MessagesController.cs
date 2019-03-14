using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LabWork1Back.Controllers
{
  [Route("api/messages")]
  [ApiController]
  public class MessagesController : ControllerBase
  {
    IDBApiContext _context = new FileDBContext("local.db", DBFileType.BinaryFile);

    [HttpGet]
    public ActionResult<IEnumerable<Message>> GetAllMessages()
    {
      return new ActionResult<IEnumerable<Message>>(_context.GetAllMessages());
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMessage(int id)
    {
      if (!_context.MessageExist(id))
        return NotFound("Message with such ID does not exist");
      _context.DeleteMessage(id);
      _context.SaveChanges();
      return Ok();
    }

    [HttpGet("add/")]
    public ActionResult<Message> AddMessage()
    {
      var m = _context.AddMessage();
      _context.SaveChanges();
      return m;
    }

    [HttpGet("add/random")]
    public ActionResult<Message> AddRandomMessage()
    {
      var m = _context.AddRandomMessage();
      _context.SaveChanges();
      return m;
    }

    [HttpGet("ends/")]
    public ActionResult<IEnumerable<Message>> GetMessagesEndWith([FromHeader] string pattern)
    {
      if (String.IsNullOrEmpty(pattern))
        return null;

      return new ActionResult<IEnumerable<Message>>(_context.GetMessages(pattern));
    }

    [HttpPost("edit/")]
    public IActionResult EditMessage(Message m)
    {
      _context.EditMessage(m);
      _context.SaveChanges();
      return Ok();
    }

    [HttpGet("after/")]
    public ActionResult<IEnumerable<Message>> getMessagesAfterTimeStamp([FromHeader] DateTime timeStamp)
    {
      if (timeStamp == null)
        return null;

      return new ActionResult<IEnumerable<Message>>(_context.GetMessages(timeStamp));
    }

    [HttpGet("find/")]
    public ActionResult<IEnumerable<Message>> getMessagesWithCertaingType([FromHeader] long fromUserID, [FromHeader] long toUserID, [FromHeader] int messageType)
    {
      return new ActionResult<IEnumerable<Message>>(_context.GetMessages(fromUserID, toUserID, (MessageTypeEnum)messageType));
    }

    [HttpGet("benchmark/")]
    public ActionResult<IEnumerable<BenchmarkResult>> GetBenchmarkResults()
    {
      return new ActionResult<IEnumerable<BenchmarkResult>>(Benchmark.Run());
    }

  }
}
