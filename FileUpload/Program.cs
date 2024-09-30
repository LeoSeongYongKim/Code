using Microsoft.OpenApi.Models;
using FileUpload.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<FileUploadQueue>();
builder.Services.AddSingleton<FileProcessService>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "File Upload Async(CodingTest)",
        Version = "v1",
        Description = "",
        Contact = new OpenApiContact
        {
            Name = "JIN CL",
            Email = "kimsonghye@yahoo.com"
        }
    });
});

var app = builder.Build();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

var service = app.Services.GetRequiredService<FileProcessService>();
var cancellationTokenSource = new CancellationTokenSource();
_ = Task.Run(() => service.FileUploadSimulation(cancellationTokenSource.Token));

app.Run();
