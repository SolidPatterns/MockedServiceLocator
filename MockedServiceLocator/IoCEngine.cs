using System;
using MockedServiceLocator.Core;

namespace MockedServiceLocator.Core
{
    public class IoCEngine : IServiceLocator
    {
        public T GetService<T>()
        {
            //your actual locator implementation
            throw new NotImplementedException();
        }
    }
}