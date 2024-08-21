using ApiTask.Data;
using ApiTask.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{

    private readonly AppDbContext _appDbContext;
    private readonly ILogger<TaskController> _logger;
    
    
    public TaskController(AppDbContext appDbContext, ILogger<TaskController> logger)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    
    
    [HttpGet("/viewAll")]
    public async Task<ActionResult<IEnumerable<TaskList>>> GetTasks()
    {
        _logger.LogInformation("Se ejecuto consulta de vista");
        return await _appDbContext.TaskListss.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskList>> GetTask(string id)
    {
        var task = await _appDbContext.TaskListss.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Se ejecuto consulta de vista de una tarea");
        return task;
    }

    [HttpPost("/newTask")]
    public async Task<ActionResult<TaskList>> postTask(TaskList taskList)
    {
        
        _appDbContext.TaskListss.Add(taskList);
        await _appDbContext.SaveChangesAsync();
        
        _logger.LogInformation("Se creo un nuevo usuario");
        return CreatedAtAction(nameof(GetTasks), new { id = taskList.id_task }, taskList);
    }


    [HttpPut("/updateTast/{id}")]
    public async Task<ActionResult<Dictionary<string, object>>> PostTask(TaskList taskList)
    {
        _appDbContext.TaskListss.Add(taskList);
        await _appDbContext.SaveChangesAsync();

        var response = new Dictionary<string, object>();
        response.Add("mensaje", "nueva tarea creada");

        return CreatedAtAction(nameof(GetTasks), new { id = taskList.id_task }, response);
    }

    [HttpDelete ("/deleteTask/{id}")]
    public async Task<ActionResult<Dictionary<string, object>>> deleteTask(string id)
    {
        var response = new Dictionary<string, object>();
        var task = await _appDbContext.TaskListss.FindAsync(id);

        _appDbContext.TaskListss.Remove(task);
        
        try
        {
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation($"Tarea con ID {id} eliminada exitosamente.");
            response.Add("mensaje", "se elimino tarea");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar la tarea con ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la tarea.");
        }

    }
    
     
    
}
