using TodoListApp.Domain.Entities;
using TodoListApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Application.Services;
    
    public interface ITodoService
    {
        Task<List<TodoTask>> GetAllAsync();
        TodoTask Add(string title, int priority);
        TodoTask Delete(Guid id);
        void Complete(Guid id);

    }

public class TodoService : ITodoService
{
    private readonly ApplicationDbContext _context;

    public TodoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoTask>> GetAllAsync()
    {
        return await _context.TodoTasks
            .AsNoTracking()
            .ToListAsync();
    }

    public TodoTask Add(string title, int priority)
    {
        var task = new TodoTask(title, priority);
        _context.TodoTasks.Add(task);
        _context.SaveChanges();
        return task;
    }

    public TodoTask Delete(Guid id)
    {
        var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return null;

        _context.TodoTasks.Remove(task);
        _context.SaveChanges();
        return task;
    }

    public void Complete(Guid id)
    {
        var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.MarkCompleted();
            _context.SaveChanges();
        }
    }
}
