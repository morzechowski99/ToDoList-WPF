using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Models;

internal class ToDoTask
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [DefaultValue(false)]
    public bool IsCompleted { get; set; }
}