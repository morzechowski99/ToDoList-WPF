using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal class ToDoListDbContext : DbContext
{
    public ToDoListDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(x => Debug.WriteLine(x));
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        base.OnModelCreating(modelBuilder);
    }
}