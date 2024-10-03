using System;
using Catalog.Shared.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.Shared.Filters
{
	public class ModelStateValidationFilter : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelState = context.ModelState;
                var errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(d => new ValidationFailure(key, d.ErrorMessage)))
                    .ToList();
                throw new ValidationException(errors);
            }

            base.OnActionExecuting(context);
        }
    }
}

