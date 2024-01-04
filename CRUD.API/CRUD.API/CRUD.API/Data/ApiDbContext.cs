using CRUD.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
