using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CoombuPhoneApp.Resources;

namespace CoombuPhoneApp
{
    public partial class PostDetailPage : PhoneApplicationPage
    {
        public PostDetailPage()
        {
            InitializeComponent();
            ApplicationBar = ApplicationBarForPostDetailPage();
        }
        

        private void Pivot_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selectedPivot =(PivotItem) ((Pivot) sender).SelectedItem;
            int tag = int.Parse(selectedPivot.Tag.ToString());
            switch (tag)
            {
                case 0:
                    ApplicationBar = ApplicationBarForPostDetailPage();
                    break;
                case 1:
                    ApplicationBar = ApplicationBarForCommentPage();
                    break;
            }

        }

        private IApplicationBar ApplicationBarForCommentPage()
        {
            IApplicationBar appBar = new ApplicationBar();
            appBar.Mode = ApplicationBarMode.Default;
            appBar.IsMenuEnabled = false;
            appBar.Opacity = 1.0;

            ApplicationBarIconButton commentButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative));
            commentButton.Text = AppResources.AddCommentString;
            commentButton.Click += AddComment;

            appBar.Buttons.Add(commentButton);

            return appBar;
        }

        private IApplicationBar ApplicationBarForPostDetailPage()
        {
            IApplicationBar appBar = new ApplicationBar();
            appBar.Mode = ApplicationBarMode.Default;
            appBar.IsMenuEnabled = false;
            appBar.Opacity = 1.0;

            ApplicationBarIconButton deletePostButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Delete.png", UriKind.Relative));
            deletePostButton.Text = AppResources.DeletePostString;
            deletePostButton.Click += DeletePost;

            ApplicationBarIconButton likePostButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Like.png", UriKind.Relative));
            likePostButton.Text = AppResources.LikePostString;
            likePostButton.Click += LikePost;

            appBar.Buttons.Add(deletePostButton);
            appBar.Buttons.Add(likePostButton);

            return appBar;
        }

        private void LikePost(object sender, EventArgs e)
        {
            
        }

        private void DeletePost(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimez ce post?","Suppimer",MessageBoxButton.OKCancel);
        }

        private void AddComment(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/CommentAddPage.xaml",UriKind.Relative));
        }
    }
}