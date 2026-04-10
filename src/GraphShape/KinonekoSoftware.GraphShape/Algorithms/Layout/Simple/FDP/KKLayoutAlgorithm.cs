using GraphShape.Utils;
using JetBrains.Annotations;
using QuikGraph;
using static GraphShape.Utils.MathUtils;

namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Kamada-Kawai layout algorithm.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TGraph">Graph type</typeparam>
    public class KKLayoutAlgorithm<TVertex, TEdge, TGraph>
        : DefaultParameterizedLayoutAlgorithmBase<TVertex, TEdge, TGraph, KKLayoutParameters>
        where TEdge : IEdge<TVertex>
        where TGraph : IBidirectionalGraph<TVertex, TEdge>
    {
        #region Variables needed for the layout

        private double[,] _edgeLengths;
        private double[,] _springConstants;

        // Cache for speed-up
        private TVertex[] _vertices;

        /// <summary>
        /// Positions of the vertices, stored by indices.
        /// </summary>
        private Point[] _positions;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="KKLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        public KKLayoutAlgorithm( TGraph visitedGraph, [CanBeNull] KKLayoutParameters parameters = null)
            : this(visitedGraph, null, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KKLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesPositions">Vertices positions.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        public KKLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] IDictionary<TVertex, Point> verticesPositions,
            [CanBeNull] KKLayoutParameters parameters = null)
            : base(visitedGraph, verticesPositions, parameters)
        {
        }

        #region AlgorithmBase

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            // Minimal distances between the vertices
            _edgeLengths = new double[VisitedGraph.VertexCount, VisitedGraph.VertexCount];
            _springConstants = new double[VisitedGraph.VertexCount, VisitedGraph.VertexCount];
            _vertices = new TVertex[VisitedGraph.VertexCount];
            _positions = new Point[VisitedGraph.VertexCount];

            // Initialize with random positions
            InitializeWithRandomPositions(Parameters.Width, Parameters.Height);

            // Copy positions into array (speed-up)
            var index = 0;
            foreach (var vertex in VisitedGraph.Vertices)
            {
                _vertices[index] = vertex;
                _positions[index] = VerticesPositions[vertex];
                ++index;
            }

            // Calculate the diameter of the graph
            var diameter = VisitedGraph.GetDiameter<TVertex, TEdge, TGraph>(out var distances);

            // L0 is the length of a side of the display area
            var l0 = Math.Min(Parameters.Width, Parameters.Height);

            // Ideal length = L0 / max d_i,j
            var idealEdgeLength = l0 / diameter * Parameters.LengthFactor;

            // Calculate the ideal distance between the nodes
            for (var i = 0; i < VisitedGraph.VertexCount - 1; ++i)
            {
                for (var j = i + 1; j < VisitedGraph.VertexCount; ++j)
                {
                    // Distance between non-adjacent vertices
                    var dist = diameter * Parameters.DisconnectedMultiplier;

                    // Calculate the minimal distance between the vertices
                    if (!NearEqual(distances[i, j], double.MaxValue))
                    {
                        dist = Math.Min(distances[i, j], dist);
                    }
                    if (!NearEqual(distances[j, i], double.MaxValue))
                    {
                        dist = Math.Min(distances[j, i], dist);
                    }
                    distances[i, j] = distances[j, i] = dist;
                    _edgeLengths[i, j] = _edgeLengths[j, i] = idealEdgeLength * dist;
                    _springConstants[i, j] = _springConstants[j, i] = Parameters.K / Math.Pow(dist, 2);
                }
            }
        }

        /// <inheritdoc />
        protected override void InternalCompute()
        {
            var n = VisitedGraph.VertexCount;
            if (n == 0)
                return;

            for (var iteration = 0; iteration < Parameters.MaxIterations; ++iteration)
            {
                ThrowIfCancellationRequested();

                if (!RunIteration(n))
                    return;

                if (ReportOnIterationEndNeeded)
                {
                    Report(iteration);
                }
            }

            Report(Parameters.MaxIterations);
        }

        private bool RunIteration(int n)
        {
            var maxDeltaM = double.NegativeInfinity;
            var pm = -1;

            // Get the 'p' with the max delta_m
            for (var i = 0; i < n; ++i)
            {
                ThrowIfCancellationRequested();

                var deltaM = CalculateEnergyGradient(i);
                if (maxDeltaM < deltaM)
                {
                    maxDeltaM = deltaM;
                    pm = i;
                }
            }

            if (pm == -1)
                return false;

            // Calculate the delta_x & delta_y with the Newton-Raphson method
            // There is an upper-bound for the while (deltaM > epsilon) {...} cycle (100)
            for (var i = 0; i < 100; ++i)
            {
                _positions[pm] += CalculateDeltaXY(pm);

                var deltaM = CalculateEnergyGradient(pm);
                // Real stop condition
                if (deltaM < double.Epsilon)
                    break;
            }

            // What if some of the vertices would be exchanged?
            if (Parameters.ExchangeVertices && maxDeltaM < double.Epsilon)
            {
                var energy = CalculateEnergy();
                for (var i = 0; i < n - 1; ++i)
                {
                    for (var j = i + 1; j < n; ++j)
                    {
                        ThrowIfCancellationRequested();

                        var exchangedEnergy = CalculateEnergyIfExchanged(i, j);
                        if (energy > exchangedEnergy)
                        {
                            var p = _positions[i];
                            _positions[i] = _positions[j];
                            _positions[j] = p;
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        /// <summary>
        /// Reports the end of the <paramref name="iteration"/>th iteration.
        /// </summary>
        protected void Report(int iteration)
        {
            // Copy positions to VerticesPositions
            for (var i = 0; i < _vertices.Length; ++i)
            {
                VerticesPositions[_vertices[i]] = _positions[i];
            }

            OnIterationEnded(
                iteration,
                iteration / (double)Parameters.MaxIterations,
                $"Iteration {iteration} finished.",
                true);
        }

        [Pure]
        // ReSharper disable InconsistentNaming
        private static double ComputeEnergy(double l_ij, double k_ij, double dx, double dy)
        // ReSharper restore InconsistentNaming
        {
            return k_ij / 2 *
                   (dx * dx + dy * dy + l_ij * l_ij
                    -
                    2 * l_ij * Math.Sqrt(dx * dx + dy * dy));
        }

        /// <returns>
        /// Calculates the energy of the state where 
        /// the positions of the vertex 'p' and 'q' are exchanged.
        /// </returns>
        /// <param name="p">The index of a vertex.</param>
        /// <param name="q">The index of a vertex.</param>
        [Pure]
        private double CalculateEnergyIfExchanged(int p, int q)
        {
            double energy = 0;
            for (var i = 0; i < _vertices.Length - 1; ++i)
            {
                for (var j = i + 1; j < _vertices.Length; ++j)
                {
                    var ii = i == p ? q : i;
                    var jj = j == q ? p : j;

                    // ReSharper disable InconsistentNaming
                    var l_ij = _edgeLengths[i, j];
                    var k_ij = _springConstants[i, j];
                    // ReSharper restore InconsistentNaming
                    var dx = _positions[ii].X - _positions[jj].X;
                    var dy = _positions[ii].Y - _positions[jj].Y;

                    energy += ComputeEnergy(l_ij, k_ij, dx, dy);
                }
            }

            return energy;
        }

        /// <summary>
        /// Calculates the energy of the spring system.
        /// </summary>
        /// <returns>Returns with the energy of the spring system.</returns>
        [Pure]
        private double CalculateEnergy()
        {
            double energy = 0;
            for (var i = 0; i < _vertices.Length - 1; ++i)
            {
                for (var j = i + 1; j < _vertices.Length; ++j)
                {
                    // ReSharper disable InconsistentNaming
                    var l_ij = _edgeLengths[i, j];
                    var k_ij = _springConstants[i, j];
                    // ReSharper restore InconsistentNaming

                    var dx = _positions[i].X - _positions[j].X;
                    var dy = _positions[i].Y - _positions[j].Y;

                    energy += ComputeEnergy(l_ij, k_ij, dx, dy);
                }
            }

            return energy;
        }

        /// <summary>
        /// Determines a step to new position of the <paramref name="m"/>th vertex.
        /// </summary>
        /// <param name="m">The index of the vertex.</param>
        /// <returns>The delta XY of the <paramref name="m"/>th vertex.</returns>
        [Pure]
        private Vector CalculateDeltaXY(int m)
        {
            double dxm = 0;
            double dym = 0;
            double dxmdym = 0;
            double dymdxm;
            // ReSharper disable InconsistentNaming
            double d2xm = 0;
            double d2ym = 0;
            // ReSharper restore InconsistentNaming

            for (var i = 0; i < _vertices.Length; ++i)
            {
                if (i != m)
                {
                    // Common things
                    var l = _edgeLengths[m, i];
                    var k = _springConstants[m, i];
                    var dx = _positions[m].X - _positions[i].X;
                    var dy = _positions[m].Y - _positions[i].Y;

                    // Distance between the points
                    var d = Math.Sqrt(dx * dx + dy * dy);
                    var ddd = Math.Pow(d, 3);

                    dxm += k * (1 - l / d) * dx;
                    dym += k * (1 - l / d) * dy;
                    // TODO isn't it wrong?
                    d2xm += k * (1 - l * Math.Pow(dy, 2) / ddd);
                    dxmdym += k * l * dx * dy / ddd;
                    // TODO isn't it wrong?
                    d2ym += k * (1 - l * Math.Pow(dx, 2) / ddd);
                }
            }

            // d2E_dymdxm equals to d2E_dxmdym
            dymdxm = dxmdym;

            var denominator = d2xm * d2ym - dxmdym * dymdxm;
            double deltaX = 0;
            double deltaY = 0;
            if (!IsZero(denominator))
            {
                deltaX = (dxmdym * dym - d2ym * dxm) / denominator;
                deltaY = (dymdxm * dxm - d2xm * dym) / denominator;
            }

            return new Vector(deltaX, deltaY);
        }

        /// <summary>
        /// Calculates the gradient energy of <paramref name="m"/>th vertex.
        /// </summary>
        /// <param name="m">The index of the vertex.</param>
        /// <returns>The gradient energy of the <paramref name="m"/>th vertex.</returns>
        [Pure]
        private double CalculateEnergyGradient(int m)
        {
            double dxm = 0, dym = 0;
            //        {  1, if m < i
            // sign = { 
            //        { -1, if m > i
            for (var i = 0; i < _vertices.Length; i++)
            {
                if (i == m)
                    continue;

                // Differences of the positions
                var dx = _positions[m].X - _positions[i].X;
                var dy = _positions[m].Y - _positions[i].Y;

                // Distances of the two vertex (by positions)
                var d = Math.Sqrt(dx * dx + dy * dy);

                var common = _springConstants[m, i] * (1 - _edgeLengths[m, i] / d);
                dxm += common * dx;
                dym += common * dy;
            }

            // delta_m = sqrt((dE/dx)^2 + (dE/dy)^2)
            return Math.Sqrt(dxm * dxm + dym * dym);
        }
    }
}