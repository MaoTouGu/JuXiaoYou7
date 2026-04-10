using System.ComponentModel;
using QuikGraph;

namespace GraphShape.Controls
{

    /// <summary>
    /// Default graph layout control.
    /// </summary>
    /// <remarks>For general purposes, with general types.</remarks>
    public class GraphLayout : GraphLayout<object, IEdge<object>, IBidirectionalGraph<object, IEdge<object>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphLayout"/> class.
        /// </summary>
        public GraphLayout()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                var graph    = new BidirectionalGraph<object, IEdge<object>>();
                var vertices = new object[] { "S", "A", "M", "P", "L", "E" };
                var edges = new IEdge<object>[]
                {
                    new Edge<object>(vertices[0], vertices[1]),
                    new Edge<object>(vertices[1], vertices[2]),
                    new Edge<object>(vertices[1], vertices[3]),
                    new Edge<object>(vertices[3], vertices[4]),
                    new Edge<object>(vertices[0], vertices[4]),
                    new Edge<object>(vertices[4], vertices[5]),
                };

                graph.AddVerticesAndEdgeRange(edges);
                OverlapRemovalAlgorithmType = "FSA";
                LayoutAlgorithmType         = "FR";
                Graph                       = graph;
            }
        }
    }
}