namespace MockedServiceLocator.Core
{
    public static class IoCEngineContext
    {
        public static IServiceLocator Current { get; set; }
        public static void Initialize(IServiceLocator locator)
        {
            Current = locator;
        }

        public static T Resolve<T>()
        {
            return Current.GetService<T>();
        }
    }
}