using MoreMovies.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
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
using MahApps.Metro.Controls.Dialogs;

namespace MoreMovies.Views
{
    /// <summary>
    /// Interaction logic for DataMaintenanceView.xaml
    /// </summary>
    public partial class DataMaintenanceView : UserControl//, IViewFor<DataMaintenanceViewModel>
    {
        DataMaintenanceViewModel vm = new DataMaintenanceViewModel(DialogCoordinator.Instance);
        ///// <inheritdoc/>
        //public static readonly DependencyProperty ViewModelProperty =
        //    DependencyProperty.Register(
        //        "ViewModel",
        //        typeof(DataMaintenanceViewModel),
        //        typeof(ReactiveUserControl<DataMaintenanceViewModel>),
        //        new PropertyMetadata(null));


        //public DataMaintenanceViewModel ViewModel
        //{
        //    get => (DataMaintenanceViewModel)GetValue(ViewModelProperty);
        //    set => SetValue(ViewModelProperty, value);
        //}

        ///// <inheritdoc/>
        //object IViewFor.ViewModel
        //{
        //    get => ViewModel;
        //    set => ViewModel = (DataMaintenanceViewModel)value;
        //}
        public DataMaintenanceView()
        {
            InitializeComponent();

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;

            DataContext = vm;
        }
    }
}
