using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Models;
using WebAppAPI.Data;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>/
        [HttpGet]
        public IEnumerable<Tasks> Get()
        {
            return _context.AllTasks;
        }

        // GET: api/<controller>/{id}
        [HttpGet("{id:int}")]
        public Tasks Get(int id)
        {
            return _context.AllTasks.FirstOrDefault(x => x.Id == id);
        }

        // POST: api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tasks value)
        {
            value.Time = DateTime.Now;
            await _context.AllTasks.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { value.Id }, value);
        }

        // PUT: api/<controller>/{id}
        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody]Tasks value)
        {
            value.Id = id;
            value.Time = DateTime.Now;
            _context.AllTasks.Update(value);
            _context.SaveChanges();
        }

        // DELETE: api/<controller>/{id}
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            Tasks deleteTask = _context.AllTasks.FirstOrDefault(x => x.Id == id);
            _context.AllTasks.Remove(deleteTask);
            _context.SaveChanges();
        }
    }
}
