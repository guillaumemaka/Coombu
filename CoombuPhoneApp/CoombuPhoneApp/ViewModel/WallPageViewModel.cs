using CoombuPhoneApp.Models.ViewModels;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoombuPhoneApp.ViewModel
{
    public class WallPageViewModel : ViewModelBase
    {
        public WallPageViewModel()
        {
            if (IsInDesignMode)
            {
                LoadSample();
                _selectedItem = new PostViewModel { Title = "User1", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.",CreatedAt = DateTime.Now, Likes = 20 };
            }
            else
            {
                //LoadSample();
                //_selectedItem = new PostViewModel { Title = "User1", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 20 };
                Items = new ObservableCollection<PostViewModel>();

                if (App.ClientApi.IsAuthenticated)
                {
                    LoadPosts();                    
                }
            }
        }

        private ObservableCollection<SearchResultViewModel> _searchResult;
        public ObservableCollection<SearchResultViewModel> SearchResult 
        {
            get { return _searchResult; }
            set 
            {
                if (value != _searchResult)
                {
                    _searchResult = value;
                    RaisePropertyChanged("SearchResult");
                }
            }
        }

        private string _currentUserName;
        public string CurrentUserName 
        {
            get { return _currentUserName; }
            set
            {
                _currentUserName = value;
                LoadPosts();                
            }
        }

                
        private bool _hasEntry;
        public bool HasEntry 
        {
            get { return _hasEntry; }
            set
            {
                _hasEntry = value;
                RaisePropertyChanged("HasEntry");
            }
        }

        private ObservableCollection<PostViewModel> _items;

        public ObservableCollection<PostViewModel> Items
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

                    if (_items != null)
                        if (_items.Count > 0)
                            HasEntry = true;
                        else
                            HasEntry = false;

                    RaisePropertyChanged("Items");
                }
            }
        }

        private PostViewModel _selectedItem = null;
        public PostViewModel SelectedItem
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
            //throw new NotImplementedException();
        }

        

        private void LoadPosts()
        {
            if (_currentPostPage < _totalPostPage || _currentPostPage == 0)
            {
                App.ClientApi.GetAll((posts, currentPage, totalPage, size) =>
                    {
                        _currentPostPage = currentPage;
                        _totalPostPage = totalPage;

                        if (posts.ToList().Count > 0)
                        {
                            posts.ToList().ForEach(p => _items.Add(new PostViewModel
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
                    }, _currentPostPage + 1); 
            }
        }

        public void LoadSample() 
        {
            _items = new ObservableCollection<PostViewModel>() { 
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 20},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 0},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 10},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 21},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 3},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 2},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 0},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 27},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 5},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 8},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 4},
                    new PostViewModel{Title="User1",Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed volutpat erat et metus dignissim et gravida nunc vulputate. Integer ut luctus nunc. Cras posuere aliquam nunc non dapibus. Mauris commodo eros nec leo tempor fringilla. Nullam in purus libero. Morbi porttitor sagittis dignissim. Suspendisse a sem est, eu sagittis dolor. Praesent accumsan tempor ornare. Pellentesque in nisl pellentesque est euismod pulvinar. Proin gravida, nisi sed fermentum egestas, velit neque suscipit est, viverra mattis felis quam vitae risus. Sed vel magna arcu, ut scelerisque diam. Vivamus iaculis, lorem in aliquet consequat, tellus dolor commodo metus, vitae volutpat mauris augue quis dolor. Cras rutrum pulvinar lectus, sed dictum nibh congue vel. Quisque non mauris vitae metus commodo sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed felis felis, convallis in dictum quis, sollicitudin vel est.", CreatedAt = DateTime.Now, Likes = 7}
                };
        }
    }
}
