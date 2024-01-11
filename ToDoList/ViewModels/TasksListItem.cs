using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList.ViewModels;

public class TasksListItem : INotifyPropertyChanged, IComparable<TasksListItem>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

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

    public int CompareTo(TasksListItem? other)
    {
        if (other is null)
            return 1;

        if (IsCompleted == other.IsCompleted)
            return CreatedAt > other.CreatedAt ? 1 : -1;

        return IsCompleted ? 1 : -1;
    }
}