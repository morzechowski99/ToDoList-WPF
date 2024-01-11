using System.Collections.ObjectModel;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Actions.TodoTasksActions;

internal class ToggleDoneTaskAction : ToDoTasksActionBase, IAction
{
    private readonly TasksListItem _itemToToggle;
    private bool _wasRedo;

    public ToggleDoneTaskAction(ITasksService tasksService, ObservableCollection<TasksListItem> frontTasks, TasksListItem item) : base(tasksService, frontTasks)
    {
        _itemToToggle = item;
    }

    public async Task ExecuteAsync()
    {
        if (IsExecuted) throw new InvalidOperationException("Need to undo action before execute");
        if (_wasRedo)
        {
            var item = FrontTasks.First(x => x.Id == _itemToToggle.Id);
            FrontTasks.Remove(item);
            item.IsCompleted = !item.IsCompleted;
            FrontTasks.Add(item);
        }
        await ToggleDone();
        IsExecuted = true;
    }

    public async Task UndoAsync()
    {
        var item = FrontTasks.First(x => x.Id == _itemToToggle.Id);
        FrontTasks.Remove(item);
        item.IsCompleted = !item.IsCompleted;
        FrontTasks.Add(item);
        if (!IsExecuted) throw new InvalidOperationException("Need to execute action before undo");
        await ToggleDone();
        IsExecuted = false;
        _wasRedo = true;
    }

    private async Task ToggleDone()
    {
        if (!await TasksService.ToggleDone(_itemToToggle.Id))
            throw new InvalidOperationException("Failed to toggle task done");
        FrontTasks.Sort();
    }
}