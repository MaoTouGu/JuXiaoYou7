using JetBrains.Annotations;
using QuikGraph;

namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Random layout algorithm.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TGraph">Graph type.</typeparam>
    public class RandomLayoutAlgorithm<TVertex, TEdge, TGraph> : DefaultParameterizedLayoutAlgorithmBase<TVertex, TEdge, TGraph, RandomLayoutParameters>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        
        private readonly IDictionary<TVertex, Size> _verticesSizes;

        
        private readonly IDictionary<TVertex, RandomVertexType> _verticesTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesSizes">Vertices sizes.</param>
        /// <param name="verticesTypes">Vertices types.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="verticesSizes"/> is <see langword="null"/>.</exception>
        public RandomLayoutAlgorithm(
             TGraph visitedGraph,
             IDictionary<TVertex, Size> verticesSizes,
            [CanBeNull] IDictionary<TVertex, RandomVertexType> verticesTypes,
            [CanBeNull] RandomLayoutParameters parameters = null)
            : this(visitedGraph, null, verticesSizes, verticesTypes, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesPositions">Vertices positions.</param>
        /// <param name="verticesSizes">Vertices sizes.</param>
        /// <param name="verticesTypes">Vertices types.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="verticesSizes"/> is <see langword="null"/>.</exception>
        public RandomLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] IDictionary<TVertex, Point> verticesPositions,
             IDictionary<TVertex, Size> verticesSizes,
            [CanBeNull] IDictionary<TVertex, RandomVertexType> verticesTypes,
            [CanBeNull] RandomLayoutParameters parameters = null)
            : base(visitedGraph, verticesPositions, parameters)
        {
            _verticesSizes = new Dictionary<TVertex, Size>(verticesSizes);
            _verticesTypes = verticesTypes is null
                ? new Dictionary<TVertex, RandomVertexType>(0)
                : new Dictionary<TVertex, RandomVertexType>(verticesTypes);
        }


        #region AlgorithmBase

        private IDictionary<TVertex, Point> _fixedPositions;

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            _fixedPositions = VerticesPositions
                .Where(pair => _verticesTypes.TryGetValue(pair.Key, out var type)
                               && type == RandomVertexType.Fixed)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            VerticesPositions.Clear();

            foreach (var pair in _fixedPositions)
            {
                VerticesPositions.Add(pair);
            }
        }

        /// <inheritdoc />
        protected override void InternalCompute()
        {
            var x = (int)Parameters.XOffset;
            var y = (int)Parameters.YOffset;
            var xBound = (int)Parameters.Width;
            var yBound = (int)Parameters.Height;
            foreach (var vertex in VisitedGraph.Vertices.Except(_fixedPositions.Keys))
            {
                _verticesSizes.TryGetValue(vertex, out var vertexSize);
                VerticesPositions[vertex] = new Point(
                    Rand.Next(x, x + xBound - (int)vertexSize.Width),
                    Rand.Next(y, y + yBound - (int)vertexSize.Height));
            }
        }

        #endregion
    }
}