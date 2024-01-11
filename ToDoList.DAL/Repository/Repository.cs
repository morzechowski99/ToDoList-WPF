using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Contracts;
using ToDoList.DAL.Dto;

namespace ToDoList.DAL.Repository;

internal class Repository : IRepository
{
    private readonly ToDoListDbContext _dbContext;

    public Repository(ToDoListDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync()
    {
        return await _dbContext.ToDoTasks
            .Select(t => t.AsTodoTaskDto())
            .ToListAsync();
    }

    public async Task<ToDoTaskDto?> GetTaskAsync(Guid id)
    {
        return (await _dbContext.ToDoTasks.FindAsync(id))?.AsTodoTaskDto();
    }

    public async Task<ToDoTaskDto> CreateTaskAsync(CreateUpdateTaskDto createTaskDto)
    {
        if (createTaskDto?.Name is null)
            throw new ArgumentException("Invalid model", nameof(createTaskDto));
        var toDoTask = createTaskDto.AsTodoTask();
        await _dbContext.ToDoTasks.AddAsync(toDoTask);
        await _dbContext.SaveChangesAsync();
        return toDoTask.AsTodoTaskDto();
    }

    public async Task<ToDoTaskDto> UpdateTaskAsync(Guid id, CreateUpdateTaskDto updateTaskDto)
    {
        var task = await _dbContext.ToDoTasks.FindAsync(id);
        if (task is null)
            throw new ArgumentException("Task not found", nameof(id));
        task = updateTaskDto.AsTodoTask(task);
        _dbContext.ToDoTasks.Update(task);
        await _dbContext.SaveChangesAsync();
        return task.AsTodoTaskDto();
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var taskToRemove = await _dbContext.ToDoTasks.FindAsync(id);
        if (taskToRemove is null)
            throw new ArgumentException("Task not found", nameof(id));
        _dbContext.ToDoTasks.Remove(taskToRemove);
        await _dbContext.SaveChangesAsync();
    }
}