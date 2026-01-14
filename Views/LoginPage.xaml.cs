using Tracker.Mobile; // Додайте це, щоб бачити AppShell

namespace Tracker.Mobile.Views; // Переконайтесь, що тут є .Views

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        // Перехід на головне меню після входу
        Application.Current.MainPage = new AppShell();
    }
}