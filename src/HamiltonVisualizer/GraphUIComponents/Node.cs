﻿using HamiltonVisualizer.Events.EventArgs;
using HamiltonVisualizer.Events.EventHandlers;
using HamiltonVisualizer.GraphUIComponents.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HamiltonVisualizer.GraphUIComponents
{
    /// <summary>
    /// Graph node.
    /// </summary>
    /// <remarks>
    /// This node single child is <see cref="NodeLabel"/>.
    /// </remarks>
    public class Node : Border, IUIComponent
    {
        public Point TopLeftPoint { get; set; }
        public NodeLabel NodeLabel => (NodeLabel)Child;
        public Canvas ParentCanvas { get; set; }

        public const int NodeWidth = 34;

        public event NodeDeleteEventHandler? RequestNodeDelete;
        public event NodeLabelSetCompleteEventHandler? OnNodeLabelChanged;
        public event OnNodeSelectedEventHandler? OnNodeSelected;

        public Node(Point position, Canvas parent)
        {
            TopLeftPoint = position;

            StyleUIComponent();

            Child = new NodeLabel(this);
            ContextMenu = new NodeContextMenu(this);
            ParentCanvas = parent;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            RequestNodeDelete += async (sender, e) =>
            {
                Background = Brushes.Red;
                await Task.Delay(500);
                ParentCanvas.Children.Remove(this);
            };

            OnNodeSelected += (sender, e) =>
            {
                Background = Brushes.LightGreen;
            };
        }

        public void StyleUIComponent()
        {
            Width = NodeWidth;
            Height = NodeWidth;
            Background = Brushes.White;
            BorderBrush = new SolidColorBrush(Colors.Black);
            BorderThickness = new(2);
            CornerRadius = new(30);

            Canvas.SetLeft(this, TopLeftPoint.X - Width / 2);
            Canvas.SetTop(this, TopLeftPoint.Y - Height / 2);
        }

        public void DeleteNode()
        {
            RequestNodeDelete?.Invoke(this, new NodeDeleteEventArgs(this));
        }

        public void ChangeNodeLabel(string text)
        {
            OnNodeLabelChanged?.Invoke(this, new NodeSetLabelEventArgs(this, text));
        }

        public void ConnectNode()
        {
            SelectNode();

            // TODO: Finished Nodes.ConnectNode()
        }

        public void ReleaseSelectMode()
        {
            Background = Brushes.White;
        }

        public void SelectNode()
        {
            OnNodeSelected?.Invoke(this, new NodeSelectedEventArgs());
        }
    }
}
