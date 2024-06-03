namespace Puzzler_Game;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public async void OnEasyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BoardView(GameLevel.Easy));
    }

    public async void OnMediumClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BoardView(GameLevel.Medium));
    }

    public async void OnHardClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BoardView(GameLevel.Hard));
    }
}

