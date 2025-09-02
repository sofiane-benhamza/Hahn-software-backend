using TodoListApp.Domain.Entities;
using TodoListApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Application.Services;
    
    public interface ITodoService
    {
        IEnumerable<TodoTask> GetAll();
        TodoTask Add(string title);
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

    public IEnumerable<TodoTask> GetAll()
        => _context.TodoTasks.AsNoTracking().ToList();

    public TodoTask Add(string title)
    {
        var task = new TodoTask(title);
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
