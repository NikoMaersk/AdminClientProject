using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using AdminClient.Utility.HttpHelper;
using System.Collections.Generic;
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
		protected string _adminName = string.Empty;

		public string AdminName
		{
			get { return _adminName; }
			set 
			{ 
				_adminName = value; 
				OnPropertyChanged();
			}
		}

		protected string _password = string.Empty;

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

		public List<Name> names { get; set; }
		#endregion


		public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteAuthorization);
        }


		private async void ExecuteAuthorization()
		{
			if (_adminName == string.Empty && _password == string.Empty)
			{
				return;
			}

			var (success, token) = await AdminLogin.Login(_adminName, _password);

			if (success)
			{
				base.Token = token;

				// Register HttpConnections in factory
				HttpConnectionFactory.Instance.RegisterConnection(new HttpNamesConnection(token));
				HttpConnectionFactory.Instance.RegisterConnection(new HttpUserConnection(token));
				HttpConnectionFactory.Instance.RegisterConnection(new HttpMatchConnection(token));

				((App)Application.Current).ChangeUserControl(typeof(FrontPageViewModel));
			}
			else
			{
				Password = "Wrong email or password";
			}
		}
    }
}
