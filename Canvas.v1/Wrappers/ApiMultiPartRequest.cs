using System;
using System.Collections.Generic;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Wrappers
{
    /// <summary>
    /// A Box representation of a multi-part form request
    /// </summary>
    public class ApiMultiPartRequest : ApiRequest
    {
        /// <summary>
        /// Instantiates a new Box multi-part form request providing the Host URI
        /// </summary>
        /// <param name="hostUri">The host URI for the multi-part request</param>
        public ApiMultiPartRequest(Uri hostUri) : this(hostUri, string.Empty) { }

        /// <summary>
        /// Instantiates a new Box multi-part form request providing the Host URI
        /// </summary>
        /// <param name="hostUri">The host URI for the multi-part request</param>
        /// <param name="path">The path for the multi-part request</param>
        public ApiMultiPartRequest(Uri hostUri, string path) 
            : base(hostUri, path) 
        {
            Parts = new List<IRequestFormPart>();
        }

        /// <summary>
        /// Request Method is always a POST in a multipart request
        /// </summary>
        public override RequestMethod Method
        {
            get
            {
                return RequestMethod.Post;
            }
        }

        /// <summary>
        /// The different parts of the multi-part form
        /// </summary>
        public List<IRequestFormPart> Parts { get; set; }
    }
}
