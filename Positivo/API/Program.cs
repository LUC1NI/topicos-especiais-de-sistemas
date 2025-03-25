using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Namespace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

List<Curso> cursos = new List<Curso>() {
    new Curso() {Id = 1, Name = "Ciencia da computação"},
    new Curso() {Id = 2, Name = "Medicina"}
};
app.MapGet("/", ([FromServices] AppDataContext ctx) => {
    if (ctx.Cursos.Any()){
        return Results.Ok(ctx.Cursos.ToList());
    }
    return Results.NotFound();
    });
// busca o curso pelo id
app.MapGet("/{id}", ([FromRoute] int id,
                     [FromServices] AppDataContext ctx) => {
    if (ctx.Cursos.Any()) {
         return Results.Ok(cursos.Find(curso => curso.Id == id));
    }
    return Results.NotFound();
});
// cadastar um novo curso
app.MapPost("/", ([FromBody] Curso curso, 
                  [FromServices] AppDataContext ctx) => {
    if (ctx.Cursos.Any()){
        ctx.Cursos.Add(curso);
        ctx.SaveChanges();
        return Results.Created("", curso);
    }

    return Results.NotFound();

    cursos.Add(curso);
});

app.Run();
