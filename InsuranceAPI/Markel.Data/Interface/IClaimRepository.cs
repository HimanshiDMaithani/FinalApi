using Markel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Data.Interface
{
     public interface IClaimRepository
    {
        Claim GetClaim(string assuredName);
        List<Claim> GetClaimsByCompanyId(int companyId);
        void UpdateClaim(Claim claim);
    }
}
