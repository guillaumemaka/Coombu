using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CoombuPhoneApp.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using CoombuPhoneApp.Models;
using CoombuPhoneApp.Resources;

namespace CoombuPhoneApp
{
    public partial class GroupMainPage : PhoneApplicationPage
    {
        public GroupMainPage()
        {
            InitializeComponent();
        }

        private void Panorama_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            PanoramaItem selectedItem = (PanoramaItem)((Panorama)sender).SelectedItem;
            int tag = int.Parse(selectedItem.Tag.ToString());

            switch (tag)
            {
                case 0:
                    ApplicationBar = ApplicationBarForFirstPage();
                    break;
                case 1 :
                    ApplicationBar = null;
                    break;
            }
        }

        private IApplicationBar ApplicationBarForFirstPage()
        {
            IApplicationBar appbar = new ApplicationBar();
            appbar.Mode = ApplicationBarMode.Default;
            appbar.Opacity = 1;
            appbar.IsVisible = true;
            appbar.IsMenuEnabled = false;

            ApplicationBarIconButton addPost = new ApplicationBarIconButton();
            addPost.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative);
            addPost.Text = AppResources.AddPostString;
            addPost.Click += addPost_Click;
            appbar.Buttons.Add(addPost);

            return appbar;
        }
                

        private void addGroup_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void addPost_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPostPage.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = (LongListSelector)sender;

            if (list.SelectedItem.GetType() != typeof(PostViewModel))
                return;

            PostDetailViewModel vm = SimpleIoc.Default.GetInstance<PostDetailViewModel>();
            vm.Post = (PostViewModel) list.SelectedItem;
            NavigationService.Navigate(new Uri("/PostDetailPage.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = (LongListSelector)sender;

            if (list.SelectedItem.GetType() != typeof(UserProfile))
                return;

            UserPageViewModel vm = SimpleIoc.Default.GetInstance<UserPageViewModel>();
            vm.SelectedUser = (UserProfile)list.SelectedItem;
            NavigationService.Navigate(new Uri("/UserMainPage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            ApplicationBar = ApplicationBarForFirstPage();
        }

        
    }
}