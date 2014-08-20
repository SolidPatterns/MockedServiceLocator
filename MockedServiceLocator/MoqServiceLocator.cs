using System;
using System.Collections.Generic;
using System.Linq;
using MockedServiceLocator.Core.Exceptions;

namespace MockedServiceLocator.Core
{
    public class MoqServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _registeredTypes;

        public MoqServiceLocator()
        {
            _registeredTypes = new Dictionary<Type, object>();
        }

        public T GetService<T>()
        {
            return Get<T>();
        }

        private T Get<T>()
        {
            if (_registeredTypes.ContainsKey(typeof(T)))
                return (T)_registeredTypes.FirstOrDefault(x => x.Key == typeof(T)).Value;

            throw new ServiceNotFoundException(String.Format("Type '{0}' was not registered.", typeof(T)));
        }

        /// <summary>
        /// Instance to register. Behavior: Replaces the current instance if same type is already registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repositoryToRegister"></param>
        /// <returns></returns>
        public InMemoryServiceLocatorFluentRegistratorAs Register<T>(T repositoryToRegister) where T : class
        {
            var registrator = new InMemoryServiceLocatorFluentRegistrator(this);
            return registrator.Register(repositoryToRegister);
        }

        private void Register(RegisterationData data)
        {
            if (_registeredTypes.ContainsKey(data.Type))
            {
                _registeredTypes[data.Type] = data.Instance;
                return;
            }

            _registeredTypes.Add(data.Type, data.Instance);
        }

        public class InMemoryServiceLocatorFluentRegistrator
        {
            private readonly MoqServiceLocator _moqServiceLocator;
            private RegisterationData _registrationData;

            public InMemoryServiceLocatorFluentRegistrator(MoqServiceLocator moqServiceLocator)
            {
                _moqServiceLocator = moqServiceLocator;
            }

            internal InMemoryServiceLocatorFluentRegistratorAs Register<T>(T instanceToRegister) where T : class
            {
                _registrationData = new RegisterationData { Instance = instanceToRegister };
                return new InMemoryServiceLocatorFluentRegistratorAs(_moqServiceLocator, _registrationData);
            }
        }

        public class InMemoryServiceLocatorFluentRegistratorAs
        {
            private readonly MoqServiceLocator _moqServiceLocator;
            private readonly RegisterationData _registrationData;

            public InMemoryServiceLocatorFluentRegistratorAs(MoqServiceLocator moqServiceLocator, RegisterationData registrationData)
            {
                _moqServiceLocator = moqServiceLocator;
                _registrationData = registrationData;
            }

            /// <summary>
            /// Registers the given instance as a user defined interface or class.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public void As<T>() where T : class
            {
                _registrationData.Type = typeof(T);
                _moqServiceLocator.Register(_registrationData);
            }
        }

        public class RegisterationData
        {
            public object Instance { get; set; }
            public Type Type { get; set; }
        }
    }
}
