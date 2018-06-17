using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace FootStat.Core.Models
{
    public class CompetitionType : ObjectGraphType<Competition>
    {
        public CompetitionType()
        {
            Field(x => x.Id).Description("The Id of the Competition.");
            Field(x => x.Name, nullable: true).Description("The name of the Competition.");
            Field(x => x.Country, nullable: true).Description("The country of the Competition.");
        }
    }
}
