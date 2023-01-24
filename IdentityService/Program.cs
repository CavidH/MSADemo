using IdentityService.Configs;
using Serilog;
using Serilog.Events;

namespace IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Logger

            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    Path.Combine("Logs", "Log.txt"),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(2))
                .WriteTo.Console()
                //.WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            #endregion
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.WebHost.UseSerilog();

            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            
            
            builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients())
                .AddDeveloperSigningCredential();


            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseIdentityServer();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}