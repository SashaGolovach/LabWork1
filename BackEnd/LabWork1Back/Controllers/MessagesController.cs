using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

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
    public void DeleteMessage(int id)
    {
      _context.DeleteMessage(id);
      _context.SaveChanges();
    }

    [HttpPost("add/")]
    public void AddMessage(Message m)
    {
      _context.AddMessage(m);
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
