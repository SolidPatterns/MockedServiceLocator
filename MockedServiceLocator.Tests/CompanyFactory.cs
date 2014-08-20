using System.Collections.Generic;
using MockedServiceLocator.Models;

namespace MockedServiceLocator.Tests
{
    public static class CompanyFactory
    {
        public static IEnumerable<Company> Create()
        {
            yield return
                new Company
                {
                    Name = "Company1",
                    Id = 1,
                };

            yield return
                new Company
                {
                    Name = "Company2",
                    Id = 2,
                };

            yield return
                new Company
                {
                    Name = "Company3",
                    Id = 3,
                };
        }
    }
}