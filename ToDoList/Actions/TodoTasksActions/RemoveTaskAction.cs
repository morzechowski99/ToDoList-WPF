using System.Collections.ObjectModel;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Actions.TodoTasksActions;

internal class RemoveTaskAction : ToDoTasksActionBase, IAction
{
    private TasksListItem _taskToRemove;

    public RemoveTaskAction(ITasksService tasksService, ObservableCollection<TasksListItem> frontTasks, TasksListItem taskToRemove)
        : base(tasksService, frontTasks)
    {
        _taskToRemove = taskToRemove;
    }

    public async Task ExecuteAsync()
    {
        if (IsExecuted) throw new InvalidOperationException("Need to undo action before execute");
        if (!await RemoveTask(_taskToRemove))
            throw new InvalidOperationException("Failed to delete task");
        IsExecuted = true;
    }

    public async Task UndoAsync()
    {
        if (!IsExecuted) throw new InvalidOperationException("Need to execute action before undo");
        var created = await AddTask(new NewTask
        {
            Name = _taskToRemove.Name,
            IsCompleted = _taskToRemove.IsCompleted
        }, _taskToRemove.Id, _taskToRemove.CreatedAt);
        _taskToRemove = created ?? throw new InvalidOperationException("Failed to create task");
        IsExecuted = false;
    }
}