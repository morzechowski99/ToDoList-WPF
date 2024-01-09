using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal class ToDoListDbContext : DbContext
{
    public ToDoListDbContext(DbContextOptions options) : base(options)
    {

    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=toDoList;Trusted_Connection=True;");
    //    base.OnConfiguring(optionsBuilder);
    //}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.LogTo(x => Debug.WriteLine(x));

    public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
}