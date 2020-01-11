using System.Collections.Generic;
using System.Runtime.Serialization;

using BattleMech.DataLayer.PCL.Views.GameMetrics;

namespace BattleMech.WebAPI.PCL.Transports.CharacterProfile {
    [DataContract]
    public class CharacterProfileResponseItem {
        [DataMember]
        public List<PlayerGameListingView> GameHistory { get; set; }

        [DataMember]
        public PlayerCharacterView Character { get; set; }
    }
}