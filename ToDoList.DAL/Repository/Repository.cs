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

    public async Task<ToDoTaskDto> CreateTaskAsync(CreateUpdateTaskDto createTaskDto)
    {
        if (createTaskDto?.Name is null)
            throw new ArgumentException("Invalid model", nameof(createTaskDto));
        var toDoTask = createTaskDto.AsTodoTask();
        await _dbContext.ToDoTasks.AddAsync(toDoTask);
        return toDoTask.AsTodoTaskDto();
    }

}