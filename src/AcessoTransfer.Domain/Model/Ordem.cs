using System;
using System.Collections.Generic;
using System.Text;

namespace AcessoTransfer.Domain.Model
{
    public class Ordem
    {
        public Ordem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
