using System.Collections.ObjectModel;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Actions.TodoTasksActions;

internal class AddTaskAction : ToDoTasksActionBase, IAction
{
    private readonly NewTask _taskToAdd;
    private Guid? _taskGuid;
    private DateTimeOffset? _taskDate;
    private TasksListItem? _addedTask;

    public AddTaskAction(ITasksService tasksService, ObservableCollection<TasksListItem> frontTasks, NewTask taskToAdd)
        : base(tasksService, frontTasks)
    {
        _taskToAdd = taskToAdd;
    }

    public async Task ExecuteAsync()
    {
        if (_addedTask is not null) throw new InvalidOperationException("Need to undo action before execute");
        var created = await AddTask(_taskToAdd, _taskGuid, _taskDate);
        _addedTask = created ?? throw new InvalidOperationException("Failed to create task");
        _taskGuid = _addedTask.Id;
        _taskDate = _addedTask.CreatedAt;
    }

    public async Task UndoAsync()
    {
        if (_addedTask is null) throw new InvalidOperationException("Need to execute action before undo");
        if (!await RemoveTask(_addedTask))
            throw new InvalidOperationException("Failed to delete task");
        _addedTask = null;
    }
}