using System;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using ThrowAtMe.Models;

namespace ThrowAtMe.Modules
{
    public class LogModule : NancyModule
    {
        public LogModule()
            : base("/log")
        {
            this.EnableCors();

            Get["/"] = parameters => { return HttpStatusCode.OK; };

            Post["/"] = parameters =>
                            {
                                var log = this.Bind<LogMessage>(new[] {"Id"});

                                using (IDocumentSession session = Program.DocumentStore.OpenSession())
                                {
                                    session.Store(log);
                                    session.SaveChanges();
                                }

                                Console.WriteLine(log.ToString());

                                return HttpStatusCode.OK;
                            };
        }
    }
}