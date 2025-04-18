﻿using Domain.Interfaces.Infra.Data.DAL;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL
{
    public class UnitOfWork(Contexto contexto,
            IUsuarioRepository usuarioRepository,
            IPacienteRepository pacienteRepository,
            IEspecialidadeRepository especialidadeRepository,
            IMedicoRepository medicoRepository,
            IConsultaRepository consultaRepository) : IUnitOfWork
    {
        public IUsuarioRepository UsuarioRepository { get; } = usuarioRepository;

        public IPacienteRepository PacienteRepository { get; } = pacienteRepository;
        public IEspecialidadeRepository EspecialidadeRepository { get; } = especialidadeRepository;
        public IMedicoRepository MedicoRepository { get; } = medicoRepository;

        public IConsultaRepository ConsultaRepository { get; } = consultaRepository;

        public void SaveChanges() => contexto.SaveChanges();
    }
}
