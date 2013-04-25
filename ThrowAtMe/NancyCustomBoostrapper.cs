using Nancy;
using Nancy.Conventions;

namespace ThrowAtMe
{
    public class NancyCustomBoostrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", @"Assets")
                );
        }
    }
}