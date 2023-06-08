using Microsoft.EntityFrameworkCore;
using Module__10__WebAPI__Projects.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarInformationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddCors(c => c.AddPolicy("EnableCors", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers()
    .AddNewtonsoftJson(n =>
    {
        n.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        n.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    });

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("EnableCors");
app.MapControllers();
app.Run();