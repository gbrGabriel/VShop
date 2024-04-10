using System.Reflection;
using IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.Register();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Microsserviço de Produto",
            Description = "Microsserviço para Produto",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Email = "gabrielgbr.contato@gmail.com",
                Name = "Gabriel Silva",
                Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://www.mit.edu/~amini/LICENSE.md")
            }
        }
    );

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
