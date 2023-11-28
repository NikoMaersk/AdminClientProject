using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using AdminClient.Utility.HttpHelper;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
	internal class LoginViewModel : ViewModelBase
	{
		#region Fields

		public ICommand LoginCommand { get; set; }

		#endregion


		#region Properties
		private string _adminName;

		public string AdminName
		{
			get { return _adminName; }
			set 
			{ 
				_adminName = value; 
				OnPropertyChanged();
			}
		}

		private string _password;

		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value; 
				OnPropertyChanged();
			}
		}

		private string _loginText = "Login";

		public string LoginText
		{
			get { return _loginText; }
			set 
			{ 
				_loginText = value; 
				OnPropertyChanged();
			}
		}


		#endregion

		public LoginViewModel()
        {
			LoginCommand = new RelayCommand(() => ((App)Application.Current).ChangeUserControl(typeof(FrontPageViewModel)));
        }

		private void ExecuteAuthorization()
		{
			throw new NotImplementedException();
		}

		private static async Task<string?> Login()
		{
			using (var client = new AdminLoginUtil())
			{
				return await client.AdminLogin();
			}
		}
    }
}
