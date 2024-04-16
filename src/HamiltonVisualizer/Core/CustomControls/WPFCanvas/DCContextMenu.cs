﻿using HamiltonVisualizer.Core.CustomControls.Contracts;
using System.Windows.Controls;

namespace HamiltonVisualizer.Core.CustomControls.WPFCanvas
{
    public class DCContextMenu : ContextMenu, IAppContextMenu
    {
        private static DCContextMenu? _instance;

        public MenuItem Algorithms = null!;
        public MenuItem SCC = null!;
        public MenuItem HamiltonCycle = null!;

        public MenuItem Export = null!;
        public MenuItem CSV = null!;

        public MenuItem Quit = null!;

        public static DCContextMenu Instance
        {
            get
            {
                _instance ??= new DCContextMenu();
                return _instance;
            }
        }

        public void Initialize()
        {
            Algorithms = new()
            {
                Header = "Thuật toán"
            };

            SCC = new()
            {
                Header = "Bộ Phận Liên Thông"
            };
            HamiltonCycle = new()
            {
                Header = "Chu trình hamilton"
            };

            Algorithms.Items.Add(SCC);
            Algorithms.Items.Add(HamiltonCycle);

            Export = new()
            {
                Header = "Xuất tập tin"
            };

            CSV = new()
            {
                Header = "CSV"
            };
            Export.Items.Add(CSV);

            Quit = new()
            {
                Header = "Huỷ",

            };
        }

        public DCContextMenu()
        {
            Initialize();

            AddItems(
                Algorithms,
                Export,
                Quit);
        }

        public void AddItems(params MenuItem[] objects)
        {
            foreach (var item in objects)
            {
                Items.Add(item);
            }
        }
    }
}
