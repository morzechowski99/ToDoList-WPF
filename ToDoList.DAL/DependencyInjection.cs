using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.DAL.Contracts;

namespace ToDoList.DAL;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ToDoListDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IRepository, Repository.Repository>();
    }
}