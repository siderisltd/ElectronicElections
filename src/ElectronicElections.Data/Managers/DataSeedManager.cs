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

        public DataSeedManager()
        {
            this.gerbId = Guid.NewGuid();
            this.dpsId = Guid.NewGuid();
            this.atakaId = Guid.NewGuid();
        }

        internal void SeedElectionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectionType>().HasData(this.GetAllElectionTypes());
        }

        internal void SeedElectionPoliticalParties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoliticalParty>().HasData(this.GetAllPoliticalParties());
        }

        internal void SeedPoliticalPartyElectionTypeRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoliticalPartyElectionType>().HasData(this.GetAllPoliticalPartyElectionTypes());
        }

        internal void SeedPoliticians(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Politician>().HasData(this.GetAllPoliticians());
        }

        private IEnumerable<ElectionType> GetAllElectionTypes()
        {
            var electionTypes = new List<ElectionType>
            {
                new ElectionType
                {
                    Id = ElectionTypeId.NationalAssembly,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.NationalAssembly),
                    Description = "Some description",
                    WikiLink = "https://google.com"
                },
                new ElectionType
                {
                    Id = ElectionTypeId.PresidentalElections,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.PresidentalElections),
                    Description = "Some description",
                    WikiLink = "https://google.com"
                },
                new ElectionType
                {
                    Id = ElectionTypeId.EuropeanParliament,
                    Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.EuropeanParliament),
                    Description = "Some description",
                    WikiLink = "https://google.com"
                }
            };

            return electionTypes;
        }

        private IEnumerable<Politician> GetAllPoliticians()
        {
            return new List<Politician>
            {
                new Politician
                {
                    Id = Guid.NewGuid(),
                    PoliticalPartyId = this.gerbId,
                    FirstName = "Бойко",
                    LastName = "Борисов",
                    Description = "Хомосексуалист, който ограбва държавата. Мафиот",
                    Age = 50,
                    WikiLink = "https://google.com"
                },
                new Politician
                {
                    Id = Guid.NewGuid(),
                    PoliticalPartyId = this.gerbId,
                    FirstName = "Тест",
                    LastName = "Тест 2",
                    Description = "Тест. Мафиот 2",
                    Age = 40,
                    WikiLink = "https://google.com"
                },
                new Politician
                {
                    Id = Guid.NewGuid(),
                    PoliticalPartyId = this.dpsId,
                    FirstName = "Тест дпс фн",
                    LastName = "Тест дпс лн",
                    Description = "Описание дпс",
                    Age = 20,
                    WikiLink = "https://google.com"
                },
                new Politician
                {
                    Id = Guid.NewGuid(),
                    PoliticalPartyId = this.dpsId,
                    FirstName = "Тест дпс 2 фн",
                    LastName = "Тест дпс 2 лн",
                    Description = "Тест. Описание 2",
                    Age = 30,
                    WikiLink = "https://google.com"
                },
                new Politician
                {
                    Id = Guid.NewGuid(),
                    PoliticalPartyId = this.atakaId,
                    FirstName = "Волен",
                    LastName = "Сидеров",
                    Description = "Активист",
                    Age = 60,
                    WikiLink = "https://google.com"
                }
            };
        }

        private IEnumerable<PoliticalParty> GetAllPoliticalParties()
        {
            var politicalParties = new List<PoliticalParty>
            {
                new PoliticalParty()
                {
                    Id = this.gerbId,
                    Name = "ГЕРБ",
                    Description = "ГЕРБ е дясноцентристка, популистка, консервативна и проевропейска политическа партия в България. Тя е основана на 3 декември 2006 г. по инициатива на кмета на София Бойко Борисов, на основата на създаденото по-рано през същата година гражданско сдружение с име „Граждани за европейско развитие на България“ и абревиатура „ГЕРБ“.[6] Централата на партията се намира в Националния дворец на културата, на площад „България“ №1 в.",
                    WikiLink = "https://bg.wikipedia.org/wiki/ГЕРБ",
                    Goals = "Унищожаване на Българската икономика и популация. Собствена изгода",
                    LogoLink = "https://lh3.googleusercontent.com/proxy/4zqzPSm22DJIQ7pdaLVRjJXVfqVXFhx8kaIft_KMNK6S-dVFY7tA0Y4fTUBEvPVMxesXAWYe5P_gID2ANJ8Jb_LwtguNavWm"
                },
                new PoliticalParty()
                {
                    Id = this.dpsId,
                    Name = "ДПС",
                    Description = "Движението за права и свободи (ДПС) е центристка политическа партия в България, ползваща се с подкрепата главно на етническите турци и други мюсюлмани в България, определяща се като либерална партия и член на Либералния интернационал. ДПС е определяно като един от основните поддръжници на олигархичния модел на държавно управление.[1]",
                    WikiLink = "https://bg.wikipedia.org/wiki/ДПС",
                    Goals = "Голове тест 123",
                    LogoLink = "https://lh3.googleusercontent.com/proxy/XUQdtdclVg27SSLJxbP1-6An0aSnvFFmwWqZLbf5RhVY-mcxtPrTHYuQmiJu5_jqVPTutOr1OfaR6hZMk9vb2AYe09aqfDRqtkV3Tx3gP72wQHU"
                },
                new PoliticalParty()
                {
                    Id = this.atakaId,
                    Name = "Атака",
                    Description = "„Атака“ е политическа партия в България[2][3], която използва популистки послания, за да спечели симпатии от избирателите.[4] Според някои мнения „Атака“ е крайнодясна партия[1], според други – крайнолява.[5] Заема проруски позиции.[6]",
                    WikiLink = "https://bg.wikipedia.org/wiki/Атака_(партия)",
                    Goals = "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.",
                    LogoLink = "https://pia-news.com/wp-content/uploads/2015/09/ataka_logo.gif"
                }
            };

            return politicalParties;
        }

        private IEnumerable<PoliticalPartyElectionType> GetAllPoliticalPartyElectionTypes()
        {
            var politicalPartyElectionTypes = new List<PoliticalPartyElectionType>
            {
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    PoliticalPartyId = this.gerbId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    PoliticalPartyId = this.gerbId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.PresidentalElections,
                    PoliticalPartyId = this.gerbId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    PoliticalPartyId = this.atakaId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    PoliticalPartyId = this.atakaId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.NationalAssembly,
                    PoliticalPartyId = this.dpsId
                },
                new PoliticalPartyElectionType
                {
                    ElectionTypeId = ElectionTypeId.EuropeanParliament,
                    PoliticalPartyId = this.dpsId
                }
            };

            return politicalPartyElectionTypes;
        }
    }
}
