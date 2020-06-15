using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Mapping.Contracts;
using System;
using System.ComponentModel;

namespace ElectronicElections.Infrastructure.Models
{
    public class CandidateModel : IMapFrom<Candidate>
    {
        public Guid Id { get; set; }

        [DisplayName("Име")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        public string ImgLink { get; set; }

        [DisplayName("Wikipedia връзка за повече информация")]
        public string WikiLink { get; set; }
    }
}
