using System;
using System.Collections.Generic;
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
    FileDBContext _context = new FileDBContext("local.db", DBFileType.BinaryFile);

    [HttpGet]
    public ActionResult<IEnumerable<Message>> GetAllMessages()
    {
      return new ActionResult<IEnumerable<Message>>(_context.GetAllMessages());
    }

    [HttpGet("delete/{id}")]
    public IActionResult DeleteMessage(int id)
    {
      if (!_context.MessageExist(id))
        return NotFound("Message with such ID does not exist");
      _context.DeleteMessage(id);
      _context.SaveChanges();
      return Ok();
    }

    [HttpPost("add/")]
    public void AddMessage(Message m)
    {
      _context.AddMessage(m);
      _context.SaveChanges();
    }
    
    [HttpPost("edit/")]
    public void EditMessage(Message m)
    {
      _context.EditMessage(m);
      _context.SaveChanges();
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
