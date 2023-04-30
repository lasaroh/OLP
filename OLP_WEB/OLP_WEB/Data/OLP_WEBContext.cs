using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLP_WEB.Models;

namespace OLP_WEB.Data
{
    public class OLP_WEBContext : DbContext
    {
        public OLP_WEBContext (DbContextOptions<OLP_WEBContext> options)
            : base(options)
        {
        }

        public DbSet<OLP_WEB.Models.User> User { get; set; } = default!;
    }
}
