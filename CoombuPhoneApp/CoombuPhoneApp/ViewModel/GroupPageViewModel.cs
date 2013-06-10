using CoombuPhoneApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.ViewModel
{
    public class GroupPageViewModel : ViewModelBase
    {
        public GroupPageViewModel()
        {
            if (IsInDesignMode)
            {
                LoadData();
                SelectedItem = new Group { GroupName = "Group 4" };
            }
            else
            {
                Items = new ObservableCollection<Group>();
                
                if(App.ClientApi.IsAuthenticated)
                    LoadGroups();
            }
        }

        #region RelayCoommand

        public RelayCommand AddGroupCommand { get; set; }

        #endregion

        #region Properties

        private ObservableCollection<Group> _items = null;
        public ObservableCollection<Group> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    RaisePropertyChanged("Items");
                }
            }
        }

        private ObservableCollection<PostViewModel> _posts;
        public ObservableCollection<PostViewModel> Posts
        {
            get { return _posts; }
            set
            {
                if (value != _posts)
                {
                    _posts = value;
                    RaisePropertyChanged("Posts");
                }
            }
        }        

        private Group _selectedItem = null;
        public Group SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                    if (_selectedItem.Posts != null )
                    {
                        _selectedItem.Posts.ToList().ForEach(p => this._posts.Add(new PostViewModel
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
                    }

                    RaisePropertyChanged("Posts");
                }
            }
        }



        private int _currentPage = 0;
        private int _totalPage = 0;

        #endregion


        private void LoadGroups()
        {
            if (_currentPage <= _totalPage)
            {
                App.ClientApi.GetGroups((groups, currentPage, totalPage, size) =>
                {
                    _currentPage = currentPage;
                    _totalPage = totalPage;

                    if (groups != null && groups.Count > 0)
                    {
                        groups.ForEach(g => Items.Add(g));
                    }
                }, _currentPage + 1);
            }
        }

        private void LoadData()
        {
            _items = new ObservableCollection<Group>() { 
                    new Group{GroupName = "Group 1"},
                    new Group{GroupName = "Group 2"},
                    new Group{GroupName = "Group 3"},
                    new Group{GroupName = "Group 4"},
                    new Group{GroupName = "Group 5"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 7"},
                    new Group{GroupName = "Group 1"},
                    new Group{GroupName = "Group 2"},
                    new Group{GroupName = "Group 3"},
                    new Group{GroupName = "Group 4"},
                    new Group{GroupName = "Group 5"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 7"},
                    new Group{GroupName = "Group 1"},
                    new Group{GroupName = "Group 2"},
                    new Group{GroupName = "Group 3"},
                    new Group{GroupName = "Group 4"},
                    new Group{GroupName = "Group 5"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 7"},
                    new Group{GroupName = "Group 1"},
                    new Group{GroupName = "Group 2"},
                    new Group{GroupName = "Group 3"},
                    new Group{GroupName = "Group 4"},
                    new Group{GroupName = "Group 5"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 6"},
                    new Group{GroupName = "Group 7"}
                };

            RaisePropertyChanged("Items");

            SelectedItem = new Group { GroupName = "Group 5" };
        }
    }
}