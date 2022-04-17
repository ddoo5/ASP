using System.Data.SQLite;
using MetricsAgent;
using NLog.Web;
using WorkWithBD;



var builder = WebApplication.CreateBuilder(args);
var _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
_logger.Debug("Run");


try
{
    CreateTables();
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
    builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
    builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
    builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();

    var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    _logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}



void CreateTables()
{
    string[] tables =
               {
                "cpumetrics",
                "dotnetmetrics",
                "hddmetrics",
                "networkmetrics",
                "rammetrics"
            };

    foreach (var item in tables)
    {
        using (var connection = new SQLiteConnection("Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;"))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = $"DROP TABLE IF EXISTS {item};";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $@"CREATE TABLE {item}(id INTEGER PRIMARY KEY, value INT, time TEXT)";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
