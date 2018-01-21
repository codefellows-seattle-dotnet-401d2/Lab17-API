using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;
using WebAppAPI.Controllers;
using WebAppAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace XUnitTestAPI
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
                List<Tasks> testTasks = GetTestTasks();
                foreach(Tasks x in testTasks)
                {
                    _context.AllTasks.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                IEnumerable<Tasks> result = controller.Get();
                List<Tasks> resultList = result.ToList();
                // Assert
                Assert.Equal(testTasks.Count, resultList.Count);
            }
        }

        private List<Tasks> GetTestTasks()
        {
            var testTasks = new List<Tasks>();
            testTasks.Add(new Tasks { Id = 1, Title = "Task 1", Notes = "1 is peaceful" });
            testTasks.Add(new Tasks { Id = 2, Title = "Task 2", Notes = "2 is a dynamic duo" });
            testTasks.Add(new Tasks { Id = 3, Title = "Task 3", Notes = "3's a crowd" });
            testTasks.Add(new Tasks { Id = 4, Title = "Task 4", Notes = "4 is a party" });

            return testTasks;
        }
    }
}
