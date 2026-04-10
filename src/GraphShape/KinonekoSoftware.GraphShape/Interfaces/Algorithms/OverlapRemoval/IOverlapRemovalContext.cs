namespace GraphShape.Algorithms.OverlapRemoval
{
    /// <summary>
    /// Represents an overlap removal context.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    public interface IOverlapRemovalContext<TVertex>
    {
        /// <summary>
        /// Overlap rectangles.
        /// </summary>
        
        IDictionary<TVertex, Rect> Rectangles { get; }
    }
}