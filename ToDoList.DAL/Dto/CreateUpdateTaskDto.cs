namespace ToDoList.DAL.Dto;

public class CreateUpdateTaskDto
{
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}