using FootStat.Core.Data;
using FootStat.Core.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootStat.Api.Models
{
    /*public class CompetitionQuery : ObjectGraphType
    {
        public CompetitionQuery()
        {
            Field<CompetitionType>(
              "ligue1",
              resolve: context => new Competition { Id = 1, Name = "Ligue 1 1994/1995", Country = "France" }
            );
        }
    }*/
    public class CompetitionQuery : ObjectGraphType
    {
        private ICompetitionRepository _competitionRepository { get; set; }

        public CompetitionQuery(ICompetitionRepository _competitionRepository)
        {
            Field<CompetitionType>(
              "ligue1",
              resolve: context => _competitionRepository.Get(1)
            );
        }
    }
}
