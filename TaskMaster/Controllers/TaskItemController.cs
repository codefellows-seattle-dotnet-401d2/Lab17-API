using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    public class TaskItemController : Controller
    {

        private readonly TaskItemsDbContext _context;

        //Constructor requires context in order to instantiate TaskItemController.
        public TaskItemController(TaskItemsDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { taskItem.Id }, taskItem);
        } 

        //Read
        [HttpGet]
        public List<TaskItem> Get()
        {
            return _context.TaskItems.ToList();
        }

        [HttpGet("{id}")]
        public TaskItem Get(int Id)
        {
            return _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == Id);
        }

        //Update
        //public TaskItem Put([FromBody]TaskItem taskItem)
        //{
        //    if(_context.TaskItems.Where(taskitem => taskitem.Id == taskItem.Id).ToList().Count > 0)
        //    {
        //        TaskItem task_item = _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == taskItem.Id);
        //        _context.TaskItems.Update(task_item);
        //        _context.SaveChangesAsync();
        //        return ;
        //    }
        //    else
        //    {

        //    }
        //}


        //Delete
        [HttpDelete("{id}")]
        public CreatedAtActionResult Delete(int Id)
        {
            TaskItem taskItem = _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == Id);
            _context.TaskItems.Remove(taskItem);
            _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { taskItem.Id }, taskItem);
        }
    }
}