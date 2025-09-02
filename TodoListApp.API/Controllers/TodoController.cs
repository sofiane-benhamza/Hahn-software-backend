using Microsoft.AspNetCore.Mvc;
using TodoListApp.Application.Services;
using FluentValidation; 

namespace TodoListApp.API.Controllers;


public class createTaskDTO {
    public string title {get; set;}
    public int priority {get; set;}
}

public class CreateTaskDtoValidator : AbstractValidator<createTaskDTO>
{
    public CreateTaskDtoValidator()
    {
        RuleFor(x => x.title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.priority).InclusiveBetween(0, 3);
    }
}


[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todoService.GetAllAsync();
    
        var ordered = todos.OrderByDescending(t => t.PriorityLevel);
    
        return Ok(ordered);
    }


    [HttpPost]
    public IActionResult Add(createTaskDTO gotTask)
    {
        var task = _todoService.Add(gotTask.title, gotTask.priority);
        return Ok(task);
    }

    [HttpPost("{id}/complete")]
    public IActionResult Complete(Guid id)
    {
        _todoService.Complete(id);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _todoService.Delete(id);
        if (deleted == null) return NotFound();
        return Ok(null);
    }
}
