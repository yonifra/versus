using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versus.Common.Enums;

namespace Versus.Common.Base
{
    public interface IVsCompetition
    {
        DateTime GetEndingDate();
        bool UpdateVoteCount(int entityId, VoteType voteType, int userId);
        IVsEntity GetLeadingEntity();
    }
}
