using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Canvas.v1.Converter;
using Canvas.v1.Exceptions;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Extensions
{
    /// <summary>
    /// Extends the ApiResponse class with convenience methods
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Parses the ApiResponse with the provided converter
        /// </summary>
        /// <typeparam name="T">The return type of the Box response</typeparam>
        /// <param name="response">The response to parse</param>
        /// <param name="converter">The converter to use for the conversion</param>
        /// <returns></returns>
        internal static IApiResponse<T> ParseResults<T>(this IApiResponse<T> response, IJsonConverter converter)
            where T : class
        {
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                        response.ResponseObject = converter.Parse<T>(response.ContentString);
                    break;
                case ResponseStatus.Error:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                    {
                        try
                        { 
                            response.Error = converter.Parse<ApiError>(response.ContentString);
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine(string.Format("Unable to parse error message: {0}", response.ContentString));
                        }
                        // Throw formatted error if available
                        if (response.Error != null && !string.IsNullOrWhiteSpace(response.Error.ErrorReportId))
                            throw new CanvasException(string.Join("; ", response.Error.Errors.Select(e => e.Message))) { StatusCode = response.StatusCode };
                        // Throw error with full response if error object not available
                        throw new CanvasException(response.ContentString) { StatusCode = response.StatusCode };
                    }
                    throw new CanvasException() { StatusCode = response.StatusCode };
            }
            return response;
        }


        /// <summary>
        /// Attempt to extract the number of pages in a preview from the HTTP response headers. The response contains a "Link" 
        /// element in the header that includes a link to the last page of the preview. This method uses that information
        /// to extract the total number of pages
        /// </summary>
        /// <param name="response">The http response that includes total page information in its header</param>
        /// <returns>Total number of pages in the preview</returns>
        public static int BuildPagesCount<T>(this IApiResponse<T> response) where T : class
        {
            int count = 1;
            IEnumerable<string> values = new List<string>();

            if (response.Headers.TryGetValues("link", out values)) // headers names are case-insensitve
            {
                var links = values.First().Split(',');
                var last = links.FirstOrDefault(x => x.ToUpperInvariant().Contains("REL=\"LAST\""));
                if (last != null)
                {
                    string lastPageLink = last.Split(';')[0];

                    Regex rgx = new Regex(@"page=[0-9]+", RegexOptions.IgnoreCase);
                    MatchCollection matches = rgx.Matches(lastPageLink);

                    if (matches.Count > 0)
                    {
                        try
                        {
                            count = Convert.ToInt32(matches[0].Value.Split('=')[1]);
                        }
                        catch (FormatException) { }
                    }
                }
            }
            return count;
        }
    }
}
