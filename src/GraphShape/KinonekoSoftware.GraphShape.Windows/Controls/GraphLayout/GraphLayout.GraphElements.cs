using System.ComponentModel;
using System.Diagnostics;
using GraphShape.Utils;
using JetBrains.Annotations;
using QuikGraph;

namespace GraphShape.Controls
{
    public partial class GraphLayout<TVertex, TEdge, TGraph>
        where TVertex : class
        where TEdge : IEdge<TVertex>
        where TGraph : class, IBidirectionalGraph<TVertex, TEdge>
    {
        /// <summary>
        /// Removes the given <paramref name="vertex"/> from graph.
        /// </summary>
        /// <param name="vertex">Vertex to remove.</param>
        protected virtual void RemoveVertexControl( TVertex vertex)
        {
            RunDestructionTransition(VerticesControls[vertex], false);
            VerticesControls.Remove(vertex);
        }

        /// <summary>
        /// Removes the given <paramref name="edge"/> from graph.
        /// </summary>
        /// <param name="edge">Edge to remove.</param>
        protected virtual void RemoveEdgeControl( TEdge edge)
        {
            RunDestructionTransition(EdgesControls[edge], false);
            EdgesControls.Remove(edge);
        }

        /// <summary>
        /// Removes all vertices and edges from graph.
        /// </summary>
        protected void RemoveAllGraphElements()
        {
            foreach (var vertex in VerticesControls.Keys.ToArray())
            {
                RemoveVertexControl(vertex);
            }

            foreach (var edge in EdgesControls.Keys.ToArray())
            {
                RemoveEdgeControl(edge);
            }

            VerticesControls.Clear();
            EdgesControls.Clear();
        }

        /// <summary>
        /// If the graph has been changed, the elements will be regenerated.
        /// </summary>
        protected void RecreateGraphElements(bool tryKeepControls)
        {
            if (Graph is null)
            {
                RemoveAllGraphElements();
            }
            else
            {
                if (tryKeepControls && !IsCompoundMode)
                {
                    // Remove the old graph elements
                    RemoveGraphControls();
                }
                else
                {
                    RemoveAllGraphElements();
                }

                CreateGraphControls();
            }

            Sizes = null;
        }

        private void RemoveGraphControls()
        {
            foreach (KeyValuePair<TEdge, EdgeControl> pair in EdgesControls.ToArray())
            {
                var remove = false;
                try
                {
                    remove = !Graph.ContainsEdge(pair.Key.Source, pair.Key.Target)
                             || !Graph.ContainsEdge(pair.Key);
                }
                catch
                {
                    // ignored
                }

                if (remove)
                {
                    RemoveEdgeControl(pair.Key);
                }
            }

            foreach (KeyValuePair<TVertex, VertexControl> pair in VerticesControls.ToArray())
            {
                if (!Graph.ContainsVertex(pair.Key))
                {
                    RemoveVertexControl(pair.Key);
                }
            }
        }

        private void CreateGraphControls()
        {
            // Vertices controls
            foreach (var vertex in Graph.Vertices)
            {
                if (!VerticesControls.ContainsKey(vertex))
                {
                    CreateVertexControl(vertex);
                }
            }

            // Edges controls
            foreach (var edge in Graph.Edges)
            {
                if (!EdgesControls.ContainsKey(edge))
                {
                    CreateEdgeControl(edge);
                }
            }
        }

        private void DoNotificationLayout()
        {
            lock (_notificationSyncRoot)
            {
                _lastNotificationTimestamp = DateTime.Now;
            }

            if (Worker != null)
                return;

            Worker = new BackgroundWorker();
            Worker.DoWork += (sender, args) =>
            {
                var worker = (BackgroundWorker)sender;
                lock (_notificationSyncRoot)
                {
                    while (DateTime.Now - _lastNotificationTimestamp < _notificationLayoutDelay)
                    {
                        Thread.Sleep(_notificationLayoutDelay);
                        if (worker.CancellationPending)
                            break;
                    }
                }
            };

            Worker.RunWorkerCompleted += (sender, args) =>
            {
                Worker = null;
                OnMutation();
                ContinueLayout();
                HighlightAlgorithm?.ResetHighlight();
            };

            Worker.RunWorkerAsync();
        }

        private void OnMutation()
        {
            while (_edgesRemoved.Count > 0)
            {
                var edge = _edgesRemoved.Dequeue();
                RemoveEdgeControl(edge);
            }

            while (_verticesRemoved.Count > 0)
            {
                var vertex = _verticesRemoved.Dequeue();
                RemoveVertexControl(vertex);
            }

            TVertex[] verticesToInitPos = _verticesAdded.ToArray();
            while (_verticesAdded.Count > 0)
            {
                var vertex = _verticesAdded.Dequeue();
                CreateVertexControl(vertex);
            }

            while (_edgesAdded.Count > 0)
            {
                var edge = _edgesAdded.Dequeue();
                CreateEdgeControl(edge);
            }

            foreach (var vertex in verticesToInitPos)
            {
                InitializePosition(vertex);
            }
        }

        private void OnMutableGraphEdgeRemoved( TEdge edge)
        {
            if (EdgesControls.ContainsKey(edge))
            {
                _edgesRemoved.Enqueue(edge);
                DoNotificationLayout();
            }
        }

        private void OnMutableGraphEdgeAdded( TEdge edge)
        {
            _edgesAdded.Enqueue(edge);
            DoNotificationLayout();
        }

        private void OnMutableGraphVertexRemoved( TVertex vertex)
        {
            if (VerticesControls.ContainsKey(vertex))
            {
                _verticesRemoved.Enqueue(vertex);
                DoNotificationLayout();
            }
        }

        private void OnMutableGraphVertexAdded( TVertex vertex)
        {
            _verticesAdded.Enqueue(vertex);
            DoNotificationLayout();
        }

        /// <summary>
        /// Gets the <see cref="VertexControl"/> corresponding to the given <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Graph vertex.</param>
        /// <returns>The corresponding <see cref="VertexControl"/>, <see langword="null"/> otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="vertex"/> is <see langword="null"/>.</exception>
        [Pure]
        [CanBeNull]
        public VertexControl GetVertexControl( TVertex vertex)
        {
            return VerticesControls.TryGetValue(vertex, out var control)
                ? control
                : null;
        }

        /// <summary>
        /// Gets or creates a <see cref="VertexControl"/> for the given <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Graph vertex.</param>
        /// <returns>A <see cref="VertexControl"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="vertex"/> is <see langword="null"/>.</exception>
        
        protected VertexControl GetOrCreateVertexControl( TVertex vertex)
        {
            var vertexControl = GetVertexControl(vertex);
            if (vertexControl is null)
                return CreateVertexControl(vertex);
            return vertexControl;
        }

        /// <summary>
        /// Creates a <see cref="VertexControl"/> for the given <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Graph vertex.</param>
        /// <returns>A <see cref="VertexControl"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="vertex"/> is <see langword="null"/>.</exception>
        
        protected virtual VertexControl CreateVertexControl( TVertex vertex)
        {
            var compoundGraph = Graph as ICompoundGraph<TVertex, TEdge>;

            VertexControl vertexControl;
            if (IsCompoundMode && compoundGraph != null && compoundGraph.IsCompoundVertex(vertex))
            {
                var compoundVertexControl = new CompoundVertexControl
                {
                    Vertex = vertex,
                    DataContext = vertex,
                };
                vertexControl = compoundVertexControl;
            }
            else
            {
                // Create the Control of the vertex
                vertexControl = new VertexControl
                {
                    Vertex = vertex,
                    DataContext = vertex,
                };
            }

            VerticesControls[vertex] = vertexControl;
            vertexControl.RootCanvas = this;

            if (IsCompoundMode && compoundGraph != null && compoundGraph.IsChildVertex(vertex))
            {
                var parent = compoundGraph.GetParent(vertex);

                Debug.Assert(parent != null, "Vertex considered as child one has no parent.");

                var parentControl = GetOrCreateVertexControl(parent) as CompoundVertexControl;

                Debug.Assert(parentControl != null);

                parentControl.Vertices.Add(vertexControl);
            }
            else
            {
                // Add the presenter to the GraphLayout
                Children.Add(vertexControl);
            }

            // Measure & Arrange
            vertexControl.InvalidateMeasure();
            SetHighlightProperties(vertex, vertexControl);
            RunCreationTransition(vertexControl);

            return vertexControl;
        }

        /// <summary>
        /// Initializes the position of the given <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Graph vertex.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="vertex"/> is <see langword="null"/>.</exception>
        protected virtual void InitializePosition( TVertex vertex)
        {
            var vertexControl = VerticesControls[vertex];
            // Initialize position
            if (Graph.ContainsVertex(vertex)
                && Graph.Degree(vertex) > 0
                && TryComputePosition(vertex, out var position))
            {
                SetX(vertexControl, position.X);
                SetY(vertexControl, position.Y);
            }
        }

        [Pure]
        private bool TryComputePosition( TVertex vertex, out Point position)
        {
            position = default(Point);
            
            var count = 0;
            foreach (var neighbor in Graph.GetNeighbors(vertex))
            {
                if (VerticesControls.TryGetValue(neighbor, out var neighborControl))
                {
                    var x = GetX(neighborControl);
                    var y = GetY(neighborControl);
                    position.X += double.IsNaN(x) ? 0.0 : x;
                    position.Y += double.IsNaN(y) ? 0.0 : y;
                    ++count;
                }
            }

            if (count > 0)
            {
                position.X /= count;
                position.Y /= count;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the <see cref="EdgeControl"/> corresponding to the given <paramref name="edge"/>.
        /// </summary>
        /// <param name="edge">Graph edge.</param>
        /// <returns>The corresponding <see cref="EdgeControl"/>, <see langword="null"/> otherwise.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="edge"/> is <see langword="null"/>.</exception>
        [Pure]
        [CanBeNull]
        public EdgeControl GetEdgeControl( TEdge edge)
        {
            return EdgesControls.TryGetValue(edge, out var control)
                ? control
                : null;
        }

        /// <summary>
        /// Gets or creates a <see cref="EdgeControl"/> for the given <paramref name="edge"/>.
        /// </summary>
        /// <param name="edge">Graph edge.</param>
        /// <returns>A <see cref="EdgeControl"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="edge"/> is <see langword="null"/>.</exception>
        
        protected EdgeControl GetOrCreateEdgeControl( TEdge edge)
        {
            var edgeControl = GetEdgeControl(edge);
            if (edgeControl is null)
                return CreateEdgeControl(edge);
            return edgeControl;
        }

        /// <summary>
        /// Creates a <see cref="EdgeControl"/> for the given <paramref name="edge"/>.
        /// </summary>
        /// <param name="edge">Graph edge.</param>
        /// <returns>A <see cref="EdgeControl"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="edge"/> is <see langword="null"/>.</exception>
        
        protected virtual EdgeControl CreateEdgeControl( TEdge edge)
        {
            var edgeControl = new EdgeControl
            {
                Edge = edge,
                DataContext = edge,
            };

            EdgesControls[edge] = edgeControl;

            // Set the Source and the Target
            edgeControl.Source = VerticesControls[edge.Source];
            edgeControl.Target = VerticesControls[edge.Target];

            if (ActualLayoutMode == Algorithms.Layout.LayoutMode.Simple)
            {
                Children.Insert(0, edgeControl);
            }
            else
            {
                Children.Add(edgeControl);
            }
            SetHighlightProperties(edge, edgeControl);
            RunCreationTransition(edgeControl);

            return edgeControl;
        }
    }
}