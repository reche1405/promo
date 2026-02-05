using System.Text.Json;
using RecheApi.Nifty.Serializers.DataTransfer;

namespace RecheApi.Nifty.Application
{
    public class NiftyAppFactory
    {
        private WebApplication? App {get;set;}
        private WebApplicationBuilder? Builder {get;set;}

        public NiftyAppFactory() {}
        public void CreateBuilder(string[] args)
        {
            Builder = WebApplication.CreateBuilder(args);
        }
        public WebApplication Build()
        {

            App = Builder? .Build() ?? throw new Exception("No Application Builder found - Nifty Application");
            App = AddDataMiddleWare(App);
            return App;
        }

        public WebApplication AddDataMiddleWare(WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                string method = context.Request.Method;
                bool isNonQuery = false;
                string[] parsableMethods = ["POST", "PUT", "PATCH"];
                
                foreach(string m in parsableMethods)
                {
                    if(string.Compare(method, m , StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        isNonQuery = true;
                        break;
                    }
                }

                if (!isNonQuery) {
                    
                    await next(context);
                    return;
                }

                RequestData data = new();
                using var sr = new StreamReader(context.Request.Body);
                var body = await sr.ReadToEndAsync();
                if(string.IsNullOrEmpty(body))
                {
                    await next(context);
                    return;
                }

                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
                if(dict is null)
                {
                    await next(context);
                    return;
                    
                }

                foreach((string key, object value ) in dict)
                {
                    data.SetValue(key, value);
                }
                context.Items["Data"] = data;
                await next(context);
                return;
            });
            return app;

        }
        
    }
}