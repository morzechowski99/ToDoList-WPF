using ToDoList.ViewModels;

namespace ToDoList.Interfaces;

internal interface ITasksService
{
    Task<TasksListItem?> Add(NewTask task);
    Task<IEnumerable<TasksListItem>> GetAll();
}