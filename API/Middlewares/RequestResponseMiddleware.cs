using ElasticCore;
using Models;
using System.Text;

namespace API.Middlewares
{
    public class RequestResponseMiddleware
    {

        private readonly RequestDelegate next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            var originalResponseBody = httpContext.Response.Body;

            // Request here
            httpContext.Request.EnableBuffering();
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(httpContext.Request.Body, leaveOpen: true).ReadToEndAsync();
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            MemoryStream newResponseBody = new MemoryStream();
            httpContext.Response.Body = newResponseBody;

            await next.Invoke(httpContext); // Response creating this line

            // Response here

            newResponseBody.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            newResponseBody.Seek(0, SeekOrigin.Begin);

            await newResponseBody.CopyToAsync(originalResponseBody);

            using ElasticCoreService<BaseModel> elastic = new();
            BaseModel baseModel = new()
            {
                Action = (string)httpContext.Request.RouteValues["action"],
                Controller = (string)httpContext.Request.RouteValues["controller"],
                PostDate = DateTime.Now,
                RequestId = Guid.NewGuid(),
                Value = requestText
            };

            elastic.CheckExistsAndInsertLogAsync(baseModel, System.Configuration.ConfigurationManager.AppSettings["ELKRequestResponseIndex"]);
            
            baseModel.PostDate = DateTime.Now;
            baseModel.Value = responseText;

            elastic.CheckExistsAndInsertLogAsync(baseModel, System.Configuration.ConfigurationManager.AppSettings["ELKRequestResponseIndex"]);
        }
    }
}
