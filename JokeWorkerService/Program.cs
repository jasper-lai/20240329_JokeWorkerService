using JokeWorkerService;
using Serilog;

#region �]�m Serilog 
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    //.WriteTo.File("logs/JokeService-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("D:/Temp/logs/JokeService-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
#endregion

var builder = Host.CreateApplicationBuilder(args);

#region �ĥ� Serilog �@�� Log ���u��
// Configure your application
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddSerilog(); // Use Serilog for logging
#endregion

// �o�O���F�n�H Windows Service �Ҧ�����.
// ���p�G�S�����U�� "�A��", ���M�O�H Console �Ҧ�����.
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET8 Joke Service";
});


builder.Services.AddSingleton<JokeService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
