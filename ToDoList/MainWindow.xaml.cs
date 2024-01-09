using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<TasksListItem> _tasks = [];
    private readonly ITasksService _tasksService;
    // ReSharper disable once InconsistentNaming
    private CollectionViewSource todosViewSource;
    public NewTask Task { get; set; } = new();
    public MainWindow(ITasksService tasksService)
    {
        InitializeComponent();
        _tasksService = tasksService;
        todosViewSource = (CollectionViewSource)FindResource(nameof(todosViewSource));
    }

    private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (Validation.GetHasError(TaskNameTextBox))
        {
            StringBuilder sb = new();
            Validation.GetErrors(TaskNameTextBox).ToList().ForEach(error => sb.AppendLine(error.ErrorContent.ToString()));
            MessageBox.Show(sb.ToString(), "Form has errors", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var created = await _tasksService.Add(Task);
        if (created is null)
        {
            MessageBox.Show("Failed to create task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        created.PropertyChanged += async (o, args) =>
        {
            await TaskDoneChanged(o, args);
        };
        _tasks.Add(created);
        Task.Name = "";
    }

    private async void OnDelete(object sender, ExecutedRoutedEventArgs e)
    {
        if ((sender as DataGrid)?.SelectedItem is not TasksListItem task) return;
        if (await _tasksService.Delete(task.Id))
            _tasks.Remove(task);
        else
            MessageBox.Show("Failed to delete task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var tasks = await _tasksService.GetAll();
        var tasksListItems = tasks.ToList();
        foreach (var tasksListItem in tasksListItems)
        {
            tasksListItem.PropertyChanged += async (o, args) =>
            {
                await TaskDoneChanged(o, args);
            };
        }
        _tasks = new ObservableCollection<TasksListItem>(tasksListItems);
        todosViewSource.Source = _tasks;
        LoadingOverlay.Visibility = Visibility.Collapsed;
    }

    private async Task TaskDoneChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not TasksListItem task) return;
        if (args.PropertyName != nameof(TasksListItem.IsCompleted)) return;
        if (!await _tasksService.ToggleDone(task.Id))
            MessageBox.Show("Failed to update task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}