using System;
using System.Linq;
using TodoListApp.Application.Services;
using TodoListApp.Domain.Entities;
using TodoListApp.Infrastructure.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace TodoListApp.Tests
{
    public class TodoServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Add_Should_Add_New_Task()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TodoService(context);

            // Act
            var task = service.Add("Test Task", 3);

            // Assert
            Assert.NotNull(task);
            Assert.Equal("Test Task", task.Title);
            Assert.Equal(3, task.PriorityLevel);
            Assert.Single(context.TodoTasks);
        }

        [Fact]
        public async void GetAll_Should_Return_All_Tasks()
        {
            using var context = GetDbContext();
            var service = new TodoService(context);

            service.Add("Task 1", 2);
            service.Add("Task 2", 3);
            service.Add("Task 3", 0);

            var tasks = await service.GetAllAsync();

            Assert.Equal(3, tasks.Count);
        }

        [Fact]
        public void Delete_Should_Remove_Task_When_Exists()
        {
            using var context = GetDbContext();
            var service = new TodoService(context);

            var task = service.Add("Task to Delete", 2);

            var deleted = service.Delete(task.Id);

            Assert.NotNull(deleted);
            Assert.Empty(context.TodoTasks);
        }

        [Fact]
        public void Delete_Should_Return_Null_When_Not_Found()
        {
            using var context = GetDbContext();
            var service = new TodoService(context);

            var result = service.Delete(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public void Complete_Should_Mark_Task_As_Completed()
        {
            using var context = GetDbContext();
            var service = new TodoService(context);

            var task = service.Add("Incomplete Task", 2);

            service.Complete(task.Id);

            var completedTask = context.TodoTasks.First(t => t.Id == task.Id);
            Assert.True(completedTask.IsCompleted);
        }
    }
}
