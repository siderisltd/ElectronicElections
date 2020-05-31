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

        public ActionResult List(ElectionTypeId id)
        {
            this.ViewData["ElectionsType"] = id;
            var politicalParties = this.electionsService.GetPoliticalParties(id);

            return View(politicalParties);
        }
    }
}