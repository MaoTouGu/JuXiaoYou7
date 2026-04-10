using System.Diagnostics;
using JetBrains.Annotations;
using QuikGraph;

namespace GraphShape.Algorithms.Layout
{
    /// <summary>
    /// Balloon tree layout algorithm.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    /// <typeparam name="TGraph">Graph type.</typeparam>
	public class BalloonTreeLayoutAlgorithm<TVertex, TEdge, TGraph> : DefaultParameterizedLayoutAlgorithmBase<TVertex, TEdge, TGraph, BalloonTreeLayoutParameters>
        where TEdge : IEdge<TVertex>
        where TGraph : IBidirectionalGraph<TVertex, TEdge>
    {
        
        private readonly TVertex _root;

        
        private readonly IDictionary<TVertex, BalloonData> _data = new Dictionary<TVertex, BalloonData>();

        [NotNull, ItemNotNull]
        private readonly HashSet<TVertex> _visitedVertices = new HashSet<TVertex>();

        private sealed class BalloonData
        {
            public int D;
            public int R;
            public float A;
            public float C;
            public float F;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonTreeLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="selectedVertex">Root vertex.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="selectedVertex"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="selectedVertex"/> is not part of <paramref name="visitedGraph"/>.</exception>
        public BalloonTreeLayoutAlgorithm(
             TGraph visitedGraph,
             TVertex selectedVertex,
            [CanBeNull] BalloonTreeLayoutParameters parameters = null)
            : this(visitedGraph, null, selectedVertex, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonTreeLayoutAlgorithm{TVertex,TEdge,TGraph}"/> class.
        /// </summary>
        /// <param name="visitedGraph">Graph to layout.</param>
        /// <param name="verticesPositions">Vertices positions.</param>
        /// <param name="selectedVertex">Root vertex.</param>
        /// <param name="parameters">Optional algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="visitedGraph"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="selectedVertex"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="selectedVertex"/> is not part of <paramref name="visitedGraph"/>.</exception>
        public BalloonTreeLayoutAlgorithm(
             TGraph visitedGraph,
            [CanBeNull] IDictionary<TVertex, Point> verticesPositions,
             TVertex selectedVertex,
            [CanBeNull] BalloonTreeLayoutParameters parameters = null)
            : base(visitedGraph, verticesPositions, parameters)
        {
            if (selectedVertex == null)
                throw new ArgumentNullException(nameof(selectedVertex));
            if (!visitedGraph.ContainsVertex(selectedVertex))
                throw new ArgumentException("The provided vertex is not part of the graph.", nameof(selectedVertex));

            _root = selectedVertex;
        }

        #region AlgorithmBase

        /// <inheritdoc />
        protected override void Initialize()
        {
            InitializeData();
        }

        /// <inheritdoc />
        protected override void InternalCompute()
        {
            FirstWalk(_root);

            _visitedVertices.Clear();

            SecondWalk(_root, 0, 0, 1, 0);

            NormalizePositions();
        }

        #endregion

        private void InitializeData()
        {
            foreach (var vertex in VisitedGraph.Vertices)
            {
                _data[vertex] = new BalloonData();
            }

            _visitedVertices.Clear();
        }

        private void FirstWalk( TVertex vertex)
        {
            Debug.Assert(vertex != null);

            var data = _data[vertex];
            _visitedVertices.Add(vertex);
            data.D = 0;

            float s = 0;

            foreach (var target in VisitedGraph.OutEdges(vertex).Select(outEdge => outEdge.Target))
            {
                var otherData = _data[target];
                if (!_visitedVertices.Contains(target))
                {
                    FirstWalk(target);
                    data.D = Math.Max(data.D, otherData.R);
                    otherData.A = (float)Math.Atan((float)otherData.R / (data.D + otherData.R));
                    s += otherData.A;
                }
            }

            AdjustChildren(data, s);
            SetRadius(data);
        }

        private void SecondWalk( TVertex vertex, double x, double y, float l, float t)
        {
            Debug.Assert(vertex != null);

            var position = new Point(x, y);
            VerticesPositions[vertex] = position;
            _visitedVertices.Add(vertex);
            var data = _data[vertex];

            var dd = l * data.D;
            var p = (float)(t + Math.PI);
            var degree = VisitedGraph.OutDegree(vertex);
            var fs = degree == 0 ? 0 : data.F / degree;
            float pr = 0;

            foreach (var target in VisitedGraph.OutEdges(vertex).Select(outEdge => outEdge.Target))
            {
                if (_visitedVertices.Contains(target))
                    continue;

                var otherData = _data[target];
                var aa = data.C * otherData.A;
                var rr = (float)(data.D * Math.Tan(aa) / (1 - Math.Tan(aa)));
                p += pr + aa + fs;

                var xx = (float)((l * rr + dd) * Math.Cos(p));
                var yy = (l * rr + dd) * Math.Sign(p);
                pr = aa;
                SecondWalk(target, x + xx, y + yy, l * data.C, p);
            }
        }

        private void SetRadius( BalloonData data)
        {
            Debug.Assert(data != null);

            data.R = Math.Max(data.D / 2, Parameters.MinRadius);
        }

        private static void AdjustChildren( BalloonData data, float s)
        {
            Debug.Assert(data != null);

            if (s > Math.PI)
            {
                data.C = (float)Math.PI / s;
                data.F = 0;
            }
            else
            {
                data.C = 1;
                data.F = (float)Math.PI - s;
            }
        }
    }
}