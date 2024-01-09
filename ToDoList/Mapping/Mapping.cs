using ToDoList.DAL.Dto;
using ToDoList.ViewModels;

namespace ToDoList.Mapping;

internal static class Mapping
{
    internal static CreateUpdateTaskDto AsCreateUpdateTaskDto(this NewTask newTask) =>
        new()
        {
            Name = newTask.Name ?? string.Empty,
        };

    internal static CreateUpdateTaskDto AsCreateUpdateTaskDto(this ToDoTaskDto toDoTaskDto) =>
        new()
        {
            Name = toDoTaskDto.Name,
            IsCompleted = toDoTaskDto.IsCompleted
        };

    internal static TasksListItem AsTasksListItem(this ToDoTaskDto toDoTaskDto) =>
        new()
        {
            Id = toDoTaskDto.Id,
            Name = toDoTaskDto.Name,
            IsCompleted = toDoTaskDto.IsCompleted
        };
}