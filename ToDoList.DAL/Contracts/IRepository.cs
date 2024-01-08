using ToDoList.DAL.Dto;

namespace ToDoList.DAL.Contracts;

public interface IRepository
{
    Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync();
    //Task<ToDoTask> GetTaskAsync(int id);
    Task<ToDoTaskDto> CreateTaskAsync(CreateUpdateTaskDto createTaskDto);
    //Task<ToDoTask> UpdateTaskAsync(int id, CreateUpdateTaskDto updateTaskDto);
    //Task DeleteTaskAsync(int id);
}