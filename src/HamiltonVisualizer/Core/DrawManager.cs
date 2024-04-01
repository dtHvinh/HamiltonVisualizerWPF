﻿using HamiltonVisualizer.Core.CustomControls.WPFBorder;
using HamiltonVisualizer.Core.CustomControls.WPFLinePolygon;
using HamiltonVisualizer.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace HamiltonVisualizer.Core
{
    /// <summary>
    /// Drawing manager.
    /// </summary>
    /// <param exception="Canvas">The canvas on which this class draws.</param>
    public class DrawManager(Canvas Canvas)
    {
        /// <summary>
        /// Draw a <see cref="Line"/> and add to the collection.
        /// </summary>
        public bool Draw(Node src, Node dst, [NotNullWhen(true)] out LinePolygonWrapper? obj)
        {
            var edge = LinePolygonWrapper.Create(src.Origin, dst.Origin);

            src.Attach(new LinePolygonWrapperAttachInfo(edge, src, AttachPosition.Head));
            src.Attach(new LinePolygonWrapperAttachInfo(edge, dst, AttachPosition.Tail));

            Canvas.Children.Add(edge);
            obj = edge;
            return true;
        }
    }
}
