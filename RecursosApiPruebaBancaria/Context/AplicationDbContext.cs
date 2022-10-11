using Microsoft.EntityFrameworkCore;
using RecursosApiPruebaBancaria.ModelosTablas;

namespace RecursosApiPruebaBancaria.Context
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<MQTIPOCALCULO02> MQTIPOCALCULO02 { get; set; } 

    }
}
