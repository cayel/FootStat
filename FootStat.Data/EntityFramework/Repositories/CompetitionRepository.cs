using FootStat.Core.Data;
using FootStat.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootStat.Data.EntityFramework.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private CompetitionContext _db { get; set; }

        public CompetitionRepository(CompetitionContext db)
        {
            _db = db;
        }

        public Task<Competition> Get(int id)
        {
            return _db.Competitions.FirstOrDefaultAsync(droid => droid.Id == id);
        }
    }
}
