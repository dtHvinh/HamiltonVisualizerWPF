﻿using HamiltonVisualizer.Core.CustomControls.Contracts;
using HamiltonVisualizer.GraphUIComponents.Interfaces;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace HamiltonVisualizer.Core.CustomControls.WPFBorder
{
    public class NodeContextMenu : ContextMenu, IUIComponent, IAppContextMenu
    {
        private readonly Node _node;

        public MenuItem SelectOrReleaseSelect { get; set; }
        public MenuItem Delete { get; set; }

        public NodeContextMenu(Node node)
        {
            _node = node;
            Initialize();
            StyleUIComponent();
        }

        public void Initialize()
        {
            SelectOrReleaseSelect = new();
            SelectOrReleaseSelect.Click += SelectOrReleaseSelect_Click;

            Delete = new()
            {
                Header = "Xóa"
            };
            Delete.Click += Delete_Click;

            AddItems(
                SelectOrReleaseSelect,
                Delete);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _node.DeleteNode();
        }

        private void SelectOrReleaseSelect_Click(object sender, RoutedEventArgs e)
        {
            if (_node.IsSelected)
                _node.ReleaseSelectMode();
            else
                _node.SelectNode();
        }

        public void StyleUIComponent()
        {
        }

        public void AddItems(params MenuItem[] items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        protected override void OnOpened(RoutedEventArgs e)
        {
            if (_node.IsSelected)
            {
                SelectOrReleaseSelect.Header = "Huỷ chọn";
            }
            else
                SelectOrReleaseSelect.Header = "Chọn";

            base.OnOpened(e);
        }
    }
}
