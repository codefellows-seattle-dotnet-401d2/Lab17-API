using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskSaver.Controllers;
using TaskSaver.Data;
using TaskSaver.Models;
using Xunit;

namespace XUnitTestProject2
{
    public class UnitTest1
    {
        TaskDbContext _context;

        DbContextOptions<TaskDbContext> options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        [Fact]
        public void GetAllTasks()
        {
            using (_context = new TaskDbContext(options))
            {
                List<Task> test = GetTestTasks();
                foreach (Task x in test)
                {
                    _context.TaskList.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                IEnumerable<Task> result = controller.Get();
                List<Task> resultList = result.ToList();
                // Assert
                Assert.Equal(test.Count, resultList.Count);
            }
        }

        public List<Task> GetTestTasks()
        {
            var testTasks = new List<Task>
            {
                new Task { ID = 1, Title = "Task 1", Description = "dscription1" },
                new Task { ID = 2, Title = "Task 2", Description = "description2" },
                new Task { ID = 3, Title = "Task 3", Description = "description3" },
            };

            return testTasks;
        }
    }
}
