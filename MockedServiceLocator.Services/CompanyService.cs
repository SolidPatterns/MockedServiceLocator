using System;
using System.Collections.Generic;
using System.Linq;
using MockedServiceLocator.Core;
using MockedServiceLocator.Models;

namespace MockedServiceLocator.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IServiceLocator _serviceLocator;

        public CompanyService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companyRepository =
                _serviceLocator.GetService<ICompanyRepository>();

            return companyRepository.ToList();
        }

        public Company GetCompany(int id)
        {
            var companyRepository =
                _serviceLocator.GetService<ICompanyRepository>();

            return companyRepository.Where(x => x.Id.Equals(id)).First();
        }

        public void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("company");

            var companyRepository = _serviceLocator.GetService<ICompanyRepository>();

            companyRepository.Create(company);
            companyRepository.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("company");

            var companyRepository = _serviceLocator.GetService<ICompanyRepository>();

            companyRepository.Update(company);
            companyRepository.SaveChanges();
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("company");

            var companyRepository = _serviceLocator.GetService<ICompanyRepository>();

            companyRepository.Delete(company);
            companyRepository.SaveChanges();
        }
    }
}
