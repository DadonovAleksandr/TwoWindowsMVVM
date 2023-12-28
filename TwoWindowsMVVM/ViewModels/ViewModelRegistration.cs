using Microsoft.Extensions.DependencyInjection;
using TwoWindowsMVVM.ViewModels.MainWindowVm;
using TwoWindowsMVVM.Views;

namespace TwoWindowsMVVM.ViewModels
{
    public static class ViewModelRegistration
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddScoped<SecondaryWindowViewModel>()
            .AddTransient(s =>
                {
                    var model = s.GetRequiredService<MainWindowViewModel>();
                    var window = new MainWindow { DataContext = model };
                    model.DialogComplete += (sender, args) => window.Close();
                    return window;
                })
            .AddTransient(s =>
                {
                    var scope = s.CreateScope();
                    var model = scope.ServiceProvider.GetRequiredService<SecondaryWindowViewModel>();
                    var window = new SecondaryWindow { DataContext = model };
                    model.DialogComplete += (sender, args) => window.Close();
                    window.Closed += (_, _) => scope.Dispose();
                    return window;
                })
            .AddTransient<SecondaryWindowViewModel>()

            ;

    }
}