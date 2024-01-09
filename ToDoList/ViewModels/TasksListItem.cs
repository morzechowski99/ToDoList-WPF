using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList.ViewModels;

public class TasksListItem : INotifyPropertyChanged
{
    public int Id { get; init; }
    public required string Name { get; init; }

    private bool _isCompleted;
    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}