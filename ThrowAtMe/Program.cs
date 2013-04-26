using System;
using System.Configuration;
using Nancy.Hosting.Self;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace ThrowAtMe
{
    internal class Program
    {
        public static IDocumentStore DocumentStore;

        public static int NancyPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ThrowAtMe/Port"]); }
        }
        
        public static int RavenPort {
            get { return int.Parse(ConfigurationManager.AppSettings["Raven/Port"]); }
        }

        /// <summary>
        ///     https://github.com/NancyFx/Nancy/wiki/Self-Hosting-Nancy
        ///     netsh http add urlacl url=http://+:1234/ user=DOMAIN\username
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(RavenPort);
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(NancyPort);

            DocumentStore = new EmbeddableDocumentStore()
                                {
                                    ConnectionStringName = "ThrowAtMeRaven",
                                    UseEmbeddedHttpServer = true
                                };
            DocumentStore.Initialize();

            var nancyHost = new NancyHost(new Uri("http://localhost:" + NancyPort));
            nancyHost.Start();

            Console.ReadLine();
            nancyHost.Stop();
        }
    }
}