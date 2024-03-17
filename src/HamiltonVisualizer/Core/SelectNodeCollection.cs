﻿using HamiltonVisualizer.GraphUIComponents;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HamiltonVisualizer.Core
{
    public class SelectNodeCollection : INotifyPropertyChanged
    {
        public List<Node> Nodes { get; } = [];

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Count => Nodes.Count;

        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new(name));
        }

        public void Add(Node node)
        {
            Nodes.Add(node);
            OnPropertyChanged();
        }

        public (Node, Node) GetFirst2()
        {
            if (Nodes.Count < 2) throw new ArgumentException("Not enough element to retrieve");
            var result = (Nodes.First(), Nodes.ElementAt(1));
            Nodes.RemoveRange(0, 2);
            return result;
        }
    }
}
