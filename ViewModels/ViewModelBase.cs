using AdminClient.Utility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
	internal class ViewModelBase : INotifyPropertyChanged
	{

		public ICommand AdvStatsCommand { get; set; } = new RelayCommand(() => ((App)Application.Current).ChangeUserControl(typeof(AdvStatsViewModel)));

        public ICommand UsersCommand { get; set; } = new RelayCommand(() => ((App)Application.Current).ChangeUserControl(typeof(UsersViewModel)));

        public ICommand OverviewCommand { get; set; } = new RelayCommand(() => ((App)Application.Current).ChangeUserControl(typeof(FrontPageViewModel)));

		protected string Token = string.Empty;


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
		{
			if (object.Equals(storage, value)) return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

	}
}
