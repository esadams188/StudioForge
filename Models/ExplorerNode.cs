using System.Collections.ObjectModel;

namespace StudioForge.Models
{
    public class ExplorerNode
    {
        public string Name { get; set; }
        public ObservableCollection<ExplorerNode> Children { get; set; } = new();

        public ExplorerNode(string name)
        {
            Name = name;
        }
    }
}
