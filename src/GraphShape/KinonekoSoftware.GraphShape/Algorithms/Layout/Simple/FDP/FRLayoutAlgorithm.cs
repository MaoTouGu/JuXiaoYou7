using QuikGraph;
using JetBrains.Annotations;

namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Fruchterman-Reingold layout algorithm.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TGraph">Graph type</typeparam>
    public class FRLayoutAlgorithm<TVertex, TEdge, TGraph>
        : ParameterizedLayoutAlgorithmBase<TVertex, TEdge, TGraph, FRLayoutParametersBase>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        /// <summary>
        /// Actual temperature of the 'mass'.
        /// </summary>
        private double _temperature;

        private double _maxWidth = double.PositiveInfinity;
        private double _maxHeight = double.PositiveInfinity;

        /// <summary>
        /// Initializes a new instance of the <see cref="FRLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        public FRLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] FRLayoutParametersBase parameters = null)
            : this(visitedGraph, null, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FRLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesPositions">Vertices positions.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        public FRLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] IDictionary<TVertex, Point> verticesPositions,
            [CanBeNull] FRLayoutParametersBase parameters = null)
            : base(visitedGraph, verticesPositions, parameters)
        {
        }

        /// <inheritdoc />
        protected override FRLayoutParametersBase DefaultParameters { get; } = new FreeFRLayoutParameters();

        #region AlgorithmBase

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            // Initializing the positions
            if (Parameters is BoundedFRLayoutParameters boundedFRParams)
            {
                InitializeWithRandomPositions(boundedFRParams.Width, boundedFRParams.Height);
                _maxWidth = boundedFRParams.Width;
                _maxHeight = boundedFRParams.Height;
            }
            else
            {
                InitializeWithRandomPositions(10.0, 10.0);
            }

            Parameters.VertexCount = VisitedGraph.VertexCount;
        }

        /// <inheritdoc />
        protected override void InternalCompute()
        {
            // Actual temperature of the 'mass'. Used for cooling.
            var minimalTemperature = Parameters.InitialTemperature * 0.01;
            _temperature = Parameters.InitialTemperature;
            for (var i = 0;
                i < Parameters.MaxIterations && _temperature > minimalTemperature;
                ++i)
            {
                ThrowIfCancellationRequested();

                IterateOne();

                // Make some cooling
                switch (Parameters.CoolingFunction)
                {
                    case FRCoolingFunction.Linear:
                        _temperature *= 1.0 - i / (double)Parameters.MaxIterations;
                        break;
                    case FRCoolingFunction.Exponential:
                        _temperature *= Parameters.Lambda;
                        break;
                }

                // Iteration ended, do some report
                if (ReportOnIterationEndNeeded)
                {
                    var statusInPercent = i / (double)Parameters.MaxIterations;
                    OnIterationEnded(i, statusInPercent, string.Empty, true);
                }
            }
        }

        #endregion

        /// <summary>
        /// Compute one force application iteration.
        /// </summary>
        protected void IterateOne()
        {
            // Create the forces (zero forces)
            var forces = new Dictionary<TVertex, Vector>();

            #region Repulsive forces

            foreach (var v in VisitedGraph.Vertices)
            {
                var force = default(Vector);

                var posV = VerticesPositions[v];
                foreach (var u in VisitedGraph.Vertices)
                {
                    ThrowIfCancellationRequested();

                    // Doesn't repulse itself
                    if (EqualityComparer<TVertex>.Default.Equals(u, v))
                        continue;

                    // Calculate repulsive force
                    var delta = posV - VerticesPositions[u];
                    var length = Math.Max(delta.Length, double.Epsilon);
                    delta = delta / length * Parameters.ConstantOfRepulsion / length;

                    force += delta;
                }
                
                forces[v] = force;
            }

            #endregion

            #region Attractive forces

            foreach (var edge in VisitedGraph.Edges)
            {
                var source = edge.Source;
                var target = edge.Target;

                if (edge.IsSelfEdge())
                    continue;

                // Compute attraction point between 2 vertices
                var delta = VerticesPositions[source] - VerticesPositions[target];
                var length = Math.Max(delta.Length, double.Epsilon);
                delta = delta / length * Math.Pow(length, 2) / Parameters.ConstantOfAttraction;

                forces[source] -= delta;
                forces[target] += delta;
            }

            #endregion

            #region Limit displacement

            foreach (var vertex in VisitedGraph.Vertices)
            {
                var position = VerticesPositions[vertex];

                var delta = forces[vertex];
                if (delta != default(Vector))
                {
                    var length = Math.Max(delta.Length, double.Epsilon);
                    delta = delta / length * Math.Min(length, _temperature);

                    position += delta;

                    // Ensure bounds
                    position.X = Math.Min(_maxWidth, Math.Max(0, position.X));
                    position.Y = Math.Min(_maxHeight, Math.Max(0, position.Y));
                    VerticesPositions[vertex] = position;
                }
            }

            #endregion
        }
    }
}