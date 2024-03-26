﻿using HamiltonVisualizer.Constants;
using HamiltonVisualizer.GraphUIComponents;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HamiltonVisualizer.Core
{
    /// <summary>
    /// Drawing manager.
    /// </summary>
    /// <param name="Canvas">The canvas on which this class draws.</param>
    /// <param name="Edges">Collection of edges represented by a <see cref="Line"/>.</param>
    public class DrawManager(Canvas Canvas, List<Line> Edges)
    {
        /// <summary>
        /// Draw a <see cref="Line"/> and add to the collection.
        /// </summary>
        public bool Draw(Node src, Node dst, out Line? line)
        {
            // TODO: this method may return false if something happen

            Line myLine = new()
            {
                Stroke = Brushes.Black,
                X1 = src.Origin.X,
                X2 = dst.Origin.X,
                Y1 = src.Origin.Y,
                Y2 = dst.Origin.Y,
                StrokeThickness = 2,
            };
            // Line is sealed so :(((((((( have to inline in here
            Panel.SetZIndex(myLine, (int)ZIndexConstants.Line);

            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Canvas.Children.Add(myLine);


            Canvas.GetTop(src);

            src.ReleaseSelectMode();
            dst.ReleaseSelectMode();

            Edges.Add(myLine);

            line = myLine;

            return true;
        }
    }
}
