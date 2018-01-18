using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSaver.Data;
using TaskSaver.Models;
using Microsoft.AspNetCore.Mvc;


namespace TaskSaver.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
             return _context.TaskList;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Models.Task Get(int id)
        {
            var result = _context.TaskList.FirstOrDefault(s => s.ID == id);
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Models.Task value)
        {
            await _context.TaskList.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { value.ID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
