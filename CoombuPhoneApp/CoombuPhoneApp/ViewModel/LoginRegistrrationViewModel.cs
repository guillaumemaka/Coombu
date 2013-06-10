using Coombu.Models;
using CoombuPhoneApp.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoombuPhoneApp.ViewModel
{
    public class LoginRegistrationViewModel : ViewModelBase
    {
        private INavigationService NavigationService;

        private RegisterModel _registerModel;
        public RegisterModel RegisterModel { get { return _registerModel; } }

        private LoginModel _loginModel;
        public LoginModel LoginModel { get { return _loginModel; } }        

        private bool _pendingRequest;
        public bool PendingRequest 
        {
            get { return !_pendingRequest; }
            set 
            {
                _pendingRequest = value;
                RaisePropertyChanged("PendingRequest");
            }
        }

        public LoginRegistrationViewModel(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            this._registerModel = new RegisterModel();
            this._loginModel = new LoginModel();
            this.LoginCommand = new RelayCommand(this.Login,this.CanLogin);
            this.RegisterCommand = new RelayCommand(() => {
                NavigationService.NavigateTo(new Uri("/RegistrationPage.xaml", UriKind.Relative));
            },() => { return true; });

        }                

        public RelayCommand LoginCommand { get; private set; }

        public bool CanLogin()
        {                        
            return true;            
        }

        public void Login()
        {
            _pendingRequest = true;

            App.ClientApi.Login(this._loginModel.UserName, this._loginModel.Password, 
                userToken => {
                    if (userToken == null)
                    {
                        MessageBox.Show("Invalid Username or Password. Please check your login information");
                        return;
                    }

                    Settings.Save("LoggedUserID", userToken.User.UserId);
                    Settings.Save("LoggedUserName",userToken.User.UserName);
                    Settings.Save("Token",userToken.token);
                    
                    WallPageViewModel vm = SimpleIoc.Default.GetInstance<WallPageViewModel>();
                    vm.CurrentUserName = userToken.User.UserName;
                    
                    Messenger.Default.Send<Uri>(new Uri("/MainPage.xaml", UriKind.Relative), "NavigationRequest");
                }, this._loginModel.RememberMe);
            
        }
        #region Test

        public RelayCommand RegisterCommand { get; private set; }

        #endregion
    }
}
