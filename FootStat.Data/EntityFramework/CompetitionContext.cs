using FootStat.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootStat.Data.EntityFramework
{
    public class CompetitionContext : DbContext
    {
        public CompetitionContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Competition> Competitions { get; set; }
    }
}
