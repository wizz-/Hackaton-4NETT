using Hackaton.Web.Services.Autenticacao.Interfaces;
using Hackaton.Web.Services.Autenticacao;
using Hackaton.Web.Services.Especialidades.Interfaces;
using Hackaton.Web.Services.Especialidades;
using Hackaton.Web.Services.Medicos.Interfaces;
using Hackaton.Web.Services.Medicos;
using Hackaton.Web.Services.Pacientes.Interfaces;
using Hackaton.Web.Services.Pacientes;
using Hackaton.Web.Services.Ufs.Interfaces;
using Hackaton.Web.Services.Ufs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Radzen;
using Blazored.LocalStorage;

namespace Hackaton.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Carregar config corretamente
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: true);

            var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("BaseUrl da API não encontrada na configuração.");
            }

            // Configura HttpClient usando a baseUrl da configuração
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            });

            // Serviços normais
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
            builder.Services.AddScoped<IUsuarioLogadoService, UsuarioLogadoService>();

            builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            builder.Services.AddScoped<IUfService, UfService>();
            builder.Services.AddScoped<IEspecialidadeService, EspecialidadeService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();
            builder.Services.AddScoped<IPacienteService, PacienteService>();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            await builder.Build().RunAsync();
        }
    }
}
