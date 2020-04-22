using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyEGE.DAL.Entities
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProblemOption> ProblemOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
