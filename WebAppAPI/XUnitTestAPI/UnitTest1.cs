using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;
using WebAppAPI.Controllers;
using WebAppAPI.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [Fact]
        public void GetSingleTask()
        {
            using (_context = new TaskDbContext(options))
            {
                List<Tasks> testTasks = GetTestTasks();
                foreach (Tasks x in testTasks)
                {
                    _context.AllTasks.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                Tasks result = controller.Get(2);
                // Assert
                Assert.Equal(testTasks[1], result);
            }
        }

        [Fact]
        public void PostTask()
        {
            using (_context = new TaskDbContext(options))
            {
                List<Tasks> testTasks = GetTestTasks();
                foreach (Tasks x in testTasks)
                {
                    _context.AllTasks.Add(x);
                }
                Tasks newTask = new Tasks
                {
                    Id = 5,
                    Title = "Task 5",
                    Notes = "5 is a rave"
                };
                testTasks.Add(newTask);
                _context.SaveChanges();

                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Post(newTask);
                Tasks resultGet = controller.Get(5);
                // Assert
                Assert.NotNull(resultRequest);
                Assert.Equal(testTasks[4], resultGet);
            }
        }

        [Fact]
        public void DeleteTask()
        {
            using (_context = new TaskDbContext(options))
            {
                List<Tasks> testTasks = GetTestTasks();
                foreach (Tasks x in testTasks)
                {
                    _context.AllTasks.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                controller.Delete(2);
                var result = controller.Get(2);
                // Assert
                Assert.Null(result);
            }
        }

        private List<Tasks> GetTestTasks()
        {
            var testTasks = new List<Tasks>
            {
                new Tasks { Id = 1, Title = "Task 1", Notes = "1 is peaceful" },
                new Tasks { Id = 2, Title = "Task 2", Notes = "2 is a dynamic duo" },
                new Tasks { Id = 3, Title = "Task 3", Notes = "3's a crowd" },
                new Tasks { Id = 4, Title = "Task 4", Notes = "4 is a party" }
            };

            return testTasks;
        }
    }
}
