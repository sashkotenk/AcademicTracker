using Tracker.Mobile.ViewModels;

namespace Tracker.Mobile.Views;

public partial class StudentsPage : ContentPage
{
    public StudentsPage(StudentsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; // Зв'язуємо View і ViewModel [cite: 1050]
    }
}