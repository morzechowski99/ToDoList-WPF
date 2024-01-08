using ToDoList.DAL.Dto;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal static class Mapping
{
    internal static ToDoTask AsTodoTask(this CreateUpdateTaskDto createUpdateTaskDto) =>
        new()
        {
            Id = createUpdateTaskDto.Id,
            Name = createUpdateTaskDto.Name,
            IsCompleted = createUpdateTaskDto.IsCompleted
        };

    internal static ToDoTaskDto AsTodoTaskDto(this ToDoTask toDoTask) =>
        new()
        {
            Id = toDoTask.Id,
            Name = toDoTask.Name,
            IsCompleted = toDoTask.IsCompleted
        };
}