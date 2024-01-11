using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList.ViewModels;

public class NewTask : IDataErrorInfo, INotifyPropertyChanged, ICloneable
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }
    public bool IsCompleted { get; set; }
    public string Error => string.Empty;
    public string this[string columnName] => columnName == nameof(Name) && string.IsNullOrWhiteSpace(Name) ? "Name is required" : "";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public object Clone() => new NewTask { Name = Name, IsCompleted = IsCompleted };

}

