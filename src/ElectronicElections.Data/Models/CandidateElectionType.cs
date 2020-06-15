using System;

namespace ElectronicElections.Data.Models
{
    public class CandidateElectionType
    {
        public Guid CandidateId { get; set; }

        public ElectionTypeId ElectionTypeId { get; set; }

        public virtual ElectionType ElectionType { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
