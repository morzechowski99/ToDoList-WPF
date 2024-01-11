using ToDoList.Actions;
using ToDoList.Interfaces;

namespace ToDoList.Services;

internal class CommandManager : ICommandManager
{
    private readonly Stack<IAction> _actionsStackNormal = new();
    private readonly Stack<IAction> _actionsStackReverse = new();

    public bool CanUndo => _actionsStackNormal.Any();
    public bool CanRedo => _actionsStackReverse.Any();

    public async Task Execute(IAction action)
    {
        await action.ExecuteAsync();
        _actionsStackNormal.Push(action);
        _actionsStackReverse.Clear();
    }

    public async Task Undo()
    {
        if (!CanUndo) throw new InvalidOperationException("Nothing to undo");
        var action = _actionsStackNormal.Pop();
        await action.UndoAsync();
        _actionsStackReverse.Push(action);
    }

    public async Task Redo()
    {
        if (!CanRedo) throw new InvalidOperationException("Nothing to redo");
        var action = _actionsStackReverse.Pop();
        await action.ExecuteAsync();
        _actionsStackNormal.Push(action);
    }
}