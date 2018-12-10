using Microsoft.EntityFrameworkCore;

namespace NewZap_v2.Models
{
    public class PhoneContext : DbContext
    {
        public PhoneContext(DbContextOptions<PhoneContext> options) : base(options)
        {
        }

        public DbSet<PhoneData> Phones { get; set; } //Phones representa a tabela do banco de dados, DBSet represenata a tabela, e o DBContext represemta o Banco.
    }
}
