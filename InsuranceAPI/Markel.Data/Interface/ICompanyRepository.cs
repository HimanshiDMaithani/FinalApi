using Markel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Data.Interface
{
    public interface ICompanyRepository
    {
        Company GetCompany(int companyId);
        bool HasActiveInsurancePolicy(int companyId);
    }
}
