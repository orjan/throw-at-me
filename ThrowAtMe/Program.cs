using System;
using Nancy;
using Nancy.Hosting.Self;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace ThrowAtMe
{
    internal class Program
    {
        public static IDocumentStore DocumentStore;

        /// <summary>
        ///     https://github.com/NancyFx/Nancy/wiki/Self-Hosting-Nancy
        ///     netsh http add urlacl url=http://+:1234/ user=DOMAIN\username
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8081);
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(1234);

            DocumentStore = new EmbeddableDocumentStore()
                                {
                                    RunInMemory = true, UseEmbeddedHttpServer = true
                                };
            DocumentStore.Initialize();

            var nancyHost = new NancyHost(new Uri("http://localhost:1234"));
            nancyHost.Start();

            Console.ReadLine();
            nancyHost.Stop();
        }
    }

    
    
}