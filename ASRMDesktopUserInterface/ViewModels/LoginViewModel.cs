﻿using ASRMDesktopUI.Library.Api;
using ASRMDesktopUserInterface.Helpers;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRMDesktopUserInterface.ViewModels
{
    public class LoginViewModel:Screen
    {
		private string _userName;
		private string _password;
        private bool _isErrorVisible;
        private string _errorMessage;
        private IApiHelper _apiHelper;

        public LoginViewModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


		public string UserName
		{
			get { return _userName; }
			set 
			{ 
				_userName = value; 
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}
        public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value;
				NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
		}
		

		public string ErrorMessage
        {
			get { return _errorMessage; }
			set 
			{ 
				_errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(()=>ErrorMessage);
			}
		}

		public bool IsErrorVisible
        {
			get 
			{ 
				bool output=false;
				if(ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output;
			}
			/*set 
			{ 
				_isErrorMessage = value; 
				
			}*/
		}

		public bool CanLogIn
		{
			get{
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
		}
		public async Task LogIn()
		{
			try
			{
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(UserName, Password);
				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}

	}
}
