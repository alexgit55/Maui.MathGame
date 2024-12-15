namespace MathGame;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
	{
		InitializeComponent();
		gamesList.ItemsSource = App.GameRepository.GetGames();
	}

	private void OnDeleteGame(object sender, EventArgs e)
    {
        Button button = (Button)sender;
		App.GameRepository.DeleteGame((int)button.BindingContext);
        Navigation.PushAsync(new PreviousGames());
    }

    private void OnBackToMain(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}