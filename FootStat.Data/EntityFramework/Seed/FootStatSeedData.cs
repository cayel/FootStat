using FootStat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootStat.Data.EntityFramework.Seed
{
    public static class FootStatSeedData
    {
        public static void EnsureSeedData(this CompetitionContext db)
        {
            if (!db.Competitions.Any())
            {
                var competition = new Competition
                {
                    Name = "Liga 2017/2018",
                    Country = "Spain"
                };
                db.Competitions.Add(competition);
                db.SaveChanges();
            }
        }
    }
}
