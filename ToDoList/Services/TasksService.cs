using ToDoList.DAL.Contracts;
using ToDoList.Interfaces;
using ToDoList.Mapping;
using ToDoList.ViewModels;

namespace ToDoList.Services;

internal class TasksService : ITasksService
{
    private readonly IRepository _repository;

    public TasksService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<TasksListItem?> Add(NewTask task)
    {
        if (task is null)
            throw new ArgumentNullException(nameof(task));
        if (task.Name is null)
            throw new ArgumentException("Name required", nameof(task));

        try
        {
            var createdTask = await _repository.CreateTaskAsync(task.AsCreateUpdateTaskDto());
            return createdTask.AsTasksListItem();
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<TasksListItem>> GetAll()
    {
        return (await _repository.GetTasksAsync())
            .Select(t => t.AsTasksListItem()).ToList();
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            await _repository.DeleteTaskAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ToggleDone(int id)
    {
        try
        {
            var task = await _repository.GetTaskAsync(id);
            if (task is null)
                return false;
            task.IsCompleted = !task.IsCompleted;
            await _repository.UpdateTaskAsync(id, task.AsCreateUpdateTaskDto());
            return true;
        }
        catch
        {
            return false;
        }
    }
}