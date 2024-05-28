using DBMapping.Model;
using Microsoft.EntityFrameworkCore;

namespace DBMapping.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Employee> Employee{ get; set; }
    }
}
