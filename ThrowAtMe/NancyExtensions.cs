using Nancy;

namespace ThrowAtMe
{
    public static class NancyExtensions
    {
        public static void EnableCors(this NancyModule module)
        {
            module.After.AddItemToEndOfPipeline(x =>
                                                    {
                                                        x.Response.WithHeader("Access-Control-Allow-Origin", "*");
                                                    });
        }
    }
}