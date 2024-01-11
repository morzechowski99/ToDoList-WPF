namespace ToDoList.DAL.Dto;

public class CreateUpdateTaskDto
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
}