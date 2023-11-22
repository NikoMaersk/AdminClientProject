using AdminClient.Utility;
using AdminClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AdminClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public ContentControl ContentControlRef { get; set; } = null;
		private static Dictionary<Type, Type> typeCache = new Dictionary<Type, Type>();

		public App()
		{
			ServiceContainer.Register(() => new LoginViewModel());
			ServiceContainer.Register(() => new TempViewModel());
			ServiceContainer.Register(() => new TestViewModel());
		}

		public void ChangeUserControl(Type viewModelType)
		{
			UserControl tmpUC = CreatePage(viewModelType, null);

			var viewModel = ServiceContainer.Resolve(viewModelType);

			tmpUC.DataContext = viewModel;
			this.ContentControlRef.Content = tmpUC;
		}

		private UserControl CreatePage(Type viewModelType, object parameter)
		{
			Type pageType = GetPageTypeForViewModel(viewModelType);
			if (pageType == null)
			{
				throw new Exception($"Cannot locate page type for {viewModelType}");
			}

			//CreateInstance, will create an instance based on the type, the default constructor is always invoked
			UserControl? page = Activator.CreateInstance(pageType) as UserControl;

			return page;
		}


		public static Type GetPageTypeForViewModel(Type viewModelType)
		{
			if (typeCache.TryGetValue(viewModelType, out var cachedViewType))
			{
				return cachedViewType;
			}
			else
			{
				var viewName = viewModelType.FullName.Replace("Model", string.Empty);
				var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
				var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
				var viewType = Type.GetType(viewAssemblyName);

				if (viewType != null)
				{
					typeCache[viewModelType] = viewType;
					return viewType;
				}
				else
				{
					throw new Exception($"Cannot locate view type for {viewModelType}");
				}
			}
		}

		/// <summary>
		/// This metod will use the naming convention between the ViewModel and the View
		/// To find the name of the View that matches the ViewModel
		/// The view name is then used to find the type (the view class name)
		/// </summary>
		/// <param name="viewModelType"></param>
		/// <returns></returns>
		private Type GetPageTypeForViewModelOld(Type viewModelType)
		{
			var viewName = viewModelType.FullName.Replace("Model", string.Empty);
			var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
			var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
			var viewType = Type.GetType(viewAssemblyName);
			return viewType;
		}

	}
}
