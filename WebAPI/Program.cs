
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        //API Gateway required properties (null or empty not allowed)
        Title = "WebAPI(CodingTest)",
        Version = "v1",
        Description = "",
        Contact = new OpenApiContact
        {
            Name = "JIN CL",
            Email = "kimsonghye@yahoo.com"
        }
    });
}) ;


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  
    });
}

app.Run();
