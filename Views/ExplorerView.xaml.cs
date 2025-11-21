using System.Windows;
using System.Windows.Controls;
using StudioForge.Models;
using StudioForge.ViewModels;

namespace StudioForge.Views
{
    public partial class ExplorerView : UserControl
    {
        public ExplorerView()
        {
            InitializeComponent();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is MainViewModel vm && e.NewValue is ExplorerNode node)
            {
                if (vm.SelectExplorerNodeCommand.CanExecute(node))
                {
                    vm.SelectExplorerNodeCommand.Execute(node);
                }
            }
        }
    }
}
