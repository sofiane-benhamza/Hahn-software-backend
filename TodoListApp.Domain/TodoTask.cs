namespace TodoListApp.Domain.Entities;

public class TodoTask
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public int PriorityLevel {get; private set;} // Added fluentValidation to make sure this doesn't exceed [0, 3]
    
    public TodoTask(string title , int priorityLevel)
    {
        Title = title;
        PriorityLevel = priorityLevel;
        IsCompleted = false;
    }

    public void MarkCompleted() => IsCompleted = true;


}


    public enum Priority { 
        low= 0 , normal= 1 , high =2, urgent=3
    }