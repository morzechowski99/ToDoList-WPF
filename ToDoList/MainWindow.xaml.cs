using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.ViewModels;
using Task = ToDoList.DbModels.Task;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ObservableCollection<Task> _tasks = [];
    private CollectionViewSource todosViewSource;
    public NewTask Task { get; set; } = new();
    public MainWindow()
    {
        InitializeComponent();
        todosViewSource = (CollectionViewSource)FindResource(nameof(todosViewSource));
        todosViewSource.Source = _tasks;
    }

    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (Validation.GetHasError(TaskNameTextBox))
        {
            StringBuilder sb = new();
            Validation.GetErrors(TaskNameTextBox).ToList().ForEach(error => sb.AppendLine(error.ErrorContent.ToString()));
            MessageBox.Show(sb.ToString(), "Form has errors", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        _tasks.Add(new Task { Name = Task.Name!, IsComplete = false });
        Task.Name = "";
    }

    private void OnDelete(object sender, ExecutedRoutedEventArgs e)
    {
        if ((sender as DataGrid)?.SelectedItem is Task task)
        {
            _tasks.Remove(task);
        }
    }
}