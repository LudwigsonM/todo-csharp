var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/todo-items", () => {});
app.MapGet("/todo-items/{id}", (string id) => {
    var todoItem = new {
        Id = Guid.NewGuid().ToString(),
        Title = "My Complete To-Do in C#",
        IsCompleted = false
    };
    return Results.Ok(todoItem);
});

app.MapPost("/todo-items", () => {});
app.MapPut("/todo-items/{id}", (string id) => {});
app.MapDelete("/todo-items/{id}", (string id) => {});

app.Run();
