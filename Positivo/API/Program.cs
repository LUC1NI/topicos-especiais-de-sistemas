using Namespace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();