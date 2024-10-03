using System;
using FluentValidation.Results;

namespace Catalog.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures has occurred.")
        {
            Error = string.Empty;
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Error = string.Join(",", failures.Select(x => x.ErrorMessage));
        }

        public string Error { get; }
    }
}

