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
    public partial class CommentAddPage : PhoneApplicationPage
    {
        public CommentAddPage()
        {
            InitializeComponent();
            SetUpApplicationBar();            
        }

        private void SetUpApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.Opacity = 1.0;

            ApplicationBarIconButton commentButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative));
            commentButton.Text = AppResources.AddCommentString;
            commentButton.Click += Comment;

            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative));
            cancelButton.Text = AppResources.CancelString;
            cancelButton.Click += Cancel;

            ApplicationBar.Buttons.Add(commentButton);
            ApplicationBar.Buttons.Add(cancelButton);
        }

        private void Cancel(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Comment(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}