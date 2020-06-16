using ElectronicElections.Data.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ElectronicElections.Data.Managers
{
    public static class DataSeedManager
    {
        public static void Seed(ElectronicElectionsDbContext ctx)
        {
            if (!ctx.ElectionTypes.Any())
            {
                var electionTypes = GetAllElectionTypes();
                ctx.ElectionTypes.AddRange(electionTypes);
                ctx.SaveChanges();
            }

            if (!ctx.Candidates.Any())
            {
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                DataTable table;
                using (var stream = File.Open("../ElectronicElections.Data/TestData/Candidates.xlsx", FileMode.Open, FileAccess.Read))
                {
                    using var reader = ExcelReaderFactory.CreateReader(stream);
                    var spreadSheet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    table = spreadSheet.Tables[0];
                }

                var candidates = ExtractCandidates(table);

                ctx.Candidates.AddRange(candidates);
                ctx.SaveChanges();
            }

        }

        private static List<Candidate> ExtractCandidates(DataTable table)
        {
            var candidates = new List<Candidate>();

            foreach (DataRow row in table.Rows)
            {
                var candidateType = (CandidateTypeId)Enum.Parse(typeof(CandidateTypeId), row[5].ToString());

                var currentCandidate = new Candidate
                {
                    Name = row[0].ToString(),
                    Description = row[1].ToString(),
                    WikiLink = row[2].ToString(),
                    Goals = row[3].ToString(),
                    ImgLink = row[4].ToString(),
                    CandidateType = candidateType
                };


                var electionTypes = row[6].ToString()
                    .Split(new char[] { ',' })
                    .Select(x =>
                    {
                        var res = new CandidateElectionType();
                        res.ElectionTypeId = (ElectionTypeId)Enum.Parse(typeof(ElectionTypeId), x);

                        return res;
                    }).ToList();

                currentCandidate.ParticipantInElections = electionTypes;

                candidates.Add(currentCandidate);
            }

            return candidates;
        }

        private static IEnumerable<ElectionType> GetAllElectionTypes()
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
    }
}
