using ToDoList.Actions;

namespace ToDoList.Interfaces;

public interface ICommandManager
{
    bool CanUndo { get; }
    bool CanRedo { get; }
    Task Execute(IAction action);
    Task Undo();
    Task Redo();
}