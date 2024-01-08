namespace ToDoList.DAL.Dto;

public class ToDoTaskDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
}