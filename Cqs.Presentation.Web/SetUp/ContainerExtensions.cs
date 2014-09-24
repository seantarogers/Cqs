namespace Cqs.Presentation.Web.SetUp
{
    using System;
    using System.Linq;
    using System.Reflection;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    public static class ContainerExtensions
    {
        public static void RegisterAllImplementationsImplementingInterface<TInterface>(
            this Container container)
        {
            container.RegisterAll<TInterface>(
                typeof(TInterface).Assembly.GetExportedTypes()
                    .Where(tp => !tp.IsAbstract)
                    .Where(tp => typeof(TInterface).IsAssignableFrom(tp)));
        }

        public static void RegisterDecoratorOnlyWhereHandlerImplementsInterface(
            this Container container,
            Type tBaseHandlerInterface,
            Type tDecorator,
            Type tPredicateDecoratorInterface)
        {
            container.RegisterDecorator(tBaseHandlerInterface, tDecorator, AddPredicate(tPredicateDecoratorInterface));
        }

        public static void RegisterAllInstancesFromAssemblyInNamespace(
            this Container container,
            Assembly assembly,
            string ns,
            params string[] excludedNamespaces)
        {
            var servicesToRegister = from type in assembly.GetExportedTypes()
                                     where
                                         type.Namespace != null && type.Namespace.StartsWith(ns)
                                         && !excludedNamespaces.Contains(type.Namespace)
                                     where type.IsClass
                                     where type.GetInterfaces()
                                         .Any()
                                     select new
                                                {
                                                    Service = type.GetInterfaces()
                                         .Single(),
                                                    Implementation = type
                                                };

            foreach (var service in servicesToRegister)
            {
                container.Register(service.Service, service.Implementation, Lifestyle.Transient);
            }
        }

        private static Predicate<DecoratorPredicateContext> AddPredicate(Type type)
        {
            return (p => type.MakeGenericType(p.ServiceType.GetGenericArguments())
                             .IsAssignableFrom(p.ImplementationType));
        }
    }
}