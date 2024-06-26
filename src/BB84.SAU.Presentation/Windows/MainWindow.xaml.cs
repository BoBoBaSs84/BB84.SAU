using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using BB84.SAU.Application.Interfaces.Application.Services;
using BB84.SAU.Application.ViewModels;

namespace BB84.SAU.Presentation.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly INotificationService _notificationService;
	private readonly AboutViewModel _aboutViewModel;
	private readonly MainViewModel _mainViewModel;

	/// <summary>
	/// Initializes an instance of the <see cref="MainWindow"/> class.
	/// </summary>
	/// <param name="notificationService">The notification service instance to use.</param>
	/// <param name="aboutViewModel">The about view model instance to use.</param>
	/// <param name="mainViewModel">The view model instance to use.</param>
	public MainWindow(INotificationService notificationService, AboutViewModel aboutViewModel, MainViewModel mainViewModel)
	{
		InitializeComponent();

		_notificationService = notificationService;
		_notificationService.MessageReceived += async (s, e) => await OnMessageReceived(e);

		_aboutViewModel = aboutViewModel;
		DataContext = _mainViewModel = mainViewModel;
	}

	private async Task OnMessageReceived(string message)
	{
		StatusBarItem.Content = message;

		await Task.Delay(2500);

		StatusBarItem.Content = string.Empty;
	}

	private void HomeStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<AboutViewModel>();

	private void SettingsStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<SettingsViewModel>();

	private void UserStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<UserDataViewModel>();

	private void GamesStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<GamesViewModel>();

	private void AchievementsStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<AchievementsViewModel>();

	private void LogbookStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<LogbookViewModel>();

	private void AboutStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		=> _mainViewModel.NavigationService.NavigateTo<AboutViewModel>();

	private void GitHubStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (_aboutViewModel.Model.Repository is not null)
			_ = Process.Start("explorer", _aboutViewModel.Model.Repository);
	}

	private void StackPanelItem_MouseEnter(object sender, MouseEventArgs e)
	{
		if (sender is StackPanel panel)
			panel.Background = Brushes.Gainsboro;
	}

	private void StackPanelItem_MouseLeave(object sender, MouseEventArgs e)
	{
		if (sender is StackPanel panel)
			panel.Background = Brushes.WhiteSmoke;
	}
}
