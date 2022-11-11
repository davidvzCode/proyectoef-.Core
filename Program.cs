using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using proyectoef.Models;

var builder = WebApplication.CreateBuilder(args);


//virtual
//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
//sql server
//builder.Services.AddSqlServer<TareasContext>("Data Source=server;Initial Catalog=db;user id=sa; password=pass");
//postgresql
//builder.Services.AddDbContext<TareasContext>(options => options.UseNpgsql("Host=db;Database=TareasDB;Port=5432;Username=sa;Password=123;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddNpgsql<TareasContext>(builder.Configuration.GetConnectionString("TareasDb"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices]TareasContext dbContext) => {
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapPost("/api/tareas", async ([FromServices]TareasContext dbContext, [FromBody] Tarea tarea) => {
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreaccion = DateTime.Now;
    await dbContext.AddAsync(tarea);
    //await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices]TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) => {
    
    var tareaActual = dbContext.Tareas.Find(id);
    
    if(tareaActual != null){
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) => {
    var tareaActual = dbContext.Tareas.Find(id);

    if(tareaActual == null)
        return Results.NotFound("Task not found.");
        
    dbContext.Remove(tareaActual);
    await dbContext.SaveChangesAsync();

    return Results.Ok("Removed!");
});


app.Run();