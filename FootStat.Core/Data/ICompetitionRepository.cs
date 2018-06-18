using FootStat.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootStat.Core.Data
{
    public interface ICompetitionRepository
    {
        Task<Competition> Get(int id);
    }
}
