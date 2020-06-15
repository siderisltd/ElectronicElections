using ElectronicElections.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ElectronicElections.Data.Managers
{
    internal class DataSeedManager
    {
        private readonly Guid gerbId;
        private readonly Guid dpsId;
        private readonly Guid atakaId;
        private readonly Guid volenId;

        public DataSeedManager()
        {
            this.gerbId = Guid.NewGuid();
            this.dpsId = Guid.NewGuid();
            this.atakaId = Guid.NewGuid();
            this.volenId = Guid.NewGuid();
        }

        internal void SeedElectionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectionType>().HasData(this.GetAllElectionTypes());
        }

        internal void SeedCandidates(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasData(this.GetAllCandidates());
        }

        internal void SeedCandidateElectionTypeRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateElectionType>().HasData(this.GetAllCandidateElectionTypes());
        }

        private IEnumerable<ElectionType> GetAllElectionTypes()
        {
            var electionTypes = new List<ElectionType>
            {
                new ElectionType
                {
                    Id = ElectionTypeId.NationalAssembly,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.NationalAssembly),
                    Description = "Избори за народно събрание",
                    WikiLink = "https://bg.wikipedia.org/wiki/Избори_в_България#За_народно_събрание"
                },
                new ElectionType
                {
                    Id = ElectionTypeId.PresidentalElections,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.PresidentalElections),
                    Description = "Президентски избори",
                    WikiLink = "https://bg.wikipedia.org/wiki/Избори_в_България#За_президент"
                },
                new ElectionType
                {
                    Id = ElectionTypeId.EuropeanParliament,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.EuropeanParliament),
                    Description = "Избори за европейски парламент",
                    WikiLink = "https://bg.wikipedia.org/wiki/Избори_в_България#За_европейски_парламент"
                }
            };

            return electionTypes;
        }

        private IEnumerable<Candidate> GetAllCandidates()
        {
            var candidates = new List<Candidate>
            {
                new Candidate()
                {
                    Id = this.gerbId,
                    Name = "ГЕРБ",
                    Description = "ГЕРБ е дясноцентристка, популистка, консервативна и проевропейска политическа партия в България. Тя е основана на 3 декември 2006 г. по инициатива на кмета на София Бойко Борисов, на основата на създаденото по-рано през същата година гражданско сдружение с име „Граждани за европейско развитие на България“ и абревиатура „ГЕРБ“.[6] Централата на партията се намира в Националния дворец на културата, на площад „България“ №1 в.",
                    WikiLink = "https://m.netinfo.bg/media/images/34784/34784806/991-ratio-kotka-kuche.jpg",
                    Goals = "Унищожаване на Българската икономика и популация. Собствена изгода",
                    ImgLink = "https://m.netinfo.bg/media/images/34784/34784806/991-ratio-kotka-kuche.jpg",
                    CandidateType = CandidateTypeId.PoliticalParty
                },
                new Candidate()
                {
                    Id = this.dpsId,
                    Name = "ДПС",
                    Description = "Движението за права и свободи (ДПС) е центристка политическа партия в България, ползваща се с подкрепата главно на етническите турци и други мюсюлмани в България, определяща се като либерална партия и член на Либералния интернационал. ДПС е определяно като един от основните поддръжници на олигархичния модел на държавно управление.[1]",
                    WikiLink = "https://bg.wikipedia.org/wiki/ДПС",
                    Goals = "Голове тест 123",
                    ImgLink = "https://m5.netinfo.bg/media/images/15946/15946663/896-504-kuche-i-kote.jpg",
                    CandidateType = CandidateTypeId.PoliticalParty
                },
                new Candidate()
                {
                    Id = this.atakaId,
                    Name = "Атака",
                    Description = "„Атака“ е политическа партия в България[2][3], която използва популистки послания, за да спечели симпатии от избирателите.[4] Според някои мнения „Атака“ е крайнодясна партия[1], според други – крайнолява.[5] Заема проруски позиции.[6]",
                    WikiLink = "https://bg.wikipedia.org/wiki/Атака_(партия)",
                    Goals = "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.",
                    ImgLink = "https://m.netinfo.bg/media/images/32905/32905551/991-ratio-kotki-i-kucheta.jpg",
                    CandidateType = CandidateTypeId.PoliticalParty
                },
                new Candidate()
                {
                    Id = this.volenId,
                    Name = "Волен Сидеров",
                    Description = "Тест инфо",
                    WikiLink = "https://bg.wikipedia.org/wiki/Волен_Сидеров",
                    Goals = "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.",
                    ImgLink = "https://static.framar.bg/thumbs/6/lifestyle/usmivka-kuche.png",
                    CandidateType = CandidateTypeId.IndependentPolitician
                }
            };

            return candidates;
        }

        private IEnumerable<CandidateElectionType> GetAllCandidateElectionTypes()
        {
            var candidateElectionType = new List<CandidateElectionType>
            {
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    CandidateId = this.gerbId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    CandidateId = this.gerbId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.PresidentalElections,
                    CandidateId = this.gerbId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    CandidateId = this.atakaId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    CandidateId = this.atakaId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    CandidateId = this.dpsId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    CandidateId = this.dpsId
                },
                new CandidateElectionType
                {
                    ElectionTypeId = ElectionTypeId.PresidentalElections,
                    CandidateId = this.volenId
                }
            };

            return candidateElectionType;
        }
    }
}
