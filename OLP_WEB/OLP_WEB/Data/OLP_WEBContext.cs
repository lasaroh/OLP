using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLP_WEB.Models;
using SharedModels;

namespace OLP_WEB.Data
{
    public class OLP_WEBContext : DbContext
    {
        public OLP_WEBContext (DbContextOptions<OLP_WEBContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
    }
}
