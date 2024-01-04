using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ToDoList.DbModels;

public class Task : INotifyPropertyChanged
{
    private bool _isComplete;
    private string _name;
    private int _id;

    public int Id   
    {
        get => _id;
        set
        {
            if (value == _id) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public bool IsComplete
    {
        get => _isComplete;
        set
        {
            _isComplete = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}