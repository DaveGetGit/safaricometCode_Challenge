using Microsoft.EntityFrameworkCore;
using StudentRegistrarApi.Models;

namespace StudentRegistrarApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            //options.UseInMemoryDatabase("TestStudentDb");
        }

        public DbSet<Student> Students { get; set; }
    }
}
