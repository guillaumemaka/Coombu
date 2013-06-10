using CoombuPhoneApp.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace CoombuPhoneApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Post> _items = null;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                LoadData();
            }
            else
            {
                LoadData();
            }            
        }

        private void LoadData()
        {
            _items = new ObservableCollection<Post>() { 
                    new Post{Content = "Post 1"},
                    new Post{Content = "Post 2"},
                    new Post{Content = "Post 3"},
                    new Post{Content = "Post 4"},
                    new Post{Content = "Post 5"},
                    new Post{Content = "Post 6"},
                    new Post{Content = "Post 7"}
                };
        }

        public ObservableCollection<Post> Items
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
                    RaisePropertyChanged("Posts");
                }
            }
        }
    }
}