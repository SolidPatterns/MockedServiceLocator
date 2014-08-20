namespace MockedServiceLocator.Core
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}