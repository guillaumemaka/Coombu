using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Messaging;
using CoombuPhoneApp.Utils;

namespace CoombuPhoneApp
{
    public partial class LoginPage : PhoneApplicationPage
    {        
        public LoginPage()
        {
            InitializeComponent();
            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) =>
            {                
                NavigationService.Navigate(uri);
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            base.OnNavigatedTo(e);
        }
    }
}