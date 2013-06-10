using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using CoombuPhoneApp.Resources;
using System.IO;
using CoombuPhoneApp.Models;
using CoombuPhoneApp.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.IO.IsolatedStorage;
using CoombuPhoneApp.Utils;

namespace CoombuPhoneApp
{
    public partial class AddPostPage : PhoneApplicationPage
    {
        private PhotoChooserTask photoChooserTask;
        private CameraCaptureTask cameraCaptureTask;
        private MemoryStream selectedImage;

        public AddPostPage()
        {
            InitializeComponent();

            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.Opacity = 1.0;

            ApplicationBarIconButton publishButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative));
            publishButton.Text = AppResources.PublishString;
            publishButton.Click += Publish;            

            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative));
            cancelButton.Text = AppResources.CancelString;
            cancelButton.Click += Cancel;

            ApplicationBarMenuItem choosePhotoMenuItem = new ApplicationBarMenuItem(AppResources.ChoosePhotoString);
            choosePhotoMenuItem.Click += ApplicationBarMenuItem_Click_1;

            ApplicationBarMenuItem takePhotoMenuItem = new ApplicationBarMenuItem(AppResources.TakePhotoString);
            takePhotoMenuItem.Click += ApplicationBarMenuItem_Click_2;

            ApplicationBar.Buttons.Add(publishButton);
            ApplicationBar.Buttons.Add(cancelButton);
            ApplicationBar.MenuItems.Add(choosePhotoMenuItem);
            ApplicationBar.MenuItems.Add(takePhotoMenuItem);

            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);

            
        }

        private void Publish(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PostContent.Text))
            {
                int currentUserId = (int) Settings.Get("LoggedUserID");
                Post post = new Post { Content = PostContent.Text, User = new UserProfile {UserId = currentUserId } };

                Messenger.Default.Send<bool>(true,"ShowIndicator");

                App.ClientApi.CreatePost(post, r => {
                    if (post == null)
                    {
                        WallPageViewModel vm = SimpleIoc.Default.GetInstance<WallPageViewModel>();
                        vm.Items.Insert(0, new PostViewModel
                        {
                            PostId = r.PostId,
                            Content = r.Content,
                            CreatedAt = r.PostedAt,
                            Likes = r.Likes.Count,
                            Comments = r.Comments,
                            IsLiked = r.IsLiked,
                            Picture = r.Picture,
                            User = r.User
                        });
                    }
                    else
                    {
                        MessageBox.Show("Impossible de publier votre post, réessayez plus tard !","Information", MessageBoxButton.OK);
                    }

                    Messenger.Default.Send<bool>(false,"ShowIndicator");
                });
            }
        }        

        private void Cancel(object sender, EventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                PostPicture.Source = bmp;                    
            }
        }

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {                
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                PostPicture.Source = bmp;
            }
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            photoChooserTask.Show();
        }

        private void ApplicationBarMenuItem_Click_2(object sender, EventArgs e)
        {
            cameraCaptureTask.Show();
        }
    }
}