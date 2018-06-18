using FootStat.Api.Models;
using FootStat.Data.InMemory;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootStat.Api.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private CompetitionQuery _competitionQuery { get; set; }

        public GraphQLController(CompetitionQuery competitionQuery)
        {
            _competitionQuery = competitionQuery;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var schema = new Schema { Query = _competitionQuery };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;

            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
