using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var politicalParties = this.electionsService.GetPoliticalParties(id);

            return View(politicalParties);
        }

        public ActionResult Details(Guid id)
        {
            return View();
        }
    }
}