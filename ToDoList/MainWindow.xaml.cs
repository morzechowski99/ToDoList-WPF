using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.Actions.TodoTasksActions;
using ToDoList.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<TasksListItem> _tasks = [];
    private readonly ICommandManager _commandManager;
    private readonly ITasksService _tasksService;
    // ReSharper disable once InconsistentNaming
    private CollectionViewSource todosViewSource;
    public NewTask Task { get; set; } = new();
    public MainWindow(ICommandManager commandManager, ITasksService tasksService)
    {
        InitializeComponent();
        _commandManager = commandManager;
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

        try
        {
            var createAction = new AddTaskAction(_tasksService, _tasks, (Task.Clone() as NewTask)!);
            await _commandManager.Execute(createAction);
        }
        catch
        {
            MessageBox.Show("Failed to create task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Task.Name = "";
    }

    private async void OnDelete(object sender, ExecutedRoutedEventArgs e)
    {
        if ((sender as DataGrid)?.SelectedItem is not TasksListItem task) return;
        try
        {
            var removeAction = new RemoveTaskAction(_tasksService, _tasks, task);
            await _commandManager.Execute(removeAction);
        }
        catch
        {
            MessageBox.Show("Failed to delete task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var tasks = await _tasksService.GetAll();
        _tasks = [];
        _tasks.CollectionChanged += Tasks_CollectionChanged;
        foreach (var tasksListItem in tasks)
        {
            _tasks.Add(tasksListItem);
        }
        _tasks.Sort();
        todosViewSource.Source = _tasks;
        LoadingOverlay.Visibility = Visibility.Collapsed;
    }

    private void Tasks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
            foreach (INotifyPropertyChanged item in e.OldItems)
                item.PropertyChanged -= TaskDoneChanged;
        if (e.NewItems != null)
            foreach (INotifyPropertyChanged item in e.NewItems)
                item.PropertyChanged += TaskDoneChanged;

    }

    private async void TaskDoneChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not TasksListItem task) return;
        if (args.PropertyName != nameof(TasksListItem.IsCompleted)) return;
        try
        {
            var toggleAction = new ToggleDoneTaskAction(_tasksService, _tasks, task);
            await _commandManager.Execute(toggleAction);
        }
        catch
        {
            MessageBox.Show("Failed to update task", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void UndoButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!_commandManager.CanUndo)
        {
            MessageBox.Show("Nothing to undo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        await _commandManager.Undo();
    }

    private async void RedoButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!_commandManager.CanRedo)
        {
            MessageBox.Show("Nothing to redo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        await _commandManager.Redo();
    }
}