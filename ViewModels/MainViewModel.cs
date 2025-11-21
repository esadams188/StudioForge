using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using StudioForge.Models;

namespace StudioForge.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> MenuItems { get; } = new();
        public ObservableCollection<string> ToolbarItems { get; } = new();
        public ObservableCollection<ExplorerNode> ExplorerNodes { get; } = new();
        public ObservableCollection<LogEntry> Logs { get; } = new();
        public ObservableCollection<string> OutputFilterOptions { get; } = new();
        public ObservableCollection<string> OutputContextOptions { get; } = new();

        public ICommand MenuCommand { get; }
        public ICommand ToolbarCommand { get; }
        public ICommand SelectExplorerNodeCommand { get; }

        private string _activeTool = string.Empty;
        private ExplorerNode? _selectedExplorerNode;

        public string ActiveTool
        {
            get => _activeTool;
            set
            {
                if (_activeTool != value)
                {
                    _activeTool = value;
                    OnPropertyChanged();
                }
            }
        }

        public ExplorerNode? SelectedExplorerNode
        {
            get => _selectedExplorerNode;
            set
            {
                if (_selectedExplorerNode != value)
                {
                    _selectedExplorerNode = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            MenuCommand = new RelayCommand(OnMenuClicked);
            ToolbarCommand = new RelayCommand(OnToolbarClicked);
            SelectExplorerNodeCommand = new RelayCommand(OnExplorerNodeSelected);

            SeedMenus();
            SeedToolbar();
            SeedExplorer();
            SeedLogs();
            SeedOutputFilters();
        }

        private void OnMenuClicked(object? parameter)
        {
            var name = parameter as string ?? "Menu";
            AddLog($"Menu: {name} clicked", Brushes.White);
        }

        private void OnToolbarClicked(object? parameter)
        {
            var name = parameter as string ?? string.Empty;
            ActiveTool = name;
            AddLog($"Toolbar: {name} tool", Brushes.White);
        }

        private void OnExplorerNodeSelected(object? parameter)
        {
            if (parameter is ExplorerNode node)
            {
                SelectedExplorerNode = node;
                AddLog($"Selected: {node.Name}", Brushes.White);
            }
        }

        private void SeedMenus()
        {
            var menus = new[] { "File", "Edit", "View", "Plugins", "Test", "Avatar", "UI", "Script", "Window", "Help" };
            foreach (var item in menus)
            {
                MenuItems.Add(item);
            }
        }

        private void SeedToolbar()
        {
            var tools = new[]
            {
                "Select", "Move", "Scale", "Rotate", "Transform", "Terrain", "Part", "Character", "GUI", "Script",
                "Import 3D", "Group", "Ungroup", "Lock", "Anchor", "Material", "Color", "Properties", "Toolbox"
            };
            foreach (var tool in tools)
            {
                ToolbarItems.Add(tool);
            }
        }

        private void SeedExplorer()
        {
            ExplorerNodes.Add(new ExplorerNode("Workspace"));
            ExplorerNodes.Add(new ExplorerNode("Players"));
            ExplorerNodes.Add(new ExplorerNode("Lighting"));
            ExplorerNodes.Add(new ExplorerNode("MaterialService"));
            ExplorerNodes.Add(new ExplorerNode("ReplicatedFirst"));
            ExplorerNodes.Add(new ExplorerNode("ReplicatedStorage"));
            ExplorerNodes.Add(new ExplorerNode("ServerScriptService"));
            ExplorerNodes.Add(new ExplorerNode("ServerStorage"));
            ExplorerNodes.Add(new ExplorerNode("StarterGui"));
            ExplorerNodes.Add(new ExplorerNode("StarterPack"));

            var starterPlayer = new ExplorerNode("StarterPlayer");
            starterPlayer.Children.Add(new ExplorerNode("StarterCharacterScripts"));
            starterPlayer.Children.Add(new ExplorerNode("StarterPlayerScripts"));
            ExplorerNodes.Add(starterPlayer);

            ExplorerNodes.Add(new ExplorerNode("Teams"));
            ExplorerNodes.Add(new ExplorerNode("SoundService"));
            ExplorerNodes.Add(new ExplorerNode("TextChatService"));
        }

        private void SeedOutputFilters()
        {
            OutputFilterOptions.Add("All Messages");
            OutputContextOptions.Add("All Contexts");
        }

        private void SeedLogs()
        {
            Logs.Add(new LogEntry
            {
                Time = "16:23:39.234",
                Message = "DataModel Loading https://assetdelivery.roblox.com/v1/asset/?id=95206881",
                Category = "Studio",
                Color = Brushes.White
            });
            Logs.Add(new LogEntry
            {
                Time = "16:28:40.971",
                Message = "'Place2' auto-recovery file was created",
                Category = "Studio",
                Color = Brushes.Magenta
            });
        }

        private void AddLog(string message, Brush color)
        {
            Logs.Add(new LogEntry
            {
                Time = DateTime.Now.ToString("HH:mm:ss.fff"),
                Message = message,
                Category = "Studio",
                Color = color
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
