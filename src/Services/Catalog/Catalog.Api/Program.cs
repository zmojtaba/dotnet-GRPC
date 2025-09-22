using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.Api.Models;
using FluentValidation;
using Marten;
using Marten.Schema;
namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var assembly = typeof(Program).Assembly;
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

            });
            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Postgres")); // 1️⃣ connect to Postgres

                // Register your document types (optional; Marten will detect automatically)
                //options.Schema.For<Product>().Identity(x => x.Id);

                options.DatabaseSchemaName = "public"; // 3️⃣ schema name in Postgres (default: public)
            }).UseLightweightSessions();

            builder.Services.AddExceptionHandler<CustomeExceptionHandler>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.UseExceptionHandler(options => { });

            app.Run();
        }
    }
}
