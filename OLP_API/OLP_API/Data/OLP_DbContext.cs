using Microsoft.EntityFrameworkCore;
using SharedModels;

namespace OLP_API.Data
{
	public class OLP_DbContext : DbContext
    {
        public OLP_DbContext (DbContextOptions<OLP_DbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
		public DbSet<Course> Course { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Lesson> Lesson { get; set; }
	}
}
