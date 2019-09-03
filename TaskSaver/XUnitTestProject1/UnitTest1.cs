using Microsoft.EntityFrameworkCore;
using System;
using TaskSaver.Controllers;
using TaskSaver.Data;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]

        public void GetRetunsString()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseInMemoryDatabase(databaseName: "ReturnStringTest")
            .Options;

            //Arrange
            using (var context = new TaskDbContext(options))
            {
                var controller = new TaskController(context);

                //Act
                var result = controller.Get(2);

                //Assert
                Assert.IsType<string>(result);
            }



        }
    }
}
