using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Models;

internal class ToDoTask
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [DefaultValue(false)]
    public bool IsCompleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}