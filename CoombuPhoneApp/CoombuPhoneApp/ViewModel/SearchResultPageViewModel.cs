using Coombu.ViewModels;
using CoombuPhoneApp.Models;
using CoombuPhoneApp.Models.ViewModels;
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
    public class SearchResultPageViewModel :ViewModelBase
    {      
        public SearchResultPageViewModel()
        {
        
        }

        private void Search()
        {
            App.ClientApi.Search(SearchString, (result) => {
                SearchResult = result;                
            });            
        }
        

        private string _searchString;
        public string SearchString 
        {
            get { return _searchString; }
            set 
            {
                if (value != _searchString)
                {
                    _searchString = value;
                    RaisePropertyChanged("SearchString");
                    
                    if (App.ClientApi.IsAuthenticated)
                        Search();
                }
            }
        }

        private UserProfile _selectedResult;
        public UserProfile SelectedResult 
        {
            get { return _selectedResult; }
            set
            {
                if (value != _selectedResult)
                {
                    _selectedResult = value;
                    RaisePropertyChanged("SelectedResult");
                }
            }
        }

        private ApiSearchResultViewModel _searchResult;
        public ApiSearchResultViewModel SearchResult
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
    }
}
