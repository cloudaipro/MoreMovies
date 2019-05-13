using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyMahAppsExt
{
    public class MyBaseMetroDialog : BaseMetroDialog
    {
        public Button PART_AffirmativeButton { get; set; }
        public Button PART_NegativeButton { get; set; }

        public delegate void AffirmativeDelegate();
        public delegate void NegativeDelegate();
        public AffirmativeDelegate AffirmativeCallback { get; set; }
        public NegativeDelegate NegativeCallback { get; set; }
        protected MyBaseMetroDialog() : base(null, null)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        protected MyBaseMetroDialog(MetroWindow owningWindow, MetroDialogSettings settings = null) : base(owningWindow, settings)
        {
            //this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Themes/Dialogs/BaseMetroDialog.xaml") });
            //this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MahApps.Metro;component/styles/themes/light.taupe.xaml") });
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            Initialize();
        }

        private void Initialize()
        {
            if (DialogSettings != null)
            {
                switch (DialogSettings.ColorScheme)
                {
                    case MetroDialogColorScheme.Theme:
                        this.SetResourceReference(BackgroundProperty, "WhiteColorBrush");
                        this.SetResourceReference(ForegroundProperty, "BlackColorBrush");
                        break;
                    case MetroDialogColorScheme.Accented:
                        this.SetResourceReference(BackgroundProperty, "HighlightBrush");
                        this.SetResourceReference(ForegroundProperty, "IdealForegroundColorBrush");
                        break;
                }
            }

        }


        internal Task<MessageDialogResult> WaitForButtonPressAsync()
        {
            try
            {

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Focus();
                }));

                TaskCompletionSource<MessageDialogResult> tcs = new TaskCompletionSource<MessageDialogResult>();

                RoutedEventHandler negativeHandler = null;
                KeyEventHandler negativeKeyHandler = null;

                RoutedEventHandler affirmativeHandler = null;
                KeyEventHandler affirmativeKeyHandler = null;

                KeyEventHandler escapeKeyHandler = null;

                Action cleanUpHandlers = () =>
                {
                    this.KeyDown -= escapeKeyHandler;

                    if (PART_NegativeButton != null)
                        PART_NegativeButton.Click -= negativeHandler;
                    if (PART_AffirmativeButton != null)
                        PART_AffirmativeButton.Click -= affirmativeHandler;

                    if (PART_NegativeButton != null)
                        PART_NegativeButton.KeyDown -= negativeKeyHandler;
                    if (PART_AffirmativeButton != null)
                        PART_AffirmativeButton.KeyDown -= affirmativeKeyHandler;
                };

                escapeKeyHandler = new KeyEventHandler((sender, e) =>
                {
                    if (e.Key == Key.Escape)
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Negative);
                        if (NegativeCallback != null)
                            NegativeCallback();
                    }
                });

                negativeKeyHandler = new KeyEventHandler((sender, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Negative);
                        if (NegativeCallback != null)
                            NegativeCallback();
                    }
                });

                affirmativeKeyHandler = new KeyEventHandler((sender, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Affirmative);
                        if (AffirmativeCallback != null)
                            AffirmativeCallback();
                    }
                });


                negativeHandler = new RoutedEventHandler((sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.Negative);

                    e.Handled = true;
                    if (NegativeCallback != null)
                        NegativeCallback();
                });

                affirmativeHandler = new RoutedEventHandler((sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.Affirmative);

                    e.Handled = true;
                    if (AffirmativeCallback != null)
                        AffirmativeCallback();
                });

                if (PART_NegativeButton != null)
                    PART_NegativeButton.KeyDown += negativeKeyHandler;
                if (PART_AffirmativeButton != null)
                    PART_AffirmativeButton.KeyDown += affirmativeKeyHandler;

                this.KeyDown += escapeKeyHandler;

                if (PART_NegativeButton != null)
                    PART_NegativeButton.Click += negativeHandler;
                if (PART_AffirmativeButton != null)
                    PART_AffirmativeButton.Click += affirmativeHandler;

                return tcs.Task;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void ShowModel()
        //{
        //    try
        //    {
        //        DialogManager.ShowModalDialogAsyncExternally(this);
        //    }
        //    catch (Exception ex) { throw ex; }

        //}

        public MessageDialogResult ShowModel()
        {
            return ShowDialog();
        }

        public MessageDialogResult ShowDialog()
        {
            MetroWindow oShowedObj = new MetroWindow
            {
                Title = "",
                Content = this
            };
            MessageDialogResult result = MessageDialogResult.Negative;
            SizeChangedEventHandler onsizeChangedHandler = SetupDialog(base.OwningWindow, oShowedObj);
            RoutedEventHandler onLoadEventHandler = async (s, e) =>
            {
                result = await this.WaitForButtonPressAsync();
                oShowedObj.Close();
            };
            oShowedObj.Owner = base.OwningWindow;
            oShowedObj.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            oShowedObj.WindowStyle = WindowStyle.None;
            oShowedObj.ResizeMode = ResizeMode.NoResize;
            oShowedObj.TitleBarHeight = 0;
            oShowedObj.SizeToContent = SizeToContent.WidthAndHeight;
            oShowedObj.SizeChanged += onsizeChangedHandler;
            oShowedObj.Loaded += onLoadEventHandler;
            oShowedObj.Closed += (s, e) => {
                oShowedObj.SizeChanged -= onsizeChangedHandler;
                oShowedObj.Loaded -= onLoadEventHandler;
            };
            //await (base.OwningWindow as MetroWindow)?.ShowOverlayAsync();
            (base.OwningWindow as MetroWindow)?.ShowOverlay();
            oShowedObj.ShowDialog();
            //await (base.OwningWindow as MetroWindow)?.HideOverlayAsync();
            (base.OwningWindow as MetroWindow)?.HideOverlay();
            return result;
        }

        private static SizeChangedEventHandler SetupDialog(Window window, MetroWindow dialog)
        {

            dialog.MinHeight = window.ActualHeight / 4.0;
            dialog.MaxHeight = window.ActualHeight;
            dialog.MinWidth = window.ActualWidth / 4.0;
            dialog.MaxWidth = window.ActualWidth;

            SizeChangedEventHandler sizeHandler = (sender, args) =>
            {
                dialog.MinHeight = window.ActualHeight / 4.0;
                dialog.MaxHeight = window.ActualHeight;
                dialog.MinWidth = window.ActualWidth / 4.0;
                dialog.MaxWidth = window.ActualWidth;
            };

            return sizeHandler;
        }

    }
}
