using MahApps.Metro.Controls;
using MoreMovies.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoreMovies.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : MetroWindow, IViewFor<MainViewModel>
    {
        /// <inheritdoc/>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(MainViewModel),
                typeof(ReactiveUserControl<MainViewModel>),
                new PropertyMetadata(null));


        public MainViewModel ViewModel
        {
            get => (MainViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        /// <inheritdoc/>
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }
        public MainView()
        {
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;

            ViewModel = new MainViewModel();
            DataContext = ViewModel;

            IObservable<MetroWindow> para = Observable.Create<MetroWindow>((observer) => {
                observer.OnNext(this);
                observer.OnCompleted();
                return Disposable.Empty;
            });
            this.WhenActivated(
                        d => {
                            this.BindCommand(
                                            this.ViewModel,
                                            x => x.CmdDataMaintenance,
                                            x => x.mnuDataMaintenance,
                                            para)
                                .DisposeWith(d);
                        });

        }
    }
}
