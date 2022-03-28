using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessoTransfer.Api.Model
{
    public class Transferencia
    {
        public string AccountOrigin { get; set; } 
        public string AccountDestination { get; set; }
        public decimal Value { get; set; }        
    }
}
