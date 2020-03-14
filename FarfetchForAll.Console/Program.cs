namespace FarfetchForAll.ConsoleApp
{
    using SimpleInjector;
    internal class Program
    {        private static void Main(string[] args)
        {
            var container = new Container();

            new Startup()
                .ConfigureAggregateInfo()
                .ConfigureDependencies(container)
                .Run(container);
        }
    }
}