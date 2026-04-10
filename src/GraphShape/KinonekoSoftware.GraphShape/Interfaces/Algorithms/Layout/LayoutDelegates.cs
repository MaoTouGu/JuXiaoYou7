using QuikGraph;

namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Handler to report the progress of a layout algorithm.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="percent">The status of the progress in percent.</param>
    public delegate void ProgressChangedEventHandler( object sender, double percent);

    /// <summary>
    /// Handler for a layout iteration ended.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event arguments.</param>
    public delegate void LayoutIterationEndedEventHandler<TVertex>(
         object sender,
         ILayoutIterationEventArgs<TVertex> args);

    /// <summary>
    /// Handler for a layout iteration ended.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TVertexInfo">Vertex information type.</typeparam>
    /// <typeparam name="TEdgeInfo">Edge information type.</typeparam>
    /// <param name="sender">Event sender.</param>
    /// <param name="args">Event arguments.</param>
    public delegate void LayoutIterationEndedEventHandler<TVertex, TEdge, TVertexInfo, TEdgeInfo>(
         object sender,
         ILayoutInfoIterationEventArgs<TVertex, TEdge, TVertexInfo, TEdgeInfo> args)
        where TEdge : IEdge<TVertex>;
}