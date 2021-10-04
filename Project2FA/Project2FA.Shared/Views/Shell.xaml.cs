﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Prism.Regions;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project2FA.Views
{
    public sealed partial class ShellPage : Page, INotifyPropertyChanged
    {
        //private SystemNavigationManager _navManager;
        private bool _navigationIsAllowed = true;
        private string _title;
        private readonly IRegionManager _regionManager;
        public NavigationView ShellViewInternal { get; private set; }

        public ShellPage()
        {
            InitializeComponent();
            //_navManager = SystemNavigationManager.GetForCurrentView();
            _settingsNavigationStr = "SettingPage?PivotItem=0";

            string title = Windows.ApplicationModel.Package.Current.DisplayName;
            // determine and set if the app is started in debug mode
            Title = System.Diagnostics.Debugger.IsAttached ? "[Debug] " + title : title;

            // Hide default title bar.
            //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            //coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            //SetTitleBarAsDraggable();

            // Register a handler for when the size of the overlaid caption control changes.
            // For example, when the app moves to a screen with a different DPI.
            //coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            //if (WindowDisplayInfo.GetForCurrentView() == WindowDisplayMode.FullScreenTabletMode)
            //{
            //    AppTitleBar.Visibility = Visibility.Collapsed;
            //}

            //ShellViewInternal = ShellView;
            //ShellView.Content = MainFrame = new Frame();
            //NavigationService = NavigationFactory.Create(MainFrame).AttachGestures(Window.Current, Gesture.Back, Gesture.Forward, Gesture.Refresh);

            //SetupGestures();
            //SetupBackButton();

            //NavigationService.CanGoBackChanged += (s, args) =>
            //{
            //    //Backbutton setting
            //    if (SettingsService.Instance.UseHeaderBackButton)
            //    {
            //        _navManager.AppViewBackButtonVisibility = NavigationService.CanGoBack() ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            //    }
            //    else
            //    {
            //        if (ShellView.IsBackEnabled != NavigationService.CanGoBack())
            //        {
            //            ShellView.IsBackEnabled = NavigationService.CanGoBack();
            //            if (ShellView.IsBackButtonVisible == NavigationViewBackButtonVisible.Collapsed)
            //            {
            //                ShellView.IsBackButtonVisible = NavigationViewBackButtonVisible.Auto;
            //            }
            //        }
            //    }
            //};

            MainFrame.Navigated += (s, e) =>
            {
                //if (TryFindItem(e.SourcePageType, e.Parameter, out object item))
                //{
                //    SetSelectedItem(item, false);
                //}
            };

            ShellView.ItemInvoked += (sender, args) =>
            {
                SelectedItem = args.IsSettingsInvoked ? ShellView.SettingsItem : Find(args.InvokedItemContainer as NavigationViewItem);
            };

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().IsScreenCaptureEnabled = true;
            }
            else
            {
                //prevent screenshot capture for the app
                Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().IsScreenCaptureEnabled = false;
            }

            Loaded += ShellPage_Loaded;
        }

        private async void ShellPage_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(SettingsService.Instance.UnhandledExceptionStr))
            //{
            //    CheckUnhandledExceptionLastSession();
            //}
            //IDialogService dialogService = App.Current.Container.Resolve<IDialogService>();
            ////Rate information for the user
            //if (SystemInformation.Instance.LaunchCount == 5 || SystemInformation.Instance.LaunchCount == 15)
            //{
            //    if (!SettingsService.Instance.AppRated && (MainFrame.Content as FrameworkElement).GetType() != typeof(WelcomePage))
            //    {
            //        await dialogService.ShowAsync(new RateAppContentDialog());
            //    }
            //}

            // TODO add check for 1.0.3 to 1.0.4
            //if (SystemInformation.Instance.IsAppUpdated)
            //{
            //    ContentDialog dialog = new ContentDialog();
            //    dialog.Title = Strings.Resources.NewAppFeaturesTitle;
            //    dialog.Content = Strings.Resources.NewAppFeaturesContent;
            //    dialog.PrimaryButtonText = Strings.Resources.Confirm;
            //    dialog.PrimaryButtonStyle = App.Current.Resources["AccentButtonStyle"] as Style;
            //    await dialogService.ShowAsync(dialog);
            //}

            // If this is the first run, activate the ntp server checks
            // else => UseNTPServerCorrection is false
            //if (SystemInformation.Instance.IsFirstRun)
            //{
            //    SettingsService.Instance.UseNTPServerCorrection = true;
            //}
        }



//        public void SetupBackButton()
//        {
//            SettingsService settings = SettingsService.Instance;
//            if (settings.UseHeaderBackButton)
//            {
//                ShellView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
//                //_navManager.AppViewBackButtonVisibility = NavigationService.CanGoBack() ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
//            }
//            else
//            {
//                //_navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
//                ShellView.IsBackButtonVisible = NavigationViewBackButtonVisible.Auto;
//                ShellView.IsBackEnabled = NavigationService.CanGoBack();
//            }
//        }

//        private void SetupGestures()
//        {
//            //_navManager.BackRequested += NavManager_BackRequested;
//#pragma warning disable AsyncFixer03 // Fire-and-forget async-void methods or delegates
//            ShellView.BackRequested += async (s, e) => await NavigationService.GoBackAsync();
//#pragma warning restore AsyncFixer03 // Fire-and-forget async-void methods or delegates
//        }

//        private void CheckUnhandledExceptionLastSession()
//        {
//            ErrorDialogs.ShowUnexpectedError(SettingsService.Instance.UnhandledExceptionStr);
//        }

//        private async void NavManager_BackRequested(object sender, BackRequestedEventArgs e)
//        {
//            if (NavigationService.CanGoBack())
//            {
//                e.Handled = true;
//                await NavigationService.GoBackAsync();
//            }
//        }

//        private string _settingsNavigationStr;

//        private object PreviousItem { get; set; }

//        private object SelectedItem
//        {
//            set => SetSelectedItem(value);
//        }

//        private async Task SetSelectedItem(object selectedItem, bool withNavigation = true)
//        {
//            if (selectedItem == null)
//            {
//                ShellView.SelectedItem = null;
//            }
//            else if (selectedItem == PreviousItem)
//            {
//                // already set
//            }
//            else if (selectedItem == ShellView.SettingsItem)
//            {
//                if (withNavigation)
//                {
//                    _regionManager.RequestNavigate("InitialRegion", _settingsNavigationStr);
//                    PreviousItem = selectedItem;
//                    ShellView.SelectedItem = selectedItem;
//                }
//                else
//                {
//                    PreviousItem = selectedItem;
//                    ShellView.SelectedItem = selectedItem;
//                }
//            }
//            else if (selectedItem is NavigationViewItem item)
//            {
//                if (item.Tag is string path)
//                {
//                    if (!withNavigation)
//                    {
//                        PreviousItem = item;
//                        ShellView.SelectedItem = item;
//                    }
//                    else if ((await NavigationService.NavigateAsync(path)).Success)
//                    {
//                        PreviousItem = selectedItem;
//                        ShellView.SelectedItem = selectedItem;
//                    }
//                    else
//                    {
//                        ShellView.SelectedItem = PreviousItem;
//                    }
//                }
//            }
//        }

//        private bool TryFindItem(Type type, object parameter, out object item)
//        {
//            // is page registered?

//            if (!PageNavigationRegistry.TryGetRegistration(type, out PageNavigationInfo info))
//            {
//                item = null;
//                return false;
//            }

//            // search settings

//            if (NavigationQueue.TryParse(_settingsNavigationStr, null, out NavigationQueue settings))
//            {
//                if (type == settings.Last().View && (string)parameter == settings.Last().QueryString)
//                {
//                    item = ShellView.SettingsItem;
//                    return true;
//                }
//                else
//                {
//                    // not settings
//                }
//            }

//            // filter menu items
//            IEnumerable<(NavigationViewItem Item, string Path)> menuItems = ShellView.MenuItems
//                .OfType<NavigationViewItem>()
//                .Select(x => (
//                    Item: x,
//                    Path: x.Tag as string
//                ))
//                .Where(x => !string.IsNullOrEmpty(x.Path));

//            // search filtered items

//            foreach ((NavigationViewItem Item, string Path) in menuItems)
//            {
//                if (NavigationQueue.TryParse(Path, null, out NavigationQueue menuQueue)
//                    && Equals(menuQueue.Last().View, type) && menuQueue.Last().QueryString == (string)parameter)
//                {
//                    item = Item;
//                    return true;
//                }
//            }

//            // filter footer menu items
//            IEnumerable<(NavigationViewItem Item, string Path)> footerMenuItems = ShellView.FooterMenuItems
//                .OfType<NavigationViewItem>()
//                .Select(x => (
//                    Item: x,
//                    Path: x.Tag as string
//                ))
//                .Where(x => !string.IsNullOrEmpty(x.Path));

//            // search filtered items

//            foreach ((NavigationViewItem Item, string Path) in footerMenuItems)
//            {
//                if (NavigationQueue.TryParse(Path, null, out NavigationQueue menuQueue)
//                    && Equals(menuQueue.Last().View, type) && menuQueue.Last().QueryString == (string)parameter)
//                {
//                    item = Item;
//                    return true;
//                }
//            }

//            // not found

//            item = null;
//            return false;
//        }

//        private NavigationViewItem Find(NavigationViewItem item)
//        {
//            NavigationViewItem menuItem = ShellView.MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => x.Equals(item) && x.Tag != null);
//            if (menuItem is null)
//            {
//                menuItem = ShellView.FooterMenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => x.Equals(item) && x.Tag != null);
//            }
//            return menuItem;
//        }

        #region NotifyPropertyChanged
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <param name="onChanged">Action that is called after the property value has been changed.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        private bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        #endregion
        /// <summary>
        /// Allow or disable the NavigationView items
        /// </summary>
        public bool NavigationIsAllowed
        {
            get => _navigationIsAllowed;
            set => SetProperty(ref _navigationIsAllowed, value);
        }
        public Frame MainFrame { get; }
        public string Title { get => _title; set => SetProperty(ref _title, value); }


    }
}
