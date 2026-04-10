using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using GraphShape.Algorithms.Layout;
using GraphShape.Controls.Extensions;
using JetBrains.Annotations;

namespace GraphShape.Controls.Converters
{
    /// <summary>
    /// Converter of position and sizes of the source and target points,
    /// and the route information of an edge to a path.
    /// </summary>
    /// <remarks>The edge can bend, or it can be straight line.</remarks>
    public class EdgeRouteToPathConverter : IMultiValueConverter
    {
        #region IMultiValueConverter

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentException">
        /// At least one of 9 arguments is missing.
        /// pos (1,2), size (3,4) of source; pos (5,6), size (7,8) of target; routeInformation (9)
        /// </exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null)
                return new PathFigureCollection(0);

            if (values.Length != 9)
            {
                throw new ArgumentException(
                    $"{nameof(EdgeRouteToPathConverter)} must have 9 parameters: pos (1,2), size (3,4) of source; pos (5,6), size (7,8) of target; routeInformation (9).",
                    nameof(values));
            }

            ExtractInputs(
                values,
                out var sourcePos,
                out var targetPos,
                out var sourceSize,
                out var targetSize,
                out var routeInformation);

            var hasRouteInfo = routeInformation != null && routeInformation.Length > 0;

            // Create the path
            var p1 = LayoutUtils.GetClippingPoint(
                                                  sourceSize,
                                                  sourcePos,
                                                  hasRouteInfo ? routeInformation[0].ToGraphShapePoint() : targetPos).ToPoint();

            var p2 = LayoutUtils.GetClippingPoint(
                                                  targetSize,
                                                  targetPos,
                                                  hasRouteInfo ? routeInformation[^1].ToGraphShapePoint() : sourcePos).ToPoint();


            var segments = new PathSegment[1 + (hasRouteInfo ? routeInformation.Length : 0)];
            if (hasRouteInfo)
            {
                // Append route points
                for (var i = 0; i < routeInformation.Length; ++i)
                {
                    segments[i] = new LineSegment(routeInformation[i], true);
                }
            }

            var pLast = hasRouteInfo ? routeInformation[^1] : p1;
            var v     = pLast - p2;
            v = v / v.Length * 5;
            var n = new System.Windows.Vector(-v.Y, v.X) * 0.3;

            segments[^1] = new LineSegment(p2 + v, true);

            var pathCollection = new PathFigureCollection(2)
            {
                new PathFigure(p1, segments, false),
                new PathFigure(
                    p2,
                    new PathSegment[]
                    {
                        new LineSegment(p2 + v - n, true),
                        new LineSegment(p2 + v + n, true),
                    },
                    true),
            };

            return pathCollection;
        }

        private static void ExtractInputs(
            [NotNull, ItemNotNull] object[] values,
            out Point sourcePos,
            out Point targetPos,
            out Size sourceSize,
            out Size targetSize,
            out System.Windows.Point[] routeInformation)
        {
            
            var v0 = values[0];
            var v1 = values[1];
            var v2 = values[2];
            var v3 = values[3];
            var v4 = values[4];
            var v5 = values[5];
            var v6 = values[6];
            var v7 = values[7];
            var v8 = values[8];
            // Get the position of the source
            sourcePos = new Point(
                v0 != DependencyProperty.UnsetValue ? (double) v0 : 0.0,
                v1 != DependencyProperty.UnsetValue ? (double) v1 : 0.0);

            // Get the size of the source
            sourceSize = new Size(
                v2 != DependencyProperty.UnsetValue ? (double) v2 : 0.0,
                v3 != DependencyProperty.UnsetValue ? (double) v3 : 0.0);

            // Get the position of the target
            targetPos = new Point(
                v4 != DependencyProperty.UnsetValue ? (double) v4 : 0.0,
                v5 != DependencyProperty.UnsetValue ? (double) v5 : 0.0);

            // Get the size of the target
            targetSize = new Size(
                v6 != DependencyProperty.UnsetValue ? (double) v6 : 0.0,
                v7 != DependencyProperty.UnsetValue ? (double) v7 : 0.0);

            // Get the route information
            routeInformation = values[8] != DependencyProperty.UnsetValue ? (System.Windows.Point[]) values[8] : null;
        }

        /// <inheritdoc />
        /// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Path to edge route conversion not supported.");
        }

        #endregion
    }
}