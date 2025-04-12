using Application.Services.Medicos.Dtos;
using Application.Services.Medicos.Interfaces;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Medicos
{
    public class MedicoAppService(IUnitOfWork unitOfWork) : IMedicoAppService
    {
        public IList<MedicoParaConsultaDto> ObterMedicosPorEspecialidade(int especialidadeId)
        {
            var medicos = unitOfWork.MedicoRepository.ObterPorEspecialidade(especialidadeId);

            return medicos.Select(x =>
                    new MedicoParaConsultaDto()
                    {
                        Id = x.Id,
                        CrmNumero = x.Crm.Numero,
                        CrmUf = x.Crm.Uf.ToString(),
                        Especialidade = x.Especialidade.Nome,
                        Nome = x.Nome,
                    })
                        .ToList();
        }
    }
}
