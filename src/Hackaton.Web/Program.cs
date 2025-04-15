using Blazored.LocalStorage;
using Hackaton.Web.Services.Especialidades.Interfaces;
using Hackaton.Web.Services.Especialidades;
using Hackaton.Web.Services.Ufs;
using Hackaton.Web.Services.Ufs.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Text.Json;
using Hackaton.Web.Models.Configuracao;
using Hackaton.Web.Services.Medicos;
using Hackaton.Web.Services.Medicos.Interfaces;
using Hackaton.Web.Services.Pacientes;
using Hackaton.Web.Services.Pacientes.Interfaces;
using Hackaton.Web.Services.Autenticacao.Interfaces;
using Hackaton.Web.Services.Autenticacao;

namespace Hackaton.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Carrega appsettings.json
            var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            using var response = await http.GetAsync("configuracao/appsettings.json");
            var json = await response.Content.ReadAsStringAsync();
            var config = JsonSerializer.Deserialize<AppSettings>(json);
            var baseUrl = config?.ApiSettings?.BaseUrl ?? throw new Exception("BaseUrl não encontrada");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            });

            // Demais serviços
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
            builder.Services.AddScoped<IConsultaAutenticacaoService, ConsultaAutenticacaoService>();

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
