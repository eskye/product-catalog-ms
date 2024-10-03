using System;
namespace Catalog.Shared.AppResponse
{
    public enum ResultType
    {
        Success = 1,
        Error = 2,
        ValidationError = 3,
        Warning = 4,
        Empty
    }
}

