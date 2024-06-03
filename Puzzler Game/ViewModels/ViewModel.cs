#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace Puzzler_Game;
public partial class ViewModel : ObservableObject
{
    public ViewModel()
    {
        
    }

    [ObservableProperty]
    string title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    string statusMessage;
}
