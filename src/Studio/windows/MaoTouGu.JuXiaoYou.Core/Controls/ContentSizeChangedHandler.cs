using System.Diagnostics.CodeAnalysis;

namespace MaoTouGu.Studio.Controls
{


    /// <summary>
    /// Handler for a content size changed event.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="newSize">New content size.</param>
    public delegate void ContentSizeChangedHandler(object sender, Size newSize);

    /// <summary>
    /// Zoom view modes.
    /// </summary>
    public enum ZoomViewModifierMode
    {
        /// <summary>
        /// It does nothing at all.
        /// </summary>
        None,

        /// <summary>
        /// You can pan the view with the mouse in this mode.
        /// </summary>
        Pan,

        /// <summary>
        /// You can zoom in with the mouse in this mode.
        /// </summary>
        ZoomIn,

        /// <summary>
        /// You can zoom out with the mouse in this mode.
        /// </summary>
        ZoomOut,

        /// <summary>
        /// Zooming after the user has been selected the zooming box.
        /// </summary>
        ZoomBox,
    }

}