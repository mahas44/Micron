using ElasticCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace API.Infrastructure
{
    public class LogFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object? model = context.ActionArguments["model"];
            BaseModel baseModel = new BaseModel();
            baseModel.Action = (string)context.RouteData.Values["action"];
            baseModel.Controller = (string)context.RouteData.Values["controller"];
            baseModel.Value = model;
            baseModel.RequestId = Guid.NewGuid();
            baseModel.PostDate = DateTime.Now;

            using ElasticCoreService<BaseModel> elastic = new();
            elastic.CheckExistsAndInsertLogAsync(baseModel, System.Configuration.ConfigurationManager.AppSettings["ELKGameIndex"]);

        }
    }
}
