using ToDoList.ViewModels;

namespace ToDoList.Interfaces;

public interface ITasksService
{
    Task<TasksListItem?> Add(NewTask task, Guid? id = null, DateTimeOffset? taskDate = null);
    Task<IEnumerable<TasksListItem>> GetAll();
    Task<bool> Delete(Guid id);
    Task<bool> ToggleDone(Guid id);
}