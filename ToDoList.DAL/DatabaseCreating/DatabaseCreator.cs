using ToDoList.DAL.Contracts;

namespace ToDoList.DAL.DatabaseCreating;

internal class DatabaseCreator : IDatabaseCreator
{
    private readonly ToDoListDbContext _dbContext;
    private bool _isCreated;

    public DatabaseCreator(ToDoListDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateDatabaseIfNotExists()
    {
        if(_isCreated) return;
        await _dbContext.Database.EnsureCreatedAsync();
        _isCreated = true;
    }
}