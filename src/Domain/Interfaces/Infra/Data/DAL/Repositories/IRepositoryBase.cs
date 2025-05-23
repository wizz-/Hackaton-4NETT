﻿namespace Domain.Interfaces.Infra.Data.DAL.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> ObterTodos();
        TEntity? ObterPorId(int id);
        int Inserir(TEntity entidade);
        void Atualizar(TEntity entidade);
        void Excluir(int id);
    }
}
