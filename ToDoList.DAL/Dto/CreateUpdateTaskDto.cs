namespace ToDoList.DAL.Dto;

public class CreateUpdateTaskDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}