using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Wrappers
{
    /// <summary>
    /// A Box representation of the string part of a multi-part form
    /// </summary>
    public class StringFormPart : IRequestFormPart<string>
    {
        public string Name { get; set; } 

        public string Value { get; set; }
    }
}
