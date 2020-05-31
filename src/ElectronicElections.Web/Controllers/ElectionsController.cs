using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicElections.Web.Controllers
{
    public class ElectionsController : Controller
    {
        private readonly ElectionsService electionsService;

        public ElectionsController(ElectionsService electionsService)
        {
            this.electionsService = electionsService;
        }

        public ActionResult List(ElectionTypeId electionType)
        {
            var politicalParties = this.electionsService.GetPoliticalParties(electionType);

            return View(politicalParties);
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}