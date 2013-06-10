using CoombuPhoneApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.ViewModel
{
    public class UserPageViewModel : ViewModelBase
    {
        public UserPageViewModel()
        {
            if (IsInDesignMode)
            {
                SelectedUser = new UserProfile {LastName = "Salvia", FirstName = "Cody" };
            }
            else
            {
                if (App.ClientApi.IsAuthenticated)
                {
                    if (SelectedUser != null)
                    {
                        LoadPosts();
                        LoadGroups();
                        LoadUsers(); 
                    }
                }
            }
        }

        private int _currentPostPage = 0;
        private int _totalPostPage = 0;

        private int _currentGroupPage = 0;
        private int _totalGroupPage = 0;

        private int _currentUserPage = 0;
        private int _totalUserPage = 0;

        private void LoadUsers()
        {
            if (SelectedUser != null)
            {
                App.ClientApi.GetFollowees((users, page, total, size) =>
                    {
                        if (users != null)
                        {
                            var items = new ObservableCollection<UserProfile>();
                            users.ForEach(u => items.Add(u));
                            Users = items;

                            _currentUserPage = page;
                            _totalUserPage = total;
                        }

                    }, SelectedUser.UserId, _currentUserPage + 1); 
            }
        }

        private void LoadGroups()
        {
            if (SelectedUser != null)
            {
                App.ClientApi.GetGroups((groups, page, total, size) =>
                    {
                        if (groups != null)
                        {
                            var items = new ObservableCollection<Group>();
                            groups.ForEach(g => items.Add(g));
                            Groups = items;

                            _currentGroupPage = page;
                            _totalGroupPage = total;
                        }

                    }, SelectedUser.UserName, _currentGroupPage + 1); 
            }
        }

        private void LoadPosts()
        {
            if (SelectedUser != null)
            {
                App.ClientApi.GetAll((posts, page, total, size) =>
                {
                    if (posts != null)
                    {
                        var items = new ObservableCollection<PostViewModel>();
                        posts.ToList().ForEach(p => items.Add(new PostViewModel
                        {
                            PostId = p.PostId,
                            Title = p.User.FirstName + " " + p.User.LastName,
                            Content = p.Content,
                            Picture = p.Picture,
                            CreatedAt = p.PostedAt,
                            Likes = p.Likes.Count,
                            User = p.User,
                            Comments = p.Comments,
                            IsLiked = p.IsLiked
                        }));

                        _currentPostPage = page;
                        _currentPostPage = total;

                        Posts = items;
                    }
                }, SelectedUser.UserName, _currentPostPage + 1); 
            }
        }

        private UserProfile _selectedUser;
        public UserProfile SelectedUser 
        {
            get { return _selectedUser; }
            set
            {
                if (value != _selectedUser)
                {
                    _selectedUser = value;
                    RaisePropertyChanged("SelectedUser");
                }
            }
        }

        private ObservableCollection<PostViewModel> _posts;
        public ObservableCollection<PostViewModel> Posts { get; set; }

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups { get; set; }

        private ObservableCollection<UserProfile> _users;
        public ObservableCollection<UserProfile> Users { get; set; }
    }


}
