using System.ComponentModel.DataAnnotations;

namespace ApiTask.Models;

public class TaskList
{

    [Key] public string id_task { get; set; } = Guid.NewGuid().ToString();
    public string  task_coment { get; set; } 

    
}