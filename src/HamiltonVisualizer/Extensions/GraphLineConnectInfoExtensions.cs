﻿using HamiltonVisualizer.Core;
using System.Windows;

namespace HamiltonVisualizer.Extensions;

internal static class GraphLineConnectInfoExtensions
{
    public static void Move(this GraphLineConnectInfo attachInfo, Point newPosition)
    {
        switch (attachInfo.AttachPosition)
        {
            case ConnectPosition.Head:
                attachInfo.Edge.Body.X1 = newPosition.X;
                attachInfo.Edge.Body.Y1 = newPosition.Y;
                break;

            case ConnectPosition.Tail:
                attachInfo.Edge.Body.X2 = newPosition.X;
                attachInfo.Edge.Body.Y2 = newPosition.Y;
                break;
        }
        attachInfo.Edge.OnHeadOrTailPositionChanged();
    }
}
