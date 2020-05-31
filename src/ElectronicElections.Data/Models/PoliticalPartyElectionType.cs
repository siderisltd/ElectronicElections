using System;

namespace ElectronicElections.Data.Models
{
    public class PoliticalPartyElectionType
    {
        public Guid PoliticalPartyId { get; set; }

        public ElectionTypeId ElectionTypeId { get; set; }

        public virtual ElectionType ElectionType { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }
    }
}
