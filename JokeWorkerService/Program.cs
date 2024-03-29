using JokeWorkerService;
using Serilog;

#region 設置 Serilog 
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    //.WriteTo.File("logs/JokeService-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("D:/Temp/logs/JokeService-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
#endregion

var builder = Host.CreateApplicationBuilder(args);

#region 採用 Serilog 作為 Log 的工具
// Configure your application
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddSerilog(); // Use Serilog for logging
#endregion

// 這是為了要以 Windows Service 模式執行.
// 但如果沒有註冊到 "服務", 仍然是以 Console 模式執行.
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET8 Joke Service";
});


builder.Services.AddSingleton<JokeService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
