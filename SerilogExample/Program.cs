using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Seq(serverUrl: "http://localhost:5341", apiKey: "YbZbuiduWZeRYPb1rsaF")
//    .CreateLogger();

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
 .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
