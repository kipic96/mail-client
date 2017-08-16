using System.Windows;

namespace PublicTransportSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TimetableDatabase _database;

        public MainWindow()
        {
            InitializeComponent();            
        }

        public void InitializeDatabase()
        {
            _database = new TimetableDatabase();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InitializeDatabase();
        }
    }
}
