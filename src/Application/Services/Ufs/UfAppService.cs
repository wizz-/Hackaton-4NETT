using Application.Services.Ufs.Interfaces;
using Domain.Enums;

namespace Application.Services.Ufs
{
    public class UfAppService : IUfAppService
    {
        public IList<string> ObterUfs()
        {
            return Enum.GetNames(typeof(UnidadeFederativa)).ToList();
        }
    }
}
