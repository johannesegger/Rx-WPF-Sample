using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;

namespace WpfRxSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = new MainViewModel();
            Observable
                .FromEventPattern<CancelEventHandler, CancelEventArgs>(
                    h => Closing += h,
                    h => Closing -= h
                )
                .FirstAsync()
                .Do(p =>
                {
                    p.EventArgs.Cancel = true;
                    ((MainViewModel)DataContext).Dispose();
                })
                .Delay(TimeSpan.FromSeconds(1))
                .ObserveOnDispatcher()
                .Subscribe(_ => Application.Current.Shutdown());
        }
    }
}
