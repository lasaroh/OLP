using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLP_API.Models;

namespace OLP_API.Data
{
    public class OLP_DbContext : DbContext
    {
        public OLP_DbContext (DbContextOptions<OLP_DbContext> options)
            : base(options)
        {
        }

        public DbSet<OLP_API.Models.User> User { get; set; } = default!;
    }
}
