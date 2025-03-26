using Application.Services.Logins.Interfaces;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.LoginsAppService
{
    public class LoginAppService(IUnitOfWork unitOfWork) : ILoginAppService
    {

    }
}
