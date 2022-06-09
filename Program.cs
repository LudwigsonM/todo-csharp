using Services;
using Settings;
using Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<TodoDbSettings>(builder.Configuration.GetSection("TodoDB"));
        builder.Services.AddSingleton<ITodoItemService, TodoItemService>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();


        app.MapGet("/todo-items", async (ITodoItemService todoItemService) =>
        {
            var todos = await todoItemService.GetAllTodoItems();
            return Results.Ok(todos);
        });

        app.MapGet("/todo-items/{id}", async (string id, ITodoItemService todoItemService) =>
        {

            var todoItem = await todoItemService.GetTodoItemById(id);

            return Results.Ok(todoItem);
        });

        app.MapPost("/todo-items", async (TodoItem newVoodoo, ITodoItemService service) =>
        {
            TodoItem createdTodoItem = await service.CreateTodoItem(newVoodoo);

            return Results.Created($"todo-items/{createdTodoItem}", createdTodoItem);
        });

        app.MapPut("/todo-items/{id}", (string id) => { });
        app.MapDelete("/todo-items/{id}", (string id) => { });

        app.Run();
    }
}