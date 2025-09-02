using Microsoft.AspNetCore.Mvc;
using TodoListApp.Application.Services;

namespace TodoListApp.API.Controllers;

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
    public IActionResult GetAll() => Ok(_todoService.GetAll());

    [HttpPost]
    public IActionResult Add([FromBody] string title)
    {
        var task = _todoService.Add(title);
        return Ok(task);
    }

    [HttpPost("{id}/complete")]
    public IActionResult Complete(Guid id)
    {
        _todoService.Complete(id);
        return Ok(id);
    }

    [HttpPost("{id}/delete")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _todoService.Delete(id);
        if (deleted == null) return NotFound();
        return Ok(null);
    }
}
