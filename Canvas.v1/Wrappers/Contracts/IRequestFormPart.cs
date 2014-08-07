
namespace Canvas.v1.Wrappers.Contracts
{
    /// <summary>
    /// Interface that defines a Box form part
    /// </summary>
    public interface IRequestFormPart
    {
        /// <summary>
        /// The name of the form part
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Interface that takes different types of Form parts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequestFormPart<T> : IRequestFormPart
    {
        /// <summary>
        /// The value of the form part
        /// </summary>
        T Value { get; }
    }
}
