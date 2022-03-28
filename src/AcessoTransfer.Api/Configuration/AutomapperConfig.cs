using AcessoTransfer.Api.Model;
using AcessoTransfer.Domain.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessoTransfer.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Transferencia, EfetivacaDebito> ()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountOrigin))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Debit"));

            CreateMap<Transferencia, EfetivaCredito>()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountDestination))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Credit"));
        }
    }
}
