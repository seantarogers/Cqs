namespace Cqs.Presentation.Web
{
    using System;
    using System.IdentityModel.Services;
    using System.IdentityModel.Services.Configuration;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Cqs.Presentation.Web.ClaimsTransformation;
    using Cqs.Presentation.Web.SetUp;

    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            ContainerSetup.RegisterComponentsTo(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            AutoMapperSetUp.ConfigureMaps();
          
            FederatedAuthentication.FederationConfigurationCreated +=
                FederatedAuthenticationFederationConfigurationCreated;
        }

        private static void FederatedAuthenticationFederationConfigurationCreated(object sender, FederationConfigurationCreatedEventArgs e)
        {
            const bool RequireSsl = false;
            const string AuthCookieName = "AppliedAuth";

            e.FederationConfiguration.CookieHandler = new ChunkedCookieHandler
                {
                    Domain = string.Empty,
                    Name = AuthCookieName,
                    RequireSsl = RequireSsl,
                    PersistentSessionLifetime = new TimeSpan(0, 0, 30, 0)
                };

            e.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager = new ClaimsTransformer();             
        }
    }
}
