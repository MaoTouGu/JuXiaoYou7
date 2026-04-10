using static GraphShape.Utils.MathUtils;

namespace GraphShape.Algorithms.OverlapRemoval
{
    /// <summary>
    /// One way Force-Scan Algorithm (FSA).
    /// </summary>
    /// <typeparam name="TObject">Object type.</typeparam>
    public class OneWayFSAAlgorithm<TObject> : FSAAlgorithm<TObject, OneWayFSAParameters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayFSAAlgorithm{TObject}"/> class.
        /// </summary>
        /// <param name="rectangles">Overlap rectangles.</param>
        /// <param name="parameters">Algorithm parameters.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="rectangles"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="parameters"/> is <see langword="null"/>.</exception>
        public OneWayFSAAlgorithm(
             IDictionary<TObject, Rect> rectangles,
             OneWayFSAParameters parameters)
            : base(rectangles, parameters)
        {
        }

        #region OverlapRemovalAlgorithmBase

        /// <inheritdoc />
        protected override void RemoveOverlap()
        {
            switch (Parameters.Way)
            {
                case OneWayFSAWay.Horizontal:
                    HorizontalImproved();
                    break;
                case OneWayFSAWay.Vertical:
                    VerticalImproved();
                    break;
            }
        }

        #endregion

        /// <inheritdoc cref="FSAAlgorithm{TObject,TParameters}.HorizontalImproved"/>
        protected new double HorizontalImproved()
        {
            WrappedRectangles.Sort(XComparison);
            var i = 0;
            var n = WrappedRectangles.Count;

            // Left side
            var leftMin = WrappedRectangles[0];
            double sigma = 0;
            var x0 = leftMin.CenterX;
            var gamma = new double[WrappedRectangles.Count];
            var x = new double[WrappedRectangles.Count];
            while (i < n)
            {
                var u = WrappedRectangles[i];

                // Rectangle with the same center than Rectangle[i]
                var k = i;
                for (var j = i + 1; j < n; ++j)
                {
                    ThrowIfCancellationRequested();

                    var v = WrappedRectangles[j];
                    if (NearEqual(u.CenterX, v.CenterX))
                    {
                        u = v;
                        k = j;
                    }
                    else
                    {
                        break;
                    }
                }

                double g = 0;
                for (var z = i + 1; z <= k; ++z)
                {
                    ThrowIfCancellationRequested();

                    var v = WrappedRectangles[z];
                    v.Rectangle.X += (z - i) * 0.0001;
                }

                // For rectangles in [i, k], compute the left force
                if (u.CenterX > x0)
                {
                    for (var m = i; m <= k; ++m)
                    {
                        double ggg = 0;
                        for (var j = 0; j < i; ++j)
                        {
                            ThrowIfCancellationRequested();

                            var force = Force(WrappedRectangles[j].Rectangle, WrappedRectangles[m].Rectangle);
                            ggg = Math.Max(force.X + gamma[j], ggg);
                        }

                        var v = WrappedRectangles[m];
                        var gg = v.Rectangle.Left + ggg < leftMin.Rectangle.Left ? sigma : ggg;
                        g = Math.Max(g, gg);
                    }
                }

                // Compute offset to elements in x
                // and redefine left side
                for (var m = i; m <= k; ++m)
                {
                    ThrowIfCancellationRequested();

                    gamma[m] = g;
                    var r = WrappedRectangles[m];
                    x[m] = r.Rectangle.Left + g;
                    if (r.Rectangle.Left < leftMin.Rectangle.Left)
                    {
                        leftMin = r;
                    }
                }

                // Compute the right force of rectangles in [i, k] and store the maximal one
                // delta = max(0, max{f.x(m,j)|i<=m<=k<j<n})
                double delta = 0;
                for (var m = i; m <= k; ++m)
                {
                    for (var j = k + 1; j < n; ++j)
                    {
                        ThrowIfCancellationRequested();

                        var force = Force(WrappedRectangles[m].Rectangle, WrappedRectangles[j].Rectangle);
                        if (force.X > delta)
                        {
                            delta = force.X;
                        }
                    }
                }
                sigma += delta;
                i = k + 1;
            }

            double cost = 0;
            for (i = 0; i < n; ++i)
            {
                var r = WrappedRectangles[i];
                var oldPos = r.Rectangle.Left;
                var newPos = x[i];

                r.Rectangle.X = newPos;

                var diff = oldPos - newPos;
                cost += diff * diff;
            }

            return cost;
        }
    }
}