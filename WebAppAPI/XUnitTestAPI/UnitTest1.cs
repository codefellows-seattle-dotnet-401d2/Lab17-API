using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;
using WebAppAPI.Controllers;
using WebAppAPI.Data;

namespace XUnitTestAPI
{
    public class UnitTest1
    {
        TaskDbContext _context;

        DbContextOptions<TaskDbContext> options = new DbContextOptionsBuilder<TaskDbContext>()
                                                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                        .Options;
        Tasks myTask = new Tasks()
        {
            Title = "Task Title 1",
            Notes = "My notes for task",
            Time = DateTime.Now
        };

        [Fact]
        public void Test1()
        {

        }
    }
}
