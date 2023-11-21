using AdminClient.Utility;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
	internal class TempViewModel : ViewModelBase
	{
		public ICommand ChangePageCommand { get; set; }
		public ICommand ChangeTextCommand { get; set; }

		private string _text = "Click me";

		public string Text
		{
			get { return _text; }
			set 
			{ 
				_text = value; 
				OnPropertyChanged();
			}
		}


		public TempViewModel() 
		{
			ChangePageCommand = new RelayCommand(() => { ((App)Application.Current).ChangeUserControl(typeof(TestViewModel)); });
			ChangeTextCommand = new RelayCommand(ExecuteChangeText);
		}

		private void ExecuteChangeText()
		{
			if (Text.Equals("Click me"))
			{
				Text = "You clicked me!";
			}
			else
			{
				Text = "Click me";
			}
		}
	}
}
