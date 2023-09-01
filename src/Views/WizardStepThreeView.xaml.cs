using WizardDemo.Controls;

namespace WizardDemo.Views;

public partial class WizardStepThreeView : WizardView
{
	public WizardStepThreeView()
	{
		InitializeComponent();
	}

    void OnIncantationsTapped(object sender, TappedEventArgs e)
    {
		incantationsCheckBox.IsChecked = !incantationsCheckBox.IsChecked;
    }

    void OnSorceriesTapped(object sender, TappedEventArgs e)
    {
        sorceriesCheckBox.IsChecked = !sorceriesCheckBox.IsChecked;
    }
}
