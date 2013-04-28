using Topshelf;

namespace ThrowAtMe
{
    internal class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
                                {
                                    x.Service<ThrowAtMeApplication>(s =>
                                                                       {
                                                                           s.ConstructUsing(name => new ThrowAtMeApplication());
                                                                           s.WhenStarted(tc => tc.Start());
                                                                           s.WhenStopped(tc => tc.Stop());
                                                                       });
                                    x.RunAsLocalSystem();

                                    x.SetDescription("Javscript error logging service");
                                    x.SetDisplayName("Throw At Me");
                                    x.SetServiceName("throw-at-me");
                                });
        }
    }
}