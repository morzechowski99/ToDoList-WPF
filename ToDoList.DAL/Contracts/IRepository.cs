using ToDoList.DAL.Dto;

namespace ToDoList.DAL.Contracts;

public interface IRepository
{
    Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync();
    Task<ToDoTaskDto?> GetTaskAsync(int id);
    Task<ToDoTaskDto> CreateTaskAsync(CreateUpdateTaskDto createTaskDto);
    Task<ToDoTaskDto> UpdateTaskAsync(int id, CreateUpdateTaskDto updateTaskDto);
    Task DeleteTaskAsync(int id);
}