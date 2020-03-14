namespace FarfetchForAll.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FarfetchForAll.Simulator.Controllers;
    using FarfetchForAll.Simulator.Handlers;
    using FarfetchForAll.Simulator.Mediator;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.RequestHandlers;
    using MediatR;
    using MediatR.Pipeline;
    using SimpleInjector;

    public partial class Startup
    {
        public Startup ConfigureDependencies(Container container)
        {
            container.RegisterSingleton<SharesRepository>();
            container.RegisterSingleton<ShareMovementsRepository>();

            container.Register<ShareHandler>();
            container.Register<ShareMovementsHandler>();

            container.Register<ShareControllers>();

            var assemblies = GetAssemblies().ToArray();

            container.RegisterSingleton<IMediator, Mediator>();

            container.Register(typeof(IRequestHandler<,>), assemblies);

            RegisterHandlers(container, typeof(INotificationHandler<>), assemblies);
            RegisterHandlers(container, typeof(IRequestExceptionAction<,>), assemblies);
            RegisterHandlers(container, typeof(IRequestExceptionHandler<,,>), assemblies);

            //Pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestExceptionProcessorBehavior<,>),
                typeof(RequestExceptionActionProcessorBehavior<,>),
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
                typeof(GenericPipelineBehavior<,>)
            });
            container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) });
            container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>) });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Verify();

            return this;
        }

        private static void RegisterHandlers(Container container, Type collectionType, Assembly[] assemblies)
        {
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var handlerTypes = container.GetTypesToRegister(collectionType, assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.Collection.Register(collectionType, handlerTypes);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(ShareHandler).GetTypeInfo().Assembly;
        }
    }
}