﻿using HamiltonVisualizer.Core.CustomControls.WPFBorder;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HamiltonVisualizer.Core.Collections
{
    internal class SelectedNodePair : INotifyPropertyChanged
    {
        public HashSet<Node> Nodes { get; } = new(NodeComparer.Instance);

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Count => Nodes.Count;

        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Add(Node node)
        {
            Nodes.Add(node);
            OnPropertyChanged(nameof(Nodes));
        }

        public void Remove(Node node)
        {
            Nodes.Remove(node);
            OnPropertyChanged(nameof(Nodes));
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public (Node, Node) GetValues()
        {
            if (Nodes.Count < 2)
                throw new InvalidOperationException("Not enough element to retrieve");

            var result = (Nodes.ElementAt(0), Nodes.ElementAt(1));

            Nodes.Clear();
            return result;
        }
    }
}
