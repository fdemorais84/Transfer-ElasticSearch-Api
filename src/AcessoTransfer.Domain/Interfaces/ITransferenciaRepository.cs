using AcessoTransfer.Api.Model;
using AcessoTransfer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcessoTransfer.Domain.Interfaces
{
    public interface ITransferenciaRepository
    {
        Task<Ordem> Consultar(Guid id);
        Task<int> Adicionar(Ordem dados);
        Task<int> SaveChanges();
        void Dispose();
    }
}
