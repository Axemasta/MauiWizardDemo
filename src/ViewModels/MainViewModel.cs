using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
namespace WizardDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IUserDialogs userDialogs;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string wizardClass;

    [ObservableProperty]
    private string age;

    [ObservableProperty]
    private bool useIncantations;

    [ObservableProperty]
    private bool useSorcery;

    public MainViewModel(IUserDialogs userDialogs)
    {
        this.userDialogs = userDialogs;
    }

    partial void OnUseIncantationsChanged(bool value)
    {
        if (value && UseSorcery)
            UseSorcery = false;
    }

    partial void OnUseSorceryChanged(bool value)
    {
        if (value && UseIncantations)
            UseIncantations = false;
    }

    [RelayCommand]
    private async Task CompleteWizard()
    {
        if (!UseIncantations && !UseSorcery)
        {
            await userDialogs.AlertAsync("You must select a magic type before proceeding", "Select Magic Type", "Ok");
            return;
        }

        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine("You have completed the wizard, here are the details provided:");
        messageBuilder.AppendLine($"Name: {Name}");
        messageBuilder.AppendLine($"Age: {Age}");
        messageBuilder.AppendLine($"Class: {WizardClass}");

        var selectedMagic = UseIncantations
            ? "Incantations"
            : "Sorceries";

        messageBuilder.Append($"Magic Type: {selectedMagic}");

        var alertConfig = new AlertConfig
        {
            Title = "Form Completed",
            Message = messageBuilder.ToString(),
            OkText = "Ok",
        };

        await userDialogs.AlertAsync(alertConfig);
    }
}