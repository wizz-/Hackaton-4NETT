using Application.Services.Cadastros;
using Application.Services.Cadastros.Interfaces;
using Application.Services.Logins.Interfaces;
using Application.Services.LoginsAppService;
using Domain.Interfaces.Infra.Data.DAL;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;
using Infra.Data.DAL;
using Infra.Data.DAL.Repositories;
using Infra.DatabaseInitializers;
using Infra.DatabaseInitializers.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Infra.IoC
{
    public static class MapeamentosDaAplicacao
    {
        public static void Mapear(IServiceCollection services)
        {
            MapearApplication(services);
            MapearInfra(services);
        }
        private static void MapearApplication(IServiceCollection services)
        {
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<ICadastroAppService, CadastroAppService>();
        }

        private static void MapearInfra(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var tipos = typeof(RepositoryBase<>).Assembly.GetTypes().Where(x => x.BaseType?.Name == typeof(RepositoryBase<>).Name);

            foreach (var item in tipos)
            {
                var serviceType = item
                    .GetInterfaces()
                    .Single(x => x.Name != typeof(IRepositoryBase<>).Name);

                services.AddScoped(serviceType, item);
            }

            var retryPolicy = Policy
                .Handle<SqlException>()
                .WaitAndRetryAsync(20, retryAttempt => TimeSpan.FromSeconds(5),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"SQL: Tentativa {retryCount} de conexão ao SQL Server falhou: {exception.Message}. Tentando novamente em {timeSpan.Seconds} segundos.");
                    });

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var connectionStringTeste = configuration.GetConnectionString("TestConnection");

            Task.Run(async () =>
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    using var connection = new SqlConnection(connectionStringTeste);
                    await connection.OpenAsync();
                    Console.WriteLine("Conexão ao SQL Server estabelecida com sucesso.");

                });
            }).GetAwaiter().GetResult();

            services.AddDbContext<Contexto>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null));
            });


            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        }
    }
}
