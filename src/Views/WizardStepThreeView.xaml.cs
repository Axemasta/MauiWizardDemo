using WizardDemo.Controls;
namespace WizardDemo.Views;

public partial class WizardStepThreeView : WizardView
{
    public WizardStepThreeView()
    {
        InitializeComponent();
    }

    private void OnIncantationsTapped(object sender, TappedEventArgs e)
    {
        incantationsCheckBox.IsChecked = !incantationsCheckBox.IsChecked;
    }

    private void OnSorceriesTapped(object sender, TappedEventArgs e)
    {
        sorceriesCheckBox.IsChecked = !sorceriesCheckBox.IsChecked;
    }
}