namespace ToDoList.ViewModels;

internal class TasksListItem
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public bool IsCompleted { get; init; }
}