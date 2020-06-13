using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ElectronicElections.Infrastructure.Services
{
    public class VoteService
    {
        private readonly ElectionsManager electionsManager;

        private readonly ILogger<VoteService> logger;

        public VoteService(ILogger<VoteService> logger, ElectionsManager electionsManager)
        {
            this.electionsManager = electionsManager;
            this.logger = logger;
        }

        public bool Vote(VoteModel voteModel)
        {
            //TODO: Encrypt data

            var voter = new Voter
            {
                FirstName = voteModel.VoterFirstName,
                LastName = voteModel.VoterLastName,
                Age = voteModel.VoterAge,
                Email = voteModel.VoterEmail
            };

            var vote = new Vote
            {
                PoliticalPartyId = voteModel.PoliticalPartyId,
                VotedFromIp = voteModel.VoterIp,
                ElectionsType = voteModel.ElectionType,
                Voter = voter,
                VerificationCode = voteModel.VerificationCode
            };

            try
            {
                this.electionsManager.PostVote(vote);

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public bool Verify(Guid verificationCode)
        {
            return this.electionsManager.Verify(verificationCode);
        }

        public void SendVerificationCode(string to, Guid verificationCode)
        {
            var sendMailThread = new Thread(() => this.SendMail(to, verificationCode));
            sendMailThread.Start();
        }

        private void SendMail(string to, Guid verificationCode, int retryCount = 3)
        {
            try
            {
                var defaultMailAddress = "someorga@gmail.com";
                //TODO: get from vault
                var mailPassword = "someorgA#@!";

                using (var mail = new MailMessage())
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
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, ex.Message);

                if (retryCount != 0)
                {
                    this.logger.LogInformation($"Retrying in a second. Retries left: {retryCount}");
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    this.SendMail(to, verificationCode, retryCount - 1);
                }
                else
                {
                    this.logger.LogError("Sendin mail retried a few times without success");
                }
            }
        }
    }
}
