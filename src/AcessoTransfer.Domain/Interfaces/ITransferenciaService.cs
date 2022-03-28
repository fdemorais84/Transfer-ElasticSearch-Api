using AcessoTransfer.Api.Model;
using AcessoTransfer.Domain.DTO;
using AcessoTransfer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcessoTransfer.Domain.Interfaces
{
    public interface ITransferenciaService
    {
        Task<Guid> RealizarTransferencia(Transferencia dados);
        Task<Ordem> ConsultarStatusTranferencia(Guid transactionId);
    }
}
