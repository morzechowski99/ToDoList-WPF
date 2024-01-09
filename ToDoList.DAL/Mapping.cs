using ToDoList.DAL.Dto;
using ToDoList.DAL.Models;

namespace ToDoList.DAL;

internal static class Mapping
{
    internal static ToDoTask AsTodoTask(this CreateUpdateTaskDto createUpdateTaskDto) =>
        new()
        {
            Name = createUpdateTaskDto.Name,
            IsCompleted = createUpdateTaskDto.IsCompleted
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
            IsCompleted = toDoTask.IsCompleted
        };
}