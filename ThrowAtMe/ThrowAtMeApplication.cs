using System;
using System.Configuration;
using Nancy.Hosting.Self;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace ThrowAtMe
{
    public class ThrowAtMeApplication
    {
        public static IDocumentStore DocumentStore;
        private NancyHost nancyHost;

        public static int NancyPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ThrowAtMe/Port"]); }
        }

        public static int RavenPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["Raven/Port"]); }
        }

        public void Start()
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(RavenPort);
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(NancyPort);

            DocumentStore = new EmbeddableDocumentStore
                                {
                                    ConnectionStringName = "ThrowAtMeRaven",
                                    UseEmbeddedHttpServer = true
                                };
            DocumentStore.Initialize();

            nancyHost = new NancyHost(new Uri("http://localhost:" + NancyPort));
            nancyHost.Start();
        }

        public void Stop()
        {
            nancyHost.Stop();
        }
    }
}