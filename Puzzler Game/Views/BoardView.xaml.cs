namespace Puzzler_Game;

public partial class BoardView : ContentPage
{
	public BoardView(GameLevel level)
	{
		InitializeComponent();
		BindingContext = new BoardViewModel(level);
	}

    async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}