using Avalonia.Controls;
using MessengerApp.ViewModels;

namespace MessengerApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MessengerViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}