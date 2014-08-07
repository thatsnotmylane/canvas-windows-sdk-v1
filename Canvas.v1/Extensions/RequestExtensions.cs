using System;
using System.Collections.Generic;
using Canvas.v1.Config;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Extensions
{
    /// <summary>
    /// Extends the ApiRequest object with convenience methods
    /// </summary>
    public static class RequestExtensions
    {
        public static T Param<T>(this T request, string name, string value) where T : IApiRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.Parameters[name] = value;

            return request;
        }

        public static T Param<T>(this T request, string name, List<string> values) where T : IApiRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (values == null || values.Count == 0)
                return request;

            request.Parameters[name] = string.Join(",", values);

            return request;
        }

        public static T Header<T>(this T request, string name, string value) where T : IApiRequest
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.HttpHeaders[name] = value;

            return request;
        }

        public static T Method<T>(this T request, RequestMethod method) where T : IApiRequest
        {
            request.Method = method;

            return request;
        }

        public static T Payload<T>(this T request, string value) where T : IApiRequest
        {
            value.ThrowIfNullOrWhiteSpace("value");

            request.Payload = value;

            return request;
        }

        public static T Payload<T>(this T request, string name, string value) where T : IApiRequest
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.PayloadParameters[name] = value;

            return request;
        }

        public static T Authorize<T>(this T request, string accessToken) where T : IApiRequest
        {
            accessToken.ThrowIfNullOrWhiteSpace("accessToken");

            request.Authorization = accessToken;
            request.Header(Constants.AuthHeaderKey, accessToken);

            return request;
        }

        public static T FormPart<T>(this T request, IRequestFormPart<T> formPart) where T : ApiMultiPartRequest
        {
            formPart.ThrowIfNull("formPart");

            request.Parts.Add(formPart);

            return request;
        }

    }
}
