﻿using System;

namespace Catalog.Shared.AppResponse
{
    public class Response<T> : Response where T : class
    {
        /// <summary>
        ///     The result of the response
        /// </summary>
        public new T Data
        {
            get => (T)base.Data;

            set => base.Data = value;
        }

        /// <summary>
        ///     Creates a successful response with a given result object
        /// </summary>
        /// <param name="result">The result object to return with the response</param>
        /// <returns>The response object</returns>
        public static Response<T> Success(T result, string? message = null)
        {
            var response = new Response<T> { ResultType = ResultType.Success, Data = result, Message = message };

            return response;
        }

        /// <summary>
        ///     Creates a failed result. It takes no result object
        /// </summary>
        /// <param name="errorMessage">The error message returned with the response</param>
        /// <returns>The created response object</returns>
        public new static Response<T> Failed(string errorMessage)
        {
            var response = new Response<T> { ResultType = ResultType.Error, Message = errorMessage };

            return response;
        }


        /// <summary>
        ///     Creates a validation error response, indicating the input was invalid
        /// </summary>
        /// <param name="validationMessages">The validation message</param>
        /// <returns>The Response object</returns>
        public new static Response<T> ValidationError(List<string> validationMessages)
        {
            var response = new Response<T>
            {
                ResultType = ResultType.ValidationError,
                Message = "Response has validation errors",
                ValidationMessages = validationMessages
            };

            return response;
        }

        /// <summary>
        ///     Creates a warning result. The warning result is successful, but might have issues that should be addressed or
        ///     logged
        /// </summary>
        /// <param name="warningMessage">The warning returned with the response</param>
        /// <param name="result">The result object</param>
        /// <returns>The created response object</returns>
        public static Response<T> Warning(string warningMessage, T result)
        {
            var response = new Response<T>
            {
                ResultType = ResultType.Warning,
                Message = warningMessage,
                Data = result
            };

            return response;
        }

        /// <summary>
        ///     Creates an empty result. The empty result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <returns>The created response object</returns>
        public new static Response<T> Empty()
        {
            var response = new Response<T> { ResultType = ResultType.Empty };

            return response;
        }
    }

    /// <summary>
    ///     A simple response object with no returned object. Just indicates successful or failed requests
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Response" /> class.
        /// </summary>
        public Response()
        {
            ResultType = ResultType.Success;
        }

        /// <summary>
        ///     The result of the response
        /// </summary>
        public object Data { get; protected set; }

        /// <summary>
        ///     Indicates if the response is successful or not. Warning or success result type indicate success
        /// </summary>
        public bool Succeeded => ResultType == ResultType.Success || ResultType == ResultType.Warning;

        /// <summary>
        ///     The result type
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        ///     The message returned with the response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The validation error messages returned with the response
        /// </summary>
        public List<string> ValidationMessages { get; set; }

        /// <summary>
        ///     Creates a successful response with a given result object
        /// </summary>
        /// <returns>The response object</returns>
        public static Response Success()
        {
            var response = new Response { ResultType = ResultType.Success };

            return response;
        }

        /// <summary>
        ///     Creates a failed result. It takes no result object
        /// </summary>
        /// <param name="errorMessage">The error message returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response Failed(string errorMessage)
        {
            var response = new Response { ResultType = ResultType.Error, Message = errorMessage };

            return response;
        }

        /// <summary>
        ///     Creates a validation error response, indicating the input was invalid
        /// </summary>
        /// <param name="validationMessages">The validation message</param>
        /// <returns>The Response object</returns>
        public static Response ValidationError(List<string> validationMessages)
        {
            var response = new Response
            { ResultType = ResultType.ValidationError, ValidationMessages = validationMessages };

            return response;
        }

        /// <summary>
        ///     Creates a warning result. The warning result is successful, but might have issues that should be addressed or
        ///     logged
        /// </summary>
        /// <param name="warningMessage">The warning returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response Warning(string warningMessage)
        {
            var response = new Response { ResultType = ResultType.Warning, Message = warningMessage };

            return response;
        }


        /// <summary>
        ///     Creates an empty result. The empty result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <returns>The created response object</returns>
        public static Response Empty()
        {
            var response = new Response { ResultType = ResultType.Empty };

            return response;
        }
    }
}

