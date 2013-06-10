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
    public class RelationsPageViewModel : ViewModelBase
    {
        public RelationsPageViewModel()
        {
            if (IsInDesignMode)
            {
                LoadData();
            }
            else
            {
                if(App.ClientApi.IsAuthenticated)
                    LoadUsers();
            }
        }

        private void LoadUsers()
        {
            App.ClientApi.GetFollowees((users, page, total, size) => {
                var items = new ObservableCollection<UserProfile>();
                if (users != null)
                {
                    users.ForEach(u => items.Add(u));
                    Items = items;
                }
            });
        }

        private void LoadData()
        {
            _items = new ObservableCollection<UserProfile>
            {
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"},
                new UserProfile{LastName="Salvia",FirstName="Cody",Department="IT Manager"}
            };
        }

        private UserProfile _selectedItem;
        public UserProfile SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }
        
        private ObservableCollection<UserProfile> _items;
        public ObservableCollection<UserProfile> Items 
        {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    RaisePropertyChanged("Items");
                }
            }
        }

    }
}
