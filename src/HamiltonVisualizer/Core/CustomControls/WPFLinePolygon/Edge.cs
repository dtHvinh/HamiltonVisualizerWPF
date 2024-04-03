﻿using HamiltonVisualizer.Constants;
using HamiltonVisualizer.Core.CustomControls.WPFBorder;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HamiltonVisualizer.Core.CustomControls.WPFLinePolygon
{
    public sealed class Edge
    {
        private readonly Line _body;
        private readonly Polygon _head;

        private double HeadLength { get; set; } = 25;
        private double HeadWidth { get; set; } = 7.5;

        public Node From { get; set; }
        public Node To { get; set; }

        public Line Body => _body;
        public Polygon Head => _head;

        public Edge(Node src, Node dst)
        {
            From = src;
            To = dst;

            _body = InitLine();
            _head = CreateArrowHeadDefault();
        }

        public void ChangeColor(Brush color)
        {
            _head.Stroke = color;
            _body.Fill = color;
        }

        public void ChangeColor(Brush head, Brush body)
        {
            _head.Stroke = head;
            _body.Fill = body;
        }

        private Line InitLine()
        {
            Line line = new()
            {
                Stroke = Brushes.Black,
                X1 = From.Origin.X,
                X2 = To.Origin.X,
                Y1 = From.Origin.Y,
                Y2 = To.Origin.Y,
                StrokeThickness = 2,
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Panel.SetZIndex(line, ConstantValues.ZIndex.Line);

            return line;
        }

        private static Tuple<Point, Point, Point> CreateArrowHead(Point arrowHeadPos, double height, double sideWidth)
        {
            return Tuple.Create(arrowHeadPos, new Point(arrowHeadPos.X + sideWidth, arrowHeadPos.Y + height), new Point(arrowHeadPos.X - sideWidth, arrowHeadPos.Y + height));
        }

        private Polygon CreateArrowHeadDefault()
        {
            var ah = new Polygon()
            {
                Fill = Brushes.Black,
                Points = [new Point(To.Origin.X, To.Origin.Y), new Point(To.Origin.X + HeadWidth, To.Origin.Y + HeadLength), new Point(To.Origin.X - HeadWidth, To.Origin.Y + HeadLength)]
            };
            Panel.SetZIndex(ah, ConstantValues.ZIndex.Line);// Has the same z index with the obj this head attach To.Origin

            // rotate
            var angle = 90 + Math.Atan2(To.Origin.Y - From.Origin.Y, To.Origin.X - From.Origin.X) * (180 / Math.PI);
            ah.RenderTransform = new RotateTransform(angle, To.Origin.X, To.Origin.Y);

            return ah;
        }

        public void OnTailNodePositionChanged()
        {
            var angle = 90 + Math.Atan2(To.Origin.Y - From.Origin.Y, To.Origin.X - From.Origin.X) * (180 / Math.PI);
            var points = CreateArrowHead(To.Origin, HeadLength, HeadWidth);
            _head.Points[0] = points.Item1;
            _head.Points[1] = points.Item2;
            _head.Points[2] = points.Item3;
            _head.RenderTransform = new RotateTransform(angle, To.Origin.X, To.Origin.Y);
        }

        public void OnHeadNodePositionChanged()
        {
            var angle = 90 + Math.Atan2(To.Origin.Y - From.Origin.Y, To.Origin.X - From.Origin.X) * (180 / Math.PI);
            _head.RenderTransform = new RotateTransform(angle, To.Origin.X, To.Origin.Y);
        }
    }
}
