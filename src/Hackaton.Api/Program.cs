using Carter;
using Hackaton.Api.IoC;
using Infra.DatabaseInitializers.Interfaces;
using Prometheus;

namespace Hackaton.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigurarEMapearDependenciasDaAplicacao();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowBlazor", policy =>
            //    {
            //        policy.WithOrigins("https://localhost:7159")
            //              .AllowAnyHeader()
            //              .AllowAnyMethod();
            //    });
            //});

            var app = builder.Build();

            InicializarDatabase(app);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowBlazor");
            app.UseAuthorization();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.MapCarter();
            app.MapMetrics();

            app.Run();
        }

        private static void InicializarDatabase(WebApplication app)
        {
            using var scopo = app.Services.CreateScope();
            var dbInitializer = scopo.ServiceProvider.GetService<IDatabaseInitializer>();
            if (dbInitializer == null) return;
            dbInitializer.InicializarDatabase();
        }
    }
}
