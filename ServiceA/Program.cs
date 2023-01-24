using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ServiceA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:2000"; //IDENTITY SERVER

                    options.Audience = "ServiceA"; // resource name
                });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ReadServiceA", policy => policy.RequireClaim("scope", "ServiceA.Read"));
                opt.AddPolicy("WriteServiceA", policy => policy.RequireClaim("scope", "ServiceA.Write"));
                opt.AddPolicy("ReadWriteServiceA",policy => policy.RequireClaim("scope", "ServiceA.Write", "ServiceA.Read"));
                opt.AddPolicy("AllServiceA", policy => policy.RequireClaim("scope", "ServiceA.Admin"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}