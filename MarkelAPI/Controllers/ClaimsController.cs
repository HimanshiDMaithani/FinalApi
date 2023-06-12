using Markel.Data.Interface;
using Markel.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimRepository _claimRepository;
        private readonly ICompanyRepository _companyRepository;

        public ClaimsController(IClaimRepository claimRepository, ICompanyRepository companyRepository)
        {
            _claimRepository = claimRepository;
            _companyRepository = companyRepository;
        }

        [HttpGet("company/{companyId}")]
        public IActionResult GetCompany(int companyId)
        {
            var company = _companyRepository.GetCompany(companyId);
            if (company == null)
                return NotFound();

            var hasActiveInsurancePolicy = _companyRepository.HasActiveInsurancePolicy(companyId);
            var response = new
            {
                Company = company,
                HasActiveInsurancePolicy = hasActiveInsurancePolicy
            };

            return Ok(response);
        }

        [HttpGet("company/{companyId}/claims")]
        public IActionResult GetClaimsByCompany(int companyId)
        {
            var claims = _claimRepository.GetClaimsByCompanyId(companyId);
            return Ok(claims);
        }

        [HttpGet("claim/{assuredName}")]
        public ActionResult GetClaim(string assuredName)
        {
            var claim = _claimRepository.GetClaim(assuredName);
            if (claim == null)
                return NotFound();

            var daysOld = (DateTime.UtcNow - claim.ClaimDate).Days;
            var response = new
            {
                Claim = claim,
                DaysOld = daysOld
            };

            return Ok(response);
        }

        [HttpPut("claim/{assuredName}")]
        public IActionResult UpdateClaim(string assuredName, Claim updatedClaim)
        {
            var existingClaim = _claimRepository.GetClaim(assuredName);
            if (existingClaim == null)
                return NotFound();

            existingClaim.Closed = updatedClaim.Closed;
            _claimRepository.UpdateClaim(existingClaim);

            return Ok("Success");
        }
    }
}
