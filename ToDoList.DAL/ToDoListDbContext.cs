using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal class ToDoListDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=toDoList;Trusted_Connection=True;");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
}