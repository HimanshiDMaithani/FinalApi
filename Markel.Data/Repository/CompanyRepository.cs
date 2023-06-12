using Markel.Data.Interface;
using Markel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly List<Company> _companies;

        public CompanyRepository()
        {
            _companies = new List<Company>();
            // Added some example companies
            _companies.Add(new Company
            {
                Id = 1,
                Name = "Test Company",
                Address1 = "123 Main Street",
                Address2 = "",
                Address3 = "",
                Postcode = "XX1 1YY",
                Country = "UK",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(30)
            });
            _companies.Add(new Company
            {
                Id = 2,
                Name = "Test Company2",
                Address1 = "1234 Main Street",
                Address2 = "",
                Address3 = "",
                Postcode = "XX2 2YY",
                Country = "UK",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(60)
            });
            _companies.Add(new Company
            {
                Id = 3,
                Name = "Test Company3",
                Address1 = "12345 Main Street",
                Address2 = "",
                Address3 = "",
                Postcode = "XX3 3YY",
                Country = "UK",
                Active = false,
                InsuranceEndDate = DateTime.Now.AddDays(10)
            });
        }

        public Company GetCompany(int companyId)
        {
            return _companies.FirstOrDefault(c => c.Id == companyId);
        }

        public bool HasActiveInsurancePolicy(int companyId)
        {
            var company = _companies.FirstOrDefault(c => c.Id == companyId);
            if (company == null)
                return false;

            return company.Active && company.InsuranceEndDate >= DateTime.UtcNow;
        }
    }
}
