using System.IO;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Wrappers
{
    /// <summary>
    /// A Box representation of the string part of a multi-part form
    /// </summary>
    public class BoxStringFormPart : IBoxFormPart<string>
    {
        public string Name { get; set; } 

        public string Value { get; set; }
    }

    /// <summary>
    /// A Box representation of the file part of a multi-part form
    /// </summary>
    public class BoxFileFormPart : IBoxFormPart<Stream>
    {
        public string Name { get; set; }

        public Stream Value { get; set; }

        /// <summary>
        /// The file name 
        /// </summary>
        public string FileName { get; set; }
    }
}
