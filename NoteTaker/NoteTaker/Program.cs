
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace NoteTaker
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Note Taker API",
                        Description = "API for taking notes",
                        Version = "v1",
                    });

                c.UseInlineDefinitionsForEnums();
                c.UseAllOfToExtendReferenceSchemas();
            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    {
                        policy.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Connection String here
                var connectionString = builder.Configuration.GetConnectionString("NoteTakerConnectionString");
                options.UseNpgsql(connectionString);
            });

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnableFilter();
            });

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}