using FootStat.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootStat.Core.Models;
using System.Linq;

namespace FootStat.Data.InMemory
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private List<Competition> _competitions = new List<Competition> {
            new Competition { Id = 1, Name = "Liga 2017/2018", Country = "Spain" }
        };

        public Task<Competition> Get(int id)
        {
            return Task.FromResult(_competitions.FirstOrDefault(competition => competition.Id == id));
        }
    }
}
