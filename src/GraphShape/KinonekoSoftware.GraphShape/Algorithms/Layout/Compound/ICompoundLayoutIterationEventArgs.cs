namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Represents a compound layout iteration event arguments.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    public interface ICompoundLayoutIterationEventArgs<TVertex> : ILayoutIterationEventArgs<TVertex>
    {
        /// <summary>
        /// Inner canvas vertices sizes.
        /// </summary>
        
        IDictionary<TVertex, Size> InnerCanvasSizes { get; }
    }
}