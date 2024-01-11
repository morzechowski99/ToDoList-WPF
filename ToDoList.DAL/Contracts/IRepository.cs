using ToDoList.DAL.Dto;

namespace ToDoList.DAL.Contracts;

public interface IRepository
{
    Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync();
    Task<ToDoTaskDto?> GetTaskAsync(Guid id);
    Task<ToDoTaskDto> CreateTaskAsync(CreateUpdateTaskDto createTaskDto);
    Task<ToDoTaskDto> UpdateTaskAsync(Guid id, CreateUpdateTaskDto updateTaskDto);
    Task DeleteTaskAsync(Guid id);
}