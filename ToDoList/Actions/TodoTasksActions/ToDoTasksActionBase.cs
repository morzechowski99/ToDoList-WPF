using System.Collections.ObjectModel;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Actions.TodoTasksActions;

internal abstract class ToDoTasksActionBase
{
    protected readonly ObservableCollection<TasksListItem> FrontTasks;
    protected readonly ITasksService TasksService;
    protected bool IsExecuted;

    protected ToDoTasksActionBase(ITasksService tasksService, ObservableCollection<TasksListItem> frontTasks)
    {
        TasksService = tasksService;
        FrontTasks = frontTasks;
    }

    protected async Task<TasksListItem?> AddTask(NewTask task, Guid? id, DateTimeOffset? taskDate)
    {
        var created = await TasksService.Add(task, id);
        if (created is null) return null;
        FrontTasks.Add(created);
        FrontTasks.Sort();
        return created;
    }

    protected async Task<bool> RemoveTask(TasksListItem task)
    {
        if (!await TasksService.Delete(task.Id)) return false;
        FrontTasks.Remove(FrontTasks.First(x => x.Id == task.Id));
        return true;
    }
}