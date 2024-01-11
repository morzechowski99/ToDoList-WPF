namespace ToDoList.Actions;

public interface IAction
{
    Task ExecuteAsync();
    Task UndoAsync();
}