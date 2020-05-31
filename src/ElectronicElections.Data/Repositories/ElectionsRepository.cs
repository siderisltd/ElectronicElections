using ElectronicElections.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicElections.Data.Repositories
{
    public class ElectionsRepository
    {
        private readonly ElectronicElectionsDbContext ctx;

        public ElectionsRepository(ElectronicElectionsDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<ElectionType> GetElectionTypes()
        {
            return this.ctx.ElectionTypes.ToList();
        }
    }
}
