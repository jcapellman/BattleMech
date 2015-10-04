﻿using System.Collections.Generic;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.DataLayer.PCL.Views.GameMetrics;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class GameMetricHandler : BaseHandler {
        public GameMetricHandler(HandlerItem handlerItem) : base(handlerItem, "GameMatric") { }

        public async Task<CTI<bool>> AddGameMetric(PlayerCharacterGames gameMetric) { return await PUT<PlayerCharacterGames, bool>(gameMetric); }

        public async Task<CTI<List<PlayerGameListingView>>> GetGameMetrics() { return await GET<CTI<List<PlayerGameListingView>>>(); }
    }
}