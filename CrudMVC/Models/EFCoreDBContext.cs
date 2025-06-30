using Microsoft.EntityFrameworkCore;

namespace CrudMVC.Models
{
    public class EFCoreDBContext : DbContext
    {
        public EFCoreDBContext(DbContextOptions<EFCoreDBContext> options) : base(options)
        {
        }

        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //Adding Domain Classes as DbSet Properties
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    


}
}
