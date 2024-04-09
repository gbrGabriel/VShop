using System.Reflection;
using IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.Register();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Microsserviço de Produto",
            Description = "Microsserviço para Produto",
            Version = "v1",
            Contact = new OpenApiContact { Name = "Gabriel Silva", }
        }
    );

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
