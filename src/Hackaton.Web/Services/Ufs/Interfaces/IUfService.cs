namespace Hackaton.Web.Services.Ufs.Interfaces
{
    public interface IUfService
    {
        Task<List<string>> ObterUfAsync();
    }
}
