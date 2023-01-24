using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;

namespace MSAGateway
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
            builder.Configuration.AddJsonFile("Ocelot.json");


            var authProviderKey = "jsjsjsjsjsjjsj";

            builder.Services.AddAuthentication()
                .AddJwtBearer(authProviderKey, x =>
                {
                    x.Authority = "https://localhost:2000"; // Identity Server
                    // x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            builder.WebHost.UseSerilog();
            builder.Services.AddOcelot();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            /* if (app.Environment.IsDevelopment())
             {
                 //app.UseSwagger();
                 //app.UseSwaggerUI();
             }*/

            app.UseHttpsRedirection();
            app.UseOcelot().Wait();
            /*  app.UseAuthentication();
              app.UseAuthorization();
            */
            app.MapControllers();

            app.Run();
        }
    }
}