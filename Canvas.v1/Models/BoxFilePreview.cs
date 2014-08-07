using System.IO;
using System.Net;

namespace Canvas.v1.Models
{
    /// <summary>
    /// Represents a file preview response.
    /// </summary>
    public class BoxFilePreview
    {
        /// <summary>
        /// The stream of the preview.
        /// </summary>
        public Stream PreviewStream { get; set; }

        /// <summary>
        /// Returned HTTP status code from the preview request. Refer to API page for possible return values.
        /// </summary>
        public HttpStatusCode ReturnedStatusCode { get; set; }

        /// <summary>
        /// Total pages in the file to preview
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The page number of this preview out of the total availabe pages in the file
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
