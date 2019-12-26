using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ToDo.Web.Filters
{
    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var model = context.ActionArguments.FirstOrDefault().Value;

            if (!context.ModelState.IsValid)
                context.Result = controller.View(model);

            base.OnActionExecuting(context);
        }
    }
}
