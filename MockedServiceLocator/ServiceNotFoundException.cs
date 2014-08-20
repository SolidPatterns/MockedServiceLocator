using System;

namespace MockedServiceLocator.Core.Exceptions
{
    public class ServiceNotFoundException : ApplicationException
    {
        private readonly string _service;

        public ServiceNotFoundException(string service)
        {
            _service = service;
        }

        public override string Message
        {
            get { return String.Format("{0} service is not registered.", _service); }
        }
    }
}