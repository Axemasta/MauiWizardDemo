using WizardDemo.Controls;
namespace WizardDemo.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        stepControl.StepChanged += StepControl_StepChanged;

        Title = stepControl.CurrentSegmentTitle;

        backButton.IsEnabled = stepControl.CanGoBack();
    }

    private void StepControl_StepChanged(object sender, StepChangedEventArgs e)
    {
        Title = stepControl.CurrentSegmentTitle;

        backButton.IsEnabled = stepControl.CanGoBack();
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender == backButton)
            await stepControl.Backward();
        else if (sender == nextButton)
            await stepControl.Forward();
    }
}