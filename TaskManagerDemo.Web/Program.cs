using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Users.Repositories;
using TaskManagerDemo.Infrastructure.Database;
using TaskManagerDemo.Infrastructure.Database.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    options.UseSqlite($"Data Source=TaskManagerDemo.db");
});
    
ConfigureDependencies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


MigrateDatabase();

app.Run();
return;

void ConfigureDependencies(IServiceCollection services)
{
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ITodoRepository, TodoRepository>();
}

void MigrateDatabase()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
    context.Database.Migrate();
    DummyDataSeeder.Seed(context);
}