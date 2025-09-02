namespace TodoListApp.Domain.Entities;

public class TodoTask
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    // public PriorityLevel Priority {get; private set;}
    
    // public enum PriorityLevel {
    //     low= 0 , normal= 1 , high =2, urgent=3
    // }

    public TodoTask(string title ) //, PriorityLevel priority)
    {
        Title = title;
        // Priority = priority;
        IsCompleted = false;
    }

    public void MarkCompleted() => IsCompleted = true;


}
