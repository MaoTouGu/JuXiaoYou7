#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;
using QuikGraph;

namespace GraphShape
{
    /// <summary>
    /// Hierarchical graph.
    /// </summary>
    /// <typeparam name="TVertex">Vertex type.</typeparam>
    /// <typeparam name="TEdge">Edge type.</typeparam>
    [Serializable]
    public class HierarchicalGraph<TVertex, TEdge>
        : BidirectionalGraph<TVertex, TEdge>
        , IHierarchicalBidirectionalGraph<TVertex, TEdge>
        where TEdge : TypedEdge<TVertex>
    {
        private sealed class TypedEdgeCollectionWrapper
        {
            [NotNull, ItemNotNull]
            public readonly List<TEdge> SelfHierarchicalEdges = new List<TEdge>();

            [NotNull, ItemNotNull]
            public readonly List<TEdge> InHierarchicalEdges = new List<TEdge>();

            [NotNull, ItemNotNull]
            public readonly List<TEdge> OutHierarchicalEdges = new List<TEdge>();

            [NotNull, ItemNotNull]
            public readonly List<TEdge> SelfGeneralEdges = new List<TEdge>();

            [NotNull, ItemNotNull]
            public readonly List<TEdge> InGeneralEdges = new List<TEdge>();

            [NotNull, ItemNotNull]
            public readonly List<TEdge> OutGeneralEdges = new List<TEdge>();
        }

        
        private readonly Dictionary<TVertex, TypedEdgeCollectionWrapper> _typedEdgeCollections =
            new Dictionary<TVertex, TypedEdgeCollectionWrapper>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalGraph{TVertex,TEdge}"/> class.
        /// </summary>
        public HierarchicalGraph()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalGraph{TVertex,TEdge}"/> class.
        /// </summary>
        /// <param name="allowParallelEdges">Indicates if parallel edges are allowed.</param>
        public HierarchicalGraph(bool allowParallelEdges)
            : base(allowParallelEdges)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalGraph{TVertex,TEdge}"/> class.
        /// </summary>
        /// <param name="allowParallelEdges">Indicates if parallel edges are allowed.</param>
        /// <param name="capacity">Vertex capacity.</param>
        public HierarchicalGraph(bool allowParallelEdges, int capacity)
            : base(allowParallelEdges, capacity)
        {
        }

        #region Add/Remove Vertex

        /// <inheritdoc />
        public override bool AddVertex(TVertex vertex)
        {
            var added = base.AddVertex(vertex);
            if (!_typedEdgeCollections.ContainsKey(vertex))
            {
                _typedEdgeCollections[vertex] = new TypedEdgeCollectionWrapper();
            }
            return added;
        }

        /// <inheritdoc />
        public override bool RemoveVertex(TVertex vertex)
        {
            var removed = base.RemoveVertex(vertex);
            if (removed)
            {
                // Remove the edges from the _typedEdgeCollections
                var edgeCollection = _typedEdgeCollections[vertex];
                foreach (var edge in edgeCollection.InGeneralEdges)
                {
                    _typedEdgeCollections[edge.Source].OutGeneralEdges.Remove(edge);
                }
                foreach (var edge in edgeCollection.OutGeneralEdges)
                {
                    _typedEdgeCollections[edge.Target].InGeneralEdges.Remove(edge);
                }

                foreach (var edge in edgeCollection.InHierarchicalEdges)
                {
                    _typedEdgeCollections[edge.Source].OutHierarchicalEdges.Remove(edge);
                }
                foreach (var edge in edgeCollection.OutHierarchicalEdges)
                {
                    _typedEdgeCollections[edge.Target].InHierarchicalEdges.Remove(edge);
                }

                _typedEdgeCollections.Remove(vertex);
                return true;
            }

            return false;
        }
        
        #endregion

        #region Add/Remove Edge

        /// <inheritdoc />
        public override bool AddEdge(TEdge edge)
        {
            if (!base.AddEdge(edge))
                return false;

            if (edge.IsSelfEdge())
            {
                // Add edge to the collections (same for source and target)
                var edgeCollection = _typedEdgeCollections[edge.Source];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        edgeCollection.SelfGeneralEdges.Add(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        edgeCollection.SelfHierarchicalEdges.Add(edge);
                        break;
                }
            }
            else
            {
                // Add edge to the source collections
                var sourceEdgeCollection = _typedEdgeCollections[edge.Source];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        sourceEdgeCollection.OutGeneralEdges.Add(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        sourceEdgeCollection.OutHierarchicalEdges.Add(edge);
                        break;
                }

                // Add edge to the target collections
                var targetEdgeCollection = _typedEdgeCollections[edge.Target];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        targetEdgeCollection.InGeneralEdges.Add(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        targetEdgeCollection.InHierarchicalEdges.Add(edge);
                        break;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool RemoveEdge(TEdge edge)
        {
            if (!base.RemoveEdge(edge))
                return false;

            if (edge.IsSelfEdge())
            {
                // Remove edge from the collections (same for source and target)
                var edgeCollection = _typedEdgeCollections[edge.Source];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        edgeCollection.SelfGeneralEdges.Remove(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        edgeCollection.SelfHierarchicalEdges.Remove(edge);
                        break;
                }
            }
            else
            {
                // Remove edge from the source collections
                var sourceEdgeCollection = _typedEdgeCollections[edge.Source];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        sourceEdgeCollection.OutGeneralEdges.Remove(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        sourceEdgeCollection.OutHierarchicalEdges.Remove(edge);
                        break;
                }

                // Remove edge from the target collections
                var targetEdgeCollection = _typedEdgeCollections[edge.Target];
                switch (edge.Type)
                {
                    case EdgeTypes.General:
                        targetEdgeCollection.InGeneralEdges.Remove(edge);
                        break;
                    case EdgeTypes.Hierarchical:
                        targetEdgeCollection.InHierarchicalEdges.Remove(edge);
                        break;
                }
            }

            return true;
        }

        #endregion

        #region IHierarchicalBidirectionalGraph<TVertex,TEdge>

        [Pure]
        
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private TypedEdgeCollectionWrapper GetCollectionsAndAssertFor( TVertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException(nameof(vertex));
            if (!_typedEdgeCollections.TryGetValue(vertex, out var collections))
                throw new VertexNotFoundException();
            return collections;
        }

        #region Hierarchical Edges

        /// <inheritdoc />
        public IEnumerable<TEdge> HierarchicalEdges => Vertices.SelectMany(OutHierarchicalEdges);

        /// <inheritdoc />
        public int HierarchicalEdgeCount => Vertices.Sum(InHierarchicalEdgeCount);

        /// <inheritdoc />
        public IEnumerable<TEdge> HierarchicalEdgesFor(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InHierarchicalEdges
                .Concat(collections.OutHierarchicalEdges)
                .Concat(collections.SelfHierarchicalEdges);
        }

        /// <inheritdoc />
        public int HierarchicalEdgeCountFor(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InHierarchicalEdges.Count
                 + collections.OutHierarchicalEdges.Count
                 + collections.SelfHierarchicalEdges.Count;
        }

        /// <inheritdoc />
        public IEnumerable<TEdge> InHierarchicalEdges(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InHierarchicalEdges.Concat(collections.SelfHierarchicalEdges);
        }

        /// <inheritdoc />
        public int InHierarchicalEdgeCount(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InHierarchicalEdges.Count
                 + collections.SelfHierarchicalEdges.Count;
        }

        /// <inheritdoc />
        public IEnumerable<TEdge> OutHierarchicalEdges(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.OutHierarchicalEdges.Concat(collections.SelfHierarchicalEdges);
        }

        /// <inheritdoc />
        public int OutHierarchicalEdgeCount(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.OutHierarchicalEdges.Count
                 + collections.SelfHierarchicalEdges.Count;
        }

        #endregion

        #region General Edges

        /// <inheritdoc />
        public IEnumerable<TEdge> GeneralEdges => Vertices.SelectMany(OutGeneralEdges);

        /// <inheritdoc />
        public int GeneralEdgeCount => Vertices.Sum(InGeneralEdgeCount);

        /// <inheritdoc />
        public IEnumerable<TEdge> GeneralEdgesFor(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InGeneralEdges
                .Concat(collections.OutGeneralEdges)
                .Concat(collections.SelfGeneralEdges);
        }

        /// <inheritdoc />
        public int GeneralEdgeCountFor(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InGeneralEdges.Count
                 + collections.OutGeneralEdges.Count
                 + collections.SelfGeneralEdges.Count;
        }

        /// <inheritdoc />
        public IEnumerable<TEdge> InGeneralEdges(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InGeneralEdges.Concat(collections.SelfGeneralEdges);
        }

        /// <inheritdoc />
        public int InGeneralEdgeCount(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.InGeneralEdges.Count
                 + collections.SelfGeneralEdges.Count;
        }

        /// <inheritdoc />
        public IEnumerable<TEdge> OutGeneralEdges(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.OutGeneralEdges.Concat(collections.SelfGeneralEdges);
        }

        /// <inheritdoc />
        public int OutGeneralEdgeCount(TVertex vertex)
        {
            var collections = GetCollectionsAndAssertFor(vertex);
            return collections.OutGeneralEdges.Count
                   + collections.SelfGeneralEdges.Count;
        }

        #endregion

        #endregion
    }
}