using System;
using System.Net;

namespace Canvas.v1.Exceptions
{
    public class CanvasException : Exception
    {
        /// <summary>
        /// Instantiates a new CanvasException
        /// This exception is used when the SDK throws an exception
        /// </summary>
        public CanvasException() : base() { }

        /// <summary>
        /// Instantiates a new CanvasException with the provided message
        /// </summary>
        /// <param name="message">The message for the exception</param>
        public CanvasException(string message) : base(message) { }

        /// <summary>
        /// Instantiates a new CanvasException with the provided message and provided inner Exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception to be wrapped</param>
        public CanvasException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Http Status code for the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}

