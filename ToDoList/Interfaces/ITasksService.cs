using ToDoList.ViewModels;

namespace ToDoList.Interfaces;

public interface ITasksService
{
    Task<TasksListItem?> Add(NewTask task);
    Task<IEnumerable<TasksListItem>> GetAll();
    Task<bool> Delete(int id);
    Task<bool> ToggleDone(int id);
}