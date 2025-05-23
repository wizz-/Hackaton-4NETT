﻿using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;

namespace Domain.Interfaces.Infra.Data.DAL.Repositories
{
    public interface IConsultaRepository : IRepositoryBase<Consulta>
    {
        IList<Consulta> ObterConsultasFuturasPorMedico(int medicoId);
        IList<Consulta> ObterConsultasFuturasPorPaciente(int pacienteId);
        IList<Consulta> ObterConsultasNaoCanceladasDoDia(DateOnly dia, IList<Medico> medicosDiponiveis);
        IList<Consulta> ObterConsultasNaoCanceladasNoPeriodo(DateOnly diaInicial, DateOnly dataFinal, Medico medico);
    }
}
