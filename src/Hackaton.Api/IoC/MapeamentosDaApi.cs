using Application.Services.Cadastros;
using Application.Services.Cadastros.Interfaces;
using Application.Services.Agendas;
using Application.Services.Agendas.Interfaces;
using Application.Services.Consultas;
using Application.Services.Consultas.Interfaces;
using Application.Services.Logins.Interfaces;
using Application.Services.LoginsAppService;
using Carter;
using Hackaton.Api.Services.Jwt;
using Hackaton.Api.Services.Jwt.Interfaces;
using Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hackaton.Api.IoC
{
    public static class MapeamentosDaApi
    {
        public static void ConfigurarEMapearDependenciasDaAplicacao(this WebApplicationBuilder builder)
        {
            AdicionarAutenticacao(builder.Services, builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCarter();

            MapearApi(builder.Services);
            MapeamentosDaAplicacao.Mapear(builder.Services);
        }

        private static void AdicionarAutenticacao(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    LogValidationExceptions = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(ObterArrayDeBytesdaChaveDaApi(configuration)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                    {
                        return notBefore <= DateTime.UtcNow &&
                        expires > DateTime.UtcNow;
                    },
                };
            }).AddNegotiate();
        }

        private static byte[] ObterArrayDeBytesdaChaveDaApi(IConfiguration configuration)
        {
            var chave = configuration["Jwt:Key"];
            ArgumentException.ThrowIfNullOrEmpty(chave);
            return Encoding.ASCII.GetBytes(chave);
        }

        private static void MapearApi(IServiceCollection services)
        {
            services.AddScoped<ICadastroAppService, CadastroAppService>();
            services.AddScoped<IAgendaAppService, AgendaAppService>();
            services.AddScoped<IConsultaAppService, ConsultaAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
        }
    }
}
