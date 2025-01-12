﻿using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IMatchService : IBaseService<Match>
    {
        public bool AlreadyReserved(int fieldId, string matchDate);

        public Match? GetOwnedBy(int matchId, int playerId);
    }
}
