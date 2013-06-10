using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections;
using CoombuPhoneApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using GalaSoft.MvvmLight.Ioc;
using CoombuPhoneApp.Models;
using CoombuPhoneApp.Resources;

namespace CoombuPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<ApplicationBarIconButton> wallButtons = new List<ApplicationBarIconButton>();
        List<ApplicationBarIconButton> groupButtons = new List<ApplicationBarIconButton>();
        List<ApplicationBarIconButton> userButtons = new List<ApplicationBarIconButton>();
        // Constructeur
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = false;

            ApplicationBarIconButton addPost = new ApplicationBarIconButton();
            addPost.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative);
            addPost.Text = AppResources.AddPostString;
            addPost.Click +=addPost_Click;
            wallButtons.Add(addPost);

            ApplicationBarIconButton addGroup = new ApplicationBarIconButton();
            addGroup.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Group.Add.png", UriKind.Relative);
            addGroup.Text = AppResources.AddGroupString;
            addGroup.Click +=addGroup_Click;
            groupButtons.Add(addGroup);

            ApplicationBarIconButton searchUser = new ApplicationBarIconButton();
            searchUser.IconUri = new Uri("/Toolkit.Content/ApplicationBar.People.Magnify.png", UriKind.Relative);
            searchUser.Text = AppResources.SearchUserString;
            searchUser.Click += searchUser_Click;

            userButtons.Add(searchUser);

            Messenger.Default.Register<bool>(this, "ShowIndicator", (b) =>
            {
                ProgressIndicator progress = new ProgressIndicator
                {
                    IsVisible = b,
                    IsIndeterminate = true,
                    Text = "Posting..."
                };
                SystemTray.SetProgressIndicator(this, progress);
            });

            SearchUserBox.Visibility = Visibility.Collapsed;
            // Affecter l'exemple de données au contexte de données du contrôle ListBox
            //DataContext = App.ViewModel;            
        }

        private void searchUser_Click(object sender, EventArgs e)
        {
            SearchUserBox.Visibility = Visibility.Visible;
        }        

        private void addGroup_Click(object sender, EventArgs e)
        {
            
        }

        private void addPost_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPostPage.xaml",UriKind.Relative));
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ClientApi.IsAuthenticated)
            {
                NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            }  

            ApplicationBar.Buttons.Clear();
            wallButtons.ForEach(b => ApplicationBar.Buttons.Add(b));
        }

        // Charger les données pour les éléments ViewModel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.BackStack.Count() != 0)
                NavigationService.RemoveBackEntry();
        }

        private void Panorama_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            PanoramaItem selectedItem = (PanoramaItem)((Panorama)sender).SelectedItem;
            int tag = int.Parse(selectedItem.Tag.ToString());

            switch (tag)
            {
                case 0:
                    ApplicationBar.Buttons.Clear();
                    wallButtons.ForEach(b => ApplicationBar.Buttons.Add(b));
                    break;
                case 1:
                    ApplicationBar.Buttons.Clear();
                    groupButtons.ForEach(b => ApplicationBar.Buttons.Add(b));
                    break;
                case 2:
                    ApplicationBar.Buttons.Clear();
                    userButtons.ForEach(b => ApplicationBar.Buttons.Add(b));
                    break;
            }
        }        

        private void LongListSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = (LongListSelector)sender;
            if (list.SelectedItem.GetType() != typeof(PostViewModel))
            {
                return;
            }

            PostDetailViewModel vm = SimpleIoc.Default.GetInstance<PostDetailViewModel>();
            vm.Post = (PostViewModel)list.SelectedItem;
            NavigationService.Navigate(new Uri("/PostDetailPage.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = (LongListSelector)sender;
            if (list.SelectedItem.GetType() != typeof(Group))
            {
                return;
            }

            GroupPageViewModel vm = SimpleIoc.Default.GetInstance<GroupPageViewModel>();
            vm.SelectedItem = (Group)list.SelectedItem;
            NavigationService.Navigate(new Uri("/GroupMainPage.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = (LongListSelector)sender;
            if (list.SelectedItem.GetType() != typeof(UserProfile))
            {
                return;
            }

            UserPageViewModel vm = SimpleIoc.Default.GetInstance<UserPageViewModel>();
            vm.SelectedUser = (UserProfile)list.SelectedItem;
            NavigationService.Navigate(new Uri("/UserMainPage.xaml", UriKind.Relative));
        }

        private void AutoCompleteBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            AutoCompleteBox autoCompleteBox = (AutoCompleteBox)sender;

            if (autoCompleteBox.SelectedItem.GetType() != typeof(UserProfile))
                return;
            
            UserProfile selectedItem = (UserProfile) autoCompleteBox.SelectedItem;
            UserPageViewModel vm = SimpleIoc.Default.GetInstance<UserPageViewModel>();
            vm.SelectedUser = selectedItem;
            NavigationService.Navigate(new Uri("/UserMainPage.xaml",UriKind.Relative));
        }

        private void SearchUserBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            SearchUserBox.Visibility = Visibility.Collapsed;
        }
    }
}