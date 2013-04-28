using System;
using System.IO;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using ThrowAtMe.Models;

namespace ThrowAtMe.Modules
{
    public class LogmessageBinder : IModelBinder
    {
        private readonly IBinder defaultBinder;

        public LogmessageBinder(IBinder defaultBinder)
        {
            this.defaultBinder = defaultBinder;
        }

        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration,
                           params string[] blackList)
        {
            if (!string.IsNullOrEmpty(context.Request.Headers.ContentType))
            {
                return defaultBinder.Bind(context, modelType, instance, configuration, blackList);
            }


            DynamicDictionary body;
            using (var reader = new StreamReader(context.Request.Body))
            {
                body = reader.ReadToEnd().AsQueryDictionary();

                context.Request.Body.Position = 0;
            }

            var logMessage = new LogMessage()
                                 {
                                     ErrorMessage = body["ErrorMessage"], LineNumber = body["LineNumber"], LogType = body["LogType"], Url = body["Url"]
                                 };
            return logMessage;
        }

        public bool CanBind(Type modelType)
        {
            if (typeof(LogMessage) == modelType)
            {
                return true;
            }

            return false;
        }
    }
}