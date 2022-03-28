using AcessoTransfer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcessoTransfer.Data.Context
{
    public class AcessoDbContext : DbContext
    {
        //public AcessoDbContext(DbContextOptions<AcessoDbContext> options) : base(options) { }

        public DbSet<Ordem> Ordens { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    }
}
