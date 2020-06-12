using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Models;
using ElectronicElections.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpGet]
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
        public ActionResult Post(VoteModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.PoliticalParty = this.electionsService.GetPoliticalParty(model.PoliticalPartyId);
                return View(nameof(Vote), model);
            }

            var voterIp = this.HttpContext.Connection.RemoteIpAddress.ToString();
            if (voterIp != model.VoterIp)
            {
                this.ModelState.AddModelError(nameof(VoteModel.VoterIp), "IP то ви не съвпада");
                model.PoliticalParty = this.electionsService.GetPoliticalParty(model.PoliticalPartyId);
                return View(nameof(Vote), model);
            }

            model.VerificationCode = Guid.NewGuid();
            var isSuccess = this.voteService.Vote(model);

            if (isSuccess)
            {
                this.SendVerificationCode(model.VoterEmail, model.VerificationCode);
                return RedirectToAction(nameof(this.Verification));
            }
            else
            {
                return RedirectToAction(nameof(this.Failure));
            }
        }

        public ActionResult Verification()
        {
            return this.View(new VerifyVoteModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(VerifyVoteModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(nameof(this.Verification), model);
                }

                var isVerified = this.voteService.Verify(Guid.Parse(model.Code));

                if (isVerified)
                {
                    TempData["Verified"] = true;
                    return RedirectToAction(nameof(this.Verification), null);
                }

                this.ModelState.AddModelError(nameof(model.Code), "Кодът ви е невалиден. Моля, опитайте отново");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(nameof(model.Code), "Кодът ви е невалиден. Моля, опитайте отново");
            }

            return this.View(nameof(this.Verification), model);
        }

        public ActionResult Failure()
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }

        private void SendVerificationCode(string to, Guid verificationCode)
        {
            new Thread(() => 
            {
                var defaultMailAddress = "someorga@gmail.com";
                //TODO: get from vault
                var mailPassword = "someorgA#@!";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(defaultMailAddress);
                    mail.To.Add(to);
                    mail.Subject = "Електронно гласуване";
                    mail.Body = $"Благодарим за гласа ви! За да потвърдите гласа си, моля копирайте този код в полето за валидиране на гласа ви: {verificationCode}";
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(defaultMailAddress, mailPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }).Start();
           
        }
    }
}