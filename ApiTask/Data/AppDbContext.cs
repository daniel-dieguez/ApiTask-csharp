using ApiTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Data;

public class AppDbContext : DbContext
{
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }


    public DbSet<TaskList> TaskListss { get; set; } = null!;

}