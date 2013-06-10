/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CoombuPhoneApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CoombuPhoneApp.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CoombuPhoneApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<INavigationService,NavigationService>(true);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WallPageViewModel>();
            SimpleIoc.Default.Register<GroupPageViewModel>();
            SimpleIoc.Default.Register<LoginRegistrationViewModel>();
            SimpleIoc.Default.Register<PostDetailViewModel>();
            SimpleIoc.Default.Register<RelationsPageViewModel>();
            SimpleIoc.Default.Register<UserPageViewModel>();
            SimpleIoc.Default.Register<SearchResultPageViewModel>();            
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WallPageViewModel WallPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WallPageViewModel>();
            }
        }

        public GroupPageViewModel GroupPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GroupPageViewModel>();
            }
        }

        public LoginRegistrationViewModel LoginRegistrationViewModel 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<LoginRegistrationViewModel>();
            }
        
        }

        public PostDetailViewModel PostDetailViewModel 
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PostDetailViewModel>();
            }            
        }

        public RelationsPageViewModel RelationsPageViewModel 
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RelationsPageViewModel>();
            }
        }

        public UserPageViewModel UserPageViewModel 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<UserPageViewModel>();
            }            
        }

        public SearchResultPageViewModel SearchResultPageViewModel 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<SearchResultPageViewModel>();
            }            
        }
        
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}