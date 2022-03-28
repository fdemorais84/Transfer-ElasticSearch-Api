using AcessoTransfer.Api.Model;
using AcessoTransfer.Data.HttpRequests;
using AcessoTransfer.Domain.DTO;
using AcessoTransfer.Domain.Interfaces;
using AcessoTransfer.Domain.Model;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcessoTransfer.Domain.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly ILogger<TransferenciaService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _apiAccount;
        private readonly IMapper _mapper;

        public TransferenciaService(ITransferenciaRepository transferenciaRepository, IConfiguration configuration, IMapper mapper, ILogger<TransferenciaService> logger)
        {
            _transferenciaRepository = transferenciaRepository;
            _configuration = configuration;
            _apiAccount = _configuration.GetSection("AppSettings").GetSection("UrlApiAccount").Value;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> RealizarTransferencia(Transferencia dados)
        {
            var dadosOrigem = await ConsultarExistenciaContaOrigem(dados);

            if (String.IsNullOrEmpty(dadosOrigem.AccountNumber)) return await MontarErroCunsulta();            

            var dadosDestino = await ConsultarExistenciaContaDestino(dados);

            if (String.IsNullOrEmpty(dadosDestino.AccountNumber)) return await MontarErroCunsulta();           

            if (dados.Value > dadosOrigem.Balance) return await MontarErroSaldo();                     

            return await EfetivarTransferencia(dados);    
        }        

        public async Task<Ordem> ConsultarStatusTranferencia(Guid transactionId)           
        {
            return await _transferenciaRepository.Consultar(transactionId);            
        }

        #region PRIVATE AREA
        private async Task<DadosContaOrigemDTO> ConsultarExistenciaContaOrigem(Transferencia dados)
        {
            try
            {
                return await HTTPClientWrapper<DadosContaOrigemDTO>.Get(_apiAccount + "/Account/" + dados.AccountOrigin);                
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        private async Task<DadosContaDestinoDTO> ConsultarExistenciaContaDestino(Transferencia dados)
        {
            try
            {
                return await HTTPClientWrapper<DadosContaDestinoDTO>.Get(_apiAccount + "/Account/" + dados.AccountDestination);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Guid> EfetivarTransferencia(Transferencia dados)
        {
            try
            {
                await TranferirDebito(dados);

                await TransferirCredito(dados);

                return await MontarTransferencia();                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task TranferirDebito(Transferencia dados)
        {
            var valid = _mapper.Map<EfetivacaDebito>(dados);
            await HTTPClientWrapper<EfetivacaDebito>.PostRequest(_apiAccount + "/Account", valid);
        }

        private async Task TransferirCredito(Transferencia dados)
        {
            var valid = _mapper.Map<EfetivaCredito>(dados);
            await HTTPClientWrapper<EfetivaCredito>.PostRequest(_apiAccount + "/Account", valid);
        }        

        private async Task<Guid> MontarErroCunsulta()
        {
            var ordem = new Ordem();
            ordem.Status = "Error";
            ordem.Message = "Invalid account number";
            await _transferenciaRepository.Adicionar(ordem);
            _logger.LogInformation("Conta não encontrada");
            return ordem.Id;
        }

        private async Task<Guid> MontarErroSaldo()
        {
            var ordem = new Ordem();
            ordem.Status = "Error";
            ordem.Message = "Insufficient balance";
            await _transferenciaRepository.Adicionar(ordem);
            _logger.LogInformation("Saldo insuficiente");
            return ordem.Id;
        }

        private async Task<Guid> MontarTransferencia()
        {
            var ordem = new Ordem();
            ordem.Status = "Confirmed";
            await _transferenciaRepository.Adicionar(ordem);
            _logger.LogInformation("Transferência concluida com sucesso");
            return ordem.Id;
        }
        #endregion
    }
}
