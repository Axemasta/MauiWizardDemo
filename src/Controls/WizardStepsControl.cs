using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;

namespace WizardDemo.Controls;

public partial class WizardStepsControl : Grid
{
    // Source generated fields get erroniously marked as unused.
#pragma warning disable CS0169
    [AutoBindable]
    private readonly string currentSegmentTitle;

    [AutoBindable]
    private readonly ICommand completedCommand;
#pragma warning restore CS0169

    public event EventHandler<StepChangedEventArgs> StepChanged;

    protected override void OnChildAdded(Element child)
    {
        SetCurrentSegmentTitle(Children.FirstOrDefault());

        if (child is VisualElement ve)
        {
            ve.IsVisible = false;

            if (GetCurrentIndex() < 0)
            {
                ve.IsVisible = true;
            }
        }

        base.OnChildAdded(child);
    }

    public int GetCurrentIndex()
    {
        for (var i = 0; i < Children.Count; i++)
        {
            if (Children[i] is VisualElement ve && ve.IsVisible)
                return i;
        }

        return -1;
    }

    private void SetCurrentSegmentTitle(IView view)
    {
        if (view is WizardView wizardView)
        {
            CurrentSegmentTitle = wizardView.Title;
        }
    }
    
    public async Task Forward()
    {
        var c = GetCurrentIndex();

        var currentIndex = c;
        var nextIndex = c + 1;

        if (nextIndex >= Children.Count)
        {
            CompletedCommand?.Execute(null);
            return;
        }
        if (currentIndex == nextIndex)
            return;

        var currentView = Children[currentIndex] as VisualElement;
        var nextView = Children[nextIndex] as VisualElement;

        nextView.TranslationX = this.Width;
        nextView.IsVisible = true;

        await Task.WhenAll(
            nextView.TranslateTo(0, 0, 500, Easing.CubicInOut),
            currentView.TranslateTo(-1 * this.Width, 0, 500, Easing.CubicInOut));

        currentView.IsVisible = false;
        currentView.TranslationX = 0;

        SetCurrentSegmentTitle(nextView);

        StepChanged?.Invoke(this, new StepChangedEventArgs(currentIndex, nextIndex));
    }

    public async Task Backward()
    {
        var c = GetCurrentIndex();

        var currentIndex = c;
        var nextIndex = c - 1;

        if (nextIndex < 0)
            return;
        if (currentIndex == nextIndex)
            return;

        var currentView = Children[currentIndex] as VisualElement;
        var nextView = Children[nextIndex] as VisualElement;

        nextView.TranslationX = -1 * this.Width;
        nextView.IsVisible = true;

        await Task.WhenAll(
            nextView.TranslateTo(0, 0, 500, Easing.CubicInOut),
            currentView.TranslateTo(this.Width, 0, 500, Easing.CubicInOut));

        currentView.IsVisible = false;
        currentView.TranslationX = 0;

        SetCurrentSegmentTitle(nextView);

        StepChanged?.Invoke(this, new StepChangedEventArgs(currentIndex, nextIndex));
    }

    public bool CanGoBack()
    {
        var currentIndex = GetCurrentIndex();

        return currentIndex > 0;
    }

    public bool CanGoForward()
    {
        var currentIndex = GetCurrentIndex();
        var nextIndex = currentIndex + 1;

        return nextIndex < Children.Count;
    }
}

public class StepChangedEventArgs : EventArgs
{
    public StepChangedEventArgs(int previousStepIndex, int stepIndex)
        : base()
    {
        PreviousStepIndex = previousStepIndex;
        StepIndex = stepIndex;
    }

    public int PreviousStepIndex { get; }
    public int StepIndex { get; }
}