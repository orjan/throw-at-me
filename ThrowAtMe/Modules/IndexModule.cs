using Nancy;

namespace ThrowAtMe.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => "Simple javascript error logger";

        }
    }
}