namespace ToDoList.DAL.Dto;

public class ToDoTaskDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}