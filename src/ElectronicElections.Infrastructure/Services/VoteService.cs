using ElectronicElections.Data.Managers;

namespace ElectronicElections.Infrastructure.Services
{
    public class VoteService
    {
        private readonly ElectionsManager electionsManager;

        public VoteService(ElectionsManager electionsManager)
        {
            this.electionsManager = electionsManager;
        }
    }
}
