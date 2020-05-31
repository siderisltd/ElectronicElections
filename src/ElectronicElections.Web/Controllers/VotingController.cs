using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Models;
using ElectronicElections.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ElectronicElections.Web.Controllers
{
    public class VotingController : Controller
    {
        private readonly ElectionsService electionsService;
        private readonly VoteService voteService;

        public VotingController(ElectionsService electionsService, VoteService voteService)
        {
            this.electionsService = electionsService;
            this.voteService = voteService;
        }

        public ActionResult Vote([FromQuery]ElectionTypeId electionType, Guid politicalPartyId)
        {
            var voterIp = this.HttpContext.Connection.RemoteIpAddress.ToString();

            //TODO: Check whether this IP have voted

            var electionTypeName = string.Empty;
            switch (electionType)
            {
                case ElectionTypeId.NationalAssembly: electionTypeName = "Народно събрание"; break;
                case ElectionTypeId.PresidentalElections: electionTypeName = "Президентски избори"; break;
                case ElectionTypeId.EuropeanParliament: electionTypeName = "Избори за европейски парламент"; break;
                default: throw new InvalidOperationException("Invalid election type");
            }
            var voteModel = new VoteModel 
            {
                PoliticalParty = this.electionsService.GetPoliticalParty(politicalPartyId),
                ElectionType = electionType,
                VoterIp = voterIp,
                ElectionTypeName = electionTypeName
            };

            return View(voteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(VoteModel voteModel)
        {
            if (!this.ModelState.IsValid)
            {
                voteModel.PoliticalParty = this.electionsService.GetPoliticalParty(voteModel.PoliticalPartyId);
                return View(nameof(Vote), voteModel);
            }

            var voterIp = this.HttpContext.Connection.RemoteIpAddress.ToString();
            if(voterIp != voteModel.VoterIp)
            {
                this.ModelState.AddModelError(nameof(VoteModel.VoterIp), "IP то ви не съвпада");
                voteModel.PoliticalParty = this.electionsService.GetPoliticalParty(voteModel.PoliticalPartyId);
                return View(nameof(Vote), voteModel);
            }

            var isSuccess = this.voteService.Vote(voteModel);
            
            //TODO: Add handling
            return RedirectToAction(nameof(ElectionsController.List), "Elections", new { id = voteModel.ElectionType });
        }
    }
}