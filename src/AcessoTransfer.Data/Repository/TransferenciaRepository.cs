using AcessoTransfer.Api.Model;
using AcessoTransfer.Data.Context;
using AcessoTransfer.Domain.Interfaces;
using AcessoTransfer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcessoTransfer.Data.Repository
{
    public class TransferenciaRepository : ITransferenciaRepository
    {
        protected readonly AcessoDbContext Db;
        protected readonly DbSet<Ordem> DbSet;

        public TransferenciaRepository(AcessoDbContext db)
        {
            Db = db;
            DbSet = db.Set<Ordem>();
        }

        public async Task<Ordem> Consultar(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> Adicionar(Ordem dados)
        {
            DbSet.Add(dados);
            return await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
