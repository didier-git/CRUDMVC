using CRUDMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDMVC.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Compra> Compras { get; set; }



    }
}
