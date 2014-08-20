using System.Collections.Generic;
using MockedServiceLocator.Models;

namespace MockedServiceLocator.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies();
        Company GetCompany(int id);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
    }
}