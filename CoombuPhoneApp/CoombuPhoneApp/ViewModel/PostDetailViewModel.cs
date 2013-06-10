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
    public class PostDetailViewModel : ViewModelBase
    {
        public PostDetailViewModel()
        {
            Comments = new ObservableCollection<Comment>();
        }

        private PostViewModel _post;
        public PostViewModel Post {
            get { return _post; }

            set
            {
                if (value != _post)
                {
                    _post = value;
                    if (_post.Comments != null)
                    {
                        _post.Comments.ForEach(c => _comments.Add(c)); 
                    }
                    
                    RaisePropertyChanged("Post");
                    RaisePropertyChanged("Comments");
                }
            }
        }

        private ObservableCollection<Comment> _comments;
        public ObservableCollection<Comment> Comments
        {
            get { return _comments; }
            set
            {
                if (value != _comments)
                {
                    _comments = value;
                    RaisePropertyChanged("Comments");
                }
            }
        }
    }
}
