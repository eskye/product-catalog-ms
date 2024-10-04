using Microsoft.AspNetCore.Identity;

namespace Catalog.Shared.Exceptions
{
    public class IdentityErrorException : Exception
    {
        public IdentityErrorException() : base("One or more validation failures has occurred.")
        {
            Error = string.Empty;
        }

        public IdentityErrorException(string message) : base(message)
        {
            Error = message;
        }

        public string Error { get; }
        public IdentityErrorException(IEnumerable<IdentityError> errors) : this()
        {
            Error = errors.Select(x => x.Description).FirstOrDefault();

        }
    }
}

