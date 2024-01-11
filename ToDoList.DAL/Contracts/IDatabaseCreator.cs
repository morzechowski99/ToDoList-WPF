namespace ToDoList.DAL.Contracts;

public interface IDatabaseCreator
{
    Task CreateDatabaseIfNotExists();
}