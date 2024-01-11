using ToDoList.DAL.Dto;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal static class Mapping
{
    internal static ToDoTask AsTodoTask(this CreateUpdateTaskDto createUpdateTaskDto) =>
        new()
        {
            Id = createUpdateTaskDto.Id ?? Guid.NewGuid(),
            Name = createUpdateTaskDto.Name,
            IsCompleted = createUpdateTaskDto.IsCompleted,
            CreatedAt = createUpdateTaskDto.CreatedAt ?? DateTimeOffset.UtcNow
        };

    internal static ToDoTask AsTodoTask(this CreateUpdateTaskDto createUpdateTaskDto, ToDoTask original)
    {
        original.Name = createUpdateTaskDto.Name;
        original.IsCompleted = createUpdateTaskDto.IsCompleted;
        return original;
    }


    internal static ToDoTaskDto AsTodoTaskDto(this ToDoTask toDoTask) =>
        new()
        {
            Id = toDoTask.Id,
            Name = toDoTask.Name,
            IsCompleted = toDoTask.IsCompleted,
            CreatedAt = toDoTask.CreatedAt
        };
}