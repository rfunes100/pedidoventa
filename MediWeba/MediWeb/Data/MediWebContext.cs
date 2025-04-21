using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediWeb.Models;

namespace MediWeb.Data
{
    public class MediWebContext : DbContext
    {
        public MediWebContext (DbContextOptions<MediWebContext> options)
            : base(options)
        {
        }

        public DbSet<MediWeb.Models.EnfermedadesModel> EnfermedadesModel { get; set; } = default!;
    }
}
