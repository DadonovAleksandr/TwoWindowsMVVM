using Microsoft.Extensions.DependencyInjection;
using TwoWindowsMVVM.ViewModels.MainWindowVm;
using TwoWindowsMVVM.Views;

namespace TwoWindowsMVVM.ViewModels
{
    public static class ViewModelRegistration
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<SecondaryWindowViewModel>()
            .AddTransient(s =>
                {
                    var model = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
                    model.DialogComplete += (sender, args) => window.Close();
                    return window;
                })
            .AddTransient(s =>
                {
                    var model = s.GetRequiredService<SecondaryWindowViewModel>();
                    var window = new SecondaryWindow { DataContext = model };
                    model.DialogComplete += (sender, args) => window.Close();
                    return window;
                })
            .AddTransient<SecondaryWindowViewModel>()

            ;

    }
}