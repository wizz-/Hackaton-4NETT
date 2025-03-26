﻿using Domain.Entities.Cadastros;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Infra.Data.Context;

namespace Infra.Data.DAL.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(Contexto context) : base(context)
        {
        }

        public Medico? ObterPorCrm(string crm, Domain.Enums.UnidadeFederativa uf)
        {
            return ObterQueryable()
                    .SingleOrDefault(x => x.Crm.Numero == crm && x.Crm.Uf == uf);
        }
    }
}
