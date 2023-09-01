using WizardDemo.Pages;
using WizardDemo.ViewModels;

namespace WizardDemo;

public partial class App : Application
{
	public App(IServiceProvider services)
	{
		InitializeComponent();

		var mainViewModel= services.GetRequiredService<MainViewModel>();
		var mainPage = services.GetRequiredService<MainPage>();

		mainPage.BindingContext = mainViewModel;

		MainPage = new NavigationPage(mainPage);
	}
}

