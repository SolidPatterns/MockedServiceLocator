using System.Collections.Generic;
using System.Linq;
using MockedServiceLocator.Core;
using MockedServiceLocator.Models;
using MockedServiceLocator.Services;
using Moq;
using NUnit.Framework;

namespace MockedServiceLocator.Tests
{
    [TestFixture]
    public class CompanyServiceWithoutServiceLocatorInjectedShould
    {
        private List<Company> _companies;
        private Mock<ICompanyRepository> _companyRepository;
        private MoqServiceLocator _moqServiceLocator;

        [SetUp]
        public void Initialize()
        {
            _companies = CompanyFactory.Create().ToList();

            _companyRepository = new Mock<ICompanyRepository>(MockBehavior.Strict);
            _moqServiceLocator = new MoqServiceLocator();
            IoCEngineContext.Initialize(_moqServiceLocator);
        }

        [Test]
        public void ReturnCompaniesWhenCalled_GetAllCompanies()
        {
            _companyRepository.Setup(x => x.ToList()).Returns(_companies);
            _moqServiceLocator.Register(_companyRepository.Object).As<ICompanyRepository>();
            var companyService = new CompanyServiceWithoutServiceLocatorInjected();
            var companies = companyService.GetAllCompanies().ToList();

            Assert.IsTrue(companies.Any());
            Assert.AreEqual(companies.Count(), _companies.Count);
        }

        [Test]
        public void DeleteGivenCompanyWhenCalled_DeleteCompany()
        {
            var localCompanies = CompanyFactory.Create().ToList();
            var companyCountBeforeDeletion = localCompanies.Count;

            var companyToDelete = localCompanies.FirstOrDefault(x => x.Id == 1);
            _companyRepository.Setup(x => x.Delete(companyToDelete)).Callback(() => localCompanies.Remove(localCompanies.FirstOrDefault(x => x.Equals(companyToDelete))));

            var isSaved = false;
            _companyRepository.Setup(x => x.SaveChanges()).Callback(() => isSaved = true);

            _moqServiceLocator.Register(_companyRepository.Object).As<ICompanyRepository>();

            var companyService = new CompanyService(_moqServiceLocator);
            companyService.DeleteCompany(companyToDelete);

            _companyRepository.Verify(x => x.SaveChanges(), Times.Once);
            _companyRepository.Verify(x => x.Delete(companyToDelete), Times.Once);
            _companyRepository.VerifyAll();


            var companyCountAfterDeletion = localCompanies.Count;

            Assert.IsTrue(isSaved);
            Assert.AreEqual(companyCountBeforeDeletion, companyCountAfterDeletion + 1);
            Assert.IsFalse(localCompanies.Any(x => x.Equals(companyToDelete)));
        }

        [Test]
        public void CompanyServiceInsertCompanyMethodTest()
        {
            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(r => r.AddCompany(It.IsAny<Company>()));
            companyServiceMock.Object.AddCompany(It.IsAny<Company>());
            companyServiceMock.Verify(c => c.AddCompany(It.IsAny<Company>()), Times.Once);
        }

        [Test]
        public void CompanyServiceUpdateCompanyMethodTest()
        {
            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(r => r.UpdateCompany(It.IsAny<Company>()));
            companyServiceMock.Object.UpdateCompany(It.IsAny<Company>());
            companyServiceMock.Verify(c => c.UpdateCompany(It.IsAny<Company>()), Times.Once);
        }

    }
}