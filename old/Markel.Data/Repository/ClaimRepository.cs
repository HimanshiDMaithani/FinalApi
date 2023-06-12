using Markel.Data.Interface;
using Markel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Data.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly List<Claim> _claims;

        public ClaimRepository()
        {
            _claims = new List<Claim>();
            // Added some example claims
            _claims.Add(new Claim
            {
                UCR = "5000",
                CompanyId = 1,
                ClaimDate = (DateTime.Now.AddDays(-10)).Date,
                LossDate = (DateTime.Now.AddDays(-15).Date),
                AssuredName = "TestName1",
                IncurredLoss = 1000.11M,
                Closed = false
            });
            _claims.Add(new Claim
            {
                UCR = "4000",
                CompanyId = 1,
                ClaimDate = (DateTime.Now.AddDays(-5)).Date,
                LossDate = (DateTime.Now.AddDays(-12)).Date,
                AssuredName = "TestName2",
                IncurredLoss = 2000.12M,
                Closed = true
            });
            _claims.Add(new Claim
            {
                UCR = "5000",
                CompanyId = 2,
                ClaimDate = (DateTime.Now.AddDays(-10)).Date,
                LossDate = (DateTime.Now.AddDays(-15)).Date,
                AssuredName = "TestName3",
                IncurredLoss = 1000.11M,
                Closed = false
            });
            _claims.Add(new Claim
            {
                UCR = "4000",
                CompanyId = 2,
                ClaimDate = (DateTime.Now.AddDays(-5)).Date,
                LossDate = (DateTime.Now.AddDays(-12)).Date,
                AssuredName = "TestName4",
                IncurredLoss = 2000.22M,
                Closed = true
            });

        }

        public Claim GetClaim(string assuredName)
        {
            return _claims.FirstOrDefault(c => c.AssuredName == assuredName);
        }

        public List<Claim> GetClaimsByCompanyId(int companyId)
        {
            return _claims.Where(c => c.CompanyId == companyId).ToList();
        }

        public void UpdateClaim(Claim claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.AssuredName == claim.AssuredName);
            if (existingClaim != null)
            {
                existingClaim.UCR = claim.UCR;
                existingClaim.ClaimDate = claim.ClaimDate;
                existingClaim.LossDate = claim.LossDate;
                existingClaim.IncurredLoss = claim.IncurredLoss;
                existingClaim.Closed = claim.Closed;
            }
        }
    }
}
