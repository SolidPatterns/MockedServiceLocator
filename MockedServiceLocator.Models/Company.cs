using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockedServiceLocator.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; } 
    }
}
