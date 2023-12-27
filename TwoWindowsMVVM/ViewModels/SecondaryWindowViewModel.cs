using TwoWindowsMVVM.ViewModels.Base;

namespace TwoWindowsMVVM.ViewModels;

internal class SecondaryWindowViewModel : BaseViewModel
{

    public SecondaryWindowViewModel()
    {
        Title = $"Вторичное окно";
    }

    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title { get => Get<string>(); set => Set(value); }

}