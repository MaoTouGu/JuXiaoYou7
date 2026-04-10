// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure
{
    using System;
    using System.IO;
    using System.Reflection;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Avalonia.Media.Imaging;

    public static class VisualCaptureUtil
    {
        private const double BaseDpi = 96;

        public static void SaveVisual(Visual visual, int dpi, string filename)
        {
            // sometimes RenderTargetBitmap doesn't render the Visual or doesn't render the Visual properly
            // below i am using the trick that jamie rodriguez posted on his blog
            // where he wraps the Visual inside of a VisualBrush and then renders it.
            // http://blogs.msdn.com/b/jaimer/archive/2009/07/03/rendertargetbitmap-tips.aspx

            var visualBrush = CreateVisualBrushSafe(visual);

            if (visual is null
                || visualBrush is null)
            {
                return;
            }

            var renderTargetBitmap = RenderVisualWithHighQuality(visual, dpi, dpi);

            SaveAsPng(renderTargetBitmap, filename);
        }

        public static RenderTargetBitmap SaveVisual(Visual visual, int dpi)
        {
            // sometimes RenderTargetBitmap doesn't render the Visual or doesn't render the Visual properly
            // below i am using the trick that jamie rodriguez posted on his blog
            // where he wraps the Visual inside of a VisualBrush and then renders it.
            // http://blogs.msdn.com/b/jaimer/archive/2009/07/03/rendertargetbitmap-tips.aspx

            var visualBrush = CreateVisualBrushSafe(visual);

            if (visual is null || visualBrush is null)
            {
                return null;
            }

            return RenderVisualWithHighQuality(visual, dpi, dpi);
        }

        public static VisualBrush CreateVisualBrushSafe(Visual visual)
        {
            return IsSafeToVisualize(visual)
                ? new VisualBrush(visual)
                : null;
        }

        public static bool IsSafeToVisualize(Visual visual)
        {
            if (visual is null)
            {
                return false;
            }

            // Avalonia doesn't use PresentationSource like WPF
            return true;
        }

        private static void SaveAsPng(RenderTargetBitmap bitmap, string filename)
        {
            using (var fileStream = File.Create(filename))
            {
                bitmap.Save(fileStream);
            }
        }

        /// <summary>
        /// Draws <paramref name="visual"/> in smaller tiles using multiple <see cref="VisualBrush"/>.
        /// </summary>
        /// <remarks>
        /// This way we workaround a limitation in <see cref="VisualBrush"/> which causes poor quality for larger visuals.
        /// </remarks>
        public static RenderTargetBitmap RenderVisualWithHighQuality(Visual visual, int dpiX, int dpiY, PixelFormat? pixelFormat = null, object viewport3D = null)
        {
            var size = GetSize(visual);

            var drawingVisual = new DrawingVisual();

            using (var drawingContext = drawingVisual.RenderOpen())
            {
                DrawVisualInTiles(visual, drawingContext, size);
            }

            return RenderVisual(drawingVisual, size, dpiX, dpiY, pixelFormat, viewport3D);
        }

        public static RenderTargetBitmap RenderVisual(Visual visual, Size bounds, int dpiX, int dpiY, PixelFormat? pixelFormat = null, object viewport3D = null)
        {
            var scaleX = dpiX / BaseDpi;
            var scaleY = dpiY / BaseDpi;

            pixelFormat ??= PixelFormats.Pbgra32;

            var renderTargetBitmap = new RenderTargetBitmap(new PixelSize((int)Math.Ceiling(scaleX * bounds.Width), (int)Math.Ceiling(scaleY * bounds.Height)), new Vector(dpiX, dpiY));

            renderTargetBitmap.Render(visual);

            return renderTargetBitmap;
        }

        private static Size GetSize(Visual visual)
        {
            if (visual is Control control)
            {
                return control.Bounds.Size;
            }

            var descendantBounds = VisualTreeHelper.GetDescendantBounds(visual);
            return new Size(descendantBounds.Width, descendantBounds.Height);
        }

        /// <summary>
        /// Draws <paramref name="visual"/> in smaller tiles using multiple <see cref="VisualBrush"/> to <paramref name="drawingContext"/>.
        /// This way we workaround a limitation in <see cref="VisualBrush"/> which causes poor quality for larger visuals.
        /// </summary>
        /// <param name="visual">The visual to be drawn.</param>
        /// <param name="drawingContext">The <see cref="DrawingContext"/> to use.</param>
        /// <param name="visualSize">The size of <paramref name="visual"/>.</param>
        /// <param name="tileWidth">The width of one tile.</param>
        /// <param name="tileHeight">The height of one tile.</param>
        /// <remarks>
        /// Original version of this method was copied from https://srndolha.wordpress.com/2012/10/16/exported-drawingvisual-quality-when-using-visualbrush/
        ///
        /// A tile size of 32x32 turned out deliver the best quality while not increasing computation time too much.
        /// </remarks>
        private static void DrawVisualInTiles(Visual visual, DrawingContext drawingContext, Size visualSize, double tileWidth = 32, double tileHeight = 32)
        {
            var visualWidth  = visualSize.Width;
            var visualHeight = visualSize.Height;

            var verticalTileCount   = visualHeight / tileHeight;
            var horizontalTileCount = visualWidth  / tileWidth;

            for (var i = 0; i <= verticalTileCount; i++)
            {
                for (var j = 0; j <= horizontalTileCount; j++)
                {
                    var width  = tileWidth;
                    var height = tileHeight;

                    // Check if we would exceed the width of the visual and limit it by the remaining
                    if ((j + 1) * tileWidth > visualWidth)
                    {
                        width = visualWidth - (j * tileWidth);
                    }

                    // Check if we would exceed the height of the visual and limit it by the remaining
                    if ((i + 1) * tileHeight > visualHeight)
                    {
                        height = visualHeight - (i * tileHeight);
                    }

                    var x = j * tileWidth;
                    var y = i * tileHeight;

                    var rectangle = new Rect(x, y, width, height);

                    var contentBrush = new VisualBrush(visual)
                    {
                        Stretch      = Stretch.None,
                        AlignmentX   = AlignmentX.Left,
                        AlignmentY   = AlignmentY.Top,
                        Viewbox      = rectangle,
                        ViewboxUnits = BrushMappingMode.Absolute
                    };

                    drawingContext.DrawRectangle(contentBrush, null, rectangle);
                }
            }
        }
    }
}