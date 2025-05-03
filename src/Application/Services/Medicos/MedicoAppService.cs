using Application.Services.Medicos.Dtos;
using Application.Services.Medicos.Interfaces;
using Application.Services.Medicos.Mappers.Interfaces;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Medicos
{
    public class MedicoAppService(IUnitOfWork unitOfWork, IMapperMedicoAppService mapper) : IMedicoAppService
    {

        public CadastroMedicoDto ObterDadosDoMedico(int id)
        {
            var medico = unitOfWork.MedicoRepository.ObterPorId(id);
            if (medico == null) throw new InvalidOperationException($"Não foi possível localizar médico com id '{id}'.");

            return mapper.Map(medico);
        }
        public IList<MedicoParaConsultaDto> ObterMedicosPorEspecialidade(int especialidadeId)
        {
            var medicos = unitOfWork.MedicoRepository.ObterPorEspecialidade(especialidadeId);

            return mapper.Map(medicos);
        }

        public IList<MedicoParaConsultaDto> ObterTodosMedicos()
        {
            var medicos = unitOfWork.MedicoRepository.ObterTodos().ToList();

            return mapper.Map(medicos);
        }
    }
}
