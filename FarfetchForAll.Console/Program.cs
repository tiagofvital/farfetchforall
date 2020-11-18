namespace FarfetchForAll.ConsoleApp
{
    using System;
    using SimpleInjector;

    internal class Program
    {
        private Random random = new Random();

        private static void Main(string[] args)
        {
            var container = new Container();

            new Startup()
                .ConfigureDependencies(container)
                .Run(container);
        }
    }
}