using System.Diagnostics;
using QuikGraph;
using JetBrains.Annotations;

namespace GraphShape.Algorithms.Layout.Contextual
{
    /// <summary>
    /// Double tree layout algorithm.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TGraph">Graph type</typeparam>
    public class DoubleTreeLayoutAlgorithm<TVertex, TEdge, TGraph>
        : ParameterizedLayoutAlgorithmBase<TVertex, TEdge, TGraph, DoubleTreeVertexType, object, DoubleTreeLayoutParameters>
        where TVertex : class
        where TEdge : IEdge<TVertex>
        where TGraph : IBidirectionalGraph<TVertex, TEdge>
    {
        
        private readonly TVertex _root;

        
        private readonly IDictionary<TVertex, Size> _verticesSizes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleTreeLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesSizes">Vertices sizes.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <param name="selectedVertex">Root vertex.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="verticesSizes"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="selectedVertex"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="selectedVertex"/> is not part of <paramref name="visitedGraph"/>.</exception>
        public DoubleTreeLayoutAlgorithm(
             TGraph visitedGraph,
             IDictionary<TVertex, Size> verticesSizes,
             TVertex selectedVertex,
            [CanBeNull] DoubleTreeLayoutParameters parameters = null)
            : this(visitedGraph, null, verticesSizes, selectedVertex, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleTreeLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesPositions">Vertices positions.</param>
        /// <param name="verticesSizes">Vertices sizes.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <param name="selectedVertex">Root vertex.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="verticesSizes"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="selectedVertex"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="selectedVertex"/> is not part of <paramref name="visitedGraph"/>.</exception>
        public DoubleTreeLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] IDictionary<TVertex, Point> verticesPositions,
             IDictionary<TVertex, Size> verticesSizes,
             TVertex selectedVertex,
            [CanBeNull] DoubleTreeLayoutParameters parameters = null)
            : base(visitedGraph, verticesPositions, parameters)
        {
            _root = selectedVertex ?? throw new ArgumentNullException(nameof(selectedVertex));
            if (!visitedGraph.ContainsVertex(selectedVertex))
                throw new ArgumentException("The provided vertex is not part of the graph.", nameof(selectedVertex));

            _verticesSizes = verticesSizes ?? throw new ArgumentNullException(nameof(verticesSizes));
        }

        /// <inheritdoc />
        protected override DoubleTreeLayoutParameters DefaultParameters { get; } = new DoubleTreeLayoutParameters();

        #region AlgorithmBase

        /// <inheritdoc />
        protected override void InternalCompute()
        {
            // Separate the two sides
            SeparateSides(
                VisitedGraph,
                _root,
                out var side1,
                out var side2);

            // Build the temporary graph for the two sides
            BuildTemporaryGraphs(
                side1,
                side2,
                out var graph1,
                out var graph2);

            VerticesInfos[_root] = DoubleTreeVertexType.Center;

            ComputeLayoutDirections(out var side1Direction, out var side2Direction);

            // SimpleTree layout on the two side
            var side1LayoutAlgorithm = new SimpleTreeLayoutAlgorithm<TVertex, Edge<TVertex>, BidirectionalGraph<TVertex, Edge<TVertex>>>(
                graph1,
                VerticesPositions,
                _verticesSizes,
                new SimpleTreeLayoutParameters
                {
                    LayerGap = Parameters.LayerGap,
                    VertexGap = Parameters.VertexGap,
                    Direction = side1Direction,
                    SpanningTreeGeneration = SpanningTreeGeneration.BFS,
                });

            var side2LayoutAlgorithm = new SimpleTreeLayoutAlgorithm<TVertex, TEdge, BidirectionalGraph<TVertex, TEdge>>(
                graph2,
                VerticesPositions,
                _verticesSizes,
                new SimpleTreeLayoutParameters
                {
                    LayerGap = Parameters.LayerGap,
                    VertexGap = Parameters.VertexGap,
                    Direction = side2Direction,
                    SpanningTreeGeneration = SpanningTreeGeneration.BFS,
                });

            side1LayoutAlgorithm.Compute();
            side2LayoutAlgorithm.Compute();

            // Merge the layouts
            var side2Translate = side1LayoutAlgorithm.VerticesPositions[_root] - side2LayoutAlgorithm.VerticesPositions[_root];
            foreach (var vertex in side1)
            {
                VerticesPositions[vertex] = side1LayoutAlgorithm.VerticesPositions[vertex];
            }

            foreach (var vertex in side2)
            {
                VerticesPositions[vertex] = side2LayoutAlgorithm.VerticesPositions[vertex] + side2Translate;
            }

            NormalizePositions();
        }

        private void BuildTemporaryGraphs(
             ICollection<TVertex> side1,
             ICollection<TVertex> side2,
             out BidirectionalGraph<TVertex, Edge<TVertex>> graph1,
             out BidirectionalGraph<TVertex, TEdge> graph2)
        {
            //
            // The IN side
            //
            // on the IN side we should reverse the edges
            graph1 = new BidirectionalGraph<TVertex, Edge<TVertex>>();
            graph1.AddVertexRange(side1);
            foreach (var vertex in side1)
            {
                VerticesInfos[vertex] = DoubleTreeVertexType.Backward;
                foreach (var edge in VisitedGraph.InEdges(vertex))
                {
                    if (!side1.Contains(edge.Source) || edge.IsSelfEdge())
                        continue;

                    // Reverse the edge
                    graph1.AddEdge(new Edge<TVertex>(edge.Target, edge.Source));
                }
            }

            //
            // The OUT side
            //
            graph2 = new BidirectionalGraph<TVertex, TEdge>();
            graph2.AddVertexRange(side2);
            foreach (var vertex in side2)
            {
                VerticesInfos[vertex] = DoubleTreeVertexType.Forward;
                foreach (var edge in VisitedGraph.OutEdges(vertex))
                {
                    if (!side2.Contains(edge.Target) || edge.IsSelfEdge())
                        continue;

                    // Simply add the edge
                    graph2.AddEdge(edge);
                }
            }
        }

        private void ComputeLayoutDirections(out LayoutDirection side1Direction, out LayoutDirection side2Direction)
        {
            side1Direction = Parameters.Direction;
            side2Direction = Parameters.Direction;
            switch (side2Direction)
            {
                case LayoutDirection.BottomToTop:
                    side1Direction = LayoutDirection.TopToBottom;
                    break;
                case LayoutDirection.LeftToRight:
                    side1Direction = LayoutDirection.RightToLeft;
                    break;
                case LayoutDirection.RightToLeft:
                    side1Direction = LayoutDirection.LeftToRight;
                    break;
                case LayoutDirection.TopToBottom:
                    side1Direction = LayoutDirection.BottomToTop;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Separates the points of the graph according to the given <paramref name="splitVertex"/>.
        /// </summary>
        /// <param name="graph">Graph to split.</param>
        /// <param name="splitVertex">Split vertex.</param>
        /// <param name="side1">First side.</param>
        /// <param name="side2">Second side.</param>
        private static void SeparateSides(
             TGraph graph,
             TVertex splitVertex,
            [NotNull, ItemNotNull] out HashSet<TVertex> side1,
            [NotNull, ItemNotNull] out HashSet<TVertex> side2)
        {
            Debug.Assert(graph != null);
            Debug.Assert(splitVertex != null);

            var visitedVertices = new HashSet<TVertex> { splitVertex };

            // Create the set of the side 1 and 2
            side1 = new HashSet<TVertex> { splitVertex };
            side2 = new HashSet<TVertex> { splitVertex };

            var queue1 = new Queue<TVertex>();
            var queue2 = new Queue<TVertex>();
            queue1.Enqueue(splitVertex);
            queue2.Enqueue(splitVertex);

            do
            {
                // Get the next layer of vertices on the IN side
                for (int i = 0, n = queue1.Count; i < n; ++i)
                {
                    var vertex = queue1.Dequeue();
                    foreach (var source in graph.InEdges(vertex).Where(e => !e.IsSelfEdge()).Select(e => e.Source))
                    {
                        if ((graph.ContainsVertex(source) && visitedVertices.Add(source))
                            || EqualityComparer<TVertex>.Default.Equals(vertex, splitVertex))
                        {
                            queue1.Enqueue(source);
                            side1.Add(source);
                        }
                    }
                }

                // Get the next layer of vertices on the OUT side
                for (int i = 0, n = queue2.Count; i < n; ++i)
                {
                    var vertex = queue2.Dequeue();
                    foreach (var target in graph.OutEdges(vertex).Where(e => !e.IsSelfEdge()).Select(e => e.Target))
                    {
                        if ((graph.ContainsVertex(target) && visitedVertices.Add(target))
                            || EqualityComparer<TVertex>.Default.Equals(vertex, splitVertex))
                        {
                            queue2.Enqueue(target);
                            side2.Add(target);
                        }
                    }
                }
            } while (queue1.Count > 0 || queue2.Count > 0);
            // We got the 2 sides
        }
    }
}