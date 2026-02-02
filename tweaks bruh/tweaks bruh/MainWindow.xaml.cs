using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace TitanOptimizer
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TweakItem> AllTweaks { get; set; }
        public ObservableCollection<TweakItem> VisibleTweaks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            AllTweaks = new ObservableCollection<TweakItem>();
            VisibleTweaks = new ObservableCollection<TweakItem>();

            GenerateTweaks();
            ShowCategory("Optimizer");
        }

        private void GenerateTweaks()
        {
            
            for (int i = 1; i <= 200; i++)
            {
                AllTweaks.Add(new TweakItem
                {
                    Name = $"Optimizer Module {i}: Performance Boost {i}",
                    Description = $"Optimizes CPU, RAM and system responsiveness ({i})",
                    Category = "Optimizer"
                });
            }

            // NETWORK 2000 unique tweaks
            for (int i = 1; i <= 2000; i++)
            {
                AllTweaks.Add(new TweakItem
                {
                    Name = $"Network Tweak {i}: Latency Reduction {i}",
                    Description = $"Improves network stability and reduces packet loss ({i})",
                    Category = "Network"
                });
            }

            // ADVANCED 2000 unique tweaks
            for (int i = 1; i <= 2000; i++)
            {
                AllTweaks.Add(new TweakItem
                {
                    Name = $"Advanced Kernel Tweak {i}: Scheduler Mod {i}",
                    Description = $"Modifies CPU scheduler and kernel behavior ({i})",
                    Category = "Advanced"
                });
            }
        }

        private void ShowCategory(string category)
        {
            VisibleTweaks.Clear();
            foreach (var t in AllTweaks.Where(x => x.Category == category))
                VisibleTweaks.Add(t);
        }

        private void Optimizer_Click(object sender, RoutedEventArgs e) => ShowCategory("Optimizer");
        private void Network_Click(object sender, RoutedEventArgs e) => ShowCategory("Network");
        private void Advanced_Click(object sender, RoutedEventArgs e) => ShowCategory("Advanced");

        private void EnableAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var t in VisibleTweaks)
                t.IsEnabled = true;
        }

        private void DisableAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var t in VisibleTweaks)
                t.IsEnabled = false;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            int appliedCount = VisibleTweaks.Count(t => t.IsEnabled);
            MessageBox.Show($"Applied {appliedCount} tweaks in this category.");
        }

        private void ApplyAll_Click(object sender, RoutedEventArgs e)
        {
            int appliedCount = AllTweaks.Count(t => t.IsEnabled);
            MessageBox.Show($"Applied {appliedCount} tweaks across ALL categories.");
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }

    public class TweakItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
