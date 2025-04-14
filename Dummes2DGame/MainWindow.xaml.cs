using System.Windows;
using System.Windows.Input;

namespace Dummes2DGame;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();

        map.ItemsSource = MainView.Map;
        Map.InitMap();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e) {
        if (Map.Player != null)
            Map.Player.Move(e);
    }
}
